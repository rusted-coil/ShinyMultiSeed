using ShinyMultiSeed.Infrastructure;
using ShinyMultiSeed.Main.Internal;

namespace ShinyMultiSeed
{
    internal static class Program
    {
        const string c_ConfigPath = "config.json";
        const string c_Gen4ConfigPath = "config.gen4.json";

        [STAThread]
        static void Main()
        {
            // 必要なデータを読み込んでインスタンス保持
            Config.Internal.GeneralConfig generalConfig = Serializer.Deserialize<Config.Internal.GeneralConfig>(c_ConfigPath);
            Config.Internal.Gen4Config gen4Config = Serializer.Deserialize<Config.Internal.Gen4Config>(c_Gen4ConfigPath);

            ApplicationConfiguration.Initialize();
            var mainForm = new MainForm();

            using (var configPresenter = new MainFormConfigPresenter(mainForm, generalConfig, CreateSerializeAction(c_ConfigPath, generalConfig)))
            using (var gen4Presenter =  new MainFormGen4Presenter(mainForm, generalConfig, gen4Config, CreateSerializeAction(c_Gen4ConfigPath, gen4Config)))
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