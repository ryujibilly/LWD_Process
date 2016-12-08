namespace LWD_DataProcess
{
    partial class GammaCorrection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox_GammaSettings = new System.Windows.Forms.GroupBox();
            this.checkBox_Annulus = new System.Windows.Forms.CheckBox();
            this.checkBox_Collar = new System.Windows.Forms.CheckBox();
            this.textBox_WellName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button_Load = new System.Windows.Forms.Button();
            this.checkBox_WellDiameter = new System.Windows.Forms.CheckBox();
            this.checkBox_Save2Source = new System.Windows.Forms.CheckBox();
            this.numericUpDown_WellDiameter = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_MudDensity = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.comboBox_BariteContainment = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDown_CircleInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_PipeWallSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_DrillPipeSize = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox_AllDepth = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox_EndDep = new System.Windows.Forms.TextBox();
            this.textBox_StartDep = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CurveName_AC = new System.Windows.Forms.TextBox();
            this.button_Correct = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox_GammaSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_WellDiameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MudDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CircleInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PipeWallSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "选择伽马文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(99, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(292, 21);
            this.textBox1.TabIndex = 1;
            // 
            // groupBox_GammaSettings
            // 
            this.groupBox_GammaSettings.Controls.Add(this.checkBox_Annulus);
            this.groupBox_GammaSettings.Controls.Add(this.checkBox_Collar);
            this.groupBox_GammaSettings.Controls.Add(this.textBox_WellName);
            this.groupBox_GammaSettings.Controls.Add(this.label8);
            this.groupBox_GammaSettings.Controls.Add(this.button_Load);
            this.groupBox_GammaSettings.Controls.Add(this.checkBox_WellDiameter);
            this.groupBox_GammaSettings.Controls.Add(this.checkBox_Save2Source);
            this.groupBox_GammaSettings.Controls.Add(this.numericUpDown_WellDiameter);
            this.groupBox_GammaSettings.Controls.Add(this.numericUpDown_MudDensity);
            this.groupBox_GammaSettings.Controls.Add(this.textBox1);
            this.groupBox_GammaSettings.Controls.Add(this.button1);
            this.groupBox_GammaSettings.Controls.Add(this.label20);
            this.groupBox_GammaSettings.Controls.Add(this.comboBox_BariteContainment);
            this.groupBox_GammaSettings.Controls.Add(this.label19);
            this.groupBox_GammaSettings.Controls.Add(this.numericUpDown_CircleInterval);
            this.groupBox_GammaSettings.Controls.Add(this.numericUpDown_PipeWallSize);
            this.groupBox_GammaSettings.Controls.Add(this.label4);
            this.groupBox_GammaSettings.Controls.Add(this.comboBox_DrillPipeSize);
            this.groupBox_GammaSettings.Location = new System.Drawing.Point(12, 11);
            this.groupBox_GammaSettings.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_GammaSettings.Name = "groupBox_GammaSettings";
            this.groupBox_GammaSettings.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_GammaSettings.Size = new System.Drawing.Size(701, 132);
            this.groupBox_GammaSettings.TabIndex = 26;
            this.groupBox_GammaSettings.TabStop = false;
            this.groupBox_GammaSettings.Text = "环境参数设置";
            // 
            // checkBox_Annulus
            // 
            this.checkBox_Annulus.AutoSize = true;
            this.checkBox_Annulus.Location = new System.Drawing.Point(495, 62);
            this.checkBox_Annulus.Name = "checkBox_Annulus";
            this.checkBox_Annulus.Size = new System.Drawing.Size(96, 16);
            this.checkBox_Annulus.TabIndex = 32;
            this.checkBox_Annulus.Text = "环空间隔(mm)";
            this.checkBox_Annulus.UseVisualStyleBackColor = true;
            this.checkBox_Annulus.CheckedChanged += new System.EventHandler(this.checkBox_Annulus_CheckedChanged);
            // 
            // checkBox_Collar
            // 
            this.checkBox_Collar.AutoSize = true;
            this.checkBox_Collar.Location = new System.Drawing.Point(217, 62);
            this.checkBox_Collar.Name = "checkBox_Collar";
            this.checkBox_Collar.Size = new System.Drawing.Size(132, 16);
            this.checkBox_Collar.TabIndex = 31;
            this.checkBox_Collar.Text = "钻铤壁厚(内径)(mm)";
            this.checkBox_Collar.UseVisualStyleBackColor = true;
            this.checkBox_Collar.CheckedChanged += new System.EventHandler(this.checkBox_Collar_CheckedChanged);
            // 
            // textBox_WellName
            // 
            this.textBox_WellName.Location = new System.Drawing.Point(432, 19);
            this.textBox_WellName.Name = "textBox_WellName";
            this.textBox_WellName.Size = new System.Drawing.Size(100, 21);
            this.textBox_WellName.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(397, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 29;
            this.label8.Text = "井名";
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(538, 17);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(45, 23);
            this.button_Load.TabIndex = 28;
            this.button_Load.Text = "加载";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // checkBox_WellDiameter
            // 
            this.checkBox_WellDiameter.AutoSize = true;
            this.checkBox_WellDiameter.Location = new System.Drawing.Point(495, 95);
            this.checkBox_WellDiameter.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_WellDiameter.Name = "checkBox_WellDiameter";
            this.checkBox_WellDiameter.Size = new System.Drawing.Size(96, 16);
            this.checkBox_WellDiameter.TabIndex = 12;
            this.checkBox_WellDiameter.Text = "偏心井径(mm)";
            this.checkBox_WellDiameter.UseVisualStyleBackColor = true;
            this.checkBox_WellDiameter.CheckedChanged += new System.EventHandler(this.checkBox_WellDiameter_CheckedChanged);
            // 
            // checkBox_Save2Source
            // 
            this.checkBox_Save2Source.AutoSize = true;
            this.checkBox_Save2Source.Location = new System.Drawing.Point(589, 21);
            this.checkBox_Save2Source.Name = "checkBox_Save2Source";
            this.checkBox_Save2Source.Size = new System.Drawing.Size(96, 16);
            this.checkBox_Save2Source.TabIndex = 27;
            this.checkBox_Save2Source.Text = "保存到源目录";
            this.checkBox_Save2Source.UseVisualStyleBackColor = true;
            this.checkBox_Save2Source.CheckedChanged += new System.EventHandler(this.checkBox_Save2Source_CheckedChanged);
            // 
            // numericUpDown_WellDiameter
            // 
            this.numericUpDown_WellDiameter.DecimalPlaces = 1;
            this.numericUpDown_WellDiameter.Enabled = false;
            this.numericUpDown_WellDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_WellDiameter.Location = new System.Drawing.Point(600, 93);
            this.numericUpDown_WellDiameter.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_WellDiameter.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown_WellDiameter.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_WellDiameter.Name = "numericUpDown_WellDiameter";
            this.numericUpDown_WellDiameter.Size = new System.Drawing.Size(96, 21);
            this.numericUpDown_WellDiameter.TabIndex = 11;
            this.numericUpDown_WellDiameter.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numericUpDown_WellDiameter.ValueChanged += new System.EventHandler(this.numericUpDown_WellDiameter_ValueChanged);
            // 
            // numericUpDown_MudDensity
            // 
            this.numericUpDown_MudDensity.DecimalPlaces = 2;
            this.numericUpDown_MudDensity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_MudDensity.Location = new System.Drawing.Point(366, 94);
            this.numericUpDown_MudDensity.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_MudDensity.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            131072});
            this.numericUpDown_MudDensity.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.numericUpDown_MudDensity.Name = "numericUpDown_MudDensity";
            this.numericUpDown_MudDensity.Size = new System.Drawing.Size(96, 21);
            this.numericUpDown_MudDensity.TabIndex = 9;
            this.numericUpDown_MudDensity.Value = new decimal(new int[] {
            120,
            0,
            0,
            131072});
            this.numericUpDown_MudDensity.ValueChanged += new System.EventHandler(this.numericUpDown_MudDensity_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(248, 96);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(101, 12);
            this.label20.TabIndex = 8;
            this.label20.Text = "泥浆密度(g/cm^2)";
            // 
            // comboBox_BariteContainment
            // 
            this.comboBox_BariteContainment.FormattingEnabled = true;
            this.comboBox_BariteContainment.Items.AddRange(new object[] {
            "是",
            "不是"});
            this.comboBox_BariteContainment.Location = new System.Drawing.Point(108, 93);
            this.comboBox_BariteContainment.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_BariteContainment.Name = "comboBox_BariteContainment";
            this.comboBox_BariteContainment.Size = new System.Drawing.Size(65, 20);
            this.comboBox_BariteContainment.TabIndex = 7;
            this.comboBox_BariteContainment.Text = "是";
            this.comboBox_BariteContainment.SelectedIndexChanged += new System.EventHandler(this.comboBox_BariteContainment_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(16, 96);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 12);
            this.label19.TabIndex = 6;
            this.label19.Text = "是否含重晶石";
            // 
            // numericUpDown_CircleInterval
            // 
            this.numericUpDown_CircleInterval.DecimalPlaces = 2;
            this.numericUpDown_CircleInterval.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_CircleInterval.Location = new System.Drawing.Point(600, 58);
            this.numericUpDown_CircleInterval.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_CircleInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_CircleInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_CircleInterval.Name = "numericUpDown_CircleInterval";
            this.numericUpDown_CircleInterval.Size = new System.Drawing.Size(96, 21);
            this.numericUpDown_CircleInterval.TabIndex = 5;
            this.numericUpDown_CircleInterval.Value = new decimal(new int[] {
            800,
            0,
            0,
            131072});
            this.numericUpDown_CircleInterval.ValueChanged += new System.EventHandler(this.numericUpDown_CircleInterval_ValueChanged);
            // 
            // numericUpDown_PipeWallSize
            // 
            this.numericUpDown_PipeWallSize.DecimalPlaces = 2;
            this.numericUpDown_PipeWallSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_PipeWallSize.Location = new System.Drawing.Point(366, 59);
            this.numericUpDown_PipeWallSize.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_PipeWallSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_PipeWallSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_PipeWallSize.Name = "numericUpDown_PipeWallSize";
            this.numericUpDown_PipeWallSize.Size = new System.Drawing.Size(96, 21);
            this.numericUpDown_PipeWallSize.TabIndex = 3;
            this.numericUpDown_PipeWallSize.ThousandsSeparator = true;
            this.numericUpDown_PipeWallSize.Value = new decimal(new int[] {
            1000,
            0,
            0,
            131072});
            this.numericUpDown_PipeWallSize.ValueChanged += new System.EventHandler(this.numericUpDown_PipeWallSize_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 61);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "钻铤尺寸(inch)";
            // 
            // comboBox_DrillPipeSize
            // 
            this.comboBox_DrillPipeSize.FormattingEnabled = true;
            this.comboBox_DrillPipeSize.Items.AddRange(new object[] {
            "4.75",
            "6.75"});
            this.comboBox_DrillPipeSize.Location = new System.Drawing.Point(108, 58);
            this.comboBox_DrillPipeSize.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_DrillPipeSize.Name = "comboBox_DrillPipeSize";
            this.comboBox_DrillPipeSize.Size = new System.Drawing.Size(65, 20);
            this.comboBox_DrillPipeSize.Sorted = true;
            this.comboBox_DrillPipeSize.TabIndex = 0;
            this.comboBox_DrillPipeSize.Text = "4.75";
            this.comboBox_DrillPipeSize.SelectedIndexChanged += new System.EventHandler(this.comboBox_DrillPipeSize_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(2, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(681, 412);
            this.dataGridView1.TabIndex = 28;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(701, 464);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "环境校正数据";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(6, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(689, 438);
            this.tabControl1.TabIndex = 29;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(681, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "待校数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(681, 412);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "校正数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 1);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(681, 412);
            this.dataGridView2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chart1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(681, 412);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "曲线对比";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(6, 6);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(669, 400);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "曲线对比图";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.button_Correct);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(701, 94);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "曲线设置";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(612, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 31);
            this.button2.TabIndex = 35;
            this.button2.Text = "曲线对比";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.checkBox_AllDepth);
            this.groupBox6.Controls.Add(this.comboBox2);
            this.groupBox6.Controls.Add(this.textBox_EndDep);
            this.groupBox6.Controls.Add(this.textBox_StartDep);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Location = new System.Drawing.Point(339, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(267, 61);
            this.groupBox6.TabIndex = 34;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "处理深度范围";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(155, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 36;
            this.label7.Text = "深度";
            // 
            // checkBox_AllDepth
            // 
            this.checkBox_AllDepth.AutoSize = true;
            this.checkBox_AllDepth.Location = new System.Drawing.Point(156, 37);
            this.checkBox_AllDepth.Name = "checkBox_AllDepth";
            this.checkBox_AllDepth.Size = new System.Drawing.Size(60, 16);
            this.checkBox_AllDepth.TabIndex = 4;
            this.checkBox_AllDepth.Text = "全井段";
            this.checkBox_AllDepth.UseVisualStyleBackColor = true;
            this.checkBox_AllDepth.CheckedChanged += new System.EventHandler(this.checkBox_AllDepth_CheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(190, 13);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(70, 20);
            this.comboBox2.TabIndex = 35;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // textBox_EndDep
            // 
            this.textBox_EndDep.Location = new System.Drawing.Point(83, 33);
            this.textBox_EndDep.Name = "textBox_EndDep";
            this.textBox_EndDep.Size = new System.Drawing.Size(67, 21);
            this.textBox_EndDep.TabIndex = 3;
            this.textBox_EndDep.Text = "500";
            this.textBox_EndDep.TextChanged += new System.EventHandler(this.textBox_EndDep_TextChanged);
            // 
            // textBox_StartDep
            // 
            this.textBox_StartDep.Location = new System.Drawing.Point(8, 33);
            this.textBox_StartDep.Name = "textBox_StartDep";
            this.textBox_StartDep.Size = new System.Drawing.Size(69, 21);
            this.textBox_StartDep.TabIndex = 2;
            this.textBox_StartDep.Text = "200";
            this.textBox_StartDep.TextChanged += new System.EventHandler(this.textBox_StartDep_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(77, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "终止深度(M)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "起始深度(M)";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.CurveName_AC);
            this.groupBox5.Location = new System.Drawing.Point(226, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(107, 61);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "校正后输出曲线";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "居中伽马：";
            // 
            // CurveName_AC
            // 
            this.CurveName_AC.Location = new System.Drawing.Point(6, 34);
            this.CurveName_AC.Name = "CurveName_AC";
            this.CurveName_AC.Size = new System.Drawing.Size(95, 21);
            this.CurveName_AC.TabIndex = 0;
            this.CurveName_AC.Text = "GRAC";
            this.CurveName_AC.TextChanged += new System.EventHandler(this.CurveName_AC_TextChanged);
            // 
            // button_Correct
            // 
            this.button_Correct.Location = new System.Drawing.Point(612, 22);
            this.button_Correct.Name = "button_Correct";
            this.button_Correct.Size = new System.Drawing.Size(83, 31);
            this.button_Correct.TabIndex = 30;
            this.button_Correct.Text = "校  正";
            this.button_Correct.UseVisualStyleBackColor = true;
            this.button_Correct.Click += new System.EventHandler(this.button_Correct_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Location = new System.Drawing.Point(137, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(83, 61);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "单位";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(21, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(41, 16);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "API";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(21, 39);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(41, 16);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.Text = "CPS";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Location = new System.Drawing.Point(12, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 61);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "待校曲线";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "居中伽马：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(93, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // GammaCorrection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 724);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_GammaSettings);
            this.DoubleBuffered = true;
            this.Name = "GammaCorrection";
            this.Text = "居中伽马环境校正";
            this.Load += new System.EventHandler(this.GammaCorrection_Load);
            this.groupBox_GammaSettings.ResumeLayout(false);
            this.groupBox_GammaSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_WellDiameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MudDensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CircleInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PipeWallSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox_GammaSettings;
        private System.Windows.Forms.CheckBox checkBox_WellDiameter;
        private System.Windows.Forms.NumericUpDown numericUpDown_WellDiameter;
        private System.Windows.Forms.NumericUpDown numericUpDown_MudDensity;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox comboBox_BariteContainment;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numericUpDown_CircleInterval;
        private System.Windows.Forms.NumericUpDown numericUpDown_PipeWallSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_DrillPipeSize;
        private System.Windows.Forms.CheckBox checkBox_Save2Source;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button_Correct;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CurveName_AC;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox_EndDep;
        private System.Windows.Forms.TextBox textBox_StartDep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.CheckBox checkBox_AllDepth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_WellName;
        private System.Windows.Forms.CheckBox checkBox_Collar;
        private System.Windows.Forms.CheckBox checkBox_Annulus;
    }
}