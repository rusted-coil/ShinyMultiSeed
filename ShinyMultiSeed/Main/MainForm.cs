using FormRx.Button;

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
    }
}
