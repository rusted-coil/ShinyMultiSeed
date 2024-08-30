using FormRx.Button;
using ShinyMultiSeed.Config;
using System.Text;

namespace ShinyMultiSeed
{
    public partial class MainForm : Form
    {
        public IButton CalculateButton { get; }

        public MainForm(ConfigData config, Gen4Config gen4Config)
        {
            InitializeComponent();
            ReflectFromConfig(config, gen4Config);
            CalculateButton = ButtonFactory.CreateButton(m_CalculateButton);
        }

        void ReflectFromConfig(ConfigData config, Gen4Config gen4Config)
        {
            m_Gen4IsHgssCheck.Checked = gen4Config.IsHgss;
            m_Gen4IsShinyCheck.Checked = gen4Config.IsShiny;
            m_Gen4TidBox.Text = gen4Config.Tid.ToString();
            m_Gen4SidBox.Text = gen4Config.Sid.ToString();
            m_Gen4FiltersAtkCheck.Checked = gen4Config.FiltersAtkIV;
            m_Gen4FiltersAtkIVMinBox.Value = gen4Config.AtkIVMin;
            m_Gen4FiltersAtkIVMaxBox.Value = gen4Config.AtkIVMax;
            m_Gen4FiltersSpdCheck.Checked = gen4Config.FiltersSpdIV;
            m_Gen4FiltersSpdIVMinBox.Value = gen4Config.SpdIVMin;
            m_Gen4FiltersSpdIVMaxBox.Value = gen4Config.SpdIVMax;
            m_Gen4UsesSynchroCheck.Checked = gen4Config.UsesSynchro;
            m_Gen4FrameMin.Text = gen4Config.FrameMin.ToString();
            m_Gen4FrameMax.Text = gen4Config.FrameMax.ToString();
            m_Gen4PositionMin.Text = gen4Config.PositionMin.ToString();
            m_Gen4PositionMax.Text = gen4Config.PositionMax.ToString();
        }

        private void ValidateAndSetUInt(TextBox textBox, Action<uint> setProperty, StringBuilder sb, string fieldName)
        {
            if (uint.TryParse(textBox.Text, out uint result))
            {
                setProperty(result);
            }
            else
            {
                sb.AppendLine($"{fieldName}�̌`�����s���ł��B: {textBox.Text}");
            }
        }

        private void ValidateAndSetUInt(NumericUpDown numericUpDown, Action<uint> setProperty, StringBuilder sb, string fieldName)
        {
            if (numericUpDown.Value >= uint.MinValue && numericUpDown.Value <= uint.MaxValue)
            {
                setProperty((uint)numericUpDown.Value);
            }
            else
            {
                sb.AppendLine($"{fieldName}�̌`�����s���ł��B: {numericUpDown.Value.ToString()}");
            }
        }

        /// <summary>
        /// �t�H�[���̏�Ԃ��R���t�B�O�ɔ��f���܂��B
        /// </summary>
        /// <returns>���������ꍇ��true�A���s�����ꍇ��false��Ԃ��AerrorString�ɃG���[���b�Z�[�W���i�[���܂��B</returns>
        public bool ReflectToConfig(ConfigData config, Gen4Config gen4Config, out string errorMessage)
        {
            StringBuilder sb = new StringBuilder();

            gen4Config.IsHgss = m_Gen4IsHgssCheck.Checked;
            gen4Config.IsShiny = m_Gen4IsShinyCheck.Checked;
            ValidateAndSetUInt(m_Gen4TidBox, value => gen4Config.Tid = value, sb, "�\ID");
            ValidateAndSetUInt(m_Gen4SidBox, value => gen4Config.Sid = value, sb, "��ID");
            gen4Config.FiltersAtkIV = m_Gen4FiltersAtkCheck.Checked;
            ValidateAndSetUInt(m_Gen4FiltersAtkIVMinBox, value => gen4Config.AtkIVMin = value, sb, "A�̒lMin");
            ValidateAndSetUInt(m_Gen4FiltersAtkIVMaxBox, value => gen4Config.AtkIVMax = value, sb, "A�̒lMax");
            gen4Config.FiltersSpdIV = m_Gen4FiltersSpdCheck.Checked;
            ValidateAndSetUInt(m_Gen4FiltersSpdIVMinBox, value => gen4Config.SpdIVMin = value, sb, "S�̒lMin");
            ValidateAndSetUInt(m_Gen4FiltersSpdIVMaxBox, value => gen4Config.SpdIVMax = value, sb, "S�̒lMax");
            gen4Config.UsesSynchro = m_Gen4UsesSynchroCheck.Checked;
            ValidateAndSetUInt(m_Gen4FrameMin, value => gen4Config.FrameMin = value, sb, "�t���[��Min");
            ValidateAndSetUInt(m_Gen4FrameMax, value => gen4Config.FrameMax = value, sb, "�t���[��Max");
            ValidateAndSetUInt(m_Gen4PositionMin, value => gen4Config.PositionMin = value, sb, "���Min");
            ValidateAndSetUInt(m_Gen4PositionMax, value => gen4Config.PositionMax = value, sb, "���Max");

            if (sb.Length > 0)
            {
                errorMessage = sb.ToString();
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// ���݌v�Z�����ǂ������t�H�[���ɔ��f���܂��B
        /// </summary>
        public void SetIsCalculating(bool isCalculating)
        {
            if (isCalculating)
            {
                m_CalculateButton.Enabled = false;
                m_CalculateButton.Text = "�v�Z��...";
                m_CalculateButton.BackColor = Color.LightGray;
            }
            else
            {
                m_CalculateButton.Enabled = true;
                m_CalculateButton.Text = "�v�Z";
                m_CalculateButton.BackColor = Color.Yellow;
            }            
        }
    }
}
