using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Data.SQLite;

namespace LWD_DataProcess
{
    public partial class  GammaCorrection : Form
    {
        public static readonly String[] seperator
            = { "\t", " ", "  ", "      ", "       ", "\\", "/","_"};
        Thread CorrectionThread { get; set; }
        Thread PrintThread { get; set; }
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable dt = new DataTable("表_dt");
        DataTable dt_Merge = new DataTable("表_dt_Merge");
        DataTable dtAC = new DataTable("表_dtAC");
        DataColumn DepColumn = new DataColumn("Columns_深度列");
        /// <summary>
        /// 数据集
        /// </summary>
        DataSet ds = new DataSet();
        /// <summary>
        /// 井名
        /// </summary>
        String WellName { get; set; }
        /// <summary>
        /// 校正线程是否运行
        /// </summary>
        Boolean threadOn { get; set; }
        #region 数据加载参数
        /// <summary>
        /// 选择打开的文件名
        /// </summary>
        String FileName { get; set; }
        /// <summary>
        /// API/CPS计数率方式
        /// </summary>
        int ParaType { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //String[] row { get; set; }
        /// <summary>
        /// Temp StreamReader Rows
        /// </summary>
        static String[] NRow { get; set; }
        /// <summary>
        /// 当前记录行
        /// </summary>
        String Lines { get; set; }
        /// <summary>
        /// 需要校正的参数索引
        /// </summary>
        int SN { get; set; }
        /// <summary>
        /// 文件行长度
        /// </summary>
        int Num { get; set; }
        /// <summary>
        /// 数据行的长度
        /// </summary>
        int RecordNum { get; set; }
        /// <summary>
        /// 校正前的原始数据列
        /// </summary>
        static String[] RawData { get; set; }
        /// <summary>
        /// 原始数据队列
        /// </summary>
        static ConcurrentQueue<String> RawDatas { get; set; }
        /// <summary>
        /// 原始数据的字符串数组队列       
        /// </summary>
        static ConcurrentQueue<String[]> OriQueue { get; set; }
        /// <summary>
        /// 校正合并后的字符串数组队列
        /// </summary>
        static ConcurrentQueue<String[]> ACQueue { get; set; }
        /// <summary>
        /// 校正后的数据队列
        /// </summary>
        static ConcurrentQueue<String> OutData { get; set; }
        ///// <summary>
        ///// 校正值的控件列
        ///// </summary>
        //private DataColumn GammaColumns { get; set; }
        /// <summary>
        /// 校正值的DataTable列
        /// </summary>
        private DataColumn _GammaColumns { get; set; }
        /// <summary>
        /// 数据行
        /// </summary>
        private DataRow _ParaRow { get; set; }
        public DataRow _MergeRow { get; set; }
        /// <summary>
        /// 列的个数
        /// </summary>
        private int columnsNumber { get; set; }

        FileStream fsOutPut;
        StreamWriter swOutPut;
        String outputPath;

        #region 环境校正参数设置
        /// <summary>
        /// 是否使用源目录
        /// </summary>
        Boolean saveSource { get; set; }
        /// <summary>
        /// 伽马计数率单位 API/CPS
        /// </summary>
        GammaUnit gU { get; set; }
        /// <summary>
        /// 校正起始深度
        /// </summary>
        float StartDepth { get; set; }
        /// <summary>
        /// 校正终止深度
        /// </summary>
        float EndDepth { get; set; }
        /// <summary>
        /// 校正后曲线名称
        /// </summary>
        String Curve_AC { get; set; }
        #endregion

        private delegate void OpenFileDelegate();
        
        /// <summary>
        /// 打开文件，线程托管
        /// </summary>
        private void OpenFile()
        {
            if(this.InvokeRequired)
            {
                OpenFileDelegate openFile = new OpenFileDelegate(OpenFile);
                this.BeginInvoke(openFile);
            }
            else
            {
                openFile();
            }
        }
        void openFile()
        {
            int k = 0;
            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            while ((Lines = sr.ReadLine()) != null)
            {
                NRow = (k.ToString() + "/" + Lines).Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                //该行第二列为非数值，则该行为表头行
                if (NRow.Length>1&&!IsNumStr(NRow[1]))
                {
                    //创建ID列
                    for (int i = 0; i < NRow.Length; i++)
                    {
                        DataColumn tempColumns = new DataColumn();
                        if (i == 0)
                            tempColumns.ColumnName = "ID";
                        else if (i > 0)
                            tempColumns.ColumnName = NRow[i];
                        comboBox1.Items.Add(tempColumns.ColumnName);
                    }
                    k++;
                    //初始化非主键列DataColumn,并添加到DataTable中
                    for (int j = 0; j < comboBox1.Items.Count; j++)
                    {
                            DataColumn _dataColumns = new DataColumn();
                            _dataColumns.DataType = System.Type.GetType("System.String");
                            _dataColumns.ColumnName = comboBox1.Items[j].ToString();
                            _dataColumns.AutoIncrement = false;
                            _dataColumns.Caption = comboBox1.Items[j].ToString();
                            _dataColumns.ReadOnly = true;
                            _dataColumns.Unique = false;
                            dt.Columns.Add(_dataColumns);
                    }
                    //设置ID列为主键列
                    DataColumn[] PrimaryKeyColumns = new DataColumn[1];
                    PrimaryKeyColumns[0] = dt.Columns["ID"];
                    dt.PrimaryKey = PrimaryKeyColumns;
                    ds.Tables.Add(dt); 
                }
                //该行长度为2，则该行为统计数据长度数值。
                else
                    if (NRow.Length > 2)  //填充数据行
                    {
                        _ParaRow = dt.NewRow();
                        _ParaRow.ItemArray = NRow;
                        dt.Rows.Add(NRow);
                        OriQueue.Enqueue(NRow);
                        k++;
                    }
                RecordNum = k;
            }
            fs.Flush();
            sr.Close();
            fs.Close();
            Fill_DGV(dataGridView1, dt);//填充数值并显示
        }
        #endregion

        public GammaCorrection()
        {
            InitializeComponent();
            RawDatas = new ConcurrentQueue<string>();
            OutData = new ConcurrentQueue<string>();
            OriQueue = new ConcurrentQueue<string[]>();
            ACQueue = new ConcurrentQueue<string[]>();
            CorrectionThread = new Thread(new ThreadStart(Correction));
            PrintThread = new Thread(new ThreadStart(EndPrint));
            Gamma._Gamma.gu = GammaUnit.API;
            button_Load.Enabled = false;
            checkBox_Collar.CheckState = CheckState.Unchecked;
            checkBox_Annulus.CheckState = CheckState.Unchecked;
        }
        private void GammaCorrection_Load(object sender, EventArgs e)
        {
            Gamma._Gamma.DrillPipeSize = PipeSize.inch475;
            Gamma._Gamma.BariteContainment = true;
            Gamma._Gamma.MudDensity = (double)numericUpDown_MudDensity.Value;
            if (checkBox_Collar.CheckState.Equals(CheckState.Unchecked))
            {
                numericUpDown_PipeWallSize.Enabled = false;
                Gamma._Gamma.Factor_pws = 1;
            }
            if (checkBox_Annulus.CheckState.Equals(CheckState.Unchecked))
            {
                numericUpDown_CircleInterval.Enabled = false;
                Gamma._Gamma.Factor_ci = 1;
            }
            Gamma._Gamma.MudDensity = (double)numericUpDown_MudDensity.Value;
        }
        /// <summary>
        /// 载入原始数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Click(object sender, EventArgs e)
        {
            WellName = openFileDialog1.SafeFileName.Split('.')[0] + "_校正";
            textBox_WellName.Text= WellName;
            DataStruct.DB_PATH.Insert(DataStruct.DB_PATH.Length,'/'+WellName + ".db");
            OpenFile();
            Thread.Sleep(100);
            groupBox2.Enabled = true;
            threadOn = false;
        }
        /// <summary>
        /// 环境校正按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Correct_Click(object sender, EventArgs e)
        {
            Gamma._Gamma.getFactors();
            _GammaColumns = new DataColumn();
            Curve_AC = CurveName_AC.Text.Trim();
            try
            {
                columnsNumber = dataGridView1.Columns.Count;
                _GammaColumns.ColumnName = CurveName_AC.Text.Trim();
                _GammaColumns.AutoIncrement = false;
                threadOn = true;
                //渲染 Gamma校正列的数据
                PrePrint();
                CorrectionThread.Start();
            }
            catch (Exception ex)
            {
                threadOn = false;
                Debug.WriteLine(ex.Message);
                //MessageBox.Show("请设置校正曲线参数！\r\n"+ex.Message);
            }
        }
         /// <summary>
        /// 校正线程
        /// </summary>
        void Correction()
        {
            try
            { 
                String result = null;
                RawData=RawDatas.ToArray();
                //ArrayList itemlist = new ArrayList();//合并后的列名集合
                //itemlist.AddRange(comboBox1.Items);
                //itemlist.Add(Curve_AC);
                //for (int i = 0; i < itemlist.Count;i++ )
                //    dataColumn_Init(itemlist[i].ToString(), dt_Merge);
                //dataRow_Init(_MergeRow, dt_Merge);
                while ((RawDatas.Count > 0) && (threadOn == true))
                {
                    if (RawDatas.TryDequeue(out result))
                        Gamma._Gamma.Start(double.Parse(result));//开始校正
                    OutData.Enqueue( Math.Round(Gamma._Gamma.count,3).ToString());
                    if (RawDatas.Count == 0)
                        break;
                }
                //合并后的数组
                String[] str=new String[comboBox1.Items.Count+1];
                //把原始数据和 校正后的伽马合并到 dt_Merge 中，通过OriQueue +OutQueue拼接
                String[] temp = null;
                String grac=null;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dataColumn_Init(comboBox1.Items[i].ToString(), dtAC);
                }
                dataColumn_Init(Curve_AC, dtAC);
                //dt.Rows.Clear();
                for (int k = 0; k < RecordNum-1; k++)
                {
                    _MergeRow = dtAC.NewRow();
                    if (OriQueue.Count > 0)
                    {
                        if (OriQueue.TryDequeue(out temp))
                            temp.CopyTo(str, 0);//原表数据
                    }
                    if (OutData.Count > 0)
                    {
                        if (OutData.TryDequeue(out grac))
                            str[comboBox1.Items.Count] = grac;//校正后gamma数据
                    }
                    _MergeRow.ItemArray = str;
                    dtAC.Rows.Add(_MergeRow);
                }
                Fill_DGV(dataGridView2, dtAC);
                PrintThread.Start();
            }
            catch (Exception)
            {
            }
        }



