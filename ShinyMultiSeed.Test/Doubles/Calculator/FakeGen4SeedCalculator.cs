using ShinyMultiSeed.Calculator;

namespace ShinyMultiSeed.Test.Doubles.Calculator
{
    internal class FakeGen4SeedCalculator : ISeedCalculator<uint>
    {
        public IEnumerable<ISeedCalculatorResult<uint>> Calculate(int threadCount)
        {
            throw new NotImplementedException();
        }
    }
}
