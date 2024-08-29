using Gen4RngLib.Rng;

namespace ShinyMultiSeed.Calculator.Strategy
{
    /// <summary>
    /// 初期seedをチェックするStrategyのファクトリクラスです。
    /// </summary>
    public static class SeedCheckStrategyFactory
    {
        /// <summary>
        /// 第4世代の初期seedをチェックするStrategyを作成します。
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ISeedCheckStrategy<uint, IGen4SeedCheckResult> CreateGen4SeedCheckStrategy(Gen4SeedCheckStrategyArgs args)
        {
            return new Internal.Gen4SeedCheckStrategy(args, () => RngFactory.CreateLcgRng(0), () => RngFactory.CreateReverseLcgRng(0));
        }
    }
}
