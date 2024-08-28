using Gen4RngLib.Rng;
using ShinyMultiSeed.Calculator;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reactive.Disposables;

namespace ShinyMultiSeed.Main
{
    internal sealed class MainFormPresenter : IDisposable
    {
        readonly MainForm m_MainForm;
        readonly CompositeDisposable m_Disposables = new CompositeDisposable();

        public MainFormPresenter()
        { 
            m_MainForm = new MainForm();
        }

        public void Run()
        {
            var calculator = SeedCalculatorFactory.CreateGen4SeedCalculator(new Gen4SeedCalculatorArgs
            {
                FrameMin = 900,
                FrameMax = 4500,
                PositionMin = 0,
                PositionMax = 450,
                EncountOffset = 0,
                DeterminesNature = true,
                IsShiny = true,
                Tsv = (24485 ^ 59064) & 0xfff8,
                FiltersAtkIV = true,
                AtkIVMin = 0,
                AtkIVMax = 1,
                FiltersSpdIV = true,
                SpdIVMin = 0,
                SpdIVMax = 1,
            });

            var stopwatch = new Stopwatch(); // 処理時間測定用のストップウォッチ
            stopwatch.Start();

            calculator.CalculateAll(16);

            stopwatch.Stop();

            var sortedResults = calculator.Results.OrderBy(pair => pair.InitialSeed).ToList();
            using (var sw = new StreamWriter("output.txt"))
            {
                uint tsv = (24485 ^ 59064) & 0xfff8;
                var tempRng = RngFactory.CreateLcgRng(0);
                foreach (var result in sortedResults)
                {
                    var rng = RngFactory.CreateLcgRng(result.InitialSeed);
                    uint pid1 = rng.Next();
                    uint nature = 0;
                    for (int i = 1; i <= 450; ++i)
                    { 
                        uint pid2 = rng.Next();
                        var psv = (pid1 ^ pid2) & 0xfff8;
                        if (tsv == psv)
                        {
                            tempRng.Seed = rng.Seed;

                            // 個体値チェック
                            if (((tempRng.Next() >> 5) & 0b11111) <= 1 // A0
                                && ((tempRng.Next()) & 0b11111) <= 1) // S0
                            {
                                nature = (pid2 << 16 | pid1) % 25;
                                break;
                            }
                        }

                        pid1 = pid2;
                    }
                    sw.WriteLine($"{result.InitialSeed:X8},{result.StartPosition},{nature}");
                }
            }
            var startInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = "output.txt",
                UseShellExecute = true,
                CreateNoWindow = true,
            };
            System.Diagnostics.Process.Start(startInfo);

            MessageBox.Show($"処理時間: {stopwatch.Elapsed.TotalSeconds:F2} 秒");

            Application.Run(m_MainForm);
        }

        public void Dispose()
        {
            m_Disposables.Dispose();
        }
    }
}
