using ShinyMultiSeed.Result;

namespace ShinyMultiSeed.Main.View
{
    public interface IMainFormResultView
    {
        /// <summary>
        /// 全体の説明テキストを設定します。
        /// </summary>
        string OverViewText { set; }

        /// <summary>
        /// カラムのViewModelを設定します。
        /// </summary>
        void SetResultColumns(IReadOnlyList<IResultColumnViewModel> columnViewModels);

        /// <summary>
        /// 結果をRowとするリストのオブジェクトを設定します。
        /// </summary>
        void SetResultRows(IReadOnlyList<object> rowViewModels);
    }
}
