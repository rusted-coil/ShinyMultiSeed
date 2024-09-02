using ShinyMultiSeed.Main.View;
using ShinyMultiSeed.Result;

namespace ShinyMultiSeed.Main.Presenter.Internal
{
    internal sealed class MainFormResultPresenter : IDisposable, IMainFormResultPresenter
    {
        readonly IMainFormResultView m_View;

        public MainFormResultPresenter(IMainFormResultView view)
        {
            m_View = view;
        }

        public void Dispose()
        {
        }

        public void ShowResult(IResultViewModel viewModel)
        {
            m_View.OverViewText = viewModel.OverViewText;
            m_View.SetResultColumns(viewModel.Columns);
            m_View.SetResultRows(viewModel.Rows);

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
