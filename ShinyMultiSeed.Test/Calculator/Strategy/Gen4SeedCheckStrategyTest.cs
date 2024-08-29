using ShinyMultiSeed.Calculator.Strategy;

namespace ShinyMultiSeed.Test.Calculator.Strategy
{
    [TestClass]
    public class Gen4SeedCheckStrategyTest
    {
        [TestMethod]
        public void Check_KnownSeed_ReturnsExpectedResult()
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

            {
                var result = strategy.Check(0x29040AB6u);

                Assert.AreEqual(result.IsPassed, true);
                Assert.AreEqual(result.StartPosition, 126u);
                Assert.AreEqual(result.SynchroNature, 11);
            }

            {
                var result = strategy.Check(0x29040AB8u);

                Assert.AreEqual(result.IsPassed, true);
                Assert.AreEqual(result.StartPosition, 265u);
                Assert.AreEqual(result.SynchroNature, 8);
            }
        }
    }
}