using Gen4RngLib.Individual;
using Gen4RngLib.Rng;
using ShinyMultiSeed.Calculator;
using ShinyMultiSeed.Config;
using ShinyMultiSeed.Infrastructure;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Runtime.Versioning;

namespace ShinyMultiSeed.Main
{
    [SupportedOSPlatform("windows")]
    internal sealed class MainFormPresenter : IDisposable
    {
        const string c_ConfigPath = "config.json";
        const string c_Gen4ConfigPath = "config.gen4.json";

        readonly MainForm m_MainForm;
        readonly ConfigData m_Config;
        readonly Gen4Config m_Gen4Config;
        readonly CompositeDisposable m_Disposables = new CompositeDisposable();

        public MainFormPresenter()
        {
            m_Config = Serializer.Deserialize<ConfigData>(c_ConfigPath);
            m_Gen4Config = Serializer.Deserialize<Gen4Config>(c_Gen4ConfigPath);
            m_MainForm = new MainForm(m_Config, m_Gen4Config);

            m_Disposables.Add(m_MainForm.CalculateButton.Clicked.Subscribe(_ => Calculate()));
        }

        public void Run()
        {
            Application.Run(m_MainForm);
        }

        public void Dispose()
        {
            m_Disposables.Dispose();
        }

        void Calculate()
        {
            // フォームから設定を取得
            if (!m_MainForm.ReflectToConfig(m_Config, m_Gen4Config, out string reflectConfigError))
            {
                MessageBox.Show(reflectConfigError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 現在の状態を保存
            if (!Serializer.Serialize(c_ConfigPath, m_Config, out string configSerializeError))
            {
                MessageBox.Show($"{c_ConfigPath}の保存に失敗しました。\n----------\n" + configSerializeError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!Serializer.Serialize(c_Gen4ConfigPath, m_Gen4Config, out string gen4ConfigSerializeError))
            {
                MessageBox.Show($"{c_Gen4ConfigPath}の保存に失敗しました。\n----------\n" + gen4ConfigSerializeError, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // カリキュレータに渡す引数を生成
            var args = ConfigConverter.ConvertToGen4SeedCalculatorArgs(m_Config, m_Gen4Config);

            // 計算を実行
            var calculator = SeedCalculatorFactory.CreateGen4SeedCalculator(args);

            var stopwatch = new Stopwatch(); // 処理時間測定用のストップウォッチ
            stopwatch.Start();

            calculator.CalculateAll(16);

            stopwatch.Stop();

            // 結果を出力
            var sortedResults = calculator.Results.OrderBy(result => result.InitialSeed).ToList();
            using (var sw = new StreamWriter("output.txt"))
            {
                var tempRng = RngFactory.CreateLcgRng(0);
                Individual individual = new Individual();
                foreach (var result in sortedResults)
                {
                    var rng = RngFactory.CreateLcgRng(result.InitialSeed);
                    uint nature = 0;
                    for (int i = 0; i < result.StartPosition + args.EncountOffset; ++i)
                    {
                        rng.Next();
                    }
                    if (args.DeterminesNature)
                    {
                        nature = rng.DetermineNature(Gen4RngLib.GameVersion.HGSS, result.SynchroNature);
                    }
                    else
                    {
                        rng.GenerateIndividual(-1, individual);
                        nature = individual.GetNature();
                    }
                    sw.WriteLine($"{result.InitialSeed:X8},{result.StartPosition},{nature}");
                }
            }
            var startInfo = new ProcessStartInfo()
            {
                FileName = "output.txt",
                UseShellExecute = true,
                CreateNoWindow = true,
            };
            Process.Start(startInfo);

            MessageBox.Show($"処理時間: {stopwatch.Elapsed.TotalSeconds:F2} 秒");
        }
    }
}
