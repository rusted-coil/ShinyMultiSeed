using Gen4RngLib.Rng;
using ShinyMultiSeed.Calculator.Provider;
using ShinyMultiSeed.Infrastructure;
using ShinyMultiSeed.Main.Presenter;

namespace ShinyMultiSeed
{
    internal static class Program
    {
        const string c_ConfigPath = "config.json";
        const string c_Gen4ConfigPath = "config.gen4.json";

        [STAThread]
        static void Main()
        {
            // RNG用キャッシュを準備
            RngFactory.CalculateLcgRngCache();
            RngFactory.CalculateReverseLcgCache();

            // 必要なデータを読み込んでインスタンス保持
            Config.Internal.GeneralConfig generalConfig = Serializer.Deserialize<Config.Internal.GeneralConfig>(c_ConfigPath);
            Config.Internal.Gen4Config gen4Config = Serializer.Deserialize<Config.Internal.Gen4Config>(c_Gen4ConfigPath);

            ApplicationConfiguration.Initialize();
            var mainForm = new MainForm();

            using (var cofigPresenter = MainFormPresenterFactory.CreateConfigPresenter(generalConfig, mainForm, CreateSerializeAction(c_ConfigPath, generalConfig)))
            using (var resultPresenter = MainFormPresenterFactory.CreateResultPresenter(mainForm))
            using (var gen4Presenter = MainFormPresenterFactory.CreateGen4Presenter(
                generalConfig,
                gen4Config,
                SeedCalculatorProviderFactory.CreateGen4SeedCalculatorProvider(),
                mainForm,
                resultPresenter,
                CreateSerializeAction(c_Gen4ConfigPath, gen4Config)))
            {
                Application.Run(mainForm);
            }
        }

        static Func<bool> CreateSerializeAction<T>(string path, T data)
        {
            return () =>
            {
                if (!Serializer.Serialize(path, data, out string error))
                {
                    MessageBox.Show(error, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            };
        }
    }
}