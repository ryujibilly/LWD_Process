namespace LWD_DataProcess
{
    partial class TDP
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TDP));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Button_pathBrowser = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_InsType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.comboBox_Inter = new System.Windows.Forms.ComboBox();
            this.comboBox_Filter = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_LogMethod = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button_Inv = new System.Windows.Forms.Button();
            this.timerPrint = new System.Windows.Forms.Timer(this.components);
            this.checkBox_Save2Source = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件地址：";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(86, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(343, 23);
            this.textBox1.TabIndex = 1;
            this.textBox1.MouseHover += new System.EventHandler(this.textBox1_MouseHover);
            // 
            // Button_pathBrowser
            // 
            this.Button_pathBrowser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_pathBrowser.Location = new System.Drawing.Point(540, 3);
            this.Button_pathBrowser.Name = "Button_pathBrowser";
            this.Button_pathBrowser.Size = new System.Drawing.Size(83, 26);
            this.Button_pathBrowser.TabIndex = 2;
            this.Button_pathBrowser.Text = "打开文件夹";
            this.Button_pathBrowser.UseVisualStyleBackColor = true;
            this.Button_pathBrowser.Click += new System.EventHandler(this.Button_pathBrowser_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(342, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "仪器类型";
            // 
            // comboBox_InsType
            // 
            this.comboBox_InsType.DisplayMember = "1";
            this.comboBox_InsType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_InsType.FormattingEnabled = true;
            this.comboBox_InsType.Items.AddRange(new object[] {
            "居中伽马",
            "感应电阻率",
            "电磁波电阻率"});
            this.comboBox_InsType.Location = new System.Drawing.Point(404, 25);
            this.comboBox_InsType.Name = "comboBox_InsType";
            this.comboBox_InsType.Size = new System.Drawing.Size(156, 25);
            this.comboBox_InsType.TabIndex = 4;
            this.comboBox_InsType.Text = "居中伽马";
            this.comboBox_InsType.SelectedIndexChanged += new System.EventHandler(this.comboBox_InsType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(41, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "深度间隔";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown1.Increment = new decimal(new int[] {
            25,
            0,
            0,
            196608});
            this.numericUpDown1.Location = new System.Drawing.Point(103, 68);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(156, 23);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Lime;
            this.progressBar1.Location = new System.Drawing.Point(29, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(546, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // button_Start
            // 
            this.button_Start.Enabled = false;
            this.button_Start.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Start.Location = new System.Drawing.Point(200, 392);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(86, 26);
            this.button_Start.TabIndex = 8;
            this.button_Start.Text = "加载";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Confirm.Location = new System.Drawing.Point(79, 392);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(86, 26);
            this.button_Confirm.TabIndex = 9;
            this.button_Confirm.Text = "确认";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Location = new System.Drawing.Point(456, 392);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(86, 26);
            this.button_Exit.TabIndex = 10;
            this.button_Exit.Text = "退出";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "选择数据文件夹";
            // 
            // comboBox_Inter
            // 
            this.comboBox_Inter.Enabled = false;
            this.comboBox_Inter.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Inter.FormattingEnabled = true;
            this.comboBox_Inter.Items.AddRange(new object[] {
            "1.线性插值",
            "2.Akima插值",
            "3.三次样条插值",
            "4.Kriging插值"});
            this.comboBox_Inter.Location = new System.Drawing.Point(404, 67);
            this.comboBox_Inter.Name = "comboBox_Inter";
            this.comboBox_Inter.Size = new System.Drawing.Size(157, 25);
            this.comboBox_Inter.TabIndex = 12;
            this.comboBox_Inter.Text = "1.线性插值";
            this.comboBox_Inter.SelectedIndexChanged += new System.EventHandler(this.comboBox_Inter_SelectedIndexChanged);
            // 
            // comboBox_Filter
            // 
            this.comboBox_Filter.Enabled = false;
            this.comboBox_Filter.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Filter.FormattingEnabled = true;
            this.comboBox_Filter.Items.AddRange(new object[] {
            "1.中位值平均滤波",
            "2.卡尔曼滤波",
            "3.加权递推平均滤波",
            "4.限幅平均滤波"});
            this.comboBox_Filter.Location = new System.Drawing.Point(103, 116);
            this.comboBox_Filter.Name = "comboBox_Filter";
            this.comboBox_Filter.Size = new System.Drawing.Size(157, 25);
            this.comboBox_Filter.TabIndex = 15;
            this.comboBox_Filter.Text = "1.中位值平均滤波";
            this.comboBox_Filter.SelectedIndexChanged += new System.EventHandler(this.comboBox_Filter_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(635, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(92, 17);
            this.toolStripStatusLabel1.Text = "对深处理进程：";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel2.Text = "就绪";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Red;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(37, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 16);
            this.label11.TabIndex = 5;
            this.label11.Text = "×";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Red;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(37, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 16);
            this.label9.TabIndex = 3;
            this.label9.Text = "×";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Red;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(37, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "×";
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // progressBar2
            // 
            this.progressBar2.ForeColor = System.Drawing.Color.Lime;
            this.progressBar2.Location = new System.Drawing.Point(29, 86);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(546, 23);
            this.progressBar2.TabIndex = 18;
            // 
            // progressBar3
            // 
            this.progressBar3.ForeColor = System.Drawing.Color.Lime;
            this.progressBar3.Location = new System.Drawing.Point(30, 131);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(545, 23);
            this.progressBar3.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(68, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 17);
            this.label12.TabIndex = 20;
            this.label12.Text = "深度文件";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(68, 66);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 17);
            this.label13.TabIndex = 21;
            this.label13.Text = "测量值文件";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(68, 111);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 17);
            this.label14.TabIndex = 22;
            this.label14.Text = "时间文件";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.progressBar2);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.progressBar3);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(15, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 172);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "文件读取进度";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(287, 111);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 17);
            this.label17.TabIndex = 25;
            this.label17.Text = "进度";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(287, 68);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 17);
            this.label16.TabIndex = 24;
            this.label16.Text = "进度";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(287, 23);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 17);
            this.label15.TabIndex = 23;
            this.label15.Text = "进度";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.comboBox_LogMethod);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.comboBox_InsType);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.comboBox_Filter);
            this.groupBox3.Controls.Add(this.comboBox_Inter);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(15, 213);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(608, 162);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "预处理方法";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "测井方式";
            // 
            // comboBox_LogMethod
            // 
            this.comboBox_LogMethod.FormattingEnabled = true;
            this.comboBox_LogMethod.Items.AddRange(new object[] {
            "实测",
            "钻后测"});
            this.comboBox_LogMethod.Location = new System.Drawing.Point(103, 25);
            this.comboBox_LogMethod.Name = "comboBox_LogMethod";
            this.comboBox_LogMethod.Size = new System.Drawing.Size(156, 25);
            this.comboBox_LogMethod.TabIndex = 27;
            this.comboBox_LogMethod.Text = "实测";
            this.comboBox_LogMethod.SelectedIndexChanged += new System.EventHandler(this.comboBox_LogMethod_SelectedIndexChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(25, 117);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(75, 21);
            this.checkBox2.TabIndex = 17;
            this.checkBox2.Text = "滤波算法";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(326, 69);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 21);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "插值算法";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button_Inv
            // 
            this.button_Inv.Enabled = false;
            this.button_Inv.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Inv.Location = new System.Drawing.Point(323, 392);
            this.button_Inv.Name = "button_Inv";
            this.button_Inv.Size = new System.Drawing.Size(86, 26);
            this.button_Inv.TabIndex = 28;
            this.button_Inv.Text = "预处理";
            this.button_Inv.UseVisualStyleBackColor = true;
            this.button_Inv.Click += new System.EventHandler(this.button_Inv_Click);
            // 
            // timerPrint
            // 
            this.timerPrint.Tick += new System.EventHandler(this.timerPrint_Tick);
            // 
            // checkBox_Save2Source
            // 
            this.checkBox_Save2Source.AutoSize = true;
            this.checkBox_Save2Source.Location = new System.Drawing.Point(435, 7);
            this.checkBox_Save2Source.Name = "checkBox_Save2Source";
            this.checkBox_Save2Source.Size = new System.Drawing.Size(99, 21);
            this.checkBox_Save2Source.TabIndex = 29;
            this.checkBox_Save2Source.Text = "保存到源目录";
            this.checkBox_Save2Source.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // TDP
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(635, 462);
            this.Controls.Add(this.checkBox_Save2Source);
            this.Controls.Add(this.button_Inv);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_Confirm);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.Button_pathBrowser);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TDP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time-Depth Process";
            this.Load += new System.EventHandler(this.TDP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Button_pathBrowser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_InsType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox comboBox_Inter;
        private System.Windows.Forms.ComboBox comboBox_Filter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timer;
        public System.Windows.Forms.ProgressBar progressBar2;
        public System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button_Inv;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Timer timerPrint;
        private System.Windows.Forms.CheckBox checkBox_Save2Source;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_LogMethod;
    }
}