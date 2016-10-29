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
    public partial class DBConfig : Form
    {
        /// <summary>
        /// 文件夹路径
        /// </summary>
        String FoldBrowserPath { get; set; }
        /// <summary>
        /// 井信息数据库文件路径
        /// </summary>
        String WellDBPath { get; set; }
        /// <summary>
        /// 校正图版数据库文件路径
        /// </summary>
        String ChartDBPath { get; set; }
        public DBConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定并保存数据库路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Config.SaveConfig();// ConfigInfo "Data Source=" + FoldBrowserPath + "\\WPR_Config";
        }

        /// <summary>
        /// 退出并恢复原配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_ChartInfoDB_Click(object sender, EventArgs e)
        {
            openFileDialog_ChartInfo.Filter = "SQLite文件(*.db)|*.db|所有文件(*.*)|*.*";
            if (openFileDialog_ChartInfo.ShowDialog(Owner) == DialogResult.OK)
            {
                ChartDBPath = openFileDialog_ChartInfo.FileName;
                textBox_Chart.Text = ChartDBPath;
            }
            Properties.Settings.Default.DBPath_ChartInfo = ChartDBPath;
            Config.CfgInfo.DBPath_CorrectionChart = ChartDBPath;
        }

        private void button_WellInfoDB_Click(object sender, EventArgs e)
        {
            openFileDialog_WellInfo.Filter = "SQLite文件(*.db)|*.db|所有文件(*.*)|*.*";
            if (openFileDialog_WellInfo.ShowDialog(Owner) == DialogResult.OK)
            {
                WellDBPath= openFileDialog_WellInfo.FileName;
                textBox_Well.Text = WellDBPath;
            }
            Properties.Settings.Default.DBPath_WellInfo = WellDBPath;
            Config.CfgInfo.DBPath_Well = WellDBPath;
        }

        private void openFileDialog_WellInfo_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void DBConfig_Load(object sender, EventArgs e)
        {
            Config.GetConfig();
            textBox_Chart.Text = Config.CfgInfo.DBPath_CorrectionChart;
            textBox_Well.Text = Config.CfgInfo.DBPath_Well;
        }
    }
}
