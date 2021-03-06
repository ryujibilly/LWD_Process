﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace LWD_DataProcess
{
    public partial class WPR_Correction : Form
    {
        /// <summary>
        /// 静态SQLiteConnectionPool接口实例
        /// </summary>
        public static ISQLiteConnectionPool SQLConPool { get; set; }
        /// <summary>
        /// 断词符号集
        /// </summary>
        public static readonly String[] seperator = { "\t", " ", "  ", "      ", "       ", "\\", "/", "_" };
        /// <summary>
        /// 测量值文件名
        /// </summary>
        public static String FileName_WellData { get; set; }
        /// <summary>
        /// 当前操作节点
        /// </summary>
        private object tempNode { get; set; }
        /// <summary>
        /// SQLite基础操作帮助类对象-井信息
        /// </summary>
        private SQLiteDBHelper WellHelper { get; set; }
        /// <summary>
        /// SQLite基础操作帮助类对象-图版信息
        /// </summary>
        private SQLiteDBHelper ChartHelper { get; set; }
        /// <summary>
        /// 当前OpenFileDialog_Chart 的记录行
        /// </summary>
        private String Lines_Chart { get; set; }
        /// <summary>
        /// 当前OpenFileDialog_Data 的记录行
        /// </summary>
        private String Lines_Data { get; set; }
        /// <summary>
        /// ReadLine断句数组
        /// </summary>
        private String[] curLine { get; set; }

        public WPR_Correction()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void WPR_Correction_Load(object sender, EventArgs e)
        {
            //读取NodeSettings.xml配置
            Config.GetConfig();

            Properties.Settings.Default.DB_Well_ConnectionString = "Data Source=" + Properties.Settings.Default.DBPath_WellInfo;
            Properties.Settings.Default.DB_Chart_ConnectionString = "Data Source=" + Properties.Settings.Default.DBPath_ChartInfo;
            WellHelper = new SQLiteDBHelper(Properties.Settings.Default.DBPath_WellInfo);//XML的节点赋值
            ChartHelper = new SQLiteDBHelper(Properties.Settings.Default.DBPath_WellInfo);//XML的节点赋值
        }

        #region 井眼校正

        #endregion

        #region 树形结构

        #endregion
        #region 介电常数
        #endregion
        #region 围岩校正
        #endregion
        #region 侵入校正
        #endregion
        #region 各向异性
        #endregion
        #region 控件定义
        private void radioButton_Borehole1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Borehole1.Checked)
                numericUpDown_Borehole.Enabled = true;
            else numericUpDown_Borehole.Enabled = false;
        }

        private void radioButton_Borehole2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Borehole2.Checked)
                button_LoadBorehole.Enabled = true;
            else button_LoadBorehole.Enabled = false;
        }
        #endregion



        private void button_SelectFolder_Click(object sender, EventArgs e)
        {
            openFileDialog_WPR.Filter = "测量值文件(*.tmf)|*.tmf|文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog_WPR.ShowDialog(this.Owner) == DialogResult.OK)
            {
                FileName_WellData = openFileDialog_WPR.FileName;
                textBox_Folder.Text = FileName_WellData;
            }
            Properties.Settings.Default.RawFile = textBox_Folder.Text.Trim();
        }
        #region 图版"关联/取消关联"--操作

        /// <summary>
        /// 图版关联
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_BindChart_Click(object sender, EventArgs e)
        {
            
            if (openFile_BindChart.ShowDialog(Owner) == DialogResult.OK)
            {
                Properties.Settings.Default.ChartFolderPath=openFile_BindChart.FileName;//XML的节点赋值
                openFile_BindChart.InitialDirectory = Properties.Settings.Default.ChartFolderPath;//默认路径赋值
                OpenChart();
                Thread.Sleep(100);
            }
            BindChart();
            //Properties.Settings.Default.SettingsKey. treeView_WPR.SelectedNode.FullPath;
        }

        /// <summary>
        /// 图版导出txt文件的字符串截取流处理操作
        /// </summary>
        private void txtSplitter()
        {
            try
            {
                if (Funcs.IsParameterExpresion(curLine[0]))//参数表达式
                {

                }
                if (true)//英文开头字符串(参数)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 图版索引匹配
        /// </summary>
        private void BindChart()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        private delegate void OpenDelegate();
        /// <summary>
        /// 打开文件，线程托管
        /// </summary>
        private void OpenChart()
        {
            if (this.InvokeRequired)
            {
                OpenDelegate openChart = new OpenDelegate(OpenChart);
                this.BeginInvoke(openChart);
            }
            else
            {
                openChart();
            }
        }

        void openChart()
        {
            FileStream fs = new FileStream(openFile_BindChart.FileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            while ((Lines_Chart = sr.ReadLine()) != null)
            {
                curLine = Lines_Chart.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                txtSplitter();
            }
        }

        /// <summary>
        /// 取消关联按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_CancelBind_Click(object sender, EventArgs e)
        {
            BindCancel();//如果更取消关联图版配置，则选择该节点
        }
        /// <summary>
        /// 取消关联操作
        /// </summary>
        internal void BindCancel()
        {
            try
            {
               // SQLConPool.Add();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void contextMenuStrip_ChartInfo_Opening(object sender, CancelEventArgs e)
        {

            if (treeView_WPR.SelectedNode.Level < 5 && treeView_WPR.SelectedNode.Level > 1)
            {
                contextMenuStrip_ChartInfo.Items[0].Enabled = false;//判断节点深度，如果非叶子节点，则不能"关联图版"
                contextMenuStrip_ChartInfo.Items[1].Enabled = true;
            }
            else if (treeView_WPR.SelectedNode.Level < 1)
            {
                contextMenuStrip_ChartInfo.Items[0].Enabled = false;//判断节点深度，如果非叶子节点，则不能"关联图版"
                contextMenuStrip_ChartInfo.Items[1].Enabled = false;//..............如果是根节点,则不能"取消关联"
            }
            else if (treeView_WPR.SelectedNode.Level == 5)
            {
                contextMenuStrip_ChartInfo.Items[0].Enabled = true;//如果是叶子节点，则可以"关联图版"
                contextMenuStrip_ChartInfo.Items[1].Enabled = true;
            }
        }

        private void openFile_BindChart_FileOk(object sender, CancelEventArgs e)
        {
            openFile_BindChart.Filter = "图版导出数据文件(*.txt)|*.txt|所有文件(*.*)|*.*";
        }

        private void toolStripMenuItem_Collapse_Click(object sender, EventArgs e)
        {
            treeView_WPR.CollapseAll();
        }

        private void toolStripMenuItem_Expand_Click(object sender, EventArgs e)
        {
            treeView_WPR.ExpandAll();
        }

        private void ToolStripMenuItem_ExpandNode_Click(object sender, EventArgs e)
        {
            treeView_WPR.SelectedNode.ExpandAll();
        }

        private void ToolStripMenuItem_CollapseNode_Click(object sender, EventArgs e)
        {
            treeView_WPR.SelectedNode.Collapse();
        }

        private void button_ApplyChart_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void button_CancelChart_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
        }
        #endregion
    }
}
