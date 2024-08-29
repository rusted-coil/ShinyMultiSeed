using ShinyMultiSeed.Calculator.Strategy;

namespace ShinyMultiSeed.Calculator
{
    public static class SeedCalculatorFactory
    {
        /// <summary>
        /// 第4世代の初期seedカリキュレータを作成します。
        /// </summary>
        /// <param name="strategy">初期seedを判定するためのStrategy</param>
        /// <param name="frameMin">フレーム(下位2byte)の範囲下限</param>
        /// <param name="frameMax">フレーム(下位2byte)の範囲上限</param>
        /// <param name="multiSeedCount">多面待ち候補数</param>
        /// <returns></returns>
        public static ISeedCalculator<uint> CreateGen4SeedCalculator(
            ISeedCheckStrategy<uint, IGen4SeedCheckResult> strategy,
            uint frameMin, uint frameMax,
            uint multiSeedCount)
        {
            return new Internal.Gen4SeedCalculator(strategy, frameMin, frameMax, multiSeedCount);
        }
    }
}
