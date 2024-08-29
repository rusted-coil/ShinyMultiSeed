namespace ShinyMultiSeed.Calculator
{
    /// <summary>
    /// 初期seedカリキュレータ一つの結果を取得するインターフェースです。
    /// </summary>
    /// <typeparam name="SeedType">扱うseedの型</typeparam>
    public interface ISeedCalculatorResult<SeedType>
    {
        /// <summary>
        /// 初期seedの値を取得します。
        /// </summary>
        SeedType InitialSeed { get; }

        /// <summary>
        /// 個体生成を開始する消費数を取得します。
        /// </summary>
        uint StartPosition { get; }

        /// <summary>
        /// シンクロを使用する場合の性格を取得します。
        /// <para> * シンクロを使用しない、あるいはシンクロ不可の場合、-1を返します。</para>
        /// </summary>
        int SynchroNature { get; }
    }

    /// <summary>
    /// 初期seedを計算するカリキュレータのインターフェースです。
    /// </summary>
    /// <typeparam name="SeedType">扱うseedの型</typeparam>
    public interface ISeedCalculator<SeedType>
    {
        /// <summary>
        /// 全ての計算を実行します。
        /// <para> * threadCountに1を指定するとシングルスレッド、2以上を指定するとそのスレッド数のマルチスレッドで実行します。</para>
        /// </summary>
        IEnumerable<ISeedCalculatorResult<SeedType>> Calculate(int threadCount);
    }
}
