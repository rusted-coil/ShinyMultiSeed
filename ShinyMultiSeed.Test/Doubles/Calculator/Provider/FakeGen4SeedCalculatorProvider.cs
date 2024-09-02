using ShinyMultiSeed.Calculator;
using ShinyMultiSeed.Calculator.Provider;
using ShinyMultiSeed.Config;

namespace ShinyMultiSeed.Test.Doubles.Calculator.Provider
{
    internal class FakeGen4SeedCalculatorProvider : IGen4SeedCalculatorProvider
    {
        public IGeneralConfig? GeneralConfig { get; set; }
        public IGen4Config? Gen4Config { get; set; }

        public ISeedCalculator<uint> CreateGen4SeedCalculator(IGeneralConfig generalConfig, IGen4Config gen4Config)
        {
            GeneralConfig = generalConfig;
            Gen4Config = gen4Config;
            return new FakeGen4SeedCalculator();
        }
    }
}
