using FormRx.Button;
using ShinyMultiSeed.Main.View;
using ShinyMultiSeed.Test.Doubles.Control;
using System.Reactive.Subjects;

namespace ShinyMultiSeed.Test.Doubles.MainForm
{
    internal class FakeMainFormGen4View : IMainFormGen4View
    {
        public bool IsHgssChecked { get; set; }
        public int EncountType { get; set; }
        public bool IsShinyChecked { get; set; }
        public string TidText { get; set; } = string.Empty;
        public string SidText { get; set; } = string.Empty;
        public bool FiltersAtkIVChecked { get; set; }
        public decimal AtkIVMinValue { get; set; }
        public decimal AtkIVMaxValue { get; set; }
        public bool FiltersSpdIVChecked { get; set; }
        public decimal SpdIVMinValue { get; set; }
        public decimal SpdIVMaxValue { get; set; }
        public bool UsesSynchroChecked { get; set; }
        public string FrameMinText { get; set; } = string.Empty;
        public string FrameMaxText { get; set; } = string.Empty;
        public string PositionMinText { get; set; } = string.Empty;
        public string PositionMaxText { get; set; } = string.Empty;
        public decimal MultiSeedCount { get; set; }

        Subject<bool> m_isHgssCheckedChanged = new Subject<bool>();
        public IObservable<bool> IsHgssCheckedChanged => m_isHgssCheckedChanged;

        FakeButton m_CalculateButton = new FakeButton();
        public IButton CalculateButton => m_CalculateButton;

        public void SetIsCalculating(bool isCalculating) => throw new NotSupportedException();

        public void SetSelectableEncountTypes(IReadOnlyList<KeyValuePair<int, string>> encountTypes)
        {
        }

        public void FireCalculateButton() => m_CalculateButton.FireClicked();
    }
}
