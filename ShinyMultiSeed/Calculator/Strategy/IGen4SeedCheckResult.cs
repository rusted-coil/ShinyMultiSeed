namespace ShinyMultiSeed.Calculator.Strategy
{
    public interface IGen4SeedCheckResult
    {
        /// <summary>
        /// この初期seedが条件を満たしたかどうかを取得します。
        /// </summary>
        bool IsPassed { get; }

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
}
