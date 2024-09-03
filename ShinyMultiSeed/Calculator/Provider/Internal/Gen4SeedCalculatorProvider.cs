using ShinyMultiSeed.Calculator.Strategy;
using ShinyMultiSeed.Config;

namespace ShinyMultiSeed.Calculator.Provider.Internal
{
    internal sealed class Gen4SeedCalculatorProvider : IGen4SeedCalculatorProvider
    {
        public ISeedCalculator<uint> CreateGen4SeedCalculator(IGeneralConfig generalConfig, IGen4Config gen4Config)
        {
            var args = ConfigConverter.ConvertToGen4SeedCheckStrategyArgs(generalConfig, gen4Config);
            var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);
            return SeedCalculatorFactory.CreateGen4SeedCalculator(strategy, gen4Config.FrameMin, gen4Config.FrameMax, gen4Config.MultiSeedCount);
        }
    }
}
