using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    /// <summary>
    /// 扩展数据结构-Singleton
    /// </summary>
    sealed class DataStruct
    {
        /// <summary>
        /// 分隔符集合
        /// </summary>
        public static readonly String[] seperator = { "\t"," ", "  ", "      ", "       ","\\","/"};
        public static readonly String[] ScienceNumSign = { "e","+"};
        public static readonly String[] rn = { "\r","\n","\r\n"};
        public static String DB_PATH = "";
        /// <summary>
        /// 深度插值间隔
        /// </summary>
        public static Double Interval { get; set; }
        public static Double SKeysMax { get; set; }
        public static Double SKeysMin { get; set; }
        /// <summary>
        /// 原始文件-行数: 1,Untitled   2,Cal   3,Ori-1 4，钻进
        /// </summary>
        public static Int64 Len_Untitled, Len_Cal, Len_Ori;
        /// <summary>
        /// Untitled文件参数
        /// </summary>
        public static String[] No_Untitled, DateTime_Untitled, BitDepth, WellDepth,HookPos, HookLoad;
        public static DrillStatus[]  DrStatus;
        /// <summary>
        /// Cal文件参数
        /// </summary>
        public static String[] No_Cal;//序号
        public static String[] ParaNames;//参数名行
        public static String[] Paras;//参数数组的连结字符串，参数间用'\t'分割
        /// <summary>
        /// Ori文件参数(只需提取 0序列,1日期/时间，2日期，3时间)
        /// </summary>
        public static String[] No_Ori, DateTime_Ori, Date_Ori, TimeOri;
        /// <summary>
        /// 预处理后整合文件Compared
        /// </summary>
        public static String[] No_Compared, DateTime_Compared, Depth_Compared;
        public static DrillStatus[] Status_Compared;
        public static Double[] Paras_Compared;

        public static Boolean ReadyToPrint;

        public static DrillStatus ToDrillStatus(string str)
        {
            DrillStatus DS;
            switch(str)
            {
                case "Start": DS= DrillStatus.Start;
                    break;
                case "Stop": DS = DrillStatus.Stop;
                    break;
                case "Drilling": DS = DrillStatus.Drilling;
                    break;
                case "PullUp": DS = DrillStatus.PullUp;
                    break;
                case "PushDown": DS = DrillStatus.PushDown;
                    break;
                default: DS = DrillStatus.Unknown;
                    break;
            }
            return DS;
        }
        /// <summary>
        /// 创建Untitled.dtf转储数组-2
        /// </summary>
        /// <returns>创建是否成功</returns>
        public static Boolean CreateUData()
        {
            try
            {
                No_Untitled = new String[Len_Untitled - 2];
                DateTime_Untitled = new String[Len_Untitled - 2];
                BitDepth = new String[Len_Untitled - 2];
                WellDepth = new String[Len_Untitled - 2];
                HookPos = new String[Len_Untitled - 2];
                HookLoad = new String[Len_Untitled - 2];
                DrStatus = new DrillStatus[Len_Untitled - 2];
                return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 创建Cal.tmf转储数组-2
        /// </summary>
        /// <returns>创建是否成功</returns>
        public static Boolean CreateCData()
        {
            try
            {
                No_Cal = new String[Len_Cal - 2];
                Paras = new String[Len_Cal - 2];
                return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 创建Ori-1.txt转储数组
        /// </summary>
        /// <returns>创建是否成功</returns>
        public static Boolean CreateOData()
        {
            try
            {
                No_Ori = new String[Len_Ori];
                DateTime_Ori = new String[Len_Ori];
                Date_Ori = new String[Len_Ori];
                TimeOri = new String[Len_Ori];
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 创建用于时深匹配的compared转储数组
        /// </summary>
        /// <returns>创建是否成功</returns>
        public static Boolean CreateData(int len)
        {
            try
            {
                No_Compared = new String[len];
                DateTime_Compared= new String[len];
                Depth_Compared = new String[len];
                Status_Compared = new DrillStatus[len];
                Paras_Compared= new Double[len];
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 创建浅表副本
        /// </summary>
        public void MClone()
        {
            this.MemberwiseClone();
        }
    }
}
