using ShinyMultiSeed.Main.View;
using System.Reactive.Subjects;

namespace ShinyMultiSeed.Test.Doubles.MainForm
{
    /// <summary>
    /// MainFormConfigViewのテスト用ダブルです。
    /// </summary>
    internal class FakeMainFormConfigView : IMainFormConfigView
    {
        Subject<int> m_ThreadCountButtonClicked = new Subject<int>();
        public IObservable<int> ThreadCountButtonClicked => m_ThreadCountButtonClicked;

        public int ThreadCountIndex { get; private set; } = -1;
        public void SetThreadCountIndex(int threadCountIndex) => ThreadCountIndex = threadCountIndex;

        /// <summary>
        /// ThreadCountButtonClickedを発火させます。
        /// </summary>
        public void FireThreadCountButtonClicked(int arg) => m_ThreadCountButtonClicked.OnNext(arg);
    }
}