        #region 控件操作
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "所有文件(*.*)|*.*|测量值文件(*.tmf)|*.tmf|Las文件(*.las)|*.*";
            if (openFileDialog1.ShowDialog(this.Owner) == DialogResult.OK)
            {
                FileName = openFileDialog1.FileName;
                textBox1.Text = FileName;
                Num = GetRows(FileName);
                String[] str = openFileDialog1.FileName.Split(openFileDialog1.SafeFileNames,StringSplitOptions.RemoveEmptyEntries);
                String FilePath = str[0];
                openFileDialog1.InitialDirectory = FilePath;
            }
            button_Load.Enabled = true;
        }


        private void comboBox_DrillPipeSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_DrillPipeSize.SelectedIndex)
            {
                case 0: Gamma._Gamma.DrillPipeSize = PipeSize.inch475;
                    break;
                case 1: Gamma._Gamma.DrillPipeSize = PipeSize.inch675;
                    break;
                default: Gamma._Gamma.DrillPipeSize = PipeSize.inch475;
                    break;
            }
        }

        private void checkBox_Save2Source_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Save2Source.CheckState==CheckState.Checked)
                saveSource = true;
            else if(checkBox_Save2Source.CheckState == CheckState.Unchecked)
                saveSource = false;
        }



        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SN = comboBox1.SelectedIndex;
                ArrayList itemList = new ArrayList();
                itemList.AddRange(comboBox1.Items);
                comboBox2.DataSource=itemList;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    RawDatas.Enqueue(dataGridView1.Rows[i].Cells[SN].Value.ToString());
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void numericUpDown_PipeWallSize_ValueChanged(object sender, EventArgs e)
        {
            Gamma._Gamma.PipeWallSize = (double)this.numericUpDown_PipeWallSize.Value;
        }

        private void numericUpDown_CircleInterval_ValueChanged(object sender, EventArgs e)
        {
            Gamma._Gamma.CircleInterval = (double)this.numericUpDown_CircleInterval.Value;
        }

        private void comboBox_BariteContainment_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_BariteContainment.SelectedIndex)
            {
                case 0: Gamma._Gamma.BariteContainment = true;
                    break;
                case 1: Gamma._Gamma.BariteContainment = false;
                    break;
                default: Gamma._Gamma.BariteContainment = true;
                    break;
            }
        }

        private void numericUpDown_MudDensity_ValueChanged(object sender, EventArgs e)
        {
            Gamma._Gamma.MudDensity = (double)this.numericUpDown_MudDensity.Value;
        }

        private void numericUpDown_WellDiameter_ValueChanged(object sender, EventArgs e)
        {
            Gamma._Gamma.WellDiameter = (double)this.numericUpDown_WellDiameter.Value;
        }

        private void checkBox_WellDiameter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_WellDiameter.CheckState.Equals(CheckState.Unchecked))
            {
                Gamma._Gamma.UseWellDiameter = false;
                numericUpDown_WellDiameter.Enabled = false;
            }
            else if (checkBox_WellDiameter.CheckState.Equals(CheckState.Checked))
            {
                Gamma._Gamma.UseWellDiameter = true;
                numericUpDown_WellDiameter.Enabled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Gamma._Gamma.gu = GammaUnit.API;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                Gamma._Gamma.gu = GammaUnit.CPS;
        }

        /// <summary>
        /// 全井段处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_AllDepth_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_AllDepth.Checked)
            {
                textBox_StartDep.Enabled = false;
                textBox_EndDep.Enabled = false;
                StartDepth = float.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString());
                EndDepth = float.Parse(dataGridView1.Rows[dataGridView1.Rows.Count-2].Cells[1].Value.ToString());
            }
            else if (!checkBox_AllDepth.Checked)
            {
                textBox_StartDep.Enabled = true;
                textBox_EndDep.Enabled = true;
            }


        }
        private void CurveName_AC_TextChanged(object sender, EventArgs e)
        {
            Curve_AC = CurveName_AC.Text.Trim();
        }

        private void textBox_StartDep_TextChanged(object sender, EventArgs e)
        {
            Boolean ret = false;
            String str = null;
            try
            {
                StartDepth = float.Parse(textBox_EndDep.Text.Trim());
                if (StartDepth > 0)
                    ret = true;
                else ret = false;
            }
            catch (Exception ex)
            {
                if (!ret)
                    str = "起始深度必须为非负值，并小于终止深度";
                MessageBox.Show(str + ex.Message);
            }
        }

        private void textBox_EndDep_TextChanged(object sender, EventArgs e)
        {
            Boolean ret = false;
            String str = null;
            try
            {
                EndDepth = float.Parse(textBox_EndDep.Text.Trim());
                if (EndDepth > 0 && EndDepth > StartDepth)
                    ret = true;
                else ret = false;
            }
            catch (Exception ex)
            {
                if (!ret)
                    str = "终止深度必须为非负值，并大于起始深度";
                MessageBox.Show(str + ex.Message);
            }
        }
        #endregion 

        #region 通用函数

        public static Boolean IsNumStr(String str)
        {
            String pattern = @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$";//^[+-]?([1-9][0-9]*|0)(\.[0-9]+)?$
            return System.Text.RegularExpressions.Regex.IsMatch(str, pattern);
        }
        public static int GetRows(string FilePath)
        {
            using (StreamReader read = new StreamReader(FilePath, Encoding.Default))
            {
                return read.ReadToEnd().Split('\n').Length;
            }
        }

        /// <summary>
        /// 填充DGV
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
                    ds = new DataSet();
                    DGV.AutoGenerateColumns = false;
                    for (int i = 0; i < DGV.Columns.Count;i++ )
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
        /// <summary>
        /// 合并DataTable
        /// </summary>
        /// <param name="DGV">DataGridView</param>
        /// <param name="DTAC">被合并的DataTable After Correction</param>
        private void Merge_DGV(DataGridView DGV, DataTable sourceDT, DataTable addDT)
        {
            try
            {
                if (addDT != null && addDT.Rows.Count > 0)
                {
                    sourceDT.Merge(addDT);
                    DGV.DataSource = sourceDT;
                    DGV.AutoGenerateColumns = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// 打印文件头
        /// </summary>
        private void PrePrint()
        {
            try
            {
                if (saveSource)
                    outputPath = openFileDialog1.InitialDirectory + WellName + ".las";
                else
                outputPath = openFileDialog1.FileName+ ".las";
                fsOutPut = new FileStream(outputPath, FileMode.Append, FileAccess.Write);
                swOutPut = new StreamWriter(fsOutPut);
                String ParaString = null;
                for (int i = 0; i < comboBox1.Items.Count; i++)
                    ParaString += comboBox1.Items[i] + "\t\t";
                ParaString += Curve_AC + "\t\r\n";
                swOutPut.WriteLine("FORWARD_TEXT_FORMAT_1.0");
                swOutPut.WriteLine("STDEP  = " + Math.Round(StartDepth, 3));
                swOutPut.WriteLine("ENDEP  = " + Math.Round(EndDepth, 3));
                swOutPut.WriteLine("RLEV   = 0.1");
                swOutPut.WriteLine("CURVENAME = " + Curve_AC);
                swOutPut.WriteLine("END");
                swOutPut.WriteLine(ParaString);
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
                            swOutPut.Write(dataGridView2.Rows[i].Cells[j].Value.ToString() + "\t");
                        }
                        swOutPut.Write("\r\n");
                    }
                    break;
                }
                Application.DoEvents();
                swOutPut.WriteLine("========\t========\t========\t========\t");
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
        /// <summary>
        /// 初始化指定的DataRow
        /// </summary>
        /// <param name="datarow">指定的DataRow</param>
        /// <param name="datatable">所属DataTable</param>
        private bool dataRow_Init(DataRow datarow, DataTable datatable)
        {
            try
            {
                datarow = datatable.NewRow();
                for (int i = 1; i < comboBox1.Items.Count; i++)
                {
                    datarow[comboBox1.Items[i].ToString()+"'"] = "";
                }
                datarow[Curve_AC] = Guid.NewGuid().ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        /// <summary>
        /// 初始化指定DataColumn
        /// </summary>
        /// <param name="datacolumn">指定的DataColumn</param>
        /// <param name="datatable">列所属的DataTable</param>
        /// <returns></returns>
        private bool dataColumn_Init(String ColumnName, DataTable datatable)
        {
            try
            {
                DataColumn datacolumn = new DataColumn(ColumnName, System.Type.GetType("System.String"));
                datacolumn.AutoIncrement = false;
                datacolumn.Caption = ColumnName;
                datacolumn.ReadOnly = true;
                datacolumn.Unique = false;
                datatable.Columns.Add(datacolumn);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// 初始化dataTable，插入指定的DataColumns
        /// </summary>
        /// <param name="tablename">-DataTable.TableName</param>
        /// <param name="datatable">-DataTable</param>
        /// <param name="columnlist">-DataColumn 集合</param>
        private void DataTable_Init(String tablename,DataTable datatable,DataColumn[] columnlist)
        {
            try
            {
                if (datatable == null)
                {
                    datatable = new DataTable(tablename);
                    datatable.Columns.AddRange(columnlist);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox_Collar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Collar.CheckState.Equals(CheckState.Checked))
                numericUpDown_PipeWallSize.Enabled = true;
        }

        private void checkBox_Annulus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Annulus.CheckState.Equals(CheckState.Checked))
                numericUpDown_CircleInterval.Enabled = true;
        }

        private void textBox_WellName_TextChanged(object sender, EventArgs e)
        {
            WellName = textBox_WellName.Text.Trim();
        }
    }
}