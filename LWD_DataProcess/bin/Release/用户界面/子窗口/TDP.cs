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
        /// <summary>
        /// 伽马校正类
        /// </summary>
        Gamma Gm { get; set; }
        #region XML配置参数
        String UntitledName, CalName, OriName;//三个参数文件名
        /// <summary>
        /// 文件夹路径
        /// </summary>
        String FoldBrowserPath { get; set; }
        /// <summary>
        /// 深度间隔
        /// </summary>
        double DepthInterval { get; set; }
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

        double[] InterParas = new double[4];
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
        FileStream fsOutPut;
        StreamWriter swOutPut;
        String outputPath;

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
            Gm = Gamma._Gamma;
            Readed = false;
            LoadConfig();
            textBox1.Text = Config.CfgInfo.FoldBrowserPath;
            FoldBrowserPath = Config.CfgInfo.FoldBrowserPath;
            comboBox_DrillPipeSize.SelectedIndex = 0;
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
                    toolStripStatusLabel2.Text = "读取中...";
                    DP = new DataProcess(UntitledName, CalName, OriName);
                    DP.Start();
                    //Gm.Start();
                    groupBox_Envir.Enabled = false;
                    Thread.Sleep(1000);
                    progressBar1.Maximum = (int)DataStruct.Len_Untitled;
                    progressBar2.Maximum = (int)DataStruct.Len_Cal;
                    progressBar3.Maximum = (int)DataStruct.Len_Ori;
                    timer.Start();
                    Thread.Sleep(1000);
                    Readed = true;
                    button_Start.Enabled=true;
                }
                else Readed = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                toolStripStatusLabel2.Text = ex.ToString();
            }
            toolStripStatusLabel2.Text = "读取完毕";
        }

        /// <summary>
        /// 加载按钮操作： 开始文件预处理
        /// </summary>
        private void button_Start_Click(object sender, EventArgs e)
        {
            try
            {
                if (Readed)
                {
                    toolStripStatusLabel2.Text = "批处理中...";
                    Loaded=DP.Process();
                    button_Inv.Enabled = true;
                    button_Start.Enabled = false;
                }

                else if (!Readed)
                {
                    CommonData._CD.SList.Clear();
                    Loaded = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请等待文件加载完毕！"+ex.ToString());
                Loaded = false;
            }
            toolStripStatusLabel2.Text = "批处理完毕";
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
                    //toolStripStatusLabel2.Text = "预处理中...";
                    groupBox3.Enabled = false;
                    comboBox_Filter.Enabled = false;
                    comboBox_InsType.Enabled = false;
                    comboBox_Inter.Enabled = false;
                    DataStruct.Interval = double.Parse(numericUpDown1.Value.ToString());
                    //判断队列长度再进行插值计算
                    if (CommonData._CD.SList._SlotList.Count != 0)
                        this.setInterPoParas();
                    Calculated=DP.Calculate(COI,FA,Ins);
                    button_Inv.Enabled = false;
                    String[] wellNames = FoldBrowserPath.Split(DataStruct.seperator, StringSplitOptions.RemoveEmptyEntries);
                    String wellName = wellNames[wellNames.Length - 1];
                    outputPath = FoldBrowserPath + "\\" + wellName + ".txt";
                    fsOutPut = new FileStream(outputPath, FileMode.Append, FileAccess.Write);
                    swOutPut = new StreamWriter(fsOutPut);
                    timerPrint.Start();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("请等待预处理计算完毕！" + ex.ToString());
                Calculated = false;
            }
            toolStripStatusLabel2.Text = "预处理完毕，输出文件在根目录下！";
        }

        private void OutPutFile()
        {
            try
            {   
                String ParaString=null;
                for(int i=1;i<DataStruct.ParaNames.Length;i++)
                    ParaString+=DataStruct.ParaNames[i]+"\t,";
                swOutPut.WriteLine("FORWARD_TEXT_FORMAT_1.0");
                swOutPut.WriteLine("STDEP  = " + Math.Round(InterPolation._InterPo.keyMin, 3));
                swOutPut.WriteLine("ENDEP  = " + Math.Round(InterPolation._InterPo.keyMax, 3));
                swOutPut.WriteLine("RLEV   = " + Math.Round(InterPolation._InterPo.Interval, 3));
                swOutPut.WriteLine("CURVENAME = "+ParaString);
                swOutPut.WriteLine("END");
                swOutPut.WriteLine("#DEPTH\t"+ParaString);
                for (int i = 0; i < CommonData._CD.AIPList.Count;i++ )
                {
                    swOutPut.WriteLine(CommonData._CD.AIPList.Keys[i]+"\t"+Funcs.DoubleArrayToString(CommonData._CD.AIPList.Values[i].Paras,1,6));
                }
                    //foreach (var item in CommonData._CD.AIPList)
                    //{
                    //    swOutPut.WriteLine(item.Key + "\t" + Funcs.DoubleArrayToString(item.Value.Paras));
                    //}
                swOutPut.WriteLine("========================================================================");
                swOutPut.Flush();
                fsOutPut.Flush();
                swOutPut.Close();
                fsOutPut.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 退出窗体
        /// </summary>
        private void button_Exit_Click(object sender, EventArgs e)
        {
            DP.Stop();
            this.Dispose();
            CommonData._CD.SList._SlotList.Clear();
            CommonData._CD.AIPList.Clear();
            this.Close();
        }
        /// <summary>
        /// 定时器：控制进度条
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DP.No_U < DataStruct.Len_Untitled)
                {
                    progressBar1.Value = DP.No_U;
                    //label15.Text = DP.No_U + 2 + "/" + progressBar1.Maximum;
                }

                if (DP.No_C < DataStruct.Len_Cal)
                {
                    progressBar2.Value = DP.No_C;
                    //label16.Text = DP.No_C + 2 + "/" + progressBar2.Maximum;
                }
                if (DP.No_O < DataStruct.Len_Ori)
                {
                    progressBar3.Value = DP.No_O;
                    //label17.Text = DP.No_O + 1 + "/" + progressBar3.Maximum;
                }
                else timer.Stop();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
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
            catch (Exception)
            {
                MessageBox.Show("原始数据文件路径错误！请重新选择文件夹。");
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
        /// XML文件保存配置信息
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

        #region 选择预处理方案
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DepthInterval = double.Parse(numericUpDown1.Value.ToString());
            DataStruct.Interval = DepthInterval;
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
                case 0: this.COI= COI_Type.Linear;
                    break;
                case 1: this.COI = COI_Type.Akima;
                    break;
                case 2: this.COI = COI_Type.ThreeTimes;
                    break;
                case 3: this.COI = COI_Type.Kriging;
                    break;
                default: this.COI = COI_Type.None;
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
                    /*1.中位值平均滤波
                    2.卡尔曼滤波
                    3.加权递推平均滤波
                    4.限幅平均滤波
                     * */
                case 0: this.FA = FA_Type.MidAvr;
                    break;
                case 1: this.FA = FA_Type.Kaman;
                    break;
                case 2: this.FA = FA_Type.WeiAvr;
                    break;
                case 3: this.FA = FA_Type.AmpLimit;
                    break;
                default:this.FA = FA_Type.MidAvr;
                    break;
            }
        }
        #endregion

        private void comboBox_InsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_InsType.SelectedIndex)
            {
                case 0:
                    {
                        Ins = Ins_Type.CGR;
                        groupBox_Gamma.Enabled = true;
                        groupBox_GDIR.Enabled = false;
                        groupBox_WPR.Enabled = false;
                    }
                    break;
                case 1:
                    {
                        Ins = Ins_Type.WPR;
                        groupBox_Gamma.Enabled = false;
                        groupBox_GDIR.Enabled = false;
                        groupBox_WPR.Enabled = true;
                    }
                    break;
                case 2:
                    {
                        Ins = Ins_Type.GDIR;
                        groupBox_Gamma.Enabled = false;
                        groupBox_GDIR.Enabled = true;
                        groupBox_WPR.Enabled = false;
                    }
                    break;
                default:
                    {
                        Ins = Ins_Type.CGR;
                        groupBox_Gamma.Enabled = true;
                    }
                    break;
            }
        }
        private void setInterPoParas()
        {
            DP.ParaLength = DataStruct.Paras_Compared.Length;
            CommonData.getSKeyMinMax();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.CheckState.Equals(CheckState.Unchecked))
            {
                COI = COI_Type.None;
                comboBox_Inter.Enabled = false;
            }
            if(checkBox1.CheckState.Equals(CheckState.Checked))
            {
                comboBox_Inter.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.CheckState.Equals(CheckState.Unchecked))
            {
                FA = FA_Type.None;
                comboBox_Filter.Enabled = false;
            }
            if (checkBox2.CheckState.Equals(CheckState.Checked))
            {
                comboBox_Filter.Enabled = true;
            }
        }

        private void timerPrint_Tick(object sender, EventArgs e)
        {
            if (DataStruct.ReadyToPrint)
            {
                OutPutFile();
                timerPrint.Stop();
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 偏心井径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_WellDiameter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_WellDiameter.CheckState.Equals(CheckState.Unchecked))
            {
                Gm.UseWellDiameter = false;
                numericUpDown_WellDiameter.Enabled = false;
            }
            else if (checkBox_WellDiameter.CheckState.Equals(CheckState.Checked))
            {
                Gm.UseWellDiameter = true;
                numericUpDown_WellDiameter.Enabled = true;
            }
        }
        /// <summary>
        /// 钻铤尺寸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_DrillPipeSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox_DrillPipeSize.SelectedIndex)
            {
                case 0: Gm.DrillPipeSize = PipeSize.inch475;
                    break;
                case 1: Gm.DrillPipeSize = PipeSize.inch675;
                    break;
                default: Gm.DrillPipeSize = PipeSize.inch475;
                    break;
            }
        }
        /// <summary>
        /// 是否含重晶石
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_BariteContainment_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_BariteContainment.SelectedIndex)
            {
                case 0: Gm.BariteContainment = true;
                    break;
                case 1: Gm.BariteContainment = false;
                    break;
                default: Gm.BariteContainment = true;
                    break;
            }
        }

        private void numericUpDown_PipeWallSize_ValueChanged(object sender, EventArgs e)
        {
            Gm.PipeWallSize = (double)this.numericUpDown_PipeWallSize.Value;
        }

        private void numericUpDown_CircleInterval_ValueChanged(object sender, EventArgs e)
        {
            Gm.CircleInterval = (double)this.numericUpDown_CircleInterval.Value;
        }

        private void numericUpDown_MudDensity_ValueChanged(object sender, EventArgs e)
        {
            Gm.MudDensity = (double)this.numericUpDown_MudDensity.Value;
        }

        private void numericUpDown_WellDiameter_ValueChanged(object sender, EventArgs e)
        {
            Gm.WellDiameter = (double)this.numericUpDown_WellDiameter.Value;
        }
    }
}