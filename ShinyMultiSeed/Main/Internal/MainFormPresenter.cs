using ShinyMultiSeed.Calculator;
using ShinyMultiSeed.Calculator.Strategy;
using System.Runtime.Versioning;

namespace ShinyMultiSeed.Main.Internal
{
    [SupportedOSPlatform("windows")]
    internal sealed class MainFormPresenter
    {
        // 結果を出力
        void OutputResult(Gen4SeedCheckStrategyArgs args, IEnumerable<ISeedCalculatorResult<uint>> results, double elapsedSeconds)
        {
//            var resultViewModels = ResultConverter.ConvertFromGen4Result(results);
//            m_MainForm.SetGen4CalculationResult(elapsedSeconds, resultViewModels.Count, resultViewModels);

            /*
            var sortedResults = results.OrderBy(result => result.InitialSeed).ToList();
            using (var sw = new StreamWriter("output.txt"))
            {
                var tempRng = RngFactory.CreateLcgRng(0);
                Individual individual = new Individual();
                foreach (var result in sortedResults)
                {
                    var rng = RngFactory.CreateLcgRng(result.InitialSeed);
                    uint nature = 0;
                    for (int i = 0; i < result.StartPosition + args.EncountOffset; ++i)
                    {
                        rng.Next();
                    }
                    if (args.DeterminesNature)
                    {
                        nature = rng.DetermineNature(Gen4RngLib.GameVersion.HGSS, result.SynchroNature);
                    }
                    else
                    {
                        rng.GenerateIndividual(-1, individual);
                        nature = individual.GetNature();
                    }
                    sw.WriteLine($"{result.InitialSeed:X8},{result.StartPosition},{nature}");
                }
            }
            var startInfo = new ProcessStartInfo()
            {
                FileName = "output.txt",
                UseShellExecute = true,
                CreateNoWindow = true,
            };
            Process.Start(startInfo);
            */
        }
    }
}
