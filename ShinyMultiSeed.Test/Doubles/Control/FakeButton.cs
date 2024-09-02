using FormRx.Button;
using System.Reactive;
using System.Reactive.Subjects;

namespace ShinyMultiSeed.Test.Doubles.Control
{
    internal class FakeButton : IButton
    {
        Subject<Unit> m_Clicked = new Subject<Unit>();
        public IObservable<Unit> Clicked => m_Clicked;

        public void FireClicked() => m_Clicked.OnNext(default);
    }
}
