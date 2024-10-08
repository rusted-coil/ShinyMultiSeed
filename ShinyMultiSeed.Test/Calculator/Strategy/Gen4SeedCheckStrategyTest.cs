using ShinyMultiSeed.Calculator.Strategy;

namespace ShinyMultiSeed.Test.Calculator.Strategy
{
    [TestClass]
    public class Gen4SeedCheckStrategyTest
    {
        // DPtの伝説が上手くいくかどうかのテスト
		[TestMethod]
		public void Check_DPtLegendaryKnownSeed_ReturnsExpectedResult()
		{
			var args = new Gen4SeedCheckStrategyArgs {
				IsHgss = false,
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
                IsUnownRadio = false,
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
				Assert.AreEqual(result.StartPosition, 263u);
				Assert.AreEqual(result.SynchroNature, 8);
			}
		}

		// DPtの徘徊が上手くいくかどうかのテスト
		[TestMethod]
		public void Check_DPtRoamerKnownSeed_ReturnsExpectedResult()
		{
			var args = new Gen4SeedCheckStrategyArgs {
				IsHgss = false,
				EncountOffset = 0,
				DeterminesNature = false,
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
                IsUnownRadio = false,
            };

			var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);

			{
				var result = strategy.Check(0x29040AB6u);

				Assert.AreEqual(result.IsPassed, true);
				Assert.AreEqual(result.StartPosition, 171u);
				Assert.AreEqual(result.SynchroNature, -1);
			}

			{
				var result = strategy.Check(0x29040AB8u);

				Assert.AreEqual(result.IsPassed, true);
				Assert.AreEqual(result.StartPosition, 292u);
				Assert.AreEqual(result.SynchroNature, -1);
			}
		}

		// DPtの野生が上手くいくかどうかのテスト
		[TestMethod]
		public void Check_DPtWildKnownSeed_ReturnsExpectedResult()
		{
			var args = new Gen4SeedCheckStrategyArgs {
				IsHgss = false,
				EncountOffset = 1,
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
                IsUnownRadio = false,
            };

			var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);

			{
				var result = strategy.Check(0x29040AB6u);

				Assert.AreEqual(result.IsPassed, true);
				Assert.AreEqual(result.StartPosition, 125u);
				Assert.AreEqual(result.SynchroNature, 11);
			}

			{
				var result = strategy.Check(0x29040AB8u);

				Assert.AreEqual(result.IsPassed, true);
				Assert.AreEqual(result.StartPosition, 262u);
				Assert.AreEqual(result.SynchroNature, 8);
			}
		}

		// DPtのアンノーンが上手くいくかどうかのテスト（候補なし）
		[TestMethod]
		public void Check_DPtUnownKnownSeed_ReturnsExpectedResult()
		{
			var args = new Gen4SeedCheckStrategyArgs {
				IsHgss = false,
				EncountOffset = 1,
				DeterminesNature = true,
				PositionMin = 0,
				PositionMax = 150,
				IsShiny = true,
				Tsv = (24485 ^ 59064) & 0xfff8,
				FiltersAtkIV = false,
				AtkIVMin = 0,
				AtkIVMax = 1,
				FiltersSpdIV = false,
				SpdIVMin = 0,
				SpdIVMax = 1,
				UsesSynchro = false,
				IsUnownRadio = true,
			};

			var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);

			{
				var result = strategy.Check(0xA10703E6u);

				Assert.AreEqual(result.IsPassed, false);
			}

			{
				var result = strategy.Check(0xA10703E8u);

                Assert.AreEqual(result.IsPassed, false);
			}
		}

		// HGSSの伝説が上手くいくかどうかのテスト
		[TestMethod]
        public void Check_HGSSLegendaryKnownSeed_ReturnsExpectedResult()
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
				IsUnownRadio = false,
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

		// HGSSの徘徊が上手くいくかどうかのテスト
		[TestMethod]
		public void Check_HGSSRoamerKnownSeed_ReturnsExpectedResult()
		{
			var args = new Gen4SeedCheckStrategyArgs {
				IsHgss = true,
				EncountOffset = 0,
				DeterminesNature = false,
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
				IsUnownRadio = false,
			};

			var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);

			{
				var result = strategy.Check(0x29040AB6u);

				Assert.AreEqual(result.IsPassed, true);
				Assert.AreEqual(result.StartPosition, 171u);
				Assert.AreEqual(result.SynchroNature, -1);
			}

			{
				var result = strategy.Check(0x29040AB8u);

				Assert.AreEqual(result.IsPassed, true);
				Assert.AreEqual(result.StartPosition, 292u);
				Assert.AreEqual(result.SynchroNature, -1);
			}
		}

        // HGSSの野生が上手くいくかどうかのテスト
        [TestMethod]
        public void Check_HGSSWildKnownSeed_ReturnsExpectedResult()
        {
            var args = new Gen4SeedCheckStrategyArgs
            {
                IsHgss = true,
                EncountOffset = 1,
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
				IsUnownRadio = false,
            };

            var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);

            {
                var result = strategy.Check(0x29040AB6u);

                Assert.AreEqual(result.IsPassed, true);
                Assert.AreEqual(result.StartPosition, 125u);
                Assert.AreEqual(result.SynchroNature, 11);
            }

            {
                var result = strategy.Check(0x29040AB8u);

                Assert.AreEqual(result.IsPassed, true);
                Assert.AreEqual(result.StartPosition, 264u);
                Assert.AreEqual(result.SynchroNature, 8);
            }
        }

        // HGSSのアンノーンが上手くいくかどうかのテスト
        [TestMethod]
        public void Check_HGSSUnownKnownSeed_ReturnsExpectedResult()
        {
            var args = new Gen4SeedCheckStrategyArgs
            {
                IsHgss = true,
                EncountOffset = 1,
                DeterminesNature = true,
                PositionMin = 0,
                PositionMax = 150,
                IsShiny = true,
                Tsv = (24485 ^ 59064) & 0xfff8,
                FiltersAtkIV = false,
                AtkIVMin = 0,
                AtkIVMax = 31,
                FiltersSpdIV = false,
                SpdIVMin = 0,
                SpdIVMax = 31,
                UsesSynchro = false,
                IsUnownRadio = true,
            };

            var strategy = SeedCheckStrategyFactory.CreateGen4SeedCheckStrategy(args);

            {
                var result = strategy.Check(0xA10703E6u);

                Assert.AreEqual(result.IsPassed, true);
                Assert.AreEqual(result.StartPosition, 35u);
                Assert.AreEqual(result.SynchroNature, -1);
            }

            {
                var result = strategy.Check(0xA10703E8u);

                Assert.AreEqual(result.IsPassed, true);
                Assert.AreEqual(result.StartPosition, 22u);
                Assert.AreEqual(result.SynchroNature, -1);
            }
        }
    }
}