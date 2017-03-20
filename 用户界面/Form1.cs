using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LWD_DataProcess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            IsMdiContainer = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBConfig dbc = new DBConfig();
            dbc.ShowDialog();
        }

        private void TDP_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TDP form = new TDP();//时-深
            form.MdiParent = this;
            form.AutoSize = true;
            form.Show();
        }


        private void Gamma_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GammaCorrection gr = new GammaCorrection();
            gr.MdiParent = this;
            gr.AutoSize = true;
            gr.Show();
        }

        private void GDIR_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GDIR_Correction gdir = new GDIR_Correction();
            gdir.MdiParent = this;
            gdir.AutoSize = true;
            gdir.Show();
        }

        private void WPR_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WPR_Correction wpr = new WPR_Correction();
            wpr.MdiParent = this;
            wpr.AutoSize = true;
            wpr.Show();
        }

        private void DBConfig_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBConfig dbc = new DBConfig();
            dbc.MdiParent = this;
            dbc.AutoSize = true;
            dbc.Show();
        }
    }
}
