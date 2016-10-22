using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace LWD_DataProcess
{
    public partial class TDP : Form
    {
        /// <summary>
        /// 数据处理类对象
        /// </summary>
        DataProcess DP { get; set; }
        #region XML配置参数
        String UntitledName, CalName, OriName;//三个参数文件名
        /// <summary>
        /// 文件夹路径
        /// </summary>
        String FoldBrowserPath { get; set; }
        /// <summary>
        /// 深度间隔
        /// </summary>
        float DepthInterval { get; set; }
        /// <summary>
        /// 仪器类型
        /// </summary>
        Ins_Type Ins { get; set; }
        /// <summary>
        /// 插值算法
        /// </summary>
        COI_Type COI { get; set; }
        /// <summary>
        /// 滤波算法
        /// </summary>
        FA_Type FA { get; set; }
        #endregion
        /// <summary>
        /// 按钮标记--是否已读取文件。
        /// </summary>
        Boolean Readed { get; set; }
        /// <summary>
        /// 按钮标记--是否已加载数据
        /// </summary>
        Boolean Loaded { get; set; }
        /// <summary>
        /// 预处理标记--是否已预处理数据
        /// </summary>
        Boolean Calculated { get; set; }

        public TDP()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = true;
            FoldBrowserPath = Config.CfgInfo.FoldBrowserPath;
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void TDP_Load(object sender, EventArgs e)
        {
            Readed = false;
            LoadConfig();
            textBox1.Text = Config.CfgInfo.FoldBrowserPath;
            FoldBrowserPath = Config.CfgInfo.FoldBrowserPath;
            comboBox_InsType.SelectedText = Config.CfgInfo.IT.ToString();
        }

        /// <summary>
        /// 选择数据文件夹，读取目录下的*.tdf,*.tmf文件，并保存处理后文件
        /// </summary>
        private void Button_pathBrowser_Click(object sender, EventArgs e)
        {
            Boolean pathExist;
            try
            {
                if(folderBrowserDialog1.ShowDialog(this)==DialogResult.OK)
                {
                    FoldBrowserPath = folderBrowserDialog1.SelectedPath;
                    textBox1.Text = FoldBrowserPath;
                    getFileName();
                }
                pathExist = fileExist();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 确认按钮操作： 1确认数据处理流程 2同时更新XML配置 3载入原始数据
        /// </summary>
        private void button_Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfig();
                if (fileExist())
                {
                    DP = new DataProcess(UntitledName, CalName, OriName);
                    DP.Start();
                    Thread.Sleep(1000);
                    progressBar1.Maximum = (int)DataStruct.Len_Untitled;
                    progressBar2.Maximum = (int)DataStruct.Len_Cal;
                    progressBar3.Maximum = (int)DataStruct.Len_Ori;
                    timer.Start();
                    Readed = true;
                }
                else Readed = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                toolStripStatusLabel2.Text = ex.ToString();
            }
        }

        /// <summary>
        /// 加载按钮操作： 开始文件预处理
        /// </summary>
        private void button_Start_Click(object sender, EventArgs e)
        {
            try
            {
                if (Readed)
                    Loaded=DP.Process();
                else if (!Readed)
                    CommonData.SList.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("请等待文件加载完毕！"+ex.ToString());
                Loaded = false;
            }
        }
        /// <summary>
        /// 预处理按钮，选定预处理方案，开始数据预处理
        /// </summary>
        private void button_Inv_Click(object sender, EventArgs e)
        {
            try
            {
                if (Loaded)
                {
                    groupBox3.Enabled = false;
                    comboBox_Filter.Enabled = false;
                    comboBox_InsType.Enabled = false;
                    comboBox_Inter.Enabled = false;
                    Calculated=DP.Calculate(COI,FA,Ins);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("请等待预处理计算完毕！" + ex.ToString());
                Calculated = false;
            }

        }
        /// <summary>
        /// 退出窗体
        /// </summary>
        private void button_Exit_Click(object sender, EventArgs e)
        {
            DP.Stop();
            this.Close();
        }
        /// <summary>
        /// 定时器：控制进度条
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (DP.No_U < DataStruct.Len_Untitled)
            { 
                progressBar1.Value = DP.No_U;
                label15.Text = DP.No_U+2 + "/" + progressBar1.Maximum;
            }

            if (DP.No_C < DataStruct.Len_Cal)
            {
                progressBar2.Value = DP.No_C;
                label16.Text = DP.No_C+2 + "/" + progressBar2.Maximum;
            }
            if (DP.No_O < DataStruct.Len_Ori)
            {
                progressBar3.Value = DP.No_O;
                label17.Text = DP.No_O+1 + "/" + progressBar3.Maximum;
            }
            else timer.Stop();
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
        }


        #region 非主体方法
        /// <summary>
        /// 数据文件载入验证
        /// </summary>
        private Boolean fileExist()
        {
            Boolean isChecked = false;
            Boolean a, b, c;
            try
            {
                if (File.Exists(UntitledName))
                {
                    label7.Text = "√";
                    label7.BackColor = Color.Lime;
                    a = true;
                }
                else
                {
                    label7.Text = "×";
                    label7.BackColor = Color.Red;
                    a = false;
                    File.Open(UntitledName, FileMode.Open, FileAccess.Read);
                }
                if (File.Exists(CalName))
                {
                    label9.Text = "√";
                    label9.BackColor = Color.Lime;
                    b = true;
                }
                else
                {
                    label9.Text = "×";
                    label9.BackColor = Color.Red;
                    b = false;
                    File.Open(CalName, FileMode.Open, FileAccess.Read);
                }
                if (File.Exists(OriName))
                {
                    label11.Text = "√";
                    label11.BackColor = Color.Lime;
                    c = true;
                }
                else
                {
                    label11.Text = "×";
                    label11.BackColor = Color.Red;
                    c = false;
                    File.Open(OriName, FileMode.Open, FileAccess.Read);
                }
                return a & b & c;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "原始数据文件路径错误！请重新选择文件夹。");
                return isChecked;
            }

        }

        /// <summary>
        /// 从文件夹目录获取参数文件名
        /// </summary>
        private void getFileName()
        {
            UntitledName = FoldBrowserPath + "\\untitled.dtf";
            CalName = FoldBrowserPath + "\\Cal.tmf";
            OriName = FoldBrowserPath + "\\Ori-1.txt";
        }

        /// <summary>
        /// 保存XML配置信息
        /// </summary>
        void SaveConfig()
        {
            try
            {
                Config.CfgInfo.FoldBrowserPath = FoldBrowserPath;
                Config.CfgInfo.DepthInterval = DepthInterval;
                Config.CfgInfo.IT = Ins;
                Config.CfgInfo.CT = COI;
                Config.CfgInfo.FT = FA;
                Config.SaveConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存配置出错，请检查数据格式是否正确! \r\n" + ex.ToString());
            }
        }
        void LoadConfig()
        {
            try
            {
                Config.GetConfig();
                FoldBrowserPath = Config.CfgInfo.FoldBrowserPath;
                getFileName();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取配置出错！\r\n" + ex.ToString());
            }
        }
        #endregion

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_InsType.SelectedValue.ToString() == "居中伽马")
                this.Ins = Ins_Type.CGR;
            if (comboBox_InsType.SelectedValue.ToString() == "感应电阻率")
                this.Ins = Ins_Type.GDIR;
            if (comboBox_InsType.SelectedValue.ToString() == "电磁波电阻率")
                this.Ins = Ins_Type.WPR;
        }


        #region 选择预处理方案
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DepthInterval = float.Parse(numericUpDown1.Value.ToString());
        }

        /// <summary>
        /// 选择插值算法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Inter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(this.comboBox_Inter.SelectedIndex)
            {
                case 0: COI= COI_Type.Linear;
                    break;
                case 1: COI = COI_Type.Akima;
                    break;
                case 2: COI = COI_Type.ThreeTimes;
                    break;
                case 3: COI = COI_Type.Kriging;
                    break;
                default: COI = COI_Type.Linear;
                    break;
            }
        }
        /// <summary>
        /// 选择滤波算法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Filter.SelectedIndex)
            {
                case 0: FA = FA_Type.X;
                    break;
                case 1: FA = FA_Type.Y;
                    break;
                case 2: FA = FA_Type.Z;
                    break;
                default: FA = FA_Type.X;
                    break;
            }
        }
        #endregion

        private void comboBox_InsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_InsType.SelectedIndex)
            {
                case 0: FA = FA_Type.X;
                    break;
                case 1: FA = FA_Type.Y;
                    break;
                case 2: FA = FA_Type.Z;
                    break;
                default: FA = FA_Type.X;
                    break;
            }
        }

    }
}