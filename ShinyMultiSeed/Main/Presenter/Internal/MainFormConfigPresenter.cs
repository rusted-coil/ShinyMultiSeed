using ShinyMultiSeed.Config;
using ShinyMultiSeed.Main.View;
using System.Reactive.Disposables;

namespace ShinyMultiSeed.Main.Presenter.Internal
{
    /// <summary>
    /// MainFormの設定に関わる部分のPresenterです。
    /// </summary>
    internal sealed class MainFormConfigPresenter : IMainFormConfigPresenter
    {
        int[] m_ThreadCounts = [1, 2, 4, 8, 16, 32];

        readonly IModifiableGeneralConfig m_Config;
        readonly IMainFormConfigView m_View;
        readonly Func<bool> m_SerializeConfig;
        readonly CompositeDisposable m_Disposables = new CompositeDisposable();

        public MainFormConfigPresenter(
            IModifiableGeneralConfig config,
            IMainFormConfigView view, 
            Func<bool> serializeConfig)
        {
            m_Config = config;
            m_View = view;
            m_SerializeConfig = serializeConfig;

            InitializeView();

            m_Disposables.Add(view.ThreadCountButtonClicked.Subscribe(SetThreadCount));
        }

        public void Dispose()
        {
            m_Disposables.Dispose();
        }

        void InitializeView()
        {
            int index = 0;
            for (index = 0; index < m_ThreadCounts.Length; ++index)
            {
                if (m_ThreadCounts[index] == m_Config.ThreadCount)
                {
                    break;
                }
            }
            m_View.SetThreadCountIndex(index);
        }

        void SetThreadCount(int threadCountIndex)
        {
            m_Config.ThreadCount = m_ThreadCounts[threadCountIndex];
            if (m_SerializeConfig())
            {
                m_View.SetThreadCountIndex(threadCountIndex);
            }
        }
    }
}
