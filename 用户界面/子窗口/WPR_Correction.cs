using System;
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
        #region 全局变量
        Boolean save2root = false;
        String outputPath = "";
        FileStream fsOutPut;
        StreamWriter swOutPut;
        private String[] EmptyString8 = new String[8];
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
        /// 当前OpenFileDialog_WPR 的记录行
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

        /// <summary>
        /// 原始数据的数据集
        /// </summary>
        private DataSet rawDataSet = new DataSet();
        /// <summary>
        /// 校后数据的数据集
        /// </summary>
        private DataSet corDataSet = new DataSet();
        /// <summary>
        /// 原始数据 数据表单
        /// </summary>
        private DataTable rawDataTable = new DataTable();
        /// <summary>
        /// 校后数据 数据表单
        /// </summary>
        private DataTable corDataTable = new DataTable();
        /// <summary>
        /// 校正线程
        /// </summary>
        Thread CorrectionThread { get; set; }
        /// <summary>
        /// 输出到井数据库
        /// </summary>
        Thread WriteDBThread { get; set; }
        /// <summary>
        /// 校正类型
        /// </summary>
        private WPR_CorMethod corMethod {get;set;}
        /// <summary>
        /// 校正参数
        /// </summary>
        public float Para
        {
            get
            {
                return para;
            }

            set
            {
                para = value;
            }
        }

        String[] borehole675 = { "8.000", "9.000", "10.000", "12.000", "14.000", "16.000", "18.000" };
        String[] borehole475 = { "5.625", "6.000", "6.500", "7.000", "8.000", "10.000", "12.000" };

        private float para = 0.0f;
        #endregion

        public WPR_Correction()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void WPR_Correction_Load(object sender, EventArgs e)
        {
            Config.GetConfig();
            Config.SaveConfig();
            //读取NodeSettings.xml配置
            Properties.Settings.Default.DB_Well_ConnectionString = "Data Source=" + Properties.Settings.Default.DBPath_WellInfo;
            Properties.Settings.Default.DB_Chart_ConnectionString = "Data Source=" +Properties.Settings.Default.DBPath_ChartInfo;
            numericUpDown_SBR.Value = Properties.Settings.Default.SBR;
            numericUpDown_BedThickness.Value = Properties.Settings.Default.Tb;
            WellHelper = new SQLiteDBHelper(Properties.Settings.Default.DBPath_WellInfo);//XML的节点赋值
            ChartHelper = new SQLiteDBHelper(Properties.Settings.Default.DBPath_ChartInfo);//XML的节点赋值
            WPR._wpr.DBHelper = new SQLiteDBHelper(Properties.Settings.Default.DBPath_ChartInfo);//XML的节点赋值
            CorrectionThread = new Thread(new ThreadStart(Correct));
            WriteDBThread = new Thread(new ThreadStart(WriteDB));
            tabControl2.SelectedTab = tabPage3;
            //初始化井眼尺寸、仪器尺寸
            comboBox_ToolSize.Text = "6.75";
            WPR._wpr.ToolSize = "6.75";
            comboBox_BoreHole.Items.AddRange(borehole675);
            comboBox_BoreHole.SelectedIndex = 0;
            button_Load.Enabled = false;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SelectFolder_Click(object sender, EventArgs e)
        {
            openFileDialog_WPR.Filter = "文本文件(*.txt)|*.txt|测量值文件(*.tmf)|*.tmf|所有文件(*.*)|*.*";
            if (openFileDialog_WPR.ShowDialog(this.Owner) == DialogResult.OK)
            {
                FileName_WellData = openFileDialog_WPR.FileName;
                textBox_Folder.Text = FileName_WellData;
                Properties.Settings.Default.RawFile = textBox_Folder.Text.Trim();
                button_Load.Enabled = true;
                String[] str = openFileDialog_WPR.FileName.Split(openFileDialog_WPR.SafeFileNames, StringSplitOptions.RemoveEmptyEntries);
                String FilePath = str[0];
                openFileDialog_WPR.InitialDirectory = FilePath;
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
                CreateIndexs();//生成索引集合
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
                if (curTableName.Contains("A")|| curTableName.Contains("a"))
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
        ///线程托管
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
        /// <summary>
        /// 打开图版导出文件
        /// </summary>
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
                if (Funcs.IsParameterExpression(curLine[curLine.Length-1]))//参数-表达式
                {
                    CommonData.ChartParaExpression= curLine[curLine.Length - 1];
                    CommonData.getParaValue(curLine[curLine.Length - 1]);//分解表达式
                }
                if (Funcs.IsScienceNumber(curLine[0])&& Funcs.IsScienceNumber(curLine[1]))//验证科学记数法-表达式
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
            groupBox1.Enabled = false;
            OpenRaw();
            SaveRawData();
            Fill_DGV(dataGridView1, rawDataTable);
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
                //提取原始数据：深度+8条曲线
                while ((Lines_Data = sr.ReadLine()) != null)
                {
                    curRawLine = Lines_Data.Split(this.seperator, StringSplitOptions.RemoveEmptyEntries);
                    if (curRawLine.Length > 0)
                    {
                        RawSplitter(i);
                        i++;
                    }
                }
                //测量值永久化保存到List中
                WPR._wpr.QueueToArray();
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
            if (count == 0)
                RawCurveNames = curRawLine;//列名集合
            if (count > 0 && curRawLine.Length >= 9)
            {
                //1 深度,
                WPR._wpr.Queue_DEPTH.Enqueue(float.Parse(curRawLine[0]));
                //WPR._wpr.Queue_DEPTH.ToArray();
                //2 八条电阻率曲线. 暂存到Queue中
                get8ResCurve(curRawLine);

            }
        }
        /// <summary>
        /// 获得八条电阻率曲线
        /// </summary>
        private void get8ResCurve(String[] rawline)
        {
            for (int i = 1; i < 9; i++)
                switch (RawCurveNames[i])
                {
                    case "RACECHM":
                        WPR._wpr.Queue_RACECHM.Enqueue(float.Parse(rawline[i]));
                        break;
                    case "RACECLM":
                        WPR._wpr.Queue_RACECLM.Enqueue(float.Parse(rawline[i]));
                        break;
                    case "RACECSHM":
                        WPR._wpr.Queue_RACECSHM.Enqueue(float.Parse(rawline[i]));
                        break;
                    case "RACECSLM":
                        WPR._wpr.Queue_RACECSLM.Enqueue(float.Parse(rawline[i]));
                        break;
                    case "RPCECHM":
                        WPR._wpr.Queue_RPCECHM.Enqueue(float.Parse(rawline[i]));
                        break;
                    case "RPCECLM":
                        WPR._wpr.Queue_RPCECLM.Enqueue(float.Parse(rawline[i]));
                        break;
                    case "RPCECSHM":
                        WPR._wpr.Queue_RPCECSHM.Enqueue(float.Parse(rawline[i]));
                        break;
                    case "RPCECSLM":
                        WPR._wpr.Queue_RPCECSLM.Enqueue(float.Parse(rawline[i]));
                        break;
                    default: break;
                }
        }
        /// <summary>
        /// 将指定的原始数据DataTable存储到DataSet
        /// </summary>
        /// <returns></returns>
        private Boolean SaveRawData()
        {
            float[] temp = new float[9];
            object[] ot = new object[9];
            DataRow _MergeRow;
            try
            {
                InitDT(rawDataTable, RawCurveNames);
                //InitChartPrefix(RawCurveNames);
                //数据行
                while (WPR._wpr.Queue_DEPTH.Count > 0)
                {
                    temp = FillRawTableRow();
                    if (temp != null)
                    {
                        _MergeRow = rawDataTable.NewRow();
                        for (int i = 0; i < 9; i++)
                            ot[i] = (object)temp[i];
                        _MergeRow.ItemArray = ot;
                        rawDataTable.Rows.Add(_MergeRow);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        //private void InitChartPrefix(string[] rawCurveNames)
        //{

        //}

        /// <summary>
        /// 初始化DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnNames"></param>
        private void InitDT(DataTable dt, String[] columnNames)
        {
            for (int i = 0; i < columnNames.Length; i++)
            {
                DataColumn _dataColumns = new DataColumn();
                _dataColumns.DataType = System.Type.GetType("System.String");
                _dataColumns.ColumnName = columnNames[i];
                _dataColumns.AutoIncrement = false;
                _dataColumns.Caption = columnNames[i];
                _dataColumns.ReadOnly = true;
                _dataColumns.Unique = false;
                dt.Columns.Add(_dataColumns);
            }
        }

        /// <summary>
        /// 填充 rawDataTable的DataRow
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private float[] FillRawTableRow()
        {
            float[] temp = new float[9];
            try
            {
                WPR._wpr.Queue_DEPTH.TryDequeue(out temp[0]);
                WPR._wpr.Queue_RACECHM.TryDequeue(out temp[1]);
                WPR._wpr.Queue_RACECLM.TryDequeue(out temp[2]);
                WPR._wpr.Queue_RACECSHM.TryDequeue(out temp[3]);
                WPR._wpr.Queue_RACECSLM.TryDequeue(out temp[4]);
                WPR._wpr.Queue_RPCECHM.TryDequeue(out temp[5]);
                WPR._wpr.Queue_RPCECLM.TryDequeue(out temp[6]);
                WPR._wpr.Queue_RPCECSHM.TryDequeue(out temp[7]);
                WPR._wpr.Queue_RPCECSLM.TryDequeue(out temp[8]);
                return temp;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 用DataTable填充DGV
        /// </summary>
        /// <param name="DGV">DataGridView</param>
        /// <param name="DT">DataTable</param>
        private void Fill_DGV(DataGridView DGV, DataTable DT)
        {
            try
            {
                if (DT != null && DT.Rows.Count > 0)
                {
                    DGV.DataSource = DT;
                    DGV.AutoGenerateColumns = false;
                    for (int i = 0; i < DGV.Columns.Count; i++)
                    {
                        DGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    DGV.Refresh();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 树形结构

        #endregion
        #region 井眼校正
        /// <summary>
        /// 加入参数文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_LoadBorehole_Click(object sender, EventArgs e)
        {

        }

        private void comboBox_BoreHole_SelectedIndexChanged(object sender, EventArgs e)
        {
            WPR._wpr.HD = float.Parse(comboBox_BoreHole.Text.Trim());
            Config.CfgInfo.WPR_Borehole = WPR._wpr.HD;
            Properties.Settings.Default.WPR_Borehole= WPR._wpr.HD.ToString("F3");
        }

        private void numericUpDown_MudResistivity_ValueChanged(object sender, EventArgs e)
        {
            WPR._wpr.Rm = float.Parse(numericUpDown_MudResistivity.Value.ToString());
            Config.CfgInfo.WPR_MudRes = WPR._wpr.Rm;
            Properties.Settings.Default.WPR_MudRes= (decimal)WPR._wpr.Rm;
        }
        #endregion
        #region 介电常数
        #endregion
        #region 围岩校正
        private void radioButton_ShoulderBedPara_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_ShoulderBedPara.Checked)
            {
                numericUpDown_SBR.Enabled = true;
                numericUpDown_BedThickness.Enabled = true;
                button_ShoulderBedFile.Enabled = false;
            }
            //else if (!radioButton_ShoulderBedPara.Checked)
            //{
            //    numericUpDown_SBR.Enabled = false;
            //    numericUpDown_BedThickness.Enabled = false;
            //    button_ShoulderBedFile.Enabled = true;
            //}
        }

        private void radioButton_ShoulderBedFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_ShoulderBedPara.Checked)
            {
                numericUpDown_SBR.Enabled = false;
                numericUpDown_BedThickness.Enabled = false;
                button_ShoulderBedFile.Enabled = true;
            }
            //else if (!radioButton_ShoulderBedPara.Checked)
            //{
            //    numericUpDown_SBR.Enabled = true;
            //    numericUpDown_BedThickness.Enabled = true;
            //    button_ShoulderBedFile.Enabled = false;
            //}
        }

        private void numericUpDown_SBR_ValueChanged(object sender, EventArgs e)
        {
            WPR._wpr.SBR = float.Parse(numericUpDown_SBR.Value.ToString());
            Config.CfgInfo.WPR_SBR = WPR._wpr.SBR;
            Properties.Settings.Default.SBR = (decimal)Config.CfgInfo.WPR_SBR;
        }

        private void numericUpDown_BedThickness_ValueChanged(object sender, EventArgs e)
        {
            WPR._wpr.Tb = (float)numericUpDown_BedThickness.Value;
            Para = WPR._wpr.Tb;
            Config.CfgInfo.WPR_Tb = WPR._wpr.Tb;
            Properties.Settings.Default.Tb = (decimal)Config.CfgInfo.WPR_Tb;
        }

        #endregion
        #region 侵入校正
        #endregion
        #region 各向异性
        #endregion


        #region 校正线程
        void Correct()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
        #region 控件定义
        private void radioButton_Borehole1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Borehole1.Checked)
                comboBox_BoreHole.Enabled = true;
            else comboBox_BoreHole.Enabled = false;
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
        /// <summary>
        /// 选择仪器尺寸，确定井眼尺寸范围。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_ToolSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_ToolSize.SelectedItem.ToString() == "6.75")
            {
                comboBox_BoreHole.Items.Clear();
                comboBox_BoreHole.Items.AddRange(borehole675);
                comboBox_BoreHole.SelectedIndex = 0;
            }
            else if (comboBox_ToolSize.SelectedItem.ToString() == "4.75")
            {
                comboBox_BoreHole.Items.Clear();
                comboBox_BoreHole.Items.AddRange(borehole475);
                comboBox_BoreHole.SelectedIndex = 0;
            }
            ToolSize = comboBox_ToolSize.SelectedItem.ToString().Trim();
            Properties.Settings.Default.ToolSize = ToolSize;
            WPR._wpr.ToolSize = comboBox_ToolSize.SelectedItem.ToString().Trim();
        }

        /// <summary>
        /// 校正操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Correct_Click(object sender, EventArgs e)
        {
            Config.SaveConfig();
            SelectCorMethod();
            WPR._wpr.MatchChart(RawCurveNames);
            WPR._wpr.CorrectFlow(WPR._wpr.CorMethod,Para);
            DataTable dt_ac = new DataTable();
            dt_ac = WPR._wpr.getCorDataTable();
            corDataTable = dt_ac;
            if (dt_ac != null)
                Fill_DGV(dataGridView2, dt_ac);
            PrePrint();
            EndPrint();
        }
        /// <summary>
        /// 选择校正类型
        /// </summary>
        private void SelectCorMethod()
        {
            switch (this.corMethod)
            {
                case WPR_CorMethod.Dieletric:
                    WPR._wpr.CorMethod = "介电常数";
                    break;
                case WPR_CorMethod.HoleDiameter:
                    WPR._wpr.CorMethod = "井眼校正";
                    break;
                case WPR_CorMethod.ShoulderBed:
                    WPR._wpr.CorMethod = "围岩校正";
                    break;
                case WPR_CorMethod.Invasion:
                    WPR._wpr.CorMethod = "侵入校正";
                    break;
                case WPR_CorMethod.Antistropy:
                    WPR._wpr.CorMethod = "各向异性";
                    break;
                default:
                    WPR._wpr.CorMethod = tabControl2.SelectedTab.Text;
                    break;
            }
        }
        /// <summary>
        /// 输出校正结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Output_Click(object sender, EventArgs e)
        {

            try
            {
                SQLiteConnection conn = WellHelper.DbConnection;
                if (!WellHelper.IsExistTable(WellName))//创建与井名相同的校后数据表
                {
                    WellHelper.Create_WellTable(conn, WellName);
                    WellHelper.Create_WellTable(conn, WellName+"_AC");
                }
                FillWellInfo();
                WriteDB();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "----输出线程异常！");
            }
        }

        /// <summary>
        /// 校正前后数据写入井信息数据库
        /// </summary>
        private void WriteDB()
        {
            SQLiteConnection conn = WellHelper.DbConnection;
            SQLiteTransaction tran1 = conn.BeginTransaction();
            SQLiteCommand cmd1 = new SQLiteCommand(conn);
            cmd1.Transaction = tran1;
            try
            {
                WellHelper.Open();
                //校正后数据
                for (int i = 0; i < corDataTable.Rows.Count; i++)
                {
                    //设置带参数的Transact-SQL语句
                    cmd1.CommandText = "insert into [" + WellName + "] values(@ID,@Time, @Depth, @RACECHM_AC,@RACECLM_AC,@RACECSHM_AC,@RACECLSM_AC,@RPCECHM_AC,@PRCECLM_AC,@RPCECSHM_AC,@RPCECLSM_AC)";
                    cmd1.Parameters.AddRange(new[]
                    {
                        new SQLiteParameter("@ID",i),
                        new SQLiteParameter("@Time",DateTime.Now.ToLongDateString()),
                        new SQLiteParameter("@Depth",WPR._wpr.List_DEPTH[i].ToString("F3")),
                        new SQLiteParameter("@RACECHM_AC",WPR._wpr.List_RACECHM_AC[i].ToString("F3")),
                        new SQLiteParameter("@RACECLM_AC",WPR._wpr.List_RACECLM_AC[i].ToString("F3")),
                        new SQLiteParameter("@RACECSHM_AC",WPR._wpr.List_RACECSHM_AC[i].ToString("F3")),
                        new SQLiteParameter("@RACECLSM_AC",WPR._wpr.List_RACECSLM_AC[i].ToString("F3")),
                        new SQLiteParameter("@RPCECHM_AC",WPR._wpr.List_RPCECHM_AC[i].ToString("F3")),
                        new SQLiteParameter("@PRCECLM_AC",WPR._wpr.List_RPCECLM_AC[i].ToString("F3")),
                        new SQLiteParameter("@RPCECSHM_AC",WPR._wpr.List_RPCECSHM_AC[i].ToString("F3")),
                        new SQLiteParameter("@RPCECLSM_AC",WPR._wpr.List_RPCECSLM_AC[i].ToString("F3"))
                    });
                    cmd1.ExecuteNonQuery();
                }
                tran1.Commit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "----WriteDB线程 tran1 事务异常！");
                tran1.Rollback();
            }
            SQLiteTransaction tran2 = conn.BeginTransaction();
            SQLiteCommand cmd2 = new SQLiteCommand(conn);
            cmd2.Transaction = tran2;
            try
            {
            //原始数据
            for (int i = 0; i < rawDataTable.Rows.Count; i++)
                {
                    //设置带参数的Transact-SQL语句
                    cmd2.CommandText = "insert into [" + WellName + "_AC" + "] values(@ID,@Time, @Depth, @RACECHM_AC,@RACECLM_AC,@RACECSHM_AC,@RACECLSM_AC,@RPCECHM_AC,@PRCECLM_AC,@RPCECSHM_AC,@RPCECLSM_AC)";
                    cmd2.Parameters.AddRange(new[]
                    {
                        new SQLiteParameter("@ID",i),
                        new SQLiteParameter("@Time",DateTime.Now.ToLongDateString()),
                        new SQLiteParameter("@Depth",WPR._wpr.List_DEPTH[i].ToString("F3")),
                        new SQLiteParameter("@RACECHM_AC",WPR._wpr.List_RACECHM[i].ToString("F3")),
                        new SQLiteParameter("@RACECLM_AC",WPR._wpr.List_RACECLM[i].ToString("F3")),
                        new SQLiteParameter("@RACECSHM_AC",WPR._wpr.List_RACECSHM[i].ToString("F3")),
                        new SQLiteParameter("@RACECLSM_AC",WPR._wpr.List_RACECSLM[i].ToString("F3")),
                        new SQLiteParameter("@RPCECHM_AC",WPR._wpr.List_RPCECHM[i].ToString("F3")),
                        new SQLiteParameter("@PRCECLM_AC",WPR._wpr.List_RPCECLM[i].ToString("F3")),
                        new SQLiteParameter("@RPCECSHM_AC",WPR._wpr.List_RPCECSHM[i].ToString("F3")),
                        new SQLiteParameter("@RPCECLSM_AC",WPR._wpr.List_RPCECSLM[i].ToString("F3"))
                    });
                    cmd2.ExecuteNonQuery();
                }
                tran2.Commit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "----WriteDB线程 tran2 事务异常！");
                tran2.Rollback();
            }
        }
        /// <summary>
        /// 填充WellInfo表
        /// </summary>
        public void FillWellInfo()
        {
            SQLiteConnection conn = WellHelper.DbConnection;
            SQLiteTransaction tran = conn.BeginTransaction();//实例化事务  
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.Transaction = tran;
            try
            {
                WellHelper.Open();
                cmd.CommandText = "insert into [WellInfo] values(@WellName,@RawData,@CorrectionData,@ToolSize,@Remark)";
                cmd.Parameters.AddRange(new[] {//添加参数
                    new SQLiteParameter("@WellName",WellName),
                    new SQLiteParameter("@RawData",WellName),
                    new SQLiteParameter("@CorrectionData",WellName+"校正后"),
                    new SQLiteParameter("@ToolSize",ToolSize),
                    new SQLiteParameter("@Remark","")
                });
                cmd.ExecuteNonQuery();//执行插入
                tran.Commit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message+"填充井信息异常！FillWellInfo error!");
                tran.Rollback();//事务回滚
            }
        }

        /// <summary>
        /// 给WPR类的校正类型赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            //选择井眼校正
            if (tabControl2.SelectedTab == tabPage3)
            {
                corMethod = WPR_CorMethod.HoleDiameter;//井眼
                Para = float.Parse(comboBox_BoreHole.Text.Trim());
            }
            if (tabControl2.SelectedTab == tabPage4)
                corMethod = WPR_CorMethod.Dieletric;//介电
            if (tabControl2.SelectedTab == tabPage5)
            {
                corMethod = WPR_CorMethod.ShoulderBed;//围岩
                Para = (float)numericUpDown_BedThickness.Value;
            }

            if (tabControl2.SelectedTab == tabPage6)
                corMethod = WPR_CorMethod.Invasion;//侵入
            if (tabControl2.SelectedTab == tabPage7)
                corMethod = WPR_CorMethod.Antistropy;//各向异性
            WPR._wpr.CorMethod = tabControl2.SelectedTab.Text;
        }
        #endregion

        private void WPR_Correction_FormClosed(object sender, FormClosedEventArgs e)
        {
            Config.SaveConfig();
        }
        #region 
        /// <summary>
        /// 打印文件头
        /// </summary>
        private void PrePrint()
        {
            try
            {
                if (save2root)
                    outputPath = openFileDialog_WPR.InitialDirectory + WellName + ".txt";
                else
                    outputPath = openFileDialog_WPR.InitialDirectory + WellName + ".txt";
                fsOutPut = new FileStream(outputPath, FileMode.Append, FileAccess.Write);
                swOutPut = new StreamWriter(fsOutPut);
                for (int i = 0; i < RawCurveNames.Length; i++)
                    swOutPut.Write(RawCurveNames[i] + "_AC\t");
                swOutPut.WriteLine() ;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }

        private void EndPrint()
        {
            try
            {
                while (true)
                {
                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView2.Columns.Count; j++)
                        {
                            swOutPut.Write(float.Parse(dataGridView2.Rows[i].Cells[j].Value.ToString()).ToString("F3") + "\t");
                        }
                        swOutPut.Write("\r\n");
                    }
                    break;
                }
                Application.DoEvents();
                swOutPut.Flush();
                fsOutPut.Flush();
                swOutPut.Close();
                fsOutPut.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        private void checkBox_Save2Root_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Save2Root.Checked)
                save2root = true;
            if (!checkBox_Save2Root.Checked)
                save2root = false;
        }
    }
}