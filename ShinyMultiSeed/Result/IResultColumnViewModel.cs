namespace ShinyMultiSeed.Result
{
    /// <summary>
    /// 計算結果をテーブルとして表示する際のカラム情報を取得するインターフェースです。
    /// </summary>
    public interface IResultColumnViewModel
    {
        /// <summary>
        /// カラムのIDを取得します。
        /// <para> * IDはプロパティ名として使われます。</para>
        /// </summary>
        string Id { get; }

        /// <summary>
        /// ヘッダに表示されるテキストを取得します。
        /// </summary>
        string DisplayText { get; }

        /// <summary>
        /// カラムの幅を取得します。
        /// <para> * nullの場合、AutoSizeModeがColumnHeaderとなります。</para>
        /// </summary>
        int? Width { get; }
    }
}
