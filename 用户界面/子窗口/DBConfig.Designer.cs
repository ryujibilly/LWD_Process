namespace LWD_DataProcess
{
    partial class DBConfig
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
            this.button_WellInfoDB = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button_ChartInfoDB = new System.Windows.Forms.Button();
            this.openFileDialog_WellInfo = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog_ChartInfo = new System.Windows.Forms.OpenFileDialog();
            this.textBox_Chart = new System.Windows.Forms.TextBox();
            this.textBox_Well = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_WellInfoDB
            // 
            this.button_WellInfoDB.Location = new System.Drawing.Point(12, 59);
            this.button_WellInfoDB.Name = "button_WellInfoDB";
            this.button_WellInfoDB.Size = new System.Drawing.Size(94, 23);
            this.button_WellInfoDB.TabIndex = 0;
            this.button_WellInfoDB.Text = "井信息数据库";
            this.button_WellInfoDB.UseVisualStyleBackColor = true;
            this.button_WellInfoDB.Click += new System.EventHandler(this.button_WellInfoDB_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(43, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(257, 88);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "退出";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_ChartInfoDB
            // 
            this.button_ChartInfoDB.Location = new System.Drawing.Point(12, 21);
            this.button_ChartInfoDB.Name = "button_ChartInfoDB";
            this.button_ChartInfoDB.Size = new System.Drawing.Size(94, 23);
            this.button_ChartInfoDB.TabIndex = 4;
            this.button_ChartInfoDB.Text = "图版数据库";
            this.button_ChartInfoDB.UseVisualStyleBackColor = true;
            this.button_ChartInfoDB.Click += new System.EventHandler(this.button_ChartInfoDB_Click);
            // 
            // openFileDialog_WellInfo
            // 
            this.openFileDialog_WellInfo.FileName = "WellDB_Path";
            this.openFileDialog_WellInfo.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_WellInfo_FileOk);
            // 
            // openFileDialog_ChartInfo
            // 
            this.openFileDialog_ChartInfo.FileName = "ChartDB_Path";
            // 
            // textBox_Chart
            // 
            this.textBox_Chart.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LWD_DataProcess.Properties.Settings.Default, "DBPath_ChartInfo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Chart.Enabled = false;
            this.textBox_Chart.Location = new System.Drawing.Point(112, 23);
            this.textBox_Chart.Name = "textBox_Chart";
            this.textBox_Chart.Size = new System.Drawing.Size(280, 21);
            this.textBox_Chart.TabIndex = 5;
            this.textBox_Chart.Text = global::LWD_DataProcess.Properties.Settings.Default.DBPath_ChartInfo;
            // 
            // textBox_Well
            // 
            this.textBox_Well.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LWD_DataProcess.Properties.Settings.Default, "DBPath_WellInfo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Well.Enabled = false;
            this.textBox_Well.Location = new System.Drawing.Point(112, 61);
            this.textBox_Well.Name = "textBox_Well";
            this.textBox_Well.Size = new System.Drawing.Size(280, 21);
            this.textBox_Well.TabIndex = 1;
            this.textBox_Well.Text = global::LWD_DataProcess.Properties.Settings.Default.DBPath_WellInfo;
            // 
            // DBConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 124);
            this.Controls.Add(this.textBox_Chart);
            this.Controls.Add(this.button_ChartInfoDB);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_Well);
            this.Controls.Add(this.button_WellInfoDB);
            this.Name = "DBConfig";
            this.Text = "数据库配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_WellInfoDB;
        private System.Windows.Forms.TextBox textBox_Well;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox_Chart;
        private System.Windows.Forms.Button button_ChartInfoDB;
        private System.Windows.Forms.OpenFileDialog openFileDialog_WellInfo;
        private System.Windows.Forms.OpenFileDialog openFileDialog_ChartInfo;
    }
}