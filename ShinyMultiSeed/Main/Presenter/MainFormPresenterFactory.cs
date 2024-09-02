using ShinyMultiSeed.Calculator.Provider;
using ShinyMultiSeed.Config;
using ShinyMultiSeed.Main.View;

namespace ShinyMultiSeed.Main.Presenter
{
    /// <summary>
    /// MainForm関連のPresenterを作成するファクトリクラスです。
    /// </summary>
    public static class MainFormPresenterFactory
    {
        /// <summary>
        /// Config部分のPresenterを作成します。
        /// </summary>
        /// <param name="config">このPresenterが編集するGeneralConfig</param>
        /// <param name="view">Config部分のView</param>
        /// <param name="serializeConfig">GeneralConfigを保存するためのアクション</param>
        /// <returns>作成したPresenter</returns>
        public static IMainFormConfigPresenter CreateConfigPresenter(
            IModifiableGeneralConfig config,
            IMainFormConfigView view, 
            Func<bool> serializeConfig)
        {
            return new Internal.MainFormConfigPresenter(config, view, serializeConfig);
        }

        /// <summary>
        /// 第4世代部分のPresenterを作成します。
        /// </summary>
        /// <param name="generalConfig">このPresenterが読み取るGeneralConfig</param>
        /// <param name="config">このPresenterが編集するGen4Config</param>
        /// <param name="calculatorProvider">SeedCalculatorのプロバイダ</param>
        /// <param name="view">第4世代部分のView</param>
        /// <param name="resultPresenter">結果を出力するためのPresenter</param>
        /// <param name="serializeGen4Config">Gen4Configを保存するためのアクション</param>
        /// <returns>作成したPresenter</returns>
        public static IMainFormGen4Presenter CreateGen4Presenter(
            IGeneralConfig generalConfig,
            IModifiableGen4Config config,
            IGen4SeedCalculatorProvider calculatorProvider,
            IMainFormGen4View view,
            IMainFormResultPresenter resultPresenter,
            Func<bool> serializeGen4Config)            
        { 
            return new Internal.MainFormGen4Presenter(generalConfig, config, calculatorProvider, view, resultPresenter, serializeGen4Config);
        }

        /// <summary>
        /// 結果表示部分のPresenterを作成します。
        /// </summary>
        /// <param name="view">結果部分のView</param>
        /// <returns>作成したPresenter</returns>
        public static IMainFormResultPresenter CreateResultPresenter(IMainFormResultView view)
        {
            return new Internal.MainFormResultPresenter(view);
        }
    }
}
