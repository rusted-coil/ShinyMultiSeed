namespace ShinyMultiSeed.Calculator
{
    /// <summary>
    /// 初期seedを計算するカリキュレータのインターフェースです。
    /// </summary>
    /// <typeparam name="SeedType">扱うseedの型</typeparam>
    public interface ISeedCalculator<SeedType>
    {
        /// <summary>
        /// 計算結果を取得します。
        /// <para> * 目的に合致する初期seedと、その初期seedからの消費数の組の列挙子を返します。</para>
        /// </summary>
        IEnumerable<(SeedType InitialSeed, uint StartPosition)> Results { get; }

        /// <summary>
        /// 計算結果をクリアします。
        /// </summary>
        void Clear();

        /// <summary>
        /// 計算をいくつかのタスクに分割し、そのうちの一つを実行します。
        /// </summary>
        void CalculatePart(uint partIndex, uint partCount);

        /// <summary>
        /// 全ての計算を実行します。
        /// <para> * threadCountに1を指定するとシングルスレッド、2以上を指定するとそのスレッド数のマルチスレッドで実行します。</para>
        /// </summary>
        void CalculateAll(int threadCount);
    }
}
