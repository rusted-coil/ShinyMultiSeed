using FormRx.Button;
using ShinyMultiSeed.Calculator;

namespace ShinyMultiSeed
{
    public partial class MainForm : Form
    {
        public IButton CalculateButton { get; }

        public MainForm()
        {
            InitializeComponent();
            CalculateButton = ButtonFactory.CreateButton(m_CalculateButton);
        }

        public Gen4SeedCalculatorArgs GetGen4SeedCalculatorArgs()
        {
            // TODO フォームから適切に取得する
            return new Gen4SeedCalculatorArgs
            {
                FrameMin = 900,
                FrameMax = 4500,
                PositionMin = 0,
                PositionMax = 450,
                EncountOffset = 0,
                DeterminesNature = true,
                IsShiny = true,
                Tsv = (24485 ^ 59064) & 0xfff8,
                FiltersAtkIV = true,
                AtkIVMin = 0,
                AtkIVMax = 1,
                FiltersSpdIV = true,
                SpdIVMin = 0,
                SpdIVMax = 1,
                UsesSynchro = true,
            };
        }
    }
}
