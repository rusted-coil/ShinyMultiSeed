using ShinyMultiSeed.Main;

namespace ShinyMultiSeed
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            using (var presenter = new MainFormPresenter())
            {
                presenter.Run();
            }
        }
    }
}