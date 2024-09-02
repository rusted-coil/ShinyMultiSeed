namespace ShinyMultiSeed.Calculator.Provider
{
    public static class SeedCalculatorProviderFactory
    {
        /// <summary>
        /// 第4世代の初期seedカリキュレータProviderを作成します。
        /// </summary>
        public static IGen4SeedCalculatorProvider CreateGen4SeedCalculatorProvider()
        {
            return new Internal.Gen4SeedCalculatorProvider();
        }
    }
}
