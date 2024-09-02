namespace ShinyMultiSeed
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            m_Gen4IsShinyCheck = new CheckBox();
            label3 = new Label();
            m_Gen4TidBox = new TextBox();
            m_Gen4SidBox = new TextBox();
            label4 = new Label();
            m_Gen4FiltersAtkCheck = new CheckBox();
            label5 = new Label();
            m_Gen4FiltersAtkIVMinBox = new NumericUpDown();
            m_Gen4FiltersAtkIVMaxBox = new NumericUpDown();
            m_Gen4FiltersSpdIVMaxBox = new NumericUpDown();
            m_Gen4FiltersSpdIVMinBox = new NumericUpDown();
            label6 = new Label();
            m_Gen4FiltersSpdCheck = new CheckBox();
            m_Gen4FrameMin = new TextBox();
            label7 = new Label();
            m_Gen4FrameMax = new TextBox();
            m_Gen4MultiSeedCount = new NumericUpDown();
            m_CalculateButton = new Button();
            m_Gen4EncountTypeList = new ComboBox();
            label8 = new Label();
            m_Gen4PositionMax = new TextBox();
            label9 = new Label();
            m_Gen4PositionMin = new TextBox();
            label10 = new Label();
            groupBox1 = new GroupBox();
            m_Gen4UsesSynchroCheck = new CheckBox();
            m_Gen4IsHgssCheck = new CheckBox();
            groupBox2 = new GroupBox();
            m_Gen4ResultLabel = new Label();
            m_Gen4ResultDataGridView = new DataGridView();
            m_Gen4ResultBindingSource = new BindingSource(components);
            menuStrip1 = new MenuStrip();
            高速化ToolStripMenuItem = new ToolStripMenuItem();
            スレッド数ToolStripMenuItem = new ToolStripMenuItem();
            m_ThreadCountConfig1 = new ToolStripMenuItem();
            m_ThreadCountConfig2 = new ToolStripMenuItem();
            m_ThreadCountConfig4 = new ToolStripMenuItem();
            m_ThreadCountConfig8 = new ToolStripMenuItem();
            m_ThreadCountConfig16 = new ToolStripMenuItem();
            m_ThreadCountConfig32 = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)m_Gen4FiltersAtkIVMinBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4FiltersAtkIVMaxBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4FiltersSpdIVMaxBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4FiltersSpdIVMinBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4MultiSeedCount).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_Gen4ResultDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4ResultBindingSource).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 399);
            label1.Name = "label1";
            label1.Size = new Size(121, 15);
            label1.TabIndex = 0;
            label1.Text = "フレームずれ多面待ち数:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 34);
            label2.Name = "label2";
            label2.Size = new Size(125, 15);
            label2.TabIndex = 1;
            label2.Text = "待機フレーム+(年-2000):";
            // 
            // m_Gen4IsShinyCheck
            // 
            m_Gen4IsShinyCheck.AutoSize = true;
            m_Gen4IsShinyCheck.Location = new Point(19, 32);
            m_Gen4IsShinyCheck.Name = "m_Gen4IsShinyCheck";
            m_Gen4IsShinyCheck.Size = new Size(60, 19);
            m_Gen4IsShinyCheck.TabIndex = 0;
            m_Gen4IsShinyCheck.Text = "色違い";
            m_Gen4IsShinyCheck.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(85, 33);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 3;
            label3.Text = "表ID:";
            // 
            // m_Gen4TidBox
            // 
            m_Gen4TidBox.Location = new Point(124, 30);
            m_Gen4TidBox.Name = "m_Gen4TidBox";
            m_Gen4TidBox.Size = new Size(51, 23);
            m_Gen4TidBox.TabIndex = 1;
            // 
            // m_Gen4SidBox
            // 
            m_Gen4SidBox.Location = new Point(231, 30);
            m_Gen4SidBox.Name = "m_Gen4SidBox";
            m_Gen4SidBox.Size = new Size(51, 23);
            m_Gen4SidBox.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(192, 33);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 5;
            label4.Text = "裏ID:";
            // 
            // m_Gen4FiltersAtkCheck
            // 
            m_Gen4FiltersAtkCheck.AutoSize = true;
            m_Gen4FiltersAtkCheck.Location = new Point(19, 68);
            m_Gen4FiltersAtkCheck.Name = "m_Gen4FiltersAtkCheck";
            m_Gen4FiltersAtkCheck.Size = new Size(70, 19);
            m_Gen4FiltersAtkCheck.TabIndex = 3;
            m_Gen4FiltersAtkCheck.Text = "A個体値";
            m_Gen4FiltersAtkCheck.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(139, 72);
            label5.Name = "label5";
            label5.Size = new Size(19, 15);
            label5.TabIndex = 9;
            label5.Text = "～";
            // 
            // m_Gen4FiltersAtkIVMinBox
            // 
            m_Gen4FiltersAtkIVMinBox.Location = new Point(95, 67);
            m_Gen4FiltersAtkIVMinBox.Name = "m_Gen4FiltersAtkIVMinBox";
            m_Gen4FiltersAtkIVMinBox.Size = new Size(38, 23);
            m_Gen4FiltersAtkIVMinBox.TabIndex = 4;
            // 
            // m_Gen4FiltersAtkIVMaxBox
            // 
            m_Gen4FiltersAtkIVMaxBox.Location = new Point(164, 67);
            m_Gen4FiltersAtkIVMaxBox.Name = "m_Gen4FiltersAtkIVMaxBox";
            m_Gen4FiltersAtkIVMaxBox.Size = new Size(38, 23);
            m_Gen4FiltersAtkIVMaxBox.TabIndex = 5;
            // 
            // m_Gen4FiltersSpdIVMaxBox
            // 
            m_Gen4FiltersSpdIVMaxBox.Location = new Point(164, 106);
            m_Gen4FiltersSpdIVMaxBox.Name = "m_Gen4FiltersSpdIVMaxBox";
            m_Gen4FiltersSpdIVMaxBox.Size = new Size(38, 23);
            m_Gen4FiltersSpdIVMaxBox.TabIndex = 8;
            // 
            // m_Gen4FiltersSpdIVMinBox
            // 
            m_Gen4FiltersSpdIVMinBox.Location = new Point(95, 106);
            m_Gen4FiltersSpdIVMinBox.Name = "m_Gen4FiltersSpdIVMinBox";
            m_Gen4FiltersSpdIVMinBox.Size = new Size(38, 23);
            m_Gen4FiltersSpdIVMinBox.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(139, 111);
            label6.Name = "label6";
            label6.Size = new Size(19, 15);
            label6.TabIndex = 13;
            label6.Text = "～";
            // 
            // m_Gen4FiltersSpdCheck
            // 
            m_Gen4FiltersSpdCheck.AutoSize = true;
            m_Gen4FiltersSpdCheck.Location = new Point(19, 107);
            m_Gen4FiltersSpdCheck.Name = "m_Gen4FiltersSpdCheck";
            m_Gen4FiltersSpdCheck.Size = new Size(68, 19);
            m_Gen4FiltersSpdCheck.TabIndex = 6;
            m_Gen4FiltersSpdCheck.Text = "S個体値";
            m_Gen4FiltersSpdCheck.UseVisualStyleBackColor = true;
            // 
            // m_Gen4FrameMin
            // 
            m_Gen4FrameMin.Location = new Point(149, 31);
            m_Gen4FrameMin.Name = "m_Gen4FrameMin";
            m_Gen4FrameMin.Size = new Size(51, 23);
            m_Gen4FrameMin.TabIndex = 0;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(206, 34);
            label7.Name = "label7";
            label7.Size = new Size(19, 15);
            label7.TabIndex = 17;
            label7.Text = "～";
            // 
            // m_Gen4FrameMax
            // 
            m_Gen4FrameMax.Location = new Point(231, 31);
            m_Gen4FrameMax.Name = "m_Gen4FrameMax";
            m_Gen4FrameMax.Size = new Size(51, 23);
            m_Gen4FrameMax.TabIndex = 1;
            // 
            // m_Gen4MultiSeedCount
            // 
            m_Gen4MultiSeedCount.Location = new Point(140, 397);
            m_Gen4MultiSeedCount.Name = "m_Gen4MultiSeedCount";
            m_Gen4MultiSeedCount.Size = new Size(38, 23);
            m_Gen4MultiSeedCount.TabIndex = 4;
            m_Gen4MultiSeedCount.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // m_CalculateButton
            // 
            m_CalculateButton.BackColor = Color.Yellow;
            m_CalculateButton.Location = new Point(204, 393);
            m_CalculateButton.Name = "m_CalculateButton";
            m_CalculateButton.Size = new Size(113, 31);
            m_CalculateButton.TabIndex = 5;
            m_CalculateButton.Text = "計算";
            m_CalculateButton.UseVisualStyleBackColor = false;
            // 
            // m_Gen4EncountTypeList
            // 
            m_Gen4EncountTypeList.DropDownStyle = ComboBoxStyle.DropDownList;
            m_Gen4EncountTypeList.FormattingEnabled = true;
            m_Gen4EncountTypeList.Items.AddRange(new object[] { "固定シンボル(シンクロ可)", "徘徊", "野生", "アンノーン(ラジオ有り)" });
            m_Gen4EncountTypeList.Location = new Point(135, 38);
            m_Gen4EncountTypeList.Name = "m_Gen4EncountTypeList";
            m_Gen4EncountTypeList.Size = new Size(166, 23);
            m_Gen4EncountTypeList.TabIndex = 0;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(95, 41);
            label8.Name = "label8";
            label8.Size = new Size(34, 15);
            label8.TabIndex = 22;
            label8.Text = "種類:";
            // 
            // m_Gen4PositionMax
            // 
            m_Gen4PositionMax.Location = new Point(231, 68);
            m_Gen4PositionMax.Name = "m_Gen4PositionMax";
            m_Gen4PositionMax.Size = new Size(51, 23);
            m_Gen4PositionMax.TabIndex = 3;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(206, 71);
            label9.Name = "label9";
            label9.Size = new Size(19, 15);
            label9.TabIndex = 25;
            label9.Text = "～";
            // 
            // m_Gen4PositionMin
            // 
            m_Gen4PositionMin.Location = new Point(149, 68);
            m_Gen4PositionMin.Name = "m_Gen4PositionMin";
            m_Gen4PositionMin.Size = new Size(51, 23);
            m_Gen4PositionMin.TabIndex = 2;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(11, 71);
            label10.Name = "label10";
            label10.Size = new Size(106, 15);
            label10.TabIndex = 23;
            label10.Text = "性格値決定消費数:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(m_Gen4UsesSynchroCheck);
            groupBox1.Controls.Add(m_Gen4IsShinyCheck);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(m_Gen4TidBox);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(m_Gen4SidBox);
            groupBox1.Controls.Add(m_Gen4FiltersAtkCheck);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(m_Gen4FiltersAtkIVMinBox);
            groupBox1.Controls.Add(m_Gen4FiltersAtkIVMaxBox);
            groupBox1.Controls.Add(m_Gen4FiltersSpdCheck);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(m_Gen4FiltersSpdIVMinBox);
            groupBox1.Controls.Add(m_Gen4FiltersSpdIVMaxBox);
            groupBox1.Location = new Point(12, 79);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(305, 180);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "絞り込み";
            // 
            // m_Gen4UsesSynchroCheck
            // 
            m_Gen4UsesSynchroCheck.AutoSize = true;
            m_Gen4UsesSynchroCheck.Location = new Point(19, 144);
            m_Gen4UsesSynchroCheck.Name = "m_Gen4UsesSynchroCheck";
            m_Gen4UsesSynchroCheck.Size = new Size(115, 19);
            m_Gen4UsesSynchroCheck.TabIndex = 9;
            m_Gen4UsesSynchroCheck.Text = "シンクロを使用する";
            m_Gen4UsesSynchroCheck.UseVisualStyleBackColor = true;
            // 
            // m_Gen4IsHgssCheck
            // 
            m_Gen4IsHgssCheck.AutoSize = true;
            m_Gen4IsHgssCheck.Location = new Point(24, 40);
            m_Gen4IsHgssCheck.Name = "m_Gen4IsHgssCheck";
            m_Gen4IsHgssCheck.Size = new Size(55, 19);
            m_Gen4IsHgssCheck.TabIndex = 1;
            m_Gen4IsHgssCheck.Text = "HGSS";
            m_Gen4IsHgssCheck.UseVisualStyleBackColor = true;
            m_Gen4IsHgssCheck.CheckedChanged += m_Gen4IsHgssCheck_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(m_Gen4FrameMin);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(m_Gen4PositionMax);
            groupBox2.Controls.Add(m_Gen4FrameMax);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(m_Gen4PositionMin);
            groupBox2.Controls.Add(label10);
            groupBox2.Location = new Point(12, 265);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(305, 109);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "初期seed検索条件";
            // 
            // m_Gen4ResultLabel
            // 
            m_Gen4ResultLabel.AutoSize = true;
            m_Gen4ResultLabel.Location = new Point(368, 38);
            m_Gen4ResultLabel.Name = "m_Gen4ResultLabel";
            m_Gen4ResultLabel.Size = new Size(31, 15);
            m_Gen4ResultLabel.TabIndex = 23;
            m_Gen4ResultLabel.Text = "結果";
            // 
            // m_Gen4ResultDataGridView
            // 
            m_Gen4ResultDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            m_Gen4ResultDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_Gen4ResultDataGridView.Location = new Point(368, 56);
            m_Gen4ResultDataGridView.Name = "m_Gen4ResultDataGridView";
            m_Gen4ResultDataGridView.Size = new Size(451, 403);
            m_Gen4ResultDataGridView.TabIndex = 24;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ControlLightLight;
            menuStrip1.Items.AddRange(new ToolStripItem[] { 高速化ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(831, 24);
            menuStrip1.TabIndex = 25;
            menuStrip1.Text = "menuStrip1";
            // 
            // 高速化ToolStripMenuItem
            // 
            高速化ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { スレッド数ToolStripMenuItem });
            高速化ToolStripMenuItem.Name = "高速化ToolStripMenuItem";
            高速化ToolStripMenuItem.Size = new Size(43, 20);
            高速化ToolStripMenuItem.Text = "設定";
            // 
            // スレッド数ToolStripMenuItem
            // 
            スレッド数ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { m_ThreadCountConfig1, m_ThreadCountConfig2, m_ThreadCountConfig4, m_ThreadCountConfig8, m_ThreadCountConfig16, m_ThreadCountConfig32 });
            スレッド数ToolStripMenuItem.Name = "スレッド数ToolStripMenuItem";
            スレッド数ToolStripMenuItem.Size = new Size(180, 22);
            スレッド数ToolStripMenuItem.Text = "並列計算スレッド数";
            // 
            // m_ThreadCountConfig1
            // 
            m_ThreadCountConfig1.Name = "m_ThreadCountConfig1";
            m_ThreadCountConfig1.Size = new Size(180, 22);
            m_ThreadCountConfig1.Text = "1";
            m_ThreadCountConfig1.Click += ThreadCountConfig1Clicked;
            // 
            // m_ThreadCountConfig2
            // 
            m_ThreadCountConfig2.Name = "m_ThreadCountConfig2";
            m_ThreadCountConfig2.Size = new Size(180, 22);
            m_ThreadCountConfig2.Text = "2";
            m_ThreadCountConfig2.Click += ThreadCountConfig2Clicked;
            // 
            // m_ThreadCountConfig4
            // 
            m_ThreadCountConfig4.Name = "m_ThreadCountConfig4";
            m_ThreadCountConfig4.Size = new Size(180, 22);
            m_ThreadCountConfig4.Text = "4";
            m_ThreadCountConfig4.Click += ThreadCountConfig4Clicked;
            // 
            // m_ThreadCountConfig8
            // 
            m_ThreadCountConfig8.Name = "m_ThreadCountConfig8";
            m_ThreadCountConfig8.Size = new Size(180, 22);
            m_ThreadCountConfig8.Text = "8";
            m_ThreadCountConfig8.Click += ThreadCountConfig8Clicked;
            // 
            // m_ThreadCountConfig16
            // 
            m_ThreadCountConfig16.Name = "m_ThreadCountConfig16";
            m_ThreadCountConfig16.Size = new Size(180, 22);
            m_ThreadCountConfig16.Text = "16";
            m_ThreadCountConfig16.Click += ThreadCountConfig16Clicked;
            // 
            // m_ThreadCountConfig32
            // 
            m_ThreadCountConfig32.Name = "m_ThreadCountConfig32";
            m_ThreadCountConfig32.Size = new Size(180, 22);
            m_ThreadCountConfig32.Text = "32";
            m_ThreadCountConfig32.Click += ThreadCountConfig32Clicked;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(831, 471);
            Controls.Add(m_Gen4ResultDataGridView);
            Controls.Add(m_Gen4ResultLabel);
            Controls.Add(groupBox2);
            Controls.Add(m_Gen4IsHgssCheck);
            Controls.Add(groupBox1);
            Controls.Add(label8);
            Controls.Add(m_Gen4EncountTypeList);
            Controls.Add(m_CalculateButton);
            Controls.Add(m_Gen4MultiSeedCount);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "ShinyMultiSeed";
            ((System.ComponentModel.ISupportInitialize)m_Gen4FiltersAtkIVMinBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4FiltersAtkIVMaxBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4FiltersSpdIVMaxBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4FiltersSpdIVMinBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4MultiSeedCount).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_Gen4ResultDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)m_Gen4ResultBindingSource).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private CheckBox m_Gen4IsShinyCheck;
        private Label label3;
        private TextBox m_Gen4TidBox;
        private TextBox m_Gen4SidBox;
        private Label label4;
        private CheckBox m_Gen4FiltersAtkCheck;
        private Label label5;
        private NumericUpDown m_Gen4FiltersAtkIVMinBox;
        private NumericUpDown m_Gen4FiltersAtkIVMaxBox;
        private NumericUpDown m_Gen4FiltersSpdIVMaxBox;
        private NumericUpDown m_Gen4FiltersSpdIVMinBox;
        private Label label6;
        private CheckBox m_Gen4FiltersSpdCheck;
        private TextBox m_Gen4FrameMin;
        private Label label7;
        private TextBox m_Gen4FrameMax;
        private NumericUpDown m_Gen4MultiSeedCount;
        private Button m_CalculateButton;
        private ComboBox m_Gen4EncountTypeList;
        private Label label8;
        private TextBox m_Gen4PositionMax;
        private Label label9;
        private TextBox m_Gen4PositionMin;
        private Label label10;
        private GroupBox groupBox1;
        private CheckBox m_Gen4UsesSynchroCheck;
        private CheckBox m_Gen4IsHgssCheck;
        private GroupBox groupBox2;
        private Label m_Gen4ResultLabel;
        private DataGridView m_Gen4ResultDataGridView;
        private BindingSource m_Gen4ResultBindingSource;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 高速化ToolStripMenuItem;
        private ToolStripMenuItem スレッド数ToolStripMenuItem;
        private ToolStripMenuItem m_ThreadCountConfig1;
        private ToolStripMenuItem m_ThreadCountConfig2;
        private ToolStripMenuItem m_ThreadCountConfig4;
        private ToolStripMenuItem m_ThreadCountConfig8;
        private ToolStripMenuItem m_ThreadCountConfig16;
        private ToolStripMenuItem m_ThreadCountConfig32;
    }
}
