namespace LWD_DataProcess.用户界面.子窗口
{
    partial class GDIR_ChartSetting
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
            System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("深感应");
            System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("中感应");
            System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("井眼校正", new System.Windows.Forms.TreeNode[] {
            treeNode51,
            treeNode52});
            System.Windows.Forms.TreeNode treeNode54 = new System.Windows.Forms.TreeNode("深感应");
            System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("中感应");
            System.Windows.Forms.TreeNode treeNode56 = new System.Windows.Forms.TreeNode("围岩校正", new System.Windows.Forms.TreeNode[] {
            treeNode54,
            treeNode55});
            System.Windows.Forms.TreeNode treeNode57 = new System.Windows.Forms.TreeNode("深感应");
            System.Windows.Forms.TreeNode treeNode58 = new System.Windows.Forms.TreeNode("中感应");
            System.Windows.Forms.TreeNode treeNode59 = new System.Windows.Forms.TreeNode("侵入校正", new System.Windows.Forms.TreeNode[] {
            treeNode57,
            treeNode58});
            System.Windows.Forms.TreeNode treeNode60 = new System.Windows.Forms.TreeNode("6.75英寸", new System.Windows.Forms.TreeNode[] {
            treeNode53,
            treeNode56,
            treeNode59});
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关联图版ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消关联ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.折叠ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.展开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.button_Apply = new System.Windows.Forms.Button();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Location = new System.Drawing.Point(15, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 409);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图版配置";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(6, 21);
            this.treeView1.Name = "treeView1";
            treeNode51.Name = "节点8";
            treeNode51.Text = "深感应";
            treeNode52.Name = "节点9";
            treeNode52.Text = "中感应";
            treeNode53.Name = "节点1";
            treeNode53.Text = "井眼校正";
            treeNode54.Name = "节点10";
            treeNode54.Text = "深感应";
            treeNode55.Name = "节点11";
            treeNode55.Text = "中感应";
            treeNode56.Name = "节点3";
            treeNode56.Text = "围岩校正";
            treeNode57.Name = "节点12";
            treeNode57.Text = "深感应";
            treeNode58.Name = "节点13";
            treeNode58.Text = "中感应";
            treeNode59.Name = "节点7";
            treeNode59.Text = "侵入校正";
            treeNode60.ContextMenuStrip = this.contextMenuStrip1;
            treeNode60.Name = "节点0";
            treeNode60.Text = "6.75英寸";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode60});
            this.treeView1.Size = new System.Drawing.Size(209, 381);
            this.treeView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关联图版ToolStripMenuItem,
            this.取消关联ToolStripMenuItem,
            this.折叠ToolStripMenuItem,
            this.展开ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 92);
            // 
            // 关联图版ToolStripMenuItem
            // 
            this.关联图版ToolStripMenuItem.Name = "关联图版ToolStripMenuItem";
            this.关联图版ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关联图版ToolStripMenuItem.Text = "关联图版";
            // 
            // 取消关联ToolStripMenuItem
            // 
            this.取消关联ToolStripMenuItem.Name = "取消关联ToolStripMenuItem";
            this.取消关联ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.取消关联ToolStripMenuItem.Text = "取消关联";
            // 
            // 折叠ToolStripMenuItem
            // 
            this.折叠ToolStripMenuItem.Name = "折叠ToolStripMenuItem";
            this.折叠ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.折叠ToolStripMenuItem.Text = "折叠";
            // 
            // 展开ToolStripMenuItem
            // 
            this.展开ToolStripMenuItem.Name = "展开ToolStripMenuItem";
            this.展开ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.展开ToolStripMenuItem.Text = "展开";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.treeView2);
            this.groupBox2.Location = new System.Drawing.Point(249, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 409);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "详细信息";
            // 
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(7, 22);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(229, 380);
            this.treeView2.TabIndex = 0;
            // 
            // button_Apply
            // 
            this.button_Apply.Location = new System.Drawing.Point(82, 429);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(75, 31);
            this.button_Apply.TabIndex = 3;
            this.button_Apply.Text = "应用";
            this.button_Apply.UseVisualStyleBackColor = true;
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(342, 429);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(75, 31);
            this.Button_Exit.TabIndex = 4;
            this.Button_Exit.Text = "退出";
            this.Button_Exit.UseVisualStyleBackColor = true;
            // 
            // GDIR_ChartSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 472);
            this.Controls.Add(this.Button_Exit);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("黑体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "GDIR_ChartSetting";
            this.Text = "双感应图版配置";
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关联图版ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消关联ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 折叠ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 展开ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.Button Button_Exit;
    }
}