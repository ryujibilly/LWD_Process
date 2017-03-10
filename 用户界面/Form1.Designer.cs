namespace LWD_DataProcess
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TDP_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Correction_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Gamma_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GDIR_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WPR_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DBConfig_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TDP_ToolStripMenuItem,
            this.Correction_ToolStripMenuItem,
            this.DBConfig_ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1003, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // TDP_ToolStripMenuItem
            // 
            this.TDP_ToolStripMenuItem.Name = "TDP_ToolStripMenuItem";
            this.TDP_ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.TDP_ToolStripMenuItem.Text = "时深处理";
            this.TDP_ToolStripMenuItem.Click += new System.EventHandler(this.TDP_ToolStripMenuItem_Click);
            // 
            // Correction_ToolStripMenuItem
            // 
            this.Correction_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Gamma_ToolStripMenuItem,
            this.GDIR_ToolStripMenuItem,
            this.WPR_ToolStripMenuItem});
            this.Correction_ToolStripMenuItem.Name = "Correction_ToolStripMenuItem";
            this.Correction_ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.Correction_ToolStripMenuItem.Text = "环境校正";
            // 
            // Gamma_ToolStripMenuItem
            // 
            this.Gamma_ToolStripMenuItem.Name = "Gamma_ToolStripMenuItem";
            this.Gamma_ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.Gamma_ToolStripMenuItem.Text = "居中伽马环境校正";
            this.Gamma_ToolStripMenuItem.Click += new System.EventHandler(this.Gamma_ToolStripMenuItem_Click);
            // 
            // GDIR_ToolStripMenuItem
            // 
            this.GDIR_ToolStripMenuItem.Name = "GDIR_ToolStripMenuItem";
            this.GDIR_ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.GDIR_ToolStripMenuItem.Text = "双感应电阻率环境校正";
            this.GDIR_ToolStripMenuItem.Click += new System.EventHandler(this.GDIR_ToolStripMenuItem_Click);
            // 
            // WPR_ToolStripMenuItem
            // 
            this.WPR_ToolStripMenuItem.Name = "WPR_ToolStripMenuItem";
            this.WPR_ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.WPR_ToolStripMenuItem.Text = "电磁波电阻率环境校正";
            this.WPR_ToolStripMenuItem.Click += new System.EventHandler(this.WPR_ToolStripMenuItem_Click);
            // 
            // DBConfig_ToolStripMenuItem
            // 
            this.DBConfig_ToolStripMenuItem.Name = "DBConfig_ToolStripMenuItem";
            this.DBConfig_ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.DBConfig_ToolStripMenuItem.Text = "数据库配置";
            this.DBConfig_ToolStripMenuItem.Click += new System.EventHandler(this.DBConfig_ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1003, 970);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "随钻测井资料预处理";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TDP_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Correction_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Gamma_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GDIR_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WPR_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DBConfig_ToolStripMenuItem;
    }
}

