using System.Reactive.Disposables;
using System.Runtime.Versioning;

namespace ShinyMultiSeed.Main
{
    [SupportedOSPlatform("win-x64")]
    internal sealed class MainFormPresenter : IDisposable
    {
        readonly MainForm m_MainForm;
        readonly CompositeDisposable m_Disposables = new CompositeDisposable();

        public MainFormPresenter()
        { 
            m_MainForm = new MainForm();
        }

        public void Run()
        {
            Application.Run(m_MainForm);
        }

        public void Dispose()
        {
            m_Disposables.Dispose();
        }
    }
}
