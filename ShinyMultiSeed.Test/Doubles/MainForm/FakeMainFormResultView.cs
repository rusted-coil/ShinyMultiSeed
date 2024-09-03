using FormRx.Button;
using ShinyMultiSeed.Main.View;
using ShinyMultiSeed.Result;
using ShinyMultiSeed.Test.Doubles.Control;

namespace ShinyMultiSeed.Test.Doubles.MainForm
{
    internal class FakeMainFormResultView : IMainFormResultView
    {
        FakeButton m_OutputResultButton = new FakeButton();
        public IButton OutputResultButton => m_OutputResultButton;

        public string OverViewText { set => throw new NotImplementedException(); }

        public void SetResultColumns(IReadOnlyList<IResultColumnViewModel> columnViewModels)
        {
            throw new NotImplementedException();
        }

        public void SetResultRows(IReadOnlyList<object> rowViewModels)
        {
            throw new NotImplementedException();
        }
    }
}
