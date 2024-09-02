using ShinyMultiSeed.Result;

namespace ShinyMultiSeed.Main.Internal
{
    internal interface IMainFormResultPresenter
    {
        /// <summary>
        /// ResultViewModelを表示します。
        /// </summary>
        void ShowResult(IResultViewModel viewModel);
    }
}
