using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace LWD_DataProcess
{
    /// <summary>
    /// 数据预处理类
    /// </summary>
    public class DataProcess
    {

        #region 线程里的全局变量
        /// <summary>
        /// 加载文件线程
        /// </summary>
        internal Thread load_Thread_U, load_Thread_C, load_Thread_O;
        /// <summary>
        /// 时深匹配线程：参数初处理，时深匹配。（1钻进状态判断 2地面系统记录深度异常判断并补齐)
        /// </summary>
        internal Thread match_Thread { get; set; }
        /// <summary>
        /// 处理线程1：全井段等间隔插值进程
        /// </summary>
        internal Thread depthInter_Thread { get; set; }
        /// <summary>
        /// 处理线程2: 剔除超差、异常数据并滤波
        /// </summary>
        internal Thread waveFilter_Thread { get; set; }
        /// <summary>
        /// 插值算法对象
        /// </summary>
        internal InterPolation IPolation { get; set; }
        public double DepthInterval { get; set; }
        public int ParaLength { get; set; }
        public double[] SKeysMinMax { get; set; }



        /// <summary>
        /// 三个文件的地址
        /// </summary>
        internal String UntitledPath, CalPath, OriPath;

        //当前行的截断字符串数组
        internal String[] row_U, row_C, row_O;
        //重组后的新字符串数组，是row的日期时间合并后+钻进状态
        internal String[] Nrow_U;
        //钻头深度  井底深度；钻头深度标记值，井底深度标记值
        internal double bitDep, wellDep, bDTemp, wDTemp; 
        internal String Lines_U, Lines_C, Lines_O;
        internal String status;
        public int No_U = 0, No_C = 0, No_O = 0;
        /// <summary>
        /// 钻进数据的条数
        /// </summary>
        public int No_Compared { get; set; }
        int Ccolomn,Ocolomn;
        FileStream fs_U, fs_C,fs_O;
        StreamReader sr_U,sr_C,sr_O;
        //StreamWriter sw_U,sw_C,sw_O;
        /// <summary>
        ///单例模式 当前读取Slot单元（减少new操作 提高时深匹配线程性能）
        /// </summary>
        private Slot tempSlot
        {
            get;
            set;
        }
        public Slot emptySlot = new Slot();
        #endregion
        #region 预处理方案
        /// <summary>
        /// 仪器类型
        /// </summary>
        public Ins_Type InsType { get; set; }
        /// <summary>
        /// 插值算法类型
        /// </summary>
        public COI_Type InterMethod { get; set; }
        /// <summary>
        /// 滤波算法类型
        /// </summary>
        public FA_Type FilterMethod { get; set; }
        #endregion

        #region 文件读取、数据预处理主体线程函数
        /// <summary>
        /// 生成数据处理类
        /// </summary>
        /// <param name="UPath">Untitled.dtf文件地址</param>
        /// <param name="CPath">Cal.tmf.dtf文件地址</param>
        /// <param name="OPath">Ori.txt文件地址</param>
        public DataProcess(String UPath,String CPath,String OPath)
        {
            Nrow_U = new String[7];//第七列：钻进状态
            Nrow_U[6] = "Unknown";
            tempSlot = new Slot("-/-", null, null, DrillStatus.Unknown);

            UntitledPath = UPath;
            CalPath = CPath;
            OriPath = OPath;

            load_Thread_U = new Thread(new ThreadStart(load_Thread_U_Function));
            load_Thread_C = new Thread(new ThreadStart(load_Thread_C_Function));
            load_Thread_O = new Thread(new ThreadStart(load_Thread_O_Function));
            match_Thread = new Thread(new ThreadStart(Match));
            depthInter_Thread = new Thread(new ThreadStart(InterPolate));
            waveFilter_Thread = new Thread(new ThreadStart(waveFilter));
        }



        /// <summary>
        /// Process按钮
        /// </summary>
        /// <returns>是否成功</returns>
        public bool Process()
        {
            bool bRet = false;
            try
            {
                match_Thread.Start();
                bRet = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("时深匹配::Start->" + ex.Message);
                match_Thread.Abort();
                bRet = false;
            }
            return bRet;
        }
        /// <summary>
        /// 预处理按钮
        /// </summary>
        /// <returns>是否成功</returns>
        public bool Calculate(COI_Type ct,FA_Type fa,Ins_Type it)
        {
            bool bRet = false;
            try 
            {
                this.InterMethod = ct;
                this.FilterMethod = fa;
                this.InsType = it;

                if (ct != COI_Type.None)
                    depthInter_Thread.Start();
                if (fa != FA_Type.None)
                    waveFilter_Thread.Start();
                bRet = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("全井段插值处理::Start->"+ex.Message);
                depthInter_Thread.Abort();
                bRet = false;
            }
            return bRet;
        }
        /// <summary>
        /// 加载Untitled.dtf文件线程函数
        /// </summary>
        void load_Thread_U_Function()
        {
            try
            {
                //获取文件行数
                System.Diagnostics.Trace.WriteLine("加载Untitled文件进程启动！");
                DataStruct.Len_Untitled = Funcs.GetRows_U(UntitledPath);
                DataStruct.CreateUData();

                //判断钻进状态
                fs_U = new FileStream(UntitledPath, FileMode.Open, FileAccess.Read);
                sr_U = new StreamReader(fs_U);
                sr_U.BaseStream.Seek(0, SeekOrigin.Begin);


                while ((Lines_U = sr_U.ReadLine()) != null)
                {
                    row_U = Lines_U.Split(DataStruct.seperator, StringSplitOptions.RemoveEmptyEntries);
                    // row_U:              
                    // 0日期、              
                    // 1时间、
                    // 2钻头、
                    // 3井底、
                    // 4大钩高度、
                    // 5钩载、
                    // 
                    // Nrow_U:
                    // 0序号、
                    // 1日期+时间、
                    // 2钻头深度、
                    // 3井底深度、
                    // 4大钩位置、
                    // 5钩载、
                    // 6实时钻进状态、
                    if (Funcs.IsNumStr(row_U[1]) && Funcs.IsNumStr(row_U[2]) && No_U < DataStruct.Len_Untitled)
                    {
                        bitDep = double.Parse(row_U[2]);
                        wellDep = double.Parse(row_U[3]);
                        Nrow_U[0] = No_U++.ToString();//序号
                        Nrow_U[1] = row_U[0] + row_U[1];//日期+"/"+时间
                        Nrow_U[2] = row_U[2];//钻头深度
                        Nrow_U[3] = row_U[3];//井底深度
                        Nrow_U[4] = row_U[4];//大钩位置
                        Nrow_U[5] = row_U[5];//钩载

                        ////打印重构数组
                        //Funcs._funcs.print(Nrow_U);

                        if (bDTemp == 0.0 || wDTemp == 0.0)
                            Nrow_U[6] = DrillStatus.Start.ToString();//开始
                        else if (bitDep == wellDep && wellDep > wDTemp)
                        {
                            Nrow_U[6] = DrillStatus.Drilling.ToString();//钻进
                            No_Compared++;
                        }
                        else if (wellDep > bitDep && bitDep > bDTemp)
                            Nrow_U[6] = DrillStatus.PullUp.ToString();//下钻
                        else if (wellDep > bitDep && bDTemp > bitDep)
                            Nrow_U[6] = DrillStatus.PushDown.ToString();//起钻
                        else if (bitDep == wellDep && wellDep == wDTemp)
                            Nrow_U[6] = DrillStatus.HoldOn.ToString();//悬停
                        else if (wellDep == wDTemp && bitDep == bDTemp)
                            Nrow_U[6] = DrillStatus.Stop.ToString();//静止
                        else Nrow_U[6] = DrillStatus.Unknown.ToString();//未知，异常

                        status = Nrow_U[6];
                        DataStruct.No_Untitled[No_U - 1] = Nrow_U[0];
                        DataStruct.DateTime_Untitled[No_U - 1] = Nrow_U[1];
                        DataStruct.BitDepth[No_U - 1] = Nrow_U[2];
                        DataStruct.WellDepth[No_U - 1] = Nrow_U[3];
                        DataStruct.DrStatus[No_U - 1] = DataStruct.ToDrillStatus(status);
                        bDTemp = bitDep;
                        wDTemp = wellDep;
                    }
                }
                sr_U.Close();
                fs_U.Close();
                System.Diagnostics.Trace.WriteLine("Untitled.dtf-文件预处理完毕！");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 加载Cal.tmd文件线程函数
        /// </summary>
        void load_Thread_C_Function()
        {
            try
            {
                //获取文件行数
                System.Diagnostics.Trace.WriteLine("加载Cal.tmf文件进程启动！");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToLongTimeString());
                DataStruct.Len_Cal = Funcs.GetRows(CalPath);
                DataStruct.CreateCData();

                fs_C = new FileStream(CalPath, FileMode.Open, FileAccess.Read);
                sr_C = new StreamReader(fs_C);
                sr_C.BaseStream.Seek(0, SeekOrigin.Begin);
                No_C = 0;
                int k = 0;
                while (((Lines_C = sr_C.ReadLine()) != null)||(Lines_C!=""))
                {
                    if (Lines_C.StartsWith("Dot:"))
                        continue;
                    row_C = Lines_C.Split(DataStruct.seperator, StringSplitOptions.RemoveEmptyEntries);
                    // 0：序号、1：参数1\t 2参数2\t......\tn-1参数n-1\t\r\n
                    if (row_C.Length != 0)
                    {
                        if (Funcs.IsNumStr(row_C[0]) && k > 0)
                        {
                            //参数赋值
                            for (int i = 0; i < row_C.Length; i++)
                            {
                                //科学计数法转换
                                if (row_C[i].Contains("e"))
                                    row_C[i]= DataStruct.convertScienceNumber(row_C[i]);
                                DataStruct.Paras[k - 1] += row_C[i] + '\t';

                            }
                            //打印重构的参数数组
                            //Funcs._funcs.print(k, DataStruct.Paras);
                        }
                        else if (!Funcs.IsNumStr(row_C[0]))
                        {
                            //参数名赋值
                            Ccolomn = row_C.Length;//参数个数=列数-1
                            DataStruct.ParaNames = row_C;//参数命名
                            for (int i = 0; i < Ccolomn; i++)
                                DataStruct.ParaNames[i] = row_C[i];
                            ////打印表头
                            //Funcs._funcs.print(row_C);
                        }
                        No_C = k++;
                        if (k == DataStruct.Len_Cal-2)
                            break;
                    }
                }
                sr_C.Close();
                fs_C.Close();
                System.Diagnostics.Trace.WriteLine("Cal.tmf-文件预处理完毕！");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 加载Ori.txt文件线程函数
        /// </summary>
        void load_Thread_O_Function()
        {
            try
            {
                //获取文件行数
                System.Diagnostics.Trace.WriteLine("加载Ori-1.txt文件进程启动！");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToLongTimeString());
                DataStruct.Len_Ori = Funcs.GetRows(OriPath);
                DataStruct.CreateOData();

                fs_O = new FileStream(OriPath, FileMode.Open, FileAccess.Read);
                sr_O = new StreamReader(fs_O);
                sr_O.BaseStream.Seek(0, SeekOrigin.Begin);
                while ((Lines_O = sr_O.ReadLine()) != null)
                {
                    row_O = Lines_O.Split(DataStruct.seperator, StringSplitOptions.RemoveEmptyEntries);
                    // 0..1..2.. ..Oclomn-1.时间/日期
                    Ocolomn = row_O.Length;//参数个数=列数-1
                    if (Ocolomn > 1)
                    {
                        //DataStruct.No_Ori[No_O] = No_O.ToString();
                        DataStruct.DateTime_Ori[No_O] = row_O[Ocolomn - 2] + row_O[Ocolomn - 1];
                        //Funcs._funcs.print(row_O[Ocolomn - 1]);
                        No_O++;
                    }
                }
                sr_O.Close();
                fs_O.Close();
                System.Diagnostics.Trace.WriteLine("Ori-1.txt-文件预处理完毕！");
                System.Diagnostics.Trace.WriteLine(DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 时深匹配处理线程函数
        /// </summary>
        internal void Match()
        {
            System.Diagnostics.Trace.WriteLine("时深匹配进程启动！");
            DataStruct.CreateData((int)DataStruct.Len_Ori - 1);
            String[] strs = new String[DataStruct.ParaNames.Length];
            Double[] empteyParas= new Double[DataStruct.ParaNames.Length];
            Double[] tempParas;
            if (DataStruct.Len_Ori != 0)
                DataStruct.DateTime_Compared = DataStruct.DateTime_Ori;//赋值时间
            long k = 0;
            long i = 0;
            long j = 0;
            try
            {
                while (i < DataStruct.Len_Ori - 1)
                {
                    for (j = 0; j < DataStruct.Len_Untitled - 2; j++)
                    {
                        if (DataStruct.DateTime_Untitled[j] == DataStruct.DateTime_Compared[i]
                            && DataStruct.Paras != null && k < (DataStruct.Len_Ori - 1))//时间匹配   && DataStruct.DrStatus[j].Equals("Drilling")
                        {
                            tempParas = empteyParas;
                            //深度匹配
                            DataStruct.Depth_Compared[k] = DataStruct.WellDepth[j];
                            //参数匹配
                            strs = DataStruct.Paras[i].Split(DataStruct.seperator, StringSplitOptions.RemoveEmptyEntries);

                            DataStruct.Status_Compared[k] = DataStruct.DrStatus[i];//钻进状态赋值
                            //参数连接字符串拆分成数组赋值
                            //tempParas = DataStruct.Paras[k].Split(DataStruct.seperator, StringSplitOptions.RemoveEmptyEntries);
                            //生成当前匹配的SLOT
                             //tempSlot=setSlot(DataStruct.DateTime_Compared[k], DataStruct.Depth_Compared[k], tempParas,
                             //    DataStruct.Status_Compared[k]);
                            //将当前SLOT按照Key值-深度，添加到CommonData类中的队列里
                            if (k > 0)
                                if (DataStruct.Depth_Compared[k - 1] != DataStruct.Depth_Compared[k]
                                    && DataStruct.Status_Compared[k] != DrillStatus.Unknown)
                                    //选取钻进  &&DataStruct.Status_Compared[k]!=DrillStatus.PullUp
                                    CommonData._CD.SList._SlotList.Add(double.Parse(DataStruct.WellDepth[j]),
                                        setSlot(DataStruct.DateTime_Compared[k], DataStruct.Depth_Compared[k],
                                        getParas(strs),DataStruct.Status_Compared[k]));
                            k++;
                            i++;
                        }
                    }
                    break;
                }
                System.Diagnostics.Trace.WriteLine("时深匹配线程结束！\r\n"
                    + "Untitled文件光标位置：" + j + "\r\n"
                    + "Cal文件光标位置：" + i + "\r\n"
                    + "队列文件长度：" + CommonData._CD.SList._SlotList.Count.ToString() + "\r\n");
                //match_Thread.Abort();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Match()函数 时深匹配时出错！" + ex.ToString());
            }
        }
        /// <summary>
        /// 插值处理线程
        /// </summary>
        internal void InterPolate()
        {
            System.Diagnostics.Trace.WriteLine("插值计算线程启动！");
            try
            {
                InterPolation._InterPo = 
                    new InterPolation(DataStruct.SKeysMin, DataStruct.SKeysMax, DataStruct.Interval,DataStruct.ParaNames.Length);
                IPolation = InterPolation._InterPo;//插值算法调用
                switch(InterMethod)
                {
                    case COI_Type.Linear: IPolation.LinearInterPolation(CommonData._CD.SList, CommonData._CD.AIPList);
                        break;
                    case COI_Type.Kriging: IPolation.Kriging(CommonData._CD.SList, CommonData._CD.AIPList);
                        break;
                    default: IPolation.LinearInterPolation(CommonData._CD.SList, CommonData._CD.AIPList);
                        break;
                }
                System.Diagnostics.Trace.WriteLine("插值计算线程结束！");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("InterPolation()函数 预处理时出错！" + ex.ToString());
            }
        }
        /// <summary>
        /// 滤波线程
        /// </summary>
        internal void waveFilter()
        {
        }
        #endregion


        #region 线程调用到的批处理函数

        /// <summary>
        /// （3个文件的）预处理线程启动
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            bool bRet = false;
            try
            {
                load_Thread_U.Start();
                load_Thread_C.Start();
                load_Thread_O.Start();
                bRet = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("预处理线程::Start->" + ex.Message);
                bRet = false;
            }
            return bRet;
        }
        /// <summary>
        /// 预处理线程中止
        /// </summary>
        public void Stop()
        {
            try
            {
                load_Thread_U.Abort();
                load_Thread_C.Abort();
                load_Thread_O.Abort();
                match_Thread.Abort();
                depthInter_Thread.Abort();
                waveFilter_Thread.Abort();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("预处理线程::Stop->" + ex.Message);
            }
        }
        /// <summary>
        /// 给tempSlot赋值
        /// </summary>
        /// <param name="datetime">1.日期/时间</param>
        /// <param name="welldepth">2.井深</param>
        /// <param name="paras">3.参数数组</param>
        /// <param name="drillstatus">4.钻进状态</param>
        internal Slot setSlot(String datetime,String welldepth,Double[] paras,DrillStatus drillstatus)
        {
            Slot temp = new Slot();
            temp.DateTime = datetime;
            temp.WellDepth = welldepth;
            temp.DrillStatus = drillstatus;
            temp.Paras = paras;
            return temp;
        }
        /// <summary>
        /// 重置tempSlot
        /// </summary>
        //void clearSlot()
        //{
        //    tempSlot = emptySlot;
        //    tempSlot.DateTime = "-";
        //    tempSlot.WellDepth =null;
        //    tempSlot.DrillStatus = DrillStatus.Unknown;
        //    tempSlot.Paras = null;
        //}
        internal double[] getParas(String[] strs)
        {
            double[] Para = new double[DataStruct.ParaNames.Length];
            for (int m = 0; m < DataStruct.ParaNames.Length; m++)
            {
                Para[m] = Double.Parse(strs[m]);
            }
            return Para;
        }
        #endregion
    }
}
