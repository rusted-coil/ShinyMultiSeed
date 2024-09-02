using FormRx.Button;
using PKHeXUtilForms.Control;
using ShinyMultiSeed.Main.View;
using ShinyMultiSeed.Result;
using System.Reactive.Subjects;

namespace ShinyMultiSeed
{
    internal partial class MainForm : Form, IMainFormGen4View, IMainFormConfigView, IMainFormResultView
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeConfigView();
            InitializeGen4View();
            InitializeResultView();
        }


        //----------------------------------------------------------------------------------------------------------
        // ConfigView
        //----------------------------------------------------------------------------------------------------------

        ToolStripMenuItem[] m_ThreadCountMenuItems;

        Subject<int> m_ThreadCountButtonClicked = new Subject<int>();
        public IObservable<int> ThreadCountButtonClicked => m_ThreadCountButtonClicked;

        private void ThreadCountConfig1Clicked(object sender, EventArgs e) => m_ThreadCountButtonClicked.OnNext(0);
        private void ThreadCountConfig2Clicked(object sender, EventArgs e) => m_ThreadCountButtonClicked.OnNext(1);
        private void ThreadCountConfig4Clicked(object sender, EventArgs e) => m_ThreadCountButtonClicked.OnNext(2);
        private void ThreadCountConfig8Clicked(object sender, EventArgs e) => m_ThreadCountButtonClicked.OnNext(3);
        private void ThreadCountConfig16Clicked(object sender, EventArgs e) => m_ThreadCountButtonClicked.OnNext(4);
        private void ThreadCountConfig32Clicked(object sender, EventArgs e) => m_ThreadCountButtonClicked.OnNext(5);

        private void InitializeConfigView()
        {
            m_ThreadCountMenuItems = [
                m_ThreadCountConfig1,
                m_ThreadCountConfig2,
                m_ThreadCountConfig4,
                m_ThreadCountConfig8,
                m_ThreadCountConfig16,
                m_ThreadCountConfig32,
            ];
        }

        public void SetThreadCountIndex(int index)
        {
            for (int i = 0; i < m_ThreadCountMenuItems.Length; ++i)
            {
                m_ThreadCountMenuItems[i].Checked = (i == index);
            }
        }

        //----------------------------------------------------------------------------------------------------------
        // Gen4View
        //----------------------------------------------------------------------------------------------------------

        bool IMainFormGen4View.IsHgssChecked { get => m_Gen4IsHgssCheck.Checked; set => m_Gen4IsHgssCheck.Checked = value; }
        int IMainFormGen4View.EncountType {
            get {
                var currentItem = m_Gen4EncountTypeList.SelectedItem as KeyValuePair<int, string>?;
                if (currentItem.HasValue)
                {
                    return currentItem.Value.Key;
                }
                return 0;
            }
            set {
                // m_Gen4EncountTypesから、指定されたKeyが一致するアイテムを検索
                var item = m_Gen4EncountTypeList.Items
                    .Cast<KeyValuePair<int, string>>()
                    .FirstOrDefault(item => item.Key == value);

                if (!item.Equals(default(KeyValuePair<int, string>)))
                {
                    m_Gen4EncountTypeList.SelectedItem = item;
                }
                else
                {
                    m_Gen4EncountTypeList.SelectedIndex = 0;
                }
            }
        }
        bool IMainFormGen4View.IsShinyChecked { get => m_Gen4IsShinyCheck.Checked; set => m_Gen4IsShinyCheck.Checked = value; }
        string IMainFormGen4View.TidText { get => m_Gen4TidBox.Text; set => m_Gen4TidBox.Text = value; }
        string IMainFormGen4View.SidText { get => m_Gen4SidBox.Text; set => m_Gen4SidBox.Text = value; }
        bool IMainFormGen4View.FiltersAtkIVChecked { get => m_Gen4FiltersAtkCheck.Checked; set => m_Gen4FiltersAtkCheck.Checked = value; }
        decimal IMainFormGen4View.AtkIVMinValue { get => m_Gen4FiltersAtkIVMinBox.Value; set => m_Gen4FiltersAtkIVMinBox.Value = value; }
        decimal IMainFormGen4View.AtkIVMaxValue { get => m_Gen4FiltersAtkIVMaxBox.Value; set => m_Gen4FiltersAtkIVMaxBox.Value = value; }
        bool IMainFormGen4View.FiltersSpdIVChecked { get => m_Gen4FiltersSpdCheck.Checked; set => m_Gen4FiltersSpdCheck.Checked = value; }
        decimal IMainFormGen4View.SpdIVMinValue { get => m_Gen4FiltersSpdIVMinBox.Value; set => m_Gen4FiltersSpdIVMinBox.Value = value; }
        decimal IMainFormGen4View.SpdIVMaxValue { get => m_Gen4FiltersSpdIVMaxBox.Value; set => m_Gen4FiltersSpdIVMaxBox.Value = value; }
        bool IMainFormGen4View.UsesSynchroChecked { get => m_Gen4UsesSynchroCheck.Checked; set => m_Gen4UsesSynchroCheck.Checked = value; }
        string IMainFormGen4View.FrameMinText { get => m_Gen4FrameMin.Text; set => m_Gen4FrameMin.Text = value; }
        string IMainFormGen4View.FrameMaxText { get => m_Gen4FrameMax.Text; set => m_Gen4FrameMax.Text = value; }
        string IMainFormGen4View.PositionMinText { get => m_Gen4PositionMin.Text; set => m_Gen4PositionMin.Text = value; }
        string IMainFormGen4View.PositionMaxText { get => m_Gen4PositionMax.Text; set => m_Gen4PositionMax.Text = value; }

        Subject<bool> m_isHgssCheckedChanged = new Subject<bool>();
        public IObservable<bool> IsHgssCheckedChanged => m_isHgssCheckedChanged;
        IButton m_Gen4CalculateButton;
        IButton IMainFormGen4View.CalculateButton => m_Gen4CalculateButton;

        IReadOnlyList<KeyValuePair<int, string>> m_Gen4EncountTypes;

        private void InitializeGen4View()
        {
            m_Gen4EncountTypeList.DisplayMember = "Value";
            m_Gen4EncountTypeList.ValueMember = "Key";
            m_Gen4CalculateButton = ButtonFactory.CreateButton(m_CalculateButton);
        }

        private void m_Gen4IsHgssCheck_CheckedChanged(object sender, EventArgs e) => m_isHgssCheckedChanged.OnNext(m_Gen4IsHgssCheck.Checked);

        /// <summary>
        /// 選択可能なエンカウント種別の文字列を設定します。
        /// </summary>
        void IMainFormGen4View.SetSelectableEncountTypes(IReadOnlyList<KeyValuePair<int, string>> encountTypes)
        {
            m_Gen4EncountTypes = encountTypes;
            m_Gen4EncountTypeList.Items.Clear();
            foreach (var pair in encountTypes)
            {
                m_Gen4EncountTypeList.Items.Add(pair);
            }
        }

        /// <summary>
        /// 現在計算中かどうかをフォームに反映します。
        /// </summary>
        public void SetIsCalculating(bool isCalculating)
        {
            if (isCalculating)
            {
                m_CalculateButton.Enabled = false;
                m_CalculateButton.Text = "計算中...";
                m_CalculateButton.BackColor = Color.LightGray;
            }
            else
            {
                m_CalculateButton.Enabled = true;
                m_CalculateButton.Text = "計算";
                m_CalculateButton.BackColor = Color.Yellow;
            }
        }

        //----------------------------------------------------------------------------------------------------------
        // Result
        //----------------------------------------------------------------------------------------------------------

        public string OverViewText { set => m_Gen4ResultLabel.Text = value; }

        void InitializeResultView()
        {
            m_ResultDataGridView.DataSource = m_ResultBindingSource;
        }

        public void SetResultColumns(IReadOnlyList<IResultColumnViewModel> columnViewModels)
        {
            m_ResultDataGridView.Columns.Clear();
            foreach (var viewModel in columnViewModels)
            {
                m_ResultDataGridView.Columns.Add(new DataGridViewTextBoxColumnEx { Name = viewModel.Id, HeaderText = viewModel.DisplayText, DataPropertyName = viewModel.Id });
            }
        }

        public void SetResultRows(IReadOnlyList<object> rowViewModels)
        {
            m_ResultBindingSource.DataSource = rowViewModels;
        }
    }
}
