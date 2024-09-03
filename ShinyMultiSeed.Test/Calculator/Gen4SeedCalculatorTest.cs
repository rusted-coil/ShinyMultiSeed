using ShinyMultiSeed.Calculator;
using ShinyMultiSeed.Calculator.Strategy;

namespace ShinyMultiSeed.Test.Calculator
{
    [TestClass]
    public class Gen4SeedCalculatorTest
    {
        [TestMethod]
        public void Check_KnownParameter_ReturnsExpectedResult()
        {
            var args = new Gen4SeedCheckStrategyArgs
            {
                IsHgss = true,
                EncountOffset = 0,
                DeterminesNature = true,
                PositionMin = 0,
                PositionMax = 450,
                IsShiny = true,
                Tsv = (24485 ^ 59064) & 0xfff8,
                FiltersAtkIV = true,
                AtkIVMin = 0,
                AtkIVMax = 1,
                FiltersSpdIV = true,
                SpdIVMin = 0,
                SpdIVMax = 1,
                UsesSynchro = true,
            };

            var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);

            var calculator = SeedCalculatorFactory.CreateGen4SeedCalculator(strategy, 2742, 2744, 2);
            var result = calculator.Calculate(1);
            Assert.AreEqual(result.Count(), 8);
        }

        [TestMethod]
        public void Check_ZeroPosition_ReturnsExpectedResult()
        {
            var args = new Gen4SeedCheckStrategyArgs
            {
                IsHgss = true,
                EncountOffset = 0,
                DeterminesNature = true,
                PositionMin = 0,
                PositionMax = 0,
                IsShiny = true,
                Tsv = (24485 ^ 59064) & 0xfff8,
                FiltersAtkIV = false,
                FiltersSpdIV = false,
                UsesSynchro = true,
            };

            var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);

            var calculator = SeedCalculatorFactory.CreateGen4SeedCalculator(strategy, 900, 4500, 2);
            var result = calculator.Calculate(16);
            Assert.AreEqual(result.Count(), 54);
        }
    }
}
