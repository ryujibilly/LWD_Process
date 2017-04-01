namespace LWD_DataProcess
{
    partial class GDIR_Correction
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_WellName = new System.Windows.Forms.TextBox();
            this.button_Load = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button_SelectFolder = new System.Windows.Forms.Button();
            this.comboBox_ToolSize = new System.Windows.Forms.ComboBox();
            this.checkBox_Save2Root = new System.Windows.Forms.CheckBox();
            this.label_ToolSize = new System.Windows.Forms.Label();
            this.textBox_Folder = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ChartSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.textBox_WellName);
            this.groupBox2.Controls.Add(this.button_Load);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button_SelectFolder);
            this.groupBox2.Controls.Add(this.comboBox_ToolSize);
            this.groupBox2.Controls.Add(this.checkBox_Save2Root);
            this.groupBox2.Controls.Add(this.label_ToolSize);
            this.groupBox2.Controls.Add(this.textBox_Folder);
            this.groupBox2.Font = new System.Drawing.Font("黑体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(823, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工程设置";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(559, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "inch";
            // 
            // textBox_WellName
            // 
            this.textBox_WellName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LWD_DataProcess.Properties.Settings.Default, "WellName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_WellName.Location = new System.Drawing.Point(475, 17);
            this.textBox_WellName.Name = "textBox_WellName";
            this.textBox_WellName.Size = new System.Drawing.Size(173, 22);
            this.textBox_WellName.TabIndex = 5;
            this.textBox_WellName.Text = global::LWD_DataProcess.Properties.Settings.Default.WellName;
            this.textBox_WellName.TextChanged += new System.EventHandler(this.textBox_WellName_TextChanged);
            // 
            // button_Load
            // 
            this.button_Load.Font = new System.Drawing.Font("黑体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Load.Location = new System.Drawing.Point(672, 20);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(110, 46);
            this.button_Load.TabIndex = 4;
            this.button_Load.Text = "加  载";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(406, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "井    名";
            // 
            // button_SelectFolder
            // 
            this.button_SelectFolder.Location = new System.Drawing.Point(6, 16);
            this.button_SelectFolder.Name = "button_SelectFolder";
            this.button_SelectFolder.Size = new System.Drawing.Size(160, 21);
            this.button_SelectFolder.TabIndex = 3;
            this.button_SelectFolder.Text = "选择原始数据文件";
            this.button_SelectFolder.UseVisualStyleBackColor = true;
            this.button_SelectFolder.Click += new System.EventHandler(this.button_SelectFolder_Click);
            // 
            // comboBox_ToolSize
            // 
            this.comboBox_ToolSize.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LWD_DataProcess.Properties.Settings.Default, "ToolSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.comboBox_ToolSize.FormattingEnabled = true;
            this.comboBox_ToolSize.Items.AddRange(new object[] {
            "6.75",
            "4.75"});
            this.comboBox_ToolSize.Location = new System.Drawing.Point(475, 45);
            this.comboBox_ToolSize.Name = "comboBox_ToolSize";
            this.comboBox_ToolSize.Size = new System.Drawing.Size(78, 21);
            this.comboBox_ToolSize.TabIndex = 3;
            this.comboBox_ToolSize.Text = global::LWD_DataProcess.Properties.Settings.Default.ToolSize;
            this.comboBox_ToolSize.SelectedIndexChanged += new System.EventHandler(this.comboBox_ToolSize_SelectedIndexChanged);
            // 
            // checkBox_Save2Root
            // 
            this.checkBox_Save2Root.AutoSize = true;
            this.checkBox_Save2Root.Location = new System.Drawing.Point(230, 20);
            this.checkBox_Save2Root.Name = "checkBox_Save2Root";
            this.checkBox_Save2Root.Size = new System.Drawing.Size(110, 17);
            this.checkBox_Save2Root.TabIndex = 2;
            this.checkBox_Save2Root.Text = "保存到根目录";
            this.checkBox_Save2Root.UseVisualStyleBackColor = true;
            this.checkBox_Save2Root.CheckedChanged += new System.EventHandler(this.checkBox_Save2Root_CheckedChanged);
            // 
            // label_ToolSize
            // 
            this.label_ToolSize.AutoSize = true;
            this.label_ToolSize.Location = new System.Drawing.Point(406, 48);
            this.label_ToolSize.Name = "label_ToolSize";
            this.label_ToolSize.Size = new System.Drawing.Size(63, 13);
            this.label_ToolSize.TabIndex = 0;
            this.label_ToolSize.Text = "钻铤尺寸";
            // 
            // textBox_Folder
            // 
            this.textBox_Folder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LWD_DataProcess.Properties.Settings.Default, "RawFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Folder.Enabled = false;
            this.textBox_Folder.Location = new System.Drawing.Point(6, 42);
            this.textBox_Folder.Name = "textBox_Folder";
            this.textBox_Folder.Size = new System.Drawing.Size(380, 22);
            this.textBox_Folder.TabIndex = 1;
            this.textBox_Folder.Text = global::LWD_DataProcess.Properties.Settings.Default.RawFile;
            this.textBox_Folder.TextChanged += new System.EventHandler(this.textBox_Folder_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.ToolStripMenuItem_ChartSetting,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(851, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // ToolStripMenuItem_ChartSetting
            // 
            this.ToolStripMenuItem_ChartSetting.Name = "ToolStripMenuItem_ChartSetting";
            this.ToolStripMenuItem_ChartSetting.Size = new System.Drawing.Size(68, 21);
            this.ToolStripMenuItem_ChartSetting.Text = "图版配置";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // GDIR_Correction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 572);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("黑体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GDIR_Correction";
            this.Text = "GDIR双感应电阻率环境校正";
            this.Load += new System.EventHandler(this.GDIR_Correction_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_WellName;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_SelectFolder;
        private System.Windows.Forms.ComboBox comboBox_ToolSize;
        private System.Windows.Forms.CheckBox checkBox_Save2Root;
        private System.Windows.Forms.Label label_ToolSize;
        private System.Windows.Forms.TextBox textBox_Folder;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ChartSetting;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
    }
}