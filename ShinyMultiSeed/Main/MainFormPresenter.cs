using Gen4RngLib.Individual;
using Gen4RngLib.Rng;
using ShinyMultiSeed.Calculator;
using ShinyMultiSeed.Config;
using ShinyMultiSeed.Infrastructure;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Runtime.Versioning;
using System.Text;

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

        void ShowError(string message) => MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

        void Calculate()
        {
            if (!TryReflectConfig(out string reflectConfigError))
            {
                ShowError(reflectConfigError);
                return;
            }

            if (!TrySaveConfig(out string saveConfigError))
            {
                ShowError(saveConfigError);
            }

            ExecuteCalculation();
        }

        // フォームから設定を取得
        bool TryReflectConfig(out string errorMessage)
        {
            return m_MainForm.ReflectToConfig(m_Config, m_Gen4Config, out errorMessage);
        }

        // 現在の状態を保存
        bool TrySaveConfig(out string errorMessage)
        {
            StringBuilder sb = new StringBuilder();
            if (!Serializer.Serialize(c_ConfigPath, m_Config, out string configSerializeError))
            {
                sb.AppendLine($"{c_ConfigPath}の保存に失敗しました。\n----------\n" + configSerializeError);
            }
            if (!Serializer.Serialize(c_Gen4ConfigPath, m_Gen4Config, out string gen4ConfigSerializeError))
            {
                sb.AppendLine($"{c_Gen4ConfigPath}の保存に失敗しました。\n----------\n" + gen4ConfigSerializeError);
            }
            errorMessage = sb.ToString();
            return sb.Length == 0;
        }

        // 計算を実行して結果を出力
        void ExecuteCalculation()
        {
            var args = ConfigConverter.ConvertToGen4SeedCalculatorArgs(m_Config, m_Gen4Config);
            var calculator = SeedCalculatorFactory.CreateGen4SeedCalculator(args);

            var stopwatch = new Stopwatch(); // 処理時間測定用のストップウォッチ
            stopwatch.Start();

            calculator.CalculateAll(16);

            stopwatch.Stop();

            OutputResult(args, calculator.Results, stopwatch.Elapsed.TotalSeconds);
        }

        // 結果を出力
        void OutputResult(Gen4SeedCalculatorArgs args, IEnumerable<ISeedCalculatorResult<uint>> results, double elapsedSeconds)
        {
            var sortedResults = results.OrderBy(result => result.InitialSeed).ToList();
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

            MessageBox.Show($"処理時間: {elapsedSeconds:F2} 秒");
        }
    }
}
