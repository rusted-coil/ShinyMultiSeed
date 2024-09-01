using FormRx.Button;
using ShinyMultiSeed.Config;
using ShinyMultiSeed.Main;
using System.Text;

namespace ShinyMultiSeed
{
    internal partial class MainForm : Form, IMainForm
    {
        public enum EncountType
        {
            Legendary = 0,
            Roamer,
            Wild,
            Unown,
        }

        readonly List<KeyValuePair<int, string>> m_Gen4EncountTypes = new List<KeyValuePair<int, string>> {
            new KeyValuePair<int, string>((int)EncountType.Legendary, "�Œ�V���{��(�V���N����)"),
            new KeyValuePair<int, string>((int)EncountType.Roamer, "�p�j"),
            new KeyValuePair<int, string>((int)EncountType.Wild, "�쐶"),
            new KeyValuePair<int, string>((int)EncountType.Unown, "�A���m�[��(���W�I�L��)"),
        };

        public IButton CalculateButton { get; }

        public IMainFormGen4 MainFormGen4 => throw new NotImplementedException();

        public MainForm(ConfigData config, Gen4Config gen4Config)
        {
            InitializeComponent();

            InitializeMainFormConfig();

            InitializeComboBox();
            InitializeResultView();
            ReflectFromConfig(config, gen4Config);
            CalculateButton = ButtonFactory.CreateButton(m_CalculateButton);
        }

        private void m_ThreadCountConfig_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                foreach (var pair in m_ThreadCountMenuItems)
                {
                    pair.Item1.Checked = (pair.Item1 == menuItem);
                    if (pair.Item1 == menuItem)
                    {
                        m_ThreadCountChanged.OnNext(pair.Item2);
                    }
                }
            }
        }
        void InitializeComboBox()
        {
            m_Gen4EncountTypeList.DisplayMember = "Value";
            m_Gen4EncountTypeList.ValueMember = "Key";
            UpdateGen4EncountTypeList(false);
        }

        void UpdateGen4EncountTypeList(bool isHgss)
        {
            m_Gen4EncountTypeList.Items.Clear();
            m_Gen4EncountTypeList.Items.Add(m_Gen4EncountTypes[(int)EncountType.Legendary]);
            m_Gen4EncountTypeList.Items.Add(m_Gen4EncountTypes[(int)EncountType.Roamer]);
            m_Gen4EncountTypeList.Items.Add(m_Gen4EncountTypes[(int)EncountType.Wild]);
            if (isHgss) // ���W�I�L��A���m�[����HGSS�̂ݑΉ�
            {
                m_Gen4EncountTypeList.Items.Add(m_Gen4EncountTypes[(int)EncountType.Unown]);
            }
        }

        void SelectGen4EncountType(int gen4EncountType)
        {
            // m_Gen4EncountTypes����A�w�肳�ꂽKey����v����A�C�e��������
            var item = m_Gen4EncountTypeList.Items
                .Cast<KeyValuePair<int, string>>()
                .FirstOrDefault(item => item.Key == gen4EncountType);

            if (!item.Equals(default(KeyValuePair<int, string>)))
            {
                m_Gen4EncountTypeList.SelectedItem = item;
            }
            else
            {
                m_Gen4EncountTypeList.SelectedIndex = 0;
            }
        }

        int GetSelectedGen4EncountType()
        {
            var currentItem = m_Gen4EncountTypeList.SelectedItem as KeyValuePair<int, string>?;
            if (currentItem.HasValue)
            {
                return currentItem.Value.Key;
            }
            return 0;
        }

        void InitializeResultView()
        {
            m_Gen4ResultDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "InitialSeed", HeaderText = "����seed", DataPropertyName = "InitialSeed" });
            m_Gen4ResultDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "StartPosition", HeaderText = "���", DataPropertyName = "StartPosition" });
            m_Gen4ResultDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "SynchroNature", HeaderText = "�V���N��(��)", DataPropertyName = "SynchroNature" });
            m_Gen4ResultDataGridView.DataSource = m_Gen4ResultBindingSource;
        }

        void ReflectFromConfig(ConfigData config, Gen4Config gen4Config)
        {
            ReflectThreadCount(config.ThreadCount);
            m_Gen4IsHgssCheck.Checked = gen4Config.IsHgss;
            SelectGen4EncountType(gen4Config.EncountType);
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

        private void m_Gen4IsHgssCheck_CheckedChanged(object sender, EventArgs e)
        {
            var current = GetSelectedGen4EncountType();
            UpdateGen4EncountTypeList(m_Gen4IsHgssCheck.Checked);
            SelectGen4EncountType(current);
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
            gen4Config.EncountType = GetSelectedGen4EncountType();
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

        /// <summary>
        /// ��4����̌v�Z���ʂ��t�H�[���ɔ��f���܂��B
        /// </summary>
        public void SetGen4CalculationResult(double elapsedSeconds, int resultCount, object resultsViewModel)
        {
            m_Gen4ResultLabel.Text = $"�v�Z����: ���{resultCount}�� (��������: {elapsedSeconds:F2} �b)";
            m_Gen4ResultBindingSource.DataSource = resultsViewModel;
        }
    }
}
