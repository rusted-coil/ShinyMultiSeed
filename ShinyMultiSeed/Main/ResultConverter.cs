using ShinyMultiSeed.Calculator;
using ShinyMultiSeed.Result;

namespace ShinyMultiSeed.Main
{
    internal static class ResultConverter
    {
        public static IReadOnlyList<ResultViewModel> ConvertFromGen4Result(IEnumerable<ISeedCalculatorResult<uint>> results)
        {
            return results
                .OrderBy(result => result.InitialSeed)
                .Select(result => new ResultViewModel
                {
                    InitialSeed = $"{result.InitialSeed:X8}",
                    StartPosition = result.StartPosition,

                    // TODO 仮
                    SynchroNature = result.SynchroNature,
                })
                .ToArray();
        }
    }
}
