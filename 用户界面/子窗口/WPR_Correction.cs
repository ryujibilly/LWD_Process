﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using System.Collections.Concurrent;

namespace LWD_DataProcess
{
    public partial class WPR_Correction : Form
    {
        private String[] EmptyString6 = new String[6];
        /// <summary>
        /// 静态SQLiteConnectionPool接口实例
        /// </summary>
        public static ISQLiteConnectionPool SQLConPool { get; set; }
        /// <summary>
        /// 断词符号集
        /// </summary>
        public readonly String[] seperator = { "\t", " ", "  ", "      ", "       ", "\\", "/", "_" };
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
        /// OpenChart()的ReadLine断句数组
        /// </summary>
        private String[] curLine { get; set; }
        /// <summary>
        /// OpenRaw() 的ReadLine断句数组
        /// </summary>
        private String[] curRawLine { get; set; }
        /// <summary>
        /// 原始数据曲线名称集合
        /// </summary>

        private String[] RawCurveNames { get; set; }
        /// <summary>
        /// 当前ChartData的TableName
        /// </summary>
        private String curTableName { get; set;}
        /// <summary>
        /// 缺省索引集合
        /// </summary>
        private String[] DefaultIndexs = {"4.75","400K","36","1","0","介电常数","",null,null};
        /// <summary>
        /// 当前索引集合
        /// </summary>
        private String[] ChartIndexs { get; set; }
        /// <summary>
        /// 图版文件行数
        /// </summary>
        private int ChartLength { get; set; }
        /// <summary>
        /// 井名
        /// </summary>
        public String WellName { get; set; }
        /// <summary>
        /// 钻铤尺寸
        /// </summary>
        public String ToolSize { get; set; }

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
            Properties.Settings.Default.DB_Chart_ConnectionString = "Data Source=" +Properties.Settings.Default.DBPath_ChartInfo;
            WellHelper = new SQLiteDBHelper(Properties.Settings.Default.DBPath_WellInfo);//XML的节点赋值
            ChartHelper = new SQLiteDBHelper(Properties.Settings.Default.DBPath_ChartInfo);//XML的节点赋值
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SelectFolder_Click(object sender, EventArgs e)
        {
            openFileDialog_WPR.Filter = "测量值文件(*.tmf)|*.tmf|文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog_WPR.ShowDialog(this.Owner) == DialogResult.OK)
            {
                FileName_WellData = openFileDialog_WPR.FileName;
                textBox_Folder.Text = FileName_WellData;
                Properties.Settings.Default.RawFile = textBox_Folder.Text.Trim();
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
                ChartIndexs = DefaultIndexs;//清空索引集合
                Properties.Settings.Default.ChartFolderPath=openFile_BindChart.FileName;//XML的节点赋值
                openFile_BindChart.InitialDirectory = Properties.Settings.Default.ChartFolderPath;//默认路径赋值
                //当前绑定图版 ChartData的TableName
                curTableName = openFile_BindChart.SafeFileName.Trim(".txt".ToCharArray());
                //创建索引集合
                CreateIndexs();
                OpenChart();//打开图版文件
                Thread.Sleep(100);
                BindChart();
            }
        }
        /// <summary>
        /// 生成索引集合
        /// </summary>
        private void CreateIndexs()
        {
            try
            {
                curTableName.Split(this.seperator, StringSplitOptions.RemoveEmptyEntries).CopyTo(ChartIndexs, 0);
                //ParameterValue
                if (ChartIndexs[4] != "")
                    ChartIndexs[6] = ChartIndexs[4];
                //CorrectionMethod
                ChartIndexs[5] = ChartIndexs[3];
                //Distance
                if (curTableName.Contains("36"))
                {
                    ChartIndexs[2] = "36";
                }
                else ChartIndexs[2] = "22.5";
                //AmplitudeRatio\PhaseDifference
                if (curTableName.Contains("A"))
                {
                    ChartIndexs[3] = "1";
                    ChartIndexs[4] = "0";
                }
                else
                {
                    ChartIndexs[3] = "0";
                    ChartIndexs[4] = "1";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
            try
            {
                FileStream fs = new FileStream(openFile_BindChart.FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                //提取图版索引关键字
                while ((Lines_Chart = sr.ReadLine()) != null)
                {
                    curLine = Lines_Chart.Split(this.seperator, StringSplitOptions.RemoveEmptyEntries);
                    if (curLine.Length > 0)
                        txtSplitter();
                }
                ChartLength = CommonData.XValue.Count;

                fs.Flush();
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 图版导出txt文件的字符串截取流批处理操作
        /// </summary>
        private void txtSplitter()
        {
            try
            {
                if (Funcs.IsParameterExpression(curLine[0]))//参数-表达式
                {
                    CommonData.ChartParaExpression=curLine[0];
                    CommonData.getParaValue(curLine[0]);//分解表达式
                }
                if (Funcs.IsScienceNumber(curLine[0])&& Funcs.IsScienceNumber(curLine[1]))//科学记数法-表达式
                {
                    CommonData.ChartPara.Enqueue(CommonData.curPara);//保证参数列和数据列等长
                    CommonData.ParaValue.Enqueue(CommonData.curValue);
                    CommonData.XValue.Enqueue(Funcs.convertScienceNumber(curLine[0]));//科学计数法转换浮点字符串
                    CommonData.YValue.Enqueue(Funcs.convertScienceNumber(curLine[1]));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 填充ChartInfo表
        /// </summary>
        /// <param name="indexs">索引集合</param>
        public void FillChartInfo()
        {
            SQLiteConnection conn = ChartHelper.DbConnection;
            SQLiteTransaction tran1 = conn.BeginTransaction();//实例化事务  
            SQLiteCommand cmd1 = new SQLiteCommand(conn);
            cmd1.Transaction = tran1;
            try
            {
                ChartHelper.Open();
                cmd1.CommandText= "insert into ChartInfo values(@ChartName,@ParameterName,@ToolSize,@Frequency,@Distance,@AmplitudeRatio,@PhaseDifference,@CorrectionMethod,@Spare1,@Spare2)";
                cmd1.Parameters.AddRange(new[] {//添加参数
                    new SQLiteParameter("@ChartName",curTableName),
                    new SQLiteParameter("@ParameterName",ChartIndexs[6]),
                    new SQLiteParameter("@ToolSize",ChartIndexs[0]),
                    new SQLiteParameter("@Frequency",ChartIndexs[1]),
                    new SQLiteParameter("@Distance",ChartIndexs[2]),
                    new SQLiteParameter("@AmplitudeRatio",ChartIndexs[3]),
                    new SQLiteParameter("@PhaseDifference",ChartIndexs[4]),
                    new SQLiteParameter("@CorrectionMethod",ChartIndexs[5]),
                    new SQLiteParameter("@Spare1",null),
                    new SQLiteParameter("@Spare2", null)
                });
                cmd1.ExecuteNonQuery();//执行插入
                tran1.Commit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("该图版已关联！");
                tran1.Rollback();//事务回滚
            }
        }

        /// <summary>
        /// 图版绑定:1.建立索引=>2.导入数据库
        /// </summary>
        private void BindChart()
        {
            FillChartInfo();
            SQLiteConnection conn= ChartHelper.DbConnection;
            if (!ChartHelper.IsExistTable(curTableName))//创建与图版同名的图版表
                ChartHelper.Create_ChartTable(conn, curTableName);
            SQLiteTransaction tran2 = conn.BeginTransaction();//实例化事务  
            SQLiteCommand cmd2 = new SQLiteCommand(conn);//实例化SQL命令
            cmd2.Transaction = tran2;
            try
            {
                ChartHelper.Open();
                for (int i = 0; i <ChartLength; i++)//
                {
                    //设置带参SQL语句 
                    cmd2.CommandText = "insert into ["+curTableName+"] values(@ID,@ParameterName, @ParameterValue, @XValue,@YValue)";
                    cmd2.Parameters.AddRange(new[] {//添加参数
                        new SQLiteParameter("@ID",i),
                        new SQLiteParameter("@ParameterName",CommonData._CD.Dequeue_ChartPara()),
                        new SQLiteParameter("@ParameterValue", Double.Parse(CommonData._CD.Dequeue_ParaValue())),
                        new SQLiteParameter("@XValue", Double.Parse(CommonData._CD.Dequeue_XValue())),
                        new SQLiteParameter("@YValue",Double.Parse(CommonData._CD.Dequeue_YValue()))
                    });
                    cmd2.ExecuteNonQuery();//执行插入
                }
                tran2.Commit();//提交
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                tran2.Rollback();//事务回滚
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

        #endregion


        #region 载入原始数据
        /// <summary>
        /// 加载原始数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Load_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RawFile = textBox_Folder.Text.Trim();
            comboBox_ToolSize.Enabled = false;
            textBox_WellName.Enabled = false;
            textBox_Folder.Enabled = false;
            OpenRaw();
        }
        /// <summary>
        /// 打开原始数据，线程托管
        /// </summary>
        private void OpenRaw()
        {
            if (this.InvokeRequired)
            {
                OpenDelegate openRaw = new OpenDelegate(OpenRaw);
                this.BeginInvoke(openRaw);
            }
            else
            {
                openRaw();
            }
        }

        void openRaw()
        {
            try
            {
                FileStream fs = new FileStream(openFileDialog_WPR.FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                int i = 0;
                //提取图版索引关键字
                while ((Lines_Data = sr.ReadLine()) != null)
                {
                    curRawLine = Lines_Data.Split(new String[]{ "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (curRawLine.Length > 0)
                        RawSplitter(i++);
                }
                fs.Flush();
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 原始数据字符串截取流处理
        /// </summary>
        private void RawSplitter(int count)
        {
            if (count == 1)
                RawCurveNames = curRawLine;//列名集合
            if(count>1&&curRawLine.Length>9)
            {

                //1时间，2深度，3八条电阻率曲线
                String[] temp = EmptyString6;
                //日期时间（年月日/时分秒）
                temp = curRawLine[0].Split(new String[] { "-",":", " " }, StringSplitOptions.RemoveEmptyEntries);
                CommonData.Date_Time.Enqueue(temp[0] + temp[1] + temp[2] + "\\" + temp[3] + temp[4] + temp[5]);
                //深度
                CommonData.Depth.Enqueue(curRawLine[1]);
                //8条电阻率曲线
                get8ResCurve();
            }
        }
        /// <summary>
        /// 获得八条电阻率曲线
        /// </summary>
        void get8ResCurve()
        {

        }

        /// <summary>
        /// 存储原始数据
        /// </summary>
        /// <returns></returns>
        private Boolean SaveRawData()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

                    
        #endregion

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

        private void textBox_WellName_TextChanged(object sender, EventArgs e)
        {
            WellName = textBox_WellName.Text.Trim();
        }

        private void comboBox_ToolSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolSize = comboBox_ToolSize.Text.Trim();
            Properties.Settings.Default.ToolSize = ToolSize;
        }
        #endregion
    }
}
