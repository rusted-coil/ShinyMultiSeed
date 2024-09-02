namespace ShinyMultiSeed.Result
{
    /// <summary>
    /// 計算結果全体のViewModelのインターフェースです。
    /// </summary>
    public interface IResultViewModel
    {
        /// <summary>
        /// 全体の説明テキストを取得します。
        /// </summary>
        string OverviewText { get; }

        /// <summary>
        /// カラムの情報を取得します。
        /// </summary>
        IReadOnlyList<IResultColumnViewModel> Columns { get; }

        /// <summary>
        /// 結果をRowとするリストを取得します。
        /// </summary>
        IReadOnlyList<object> Rows { get; }
    }
}
