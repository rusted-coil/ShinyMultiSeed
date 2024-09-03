using ShinyMultiSeed.Result;

namespace ShinyMultiSeed.Main.Presenter
{
    /// <summary>
    /// MainFormの結果表示に関する部分のPresenterです。
    /// </summary>
    public interface IMainFormResultPresenter : IDisposable
    {
        /// <summary>
        /// ResultViewModelを表示します。
        /// </summary>
        void ShowResult(IResultViewModel viewModel);
    }
}
