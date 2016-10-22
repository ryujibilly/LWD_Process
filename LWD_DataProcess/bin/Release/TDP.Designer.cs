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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_Filter = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button_Inv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(50, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件地址：";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(158, 14);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(468, 27);
            this.textBox1.TabIndex = 1;
            this.textBox1.MouseHover += new System.EventHandler(this.textBox1_MouseHover);
            // 
            // Button_pathBrowser
            // 
            this.Button_pathBrowser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_pathBrowser.Location = new System.Drawing.Point(634, 13);
            this.Button_pathBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.Button_pathBrowser.Name = "Button_pathBrowser";
            this.Button_pathBrowser.Size = new System.Drawing.Size(116, 32);
            this.Button_pathBrowser.TabIndex = 2;
            this.Button_pathBrowser.Text = "打开文件夹";
            this.Button_pathBrowser.UseVisualStyleBackColor = true;
            this.Button_pathBrowser.Click += new System.EventHandler(this.Button_pathBrowser_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(9, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
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
            this.comboBox_InsType.Location = new System.Drawing.Point(158, 30);
            this.comboBox_InsType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_InsType.Name = "comboBox_InsType";
            this.comboBox_InsType.Size = new System.Drawing.Size(139, 27);
            this.comboBox_InsType.TabIndex = 4;
            this.comboBox_InsType.Text = "居中伽马";
            this.comboBox_InsType.SelectedIndexChanged += new System.EventHandler(this.comboBox_InsType_SelectedIndexChanged);
            this.comboBox_InsType.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(352, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "深度间隔";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(522, 30);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4);
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
            this.numericUpDown1.Size = new System.Drawing.Size(146, 27);
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
            this.progressBar1.Location = new System.Drawing.Point(8, 46);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(682, 29);
            this.progressBar1.TabIndex = 7;
            // 
            // button_Start
            // 
            this.button_Start.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Start.Location = new System.Drawing.Point(247, 752);
            this.button_Start.Margin = new System.Windows.Forms.Padding(4);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(108, 33);
            this.button_Start.TabIndex = 8;
            this.button_Start.Text = "加载";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Confirm.Location = new System.Drawing.Point(95, 752);
            this.button_Confirm.Margin = new System.Windows.Forms.Padding(4);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(108, 33);
            this.button_Confirm.TabIndex = 9;
            this.button_Confirm.Text = "确认";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Location = new System.Drawing.Point(567, 752);
            this.button_Exit.Margin = new System.Windows.Forms.Padding(4);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(108, 33);
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
            this.comboBox_Inter.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Inter.FormattingEnabled = true;
            this.comboBox_Inter.Items.AddRange(new object[] {
            "1.线性插值",
            "2.Akima插值",
            "3.三次样条插值",
            "4.Kriging插值"});
            this.comboBox_Inter.Location = new System.Drawing.Point(101, 72);
            this.comboBox_Inter.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_Inter.Name = "comboBox_Inter";
            this.comboBox_Inter.Size = new System.Drawing.Size(243, 27);
            this.comboBox_Inter.TabIndex = 12;
            this.comboBox_Inter.Text = "1.线性插值";
            this.comboBox_Inter.SelectedIndexChanged += new System.EventHandler(this.comboBox_Inter_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(9, 76);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "插值算法";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(352, 76);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "滤波算法";
            // 
            // comboBox_Filter
            // 
            this.comboBox_Filter.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Filter.FormattingEnabled = true;
            this.comboBox_Filter.Items.AddRange(new object[] {
            "1.中位值平均滤波",
            "2.卡尔曼滤波",
            "3.加权递推平均滤波",
            "4.限幅滤波"});
            this.comboBox_Filter.Location = new System.Drawing.Point(446, 72);
            this.comboBox_Filter.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_Filter.Name = "comboBox_Filter";
            this.comboBox_Filter.Size = new System.Drawing.Size(243, 27);
            this.comboBox_Filter.TabIndex = 15;
            this.comboBox_Filter.Text = "1.中位值平均滤波";
            this.comboBox_Filter.SelectedIndexChanged += new System.EventHandler(this.comboBox_Filter_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 789);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(794, 24);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(114, 19);
            this.toolStripStatusLabel1.Text = "对深处理进程：";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(39, 19);
            this.toolStripStatusLabel2.Text = "就绪";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(44, 56);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(706, 70);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据文件验证";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Red;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(533, 43);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 20);
            this.label11.TabIndex = 5;
            this.label11.Text = "×";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(519, 24);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 19);
            this.label10.TabIndex = 4;
            this.label10.Text = "Ori.txt";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Red;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(315, 43);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "×";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(292, 24);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 19);
            this.label8.TabIndex = 2;
            this.label8.Text = "Cal.tmf";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Red;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(98, 43);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "×";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(61, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Untitled.tdf";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // progressBar2
            // 
            this.progressBar2.ForeColor = System.Drawing.Color.Lime;
            this.progressBar2.Location = new System.Drawing.Point(8, 101);
            this.progressBar2.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(682, 29);
            this.progressBar2.TabIndex = 18;
            // 
            // progressBar3
            // 
            this.progressBar3.ForeColor = System.Drawing.Color.Lime;
            this.progressBar3.Location = new System.Drawing.Point(9, 157);
            this.progressBar3.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(681, 29);
            this.progressBar3.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(15, 23);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 19);
            this.label12.TabIndex = 20;
            this.label12.Text = "Untitled.dtf 文件";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(15, 79);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 19);
            this.label13.TabIndex = 21;
            this.label13.Text = "Cal.tmf 文件";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(15, 134);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 19);
            this.label14.TabIndex = 22;
            this.label14.Text = "Ori.txt 文件";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.progressBar2);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.progressBar3);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(46, 134);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(705, 196);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "文件读取进度";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(153, 134);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 19);
            this.label17.TabIndex = 25;
            this.label17.Text = "进度";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(153, 78);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 19);
            this.label16.TabIndex = 24;
            this.label16.Text = "进度";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(153, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 19);
            this.label15.TabIndex = 23;
            this.label15.Text = "进度";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.comboBox_InsType);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.comboBox_Filter);
            this.groupBox3.Controls.Add(this.comboBox_Inter);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(45, 338);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(705, 127);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "预处理方法";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(46, 472);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(705, 137);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "居中伽马";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(46, 615);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(705, 62);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "感应电阻率";
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(46, 683);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(705, 62);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "电磁波电阻率";
            // 
            // button_Inv
            // 
            this.button_Inv.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Inv.Location = new System.Drawing.Point(401, 752);
            this.button_Inv.Margin = new System.Windows.Forms.Padding(4);
            this.button_Inv.Name = "button_Inv";
            this.button_Inv.Size = new System.Drawing.Size(108, 33);
            this.button_Inv.TabIndex = 28;
            this.button_Inv.Text = "预处理";
            this.button_Inv.UseVisualStyleBackColor = true;
            this.button_Inv.Click += new System.EventHandler(this.button_Inv_Click);
            // 
            // TDP
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(794, 813);
            this.Controls.Add(this.button_Inv);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TDP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time-Depth Process";
            this.Load += new System.EventHandler(this.TDP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_Filter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button_Inv;
    }
}