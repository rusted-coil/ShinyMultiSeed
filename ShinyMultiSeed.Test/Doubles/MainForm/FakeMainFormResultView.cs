using ShinyMultiSeed.Main.View;
using ShinyMultiSeed.Result;

namespace ShinyMultiSeed.Test.Doubles.MainForm
{
    internal class FakeMainFormResultView : IMainFormResultView
    {
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
