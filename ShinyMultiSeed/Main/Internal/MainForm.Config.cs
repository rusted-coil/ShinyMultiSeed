using ShinyMultiSeed.Main;
using System.Reactive.Subjects;

namespace ShinyMultiSeed
{
    internal partial class MainForm : IMainFormConfig
    {
        public IMainFormConfig MainFormConfig => this;

        Subject<int> m_ThreadCountChanged = new Subject<int>();
        public IObservable<int> ThreadCountChanged => m_ThreadCountChanged;

        (ToolStripMenuItem, int)[] m_ThreadCountMenuItems;

        void InitializeMainFormConfig()
        {
            m_ThreadCountMenuItems = [
                (m_ThreadCountConfig1, 1),
                (m_ThreadCountConfig2, 2),
                (m_ThreadCountConfig4, 4),
                (m_ThreadCountConfig8, 8),
                (m_ThreadCountConfig16, 16),
                (m_ThreadCountConfig32, 32),
            ];
        }

        // ThreadCountの設定をコントロールに反映
        void ReflectThreadCount(int threadCount)
        {
            foreach (var pair in m_ThreadCountMenuItems)
            {
                pair.Item1.Checked = (pair.Item2 == threadCount);
            }
        }

    }
}
