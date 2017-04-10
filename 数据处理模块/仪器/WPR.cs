using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;
using System.Threading;

namespace LWD_DataProcess
{
    /// <summary>
    /// WPR环境校正类
    /// </summary>
    public class WPR
    {
        private WPR() { }
        /// <summary>
        /// 单例模式对象 DEPTH   RACECHM   RACECLM   RACECSHM    RACECSLM    RPCECHM   RPCECLM   RPCECSHM    RPCECSLM
        /// </summary>
        public readonly static WPR _wpr = new WPR();
        #region 字段
        private float depth= -999.25f;
        private float racechm = -999.25f;
        private float raceclm= -999.25f;
        private float racecshm = -999.25f;
        private float racecslm = -999.25f;
        private float rpcechm = -999.25f;
        private float rpceclm = -999.25f;
        private float rpcecshm = -999.25f;
        private float rpcecslm = -999.25f;
        private float rm = -999.25f;
        private float hd = -999.25f;
        private String toolsize = "6.75";
        private float[] RmRange = { 0.05f, 0.1f, 0.2f, 0.5f, 1.0f, 2.0f };
        private float[] sbrRange = { 0.2f, 0.5f, 1.0f, 2.0f, 5.0f, 10.0f };
        private float[] resRange = { 0.5f, 1.0f, 2.0f, 4.0f, 6.0f, 8.0f, 10.0f, 15.0f, 20.0f, 30.0f, 40.0f, 50.0f };
        private float sbr = -999.25f;
        private float tb = -999.25f;
        private String corMethod = "";
        private String chartPara = "";
        private String[] chartnameleft = new String[8];
        private String[] chartnameright = new String[8];
        private String[] chartnamemid = new String[8];
        private Boolean paraOnChart = false;
        private Boolean valueOnChart = false;
        private List<float> xleft = new List<float>();
        private List<float> xright = new List<float>();
        private List<float> xmid = new List<float>();
        private List<float> yleft = new List<float>();
        private List<float> yright = new List<float>();
        private List<float> ymid = new List<float>();
        private List<float> intery = new List<float>();
        private SQLiteDBHelper dbhelper = new SQLiteDBHelper();
        private SQLiteDataReader datareader;
        private SQLiteDataAdapter dataadapter=new SQLiteDataAdapter();
        private List<List<XYValue>> listleft = new List<List<XYValue>>();
        private List<List<XYValue>> listright = new List<List<XYValue>>();
        private List<List<XYValue>> listmid = new List<List<XYValue>>();
        private DataSet ds_xylist = new DataSet();
        private float paraLeft = -999.25f;
        private float paraRight = -999.25f;
        private float paraMid = -999.25f;
        private float paraChart = 0.0f;
        //private List<XYValue> curveLeft =new List<XYValue>() ;
        //private List<XYValue> curveRight = new List<XYValue>();
        //private List<XYValue> curveMid = new List<XYValue>();
        private List<float> tbleft = new List<float>();
        private List<float> tbright = new List<float>();
        private List<float> tbmid = new List<float>();
        private List<XYValue> chartLeftcurve = new List<XYValue>();
        private List<XYValue> chartRightcurve = new List<XYValue>();
        private List<XYValue> chartMidcurve = new List<XYValue>();
        private DataTable dt_CorData = new DataTable();
        private DataSet ds_CorData = new DataSet();
        /// <summary>
        /// 左参数 字符串
        /// </summary>
        private String LeftParaString = "";
        /// <summary>
        /// 右参数 字符串
        /// </summary>
        private String RightParaString = "";
        /// <summary>
        /// 参数 字符串
        /// </summary>
        private String MidParaString = "";
        /// <summary>
        /// 8条曲线，对应的图版名前缀数组，长度为8
        /// </summary>
        private String[] ChartPrefix = new String[8];

        /// <summary>
        /// 泥浆电阻率左边界
        /// </summary>
        private float RmLeft = 0.0f;
        /// <summary>
        /// 泥浆电阻率刚好在团版上
        /// </summary>
        private float RmMid = -999.25f;
        /// <summary>
        /// 泥浆电阻率右边界
        /// </summary>
        private float RmRight = 0.0f;
        /// <summary>
        /// 围岩电阻率左边界
        /// </summary>
        private float SBRLeft = 0.0f;
        /// <summary>
        /// 围岩电阻率刚好在图版上
        /// </summary>
        private float SBRMid = -999.25f;
        /// <summary>
        /// 围岩电阻率右边界
        /// </summary>
        private float SBRRight = 0.0f;

        ///// <summary>
        ///// 目的层电阻率测量值左边界
        ///// </summary>
        //private float ResLeft = 0.0f;
        ///// <summary>
        ///// 目的层电阻率测量值刚好在图版上
        ///// </summary>
        //private float ResMid = -999.25f;
        ///// <summary>
        ///// 目的层电阻率测量值右边界
        ///// </summary>
        //private float ResRight = 0.0f;

        #endregion

        #region 属性
        /// <summary>
        /// 左图版XY值列表之列表
        /// </summary>
        public List<List<XYValue>> ListLeft
        {
            get { return listleft; }
            set { listleft = value; }
        }
        /// <summary>
        /// 右图版XY值列表之列表
        /// </summary>
        public List<List<XYValue>> ListRight
        {
            get { return listright; }
            set { listright = value; }
        }
        /// <summary>
        /// 图版XY值列表之列表
        /// </summary>
        public List<List<XYValue>> ListMid
        {
            get { return listmid; }
            set { listmid = value; }
        }
        /// <summary>
        /// SQLDataReader 实例化对象
        /// </summary>
        public SQLiteDataReader DataReader
        {
            get { return datareader; }
            set { datareader = value; }
        }
        /// <summary>
        /// SQLDataAdapter 实例化对象
        /// </summary>
        public SQLiteDataAdapter DataAdapter
        {
            get { return dataadapter; }
            set { dataadapter = value; }
        }
        /// <summary>
        /// SQLite数据库助手
        /// </summary>
        public SQLiteDBHelper DBHelper
        {
            get { return dbhelper; }
            set { dbhelper = value; }
        }
        /// <summary>
        /// 视电阻率左侧X坐标
        /// </summary>
        public List<float> XLeft
        {
            get { return xleft; }
            set { xleft = value; }
        }
        /// <summary>
        /// 视电阻率X坐标
        /// </summary>
        public List<float> XMid
        {
            get { return xmid; }
            set { xmid = value; }
        }
        /// <summary>
        /// 视电阻率左侧X坐标
        /// </summary>
        public List<float> XRight
        {
            get { return xright; }
            set { xright = value; }
        }

        /// <summary>
        /// 图版参数是否在图版边界上
        /// </summary>
        public Boolean ParaOnChart
        {
            get { return paraOnChart; }
            set { paraOnChart = value; }
        }
        /// <summary>
        /// 左边界图版名集合数组：长度为8。
        /// </summary>
        public String[] ChartNameLeft
        {
            get { return chartnameleft; }
            set { chartnameleft = value; }
        }
        /// <summary>
        /// 右边界图版名集合数组：长度为8。
        /// </summary>
        public String[] ChartNameRight
        {
            get { return chartnameright; }
            set { chartnameright = value; }
        }
        /// <summary>
        /// 图版名集合数组：长度为8。
        /// </summary>
        public String[] ChartNameMid
        {
            get { return chartnamemid; }
            set { chartnamemid = value; }
        }

        /// <summary>
        /// 泥浆电阻率 Ω.m
        /// </summary>
        public float Rm
        {
            get { return rm; }
            set { rm = value; }
        }
        /// <summary>
        /// 井眼尺寸inch
        /// </summary>
        public float HD
        {
            get { return hd; }
            set { hd = value; }
        }
        /// <summary>
        /// 仪器尺寸 inch
        /// </summary>
        public String ToolSize
        {
            get { return toolsize; }
            set { toolsize = value; }
        }
        /// <summary>
        /// 校正类型
        /// </summary>
        public String CorMethod
        {
            get { return corMethod; }
            set { corMethod = value; }
        }
        /// <summary>
        /// 图版参数名
        /// </summary>
        public String ChartPara
        {
            get { return chartPara; }
            set { chartPara = value; }
        }
        /// <summary>
        /// 围岩电阻率(Ω.m)
        /// </summary>
        public float SBR
        {
            get { return sbr; }
            set { sbr = value; }
        }
        /// <summary>
        /// 目的层厚度(feet)
        /// </summary>
        public float Tb
        {
            get { return tb; }
            set { tb = value; }
        }
        /// <summary>
        /// 深度m
        /// </summary>
        public float DEPTH
        {
            get { return depth; }
            set { depth = value; }
        }
        /// <summary>
        /// 2Mhz长源距幅度比
        /// </summary>
        public float RACECHM
        {
            get { return racechm; }
            set { racechm = value; }
        }
        /// <summary>
        /// 400Khz长源距幅度比
        /// </summary>
        public float RACECLM
        {
            get { return raceclm; }
            set { raceclm = value; }
        }
        /// <summary>
        /// 2Mhz短源距幅度比
        /// </summary>
        public float RACECSHM
        {
            get { return racecshm; }
            set { racecshm = value; }
        }
        /// <summary>
        /// 400Khz短源距幅度比
        /// </summary>
        public float RACECSLM
        {
            get { return racecslm; }
            set { racecslm = value; }
        }
        /// <summary>
        /// 2Mhz长源距相位差
        /// </summary>
        public float RPCECHM
        {
            get { return rpcechm; }
            set { rpcechm = value; }
        }
        /// <summary>
        /// 400Khz短源距相位差
        /// </summary>
        public float RPCECLM
        {
            get { return rpceclm; }
            set { rpceclm = value; }
        }
        /// <summary>
        /// 2Mhz短源距相位差
        /// </summary>
        public float RPCECSHM
        {
            get { return rpcecshm; }
            set { rpcecshm = value; }
        }
        /// <summary>
        /// 400Khz短源距相位差
        /// </summary>
        public float RPCECSLM
        {
            get { return rpcecslm; }
            set { rpcecslm = value; }
        }
        /// <summary>
        /// 比测量值小的临近点
        /// </summary>
        public List<float> YLeft
        {
            get
            {
                return yleft;
            }

            set
            {
                yleft = value;
            }
        }
        /// <summary>
        /// 比测量值大的临近点
        /// </summary>
        public List<float> YRight
        {
            get
            {
                return yright;
            }

            set
            {
                yright = value;
            }
        }
        /// <summary>
        /// 测量值恰好在图版上
        /// </summary>
        public List<float> YMid
        {
            get
            {
                return ymid;
            }

            set
            {
                ymid = value;
            }
        }
        /// <summary>
        /// 测量值是否在曲线上
        /// </summary>
        public bool ValueOnChart
        {
            get
            {
                return valueOnChart;
            }

            set
            {
                valueOnChart = value;
            }
        }
        /// <summary>
        /// 左图版参数
        /// </summary>
        public float ParaLeft
        {
            get
            {
                return paraLeft;
            }

            set
            {
                paraLeft = value;
            }
        }
        
        /// <summary>
        /// 右图版参数
        /// </summary>
        public float ParaRight
        {
            get
            {
                return paraRight;
            }

            set
            {
                paraRight = value;
            }
        }
        /// <summary>
        /// 图版参数
        /// </summary>
        public float ParaMid
        {
            get
            {
                return paraMid;
            }

            set
            {
                paraMid = value;
            }
        }
        /// <summary>
        /// 图版电阻率的预设范围
        /// </summary>
        public float[] ResRange
        {
            get
            {
                return resRange;
            }

            set
            {
                resRange = value;
            }
        }
        /// <summary>
        /// 目的层厚左边界
        /// </summary>
        public List<float> TbLeft
        {
            get
            {
                return tbleft;
            }

            set
            {
                tbleft = value;
            }
        }
        /// <summary>
        /// 目的层厚右边界
        /// </summary>
        public List<float> TbRight
        {
            get
            {
                return tbright;
            }

            set
            {
                tbright = value;
            }
        }
        /// <summary>
        /// 目的层厚
        /// </summary>
        public List<float> TbMid
        {
            get
            {
                return tbmid;
            }

            set
            {
                tbmid = value;
            }
        }
        /// <summary>
        /// 图版参数
        /// </summary>
        public float ParaChart
        {
            get
            {
                return paraChart;
            }

            set
            {
                paraChart = value;
            }
        }

        #endregion

        #region 队列Queue 和 列表List
        /// <summary>
        /// 深度队列
        /// </summary>
        public ConcurrentQueue<float> Queue_DEPTH = new ConcurrentQueue<float>();
        /// <summary>
        /// RACECHM队列
        /// </summary>
        public  ConcurrentQueue<float> Queue_RACECHM = new ConcurrentQueue<float>();
        /// <summary>
        /// RACECLM队列
        /// </summary>
        public  ConcurrentQueue<float> Queue_RACECLM = new ConcurrentQueue<float>();
        /// <summary>
        /// RACECSHM队列
        /// </summary>
        public  ConcurrentQueue<float> Queue_RACECSHM = new ConcurrentQueue<float>();
        /// <summary>
        /// RACECSLM队列
        /// </summary>
        public  ConcurrentQueue<float> Queue_RACECSLM = new ConcurrentQueue<float>();
        /// <summary>
        /// RPCECHM队列
        /// </summary>
        public  ConcurrentQueue<float> Queue_RPCECHM = new ConcurrentQueue<float>();
        /// <summary>
        /// RPCECLM队列
        /// </summary>
        public  ConcurrentQueue<float> Queue_RPCECLM = new ConcurrentQueue<float>();
        /// <summary>
        /// RPCECSHM队列
        /// </summary>
        public  ConcurrentQueue<float> Queue_RPCECSHM = new ConcurrentQueue<float>();
        /// <summary>
        /// RPCECSLM队列
        /// </summary>
        public  ConcurrentQueue<float> Queue_RPCECSLM = new ConcurrentQueue<float>();
        /// <summary>
        /// 深度表
        /// </summary>
        public List<float> List_DEPTH = new List<float>();
        /// <summary>
        /// RACECHM表
        /// </summary>
        public List<float> List_RACECHM = new List<float>();
        /// <summary>
        /// RACECLM表
        /// </summary>
        public List<float> List_RACECLM = new List<float>();
        /// <summary>
        /// RACECSHM表
        /// </summary>
        public List<float> List_RACECSHM = new List<float>();
        /// <summary>
        /// RACECSLM表
        /// </summary>
        public List<float> List_RACECSLM = new List<float>();
        /// <summary>
        /// RPCECHM表
        /// </summary>
        public List<float> List_RPCECHM = new List<float>();
        /// <summary>
        /// RPCECLM表
        /// </summary>
        public List<float> List_RPCECLM = new List<float>();
        /// <summary>
        /// RPCECSHM表
        /// </summary>
        public List<float> List_RPCECSHM = new List<float>();
        /// <summary>
        /// RPCECSLM表
        /// </summary>
        public List<float> List_RPCECSLM = new List<float>();

        /// <summary>                                           
        /// 校正后RACECHM表                                           
        /// </summary>                                          
        public List<float> List_RACECHM_AC = new List<float>();
        /// <summary>                                           
        /// 校正后RACECLM表                                           
        /// </summary>                                          
        public List<float> List_RACECLM_AC = new List<float>();
        /// <summary>                                           
        /// 校正后RACECSHM表                                          
        /// </summary>                                          
        public List<float> List_RACECSHM_AC = new List<float>();
        /// <summary>                                           
        /// 校正后RACECSLM表                                          
        /// </summary>                                          
        public List<float> List_RACECSLM_AC = new List<float>();
        /// <summary>                                           
        /// 校正后RPCECHM表                                            
        /// </summary>                                          
        public List<float> List_RPCECHM_AC = new List<float>();
        /// <summary>                                           
        /// 校正后RPCECLM表                                           
        /// </summary>                                          
        public List<float> List_RPCECLM_AC = new List<float>();
        /// <summary>                                           
        /// 校正后RPCECSHM表                                          
        /// </summary>                                          
        public List<float> List_RPCECSHM_AC = new List<float>();
        /// <summary>                                           
        /// 校正后RPCECSLM表                                          
        /// </summary>                                          
        public List<float> List_RPCECSLM_AC = new List<float>();

        public List<DataTable> ChartDataTable = new List<DataTable>();


        /// <summary>
        /// 坐标List
        /// </summary>
        public List<Coordinates> co=new List<Coordinates>();

        #endregion 

        public void QueueToArray()
        {
            List_DEPTH = Queue_DEPTH.ToList<float>();
            List_RACECHM = Queue_RACECHM.ToList<float>();
            List_RACECLM = Queue_RACECLM.ToList<float>();
            List_RACECSHM = Queue_RACECSHM.ToList<float>();
            List_RACECSLM = Queue_RACECSLM.ToList<float>();
            List_RPCECHM = Queue_RPCECHM.ToList<float>();
            List_RPCECLM = Queue_RPCECLM.ToList<float>();
            List_RPCECSHM = Queue_RPCECSHM.ToList<float>();
            List_RPCECSLM = Queue_RPCECSLM.ToList<float>();
        }


        /// <summary>
        /// 生成图版名前缀
        /// </summary>
        /// <param name="rawCurveNames">曲线名集合</param>
        public void SetChartPrefix(String[] rawCurveNames)
        {
            for(int i=0;i<8;i++)
                switch (rawCurveNames[i+1])
                {
                    case "RACECHM":
                        ChartPrefix[i] = ToolSize + "_2M_R36A_";
                        break;
                    case "RACECLM":
                        ChartPrefix[i] = ToolSize + "_400k_R36A_";
                        break;
                    case "RACECSHM":
                        ChartPrefix[i] = ToolSize + "_2M_R22.5A_";
                        break;
                    case "RACECSLM":
                        ChartPrefix[i] = ToolSize + "_400k_R22.5A_";
                        break;
                    case "RPCECHM":
                        ChartPrefix[i] = ToolSize + "_2M_R36P_";
                        break;
                    case "RPCECLM":
                        ChartPrefix[i] = ToolSize + "_400k_R36P_";
                        break;
                    case "RPCECSHM":
                        ChartPrefix[i] = ToolSize + "_2M_R22.5P_";
                        break;
                    case "RPCECSLM":
                        ChartPrefix[i] = ToolSize + "_400k_R22.5P_";
                        break;
                    default:break;
                }
        }
        /// <summary>
        /// 生成图版名后缀
        /// </summary>
        /// <param name="para">图版参数</param>
        public void SetChartPostfix(float para)
        {
            //该参数恰好在图版上
            if(para!=-999.25f)
            {
                for (int i = 0; i < 8; i++)
                    ChartNameMid[i] = ChartPrefix[i] + this.CorMethod+"_" + MidParaString;
                ParaOnChart = true;
            }
            //该参数有左右边界,前8个位左边界图版，后8个位右边界图版
            else if(para==-999.25f)
            {
                for (int i = 0; i < 8; i++)
                {
                    ChartNameLeft[i] = ChartPrefix[i] + this.CorMethod + "_" + LeftParaString;
                }
                for (int i = 0; i < 8; i++)
                {
                    ChartNameRight[i] = ChartPrefix[i] + this.CorMethod + "_" + RightParaString;
                }
                ParaOnChart = false;
            }
        }
        /// <summary>
        /// 获取井眼校正泥浆电阻率左右边界
        /// </summary>
        /// <param name="mudRes"></param>
        public void getRmBounder()
        {
            for (int i = 0; i < RmRange.Length-1; i++)
            {
                if (RmRange[i] < Rm && RmRange[i + 1] > Rm)
                {
                    RmLeft = RmRange[i];
                    ParaLeft = RmLeft;
                    LeftParaString = "Rm=" + RmLeft.ToString("F1");
                    RmRight = RmRange[i + 1];
                    ParaRight = RmRight;
                    RightParaString = "Rm=" + RmRight.ToString("F1");
                    break;
                }
                else if (Rm == RmRange[i])
                {
                    RmMid = Rm;
                    MidParaString = "Rm=" + RmMid.ToString("F1");
                    ParaMid = RmMid;
                    break;
                }
                else if (Rm == RmRange[i + 1])
                {
                    RmMid = Rm;
                    MidParaString = "Rm=" + RmMid.ToString("F1");
                    ParaMid = RmMid;
                    break;
                }
                else if (RmRange[i] > Rm && RmRange[i + 1] > Rm)
                {
                    RmLeft = RmRange[i];
                    ParaLeft = RmLeft;
                    LeftParaString = "Rm=" + RmLeft.ToString("F1");
                    RmRight = RmRange[i + 1];
                    ParaRight = RmRight;
                    RightParaString = "Rm=" + RmRight.ToString("F1");
                    break;
                }
                else if (RmRange[i] < Rm && RmRange[i + 1] < Rm)
                    continue;
            }
        }
        /// <summary>
        /// 获取围岩校正围岩电阻率左右边界:给SBRLeft\SBRRight\SBRMid赋值；给ParaString赋值
        /// 1.参数在中间，左右边界即左右边界；
        /// 2.参数在边界外，左右边界代表反向延长线左右前后两点
        /// </summary>
        public void getSbrBounder()
        {
            for (int i = 0; i < sbrRange.Length - 1; i++)
            {
                if ((sbrRange[i] < SBR && sbrRange[i + 1] > SBR))
                {
                    SBRLeft = sbrRange[i];
                    ParaLeft = SBRLeft;
                    LeftParaString = "SBR=" + SBRLeft.ToString("F1");
                    SBRRight = sbrRange[i + 1];
                    ParaRight = SBRRight;
                    RightParaString = "SBR=" + SBRRight.ToString("F1");
                    break;
                }
                else if (SBR == sbrRange[i])
                {
                    SBRMid = SBR;
                    MidParaString = "SBR=" + RmMid.ToString("F1");
                    ParaMid = SBRMid;
                    break;
                }
                else if (SBR == sbrRange[i + 1])
                {
                    SBRMid = SBR;
                    MidParaString = "SBR=" + RmMid.ToString("F1");
                    ParaMid = SBRMid;
                    break;
                }
                else if (SBR < sbrRange[i] && SBR < sbrRange[i + 1])
                {
                    SBRLeft = sbrRange[i];
                    ParaLeft = SBRLeft;
                    LeftParaString = "SBR=" + SBRLeft.ToString("F1");
                    SBRRight = sbrRange[i + 1];
                    ParaRight = SBRRight;
                    RightParaString = "SBR=" + SBRRight.ToString("F1");
                    break;
                }
                else if (SBR > sbrRange[i] && SBR > sbrRange[i + 1])
                    continue;
            }
        }

        /// <summary>
        /// 获取目的层电阻率左右边界曲线 
        /// </summary>
        /// <param name="res">电阻率测量值</param>
        /// <param name="curveleft">左曲线</param>
        /// <param name="curveright">右曲线</param>
        /// <param name="curvemid">恰好所在的曲线</param>
        public void getResBounder(List<float> res,out List<XYValue> curveleft,out List<XYValue> curveright, out List<XYValue> curvemid)
        {
            curveleft = new List<XYValue>();
            curveright = new List<XYValue>();
            curvemid = new List<XYValue>();
            List<XYValue> left = new List<XYValue>();
            List<XYValue> right = new List<XYValue>();
            List<XYValue> mid = new List<XYValue>();
            try
            {
                //填充曲线边界参数
                for (int j = 0; j < List_DEPTH.Count; j++)
                    for (int i = 0; i < ResRange.Length - 1; i++)
                    {
                        if ((ResRange[i] < res[j] && res[j] < ResRange[i + 1]))
                        {
                            left.Add(new XYValue(Tb, -999.25f, ResRange[i]));
                            right.Add(new XYValue(Tb, -999.25f, ResRange[i + 1]));
                            mid.Add(new XYValue(Tb, -999.25f, res[j]));
                            break;
                        }
                        else if (res[j] == ResRange[i])
                        {
                            left.Add(new XYValue(Tb, -999.25f, res[j]));
                            right.Add(new XYValue(Tb, -999.25f, res[j]));
                            mid.Add(new XYValue(Tb, -999.25f, res[j]));
                            break;
                        }
                        else if (res[j] == ResRange[i + 1])
                        {
                            left.Add(new XYValue(Tb, -999.25f, res[j]));
                            right.Add(new XYValue(Tb, -999.25f, res[j]));
                            mid.Add(new XYValue(Tb, -999.25f, res[j]));
                            break;
                        }
                        else if (res[j] < ResRange[i] && res[j] < ResRange[i + 1])
                        {
                            left.Add(new XYValue(Tb, 0.8f, ResRange[0]));
                            right.Add(new XYValue(Tb, 0.8f, ResRange[1]));
                            mid.Add(new XYValue(Tb, 0.8f, ResRange[0]));
                            break;
                        }
                        else if (res[j] > ResRange[ResRange.Length - 2] && res[j] > ResRange[ResRange.Length - 1])
                        {
                            left.Add(new XYValue(Tb, 1.2f, ResRange[ResRange.Length-2]));
                            right.Add(new XYValue(Tb, 1.2f, ResRange[ResRange.Length - 1]));
                            mid.Add(new XYValue(Tb, 1.2f, ResRange[ResRange.Length - 1]));
                            break;
                        }
                    }
                curveleft.AddRange(left);
                curveright.AddRange(right);
                curvemid.AddRange(mid);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message+ "getResBounder()函数异常！");
            }
        }


        /// <summary>
        /// 打印图版曲线数据
        /// </summary>
        /// <param name="chartdata">图版曲线List<XYValue></param>
        private void ChartDataPrint(List<XYValue> chartdata)
        {
            Debug.WriteLine("ParaValue\t" + "XValue\t" + "YValue\t");
            for(int i=0;i<chartdata.Count;i++)
                Debug.WriteLine(chartdata[i].ParaValue.ToString() + "\t\t" + chartdata[i].XValue.ToString() + "\t\t" + chartdata[i].YValue.ToString() + "\t\t");
        }

        /// <summary>
        /// 剔除超差(min~max)
        /// </summary>
        public float FiltOverproof(float min, float max, float value)
        {
            if (value <= min)
                return min;
            else if (value >= max)
                return max;
            else return value;
        }

        /// <summary>
        /// 匹配图版
        /// </summary>
        /// <param name="rawcurvenames">曲线名称数组</param>
        public void MatchChart(String[] rawcurvenames)
        {
            switch(CorMethod)
            {
                case "介电常数": 
                    break;
                case "井眼校正":
                    ClearXY();
                    getRmBounder();
                    ParaMid = RmMid;
                    break;
                case "围岩校正":
                    ClearXY();
                    getSbrBounder();
                    ParaMid = SBRMid;
                    break;
                case "侵入校正": 
                    break;
                case "各向异性":
                    break;
                default:
                    break;
            }
            SetChartPrefix(rawcurvenames); 
            SetChartPostfix(ParaMid);
        }

        /// <summary>
        /// 校正线程
        /// </summary>
        /// <param name="method">校正类型</param>
        /// <param name="para">校正参数</param>
        public void CorrectFlow(String method,float para)
        {
            switch(method)
            {
                case "介电常数":
                    break;
                case "井眼校正":
                    {
                        GetChartXYValueList(RmLeft, RmRight, RmMid);
                        ParaLeft = RmLeft;
                        ParaRight = RmRight;
                        ParaChart = Rm;
                    }
                    break;
                case "围岩校正":
                    {
                        GetChartXYValueList(ResRange, SBRLeft, SBRRight, SBRMid);
                        ParaLeft = SBRLeft;
                        ParaRight = SBRRight;
                        ParaChart = SBR;
                    }
                    break;
                case "侵入校正":
                    break;
                case "各向异性":
                    break;
                default:
                    break;
            }//输入校正类型、校正参数，返回 图版XY值列表之列表 
            Thread.Sleep(10);
            GetACValue(CorMethod,para);
            //获取测量值在对应图版上临近的两个点Xleft和Xright,校正系数Yleft,YRigth
        }

        /// <summary>
        /// 获取图版的曲线XY值列表之列表：参数在曲线上获取一条；在两条曲线之间获取两条
        /// </summary>
        /// <param name="pvleft">左曲线参数</param>
        /// <param name="pvright">右曲线参数</param>
        /// <param name="pvmid">曲线参数</param>
        public void GetChartXYValueList(float pvleft, float pvright, float pvmid)
        {
            try
            {
                if (!ParaOnChart)//参数不在图版上
                    for (int i = 0; i < 8; i++)
                    {
                        List<XYValue> templ = new List<XYValue>();
                        List<XYValue> tempr = new List<XYValue>();
                        //左边界 
                        templ.AddRange(GetXYList(ChartNameLeft[i], pvleft));
                        templ.AddRange(GetXYList(ChartNameLeft[i], pvright));
                        ListLeft.Add(templ);
                        templ.Clear();
                        //右边界
                        tempr.AddRange(GetXYList(ChartNameRight[i], pvleft));
                        tempr.AddRange(GetXYList(ChartNameRight[i], pvright));
                        ListRight.Add(tempr);
                        tempr.Clear();
                    }
                if (ParaOnChart)
                    for (int i = 0; i < 8; i++)
                    {
                        //参数在图版上
                        ListMid.Add(GetXYList(ChartNameMid[i], pvmid));
                    }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 获取图版全部曲线XY值列表之列表
        /// </summary>
        /// <param name="range">曲线参数数组</param>
        public void GetChartXYValueList(float[] range,float cpvleft,float cpvright,float cpvmid)
        {
            try
            {
                if (!ParaOnChart)//参数不在图版上
                    for (int i = 0; i < 8; i++)
                    {
                        //左边界 
                        ListLeft.Add(GetXYList(ChartNameLeft[i], range, cpvleft));
                        //右边界
                        ListRight.Add(GetXYList(ChartNameRight[i], range, cpvright));
                    }
                if (ParaOnChart)
                    for (int i = 0; i < 8; i++)
                    {
                        //参数在图版上
                        ListMid.Add(GetXYList(ChartNameMid[i], range,cpvmid));
                    }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 查询图版X Y 值，存为键值对List
        /// </summary>
        /// <param name="ChartNames">图版名集合</param>
        /// <param name="pv">图版内参数</param>
        public List<XYValue> GetXYList(String ChartName,float pv)
        {
            SQLiteConnection conn = DBHelper.DbConnection;
            SQLiteCommand cmd = new SQLiteCommand(conn);//实例化SQL命令
            try
            {
                List<XYValue> templistxy = new List<XYValue>();
                dbhelper.Open();
                //带参SQL语句 查询ChartName图版，返回XValue,YValue，按照XValue 升序排列
                cmd.CommandText = "SELECT XValue,YValue FROM [" + ChartName + "] WHERE ParameterValue=@paravalue ORDER BY XValue ASC ";
                cmd.Parameters.Add(new SQLiteParameter("@paravalue", pv));
                //清空Adapter和DataSet
                DataAdapter = new SQLiteDataAdapter();
                ds_xylist.Clear();
                DataAdapter.SelectCommand = cmd;
                cmd.ExecuteNonQuery();//执行查询
                DataAdapter.Fill(ds_xylist);
                DataTable dt = ds_xylist.Tables[0];
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    XYValue tempxy = new XYValue(float.Parse(dt.Rows[i].ItemArray[0].ToString()), float.Parse(dt.Rows[i].ItemArray[1].ToString()));
                    templistxy.Add(tempxy);
                }
                DataAdapter.Dispose();
                return templistxy;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 查询图版X Y 值，存为键值对List
        /// </summary>
        /// <param name="ChartNames">图版名集合</param>
        /// <param name="pv">图版内参数数组</param>
        /// <param name="cpv">图版外参数</param>
        public List<XYValue> GetXYList(String ChartName, float[] pv,float cpv)
        {
            SQLiteConnection conn = DBHelper.DbConnection;
            SQLiteCommand cmd = new SQLiteCommand(conn);//实例化SQL命令
            List<XYValue> templistxy = new List<XYValue>();
            try
            {
                dbhelper.Open();
                for (int i = 0; i < pv.Length; i++)
                {
                    //清空Adapter和DataSet
                    DataAdapter = new SQLiteDataAdapter();
                    ds_xylist = new DataSet();
                    //带参SQL语句 查询ChartName图版，返回XValue,YValue，按照XValue 升序排列
                    cmd.CommandText = "SELECT XValue,YValue FROM [" + ChartName + "] WHERE ParameterValue=@paravalue ORDER BY XValue ASC ";
                    cmd.Parameters.Add(new SQLiteParameter("@paravalue", pv[i]));
                    DataAdapter.SelectCommand = cmd;
                    cmd.ExecuteNonQuery();//执行查询
                    DataAdapter.Fill(ds_xylist);
                    ds_xylist.Tables[0].TableName = "pv="+pv[i].ToString("F1")+"_cpv="+cpv.ToString("F1");
                    ChartDataTable.Add(ds_xylist.Tables[0]);
                }
                templistxy.Clear();
                for (int j = 0; j < ChartDataTable.Count; j++)
                {
                    for (int i = 0; i < ChartDataTable[j].Rows.Count; i++)
                    {
                        XYValue tempxy = new XYValue(float.Parse(ChartDataTable[j].Rows[i].ItemArray[0].ToString()),
                            float.Parse(ChartDataTable[j].Rows[i].ItemArray[1].ToString()),pv[j]);
                        templistxy.Add(tempxy);
                    }
                }
                DataAdapter.Dispose();
                ChartDataTable.Clear();
                return templistxy;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 获取测量值 插值处理后的校正数据 List_RACECHM=>List_RACECHM_AC...
        /// </summary>
        private void GetACValue(String method,float para)
        {
            for(int i=0;i<8;i++)
            {
                switch(i)
                {
                    case 0:
                        GetOneResValue(List_RACECHM, i,CorMethod ,para);
                        Thread.Sleep(10);
                        break;
                    case 1:
                        GetOneResValue(List_RACECLM, i, CorMethod, para);
                        Thread.Sleep(10);
                        break;
                    case 2:
                        GetOneResValue(List_RACECSHM, i, CorMethod, para);
                        Thread.Sleep(10);
                        break;
                    case 3:
                        GetOneResValue(List_RACECSLM, i, CorMethod, para);
                        Thread.Sleep(10);
                        break;
                    case 4:
                        GetOneResValue(List_RPCECHM, i, CorMethod, para);
                        Thread.Sleep(10);
                        break;
                    case 5:
                        GetOneResValue(List_RPCECLM, i, CorMethod, para);
                        Thread.Sleep(10);
                        break;
                    case 6:
                        GetOneResValue(List_RPCECSHM, i, CorMethod, para);
                        Thread.Sleep(10);
                        break;
                    case 7:
                        GetOneResValue(List_RPCECSLM, i, CorMethod, para);
                        Thread.Sleep(10);
                        break;
                }
            }
        }

        /// <summary>
        /// （同曲线 第一次插值）通过某图版左、右曲线上的临近点，获得插值后目的层厚在左、右曲线上的点.
        /// </summary>
        /// <param name="Index">测量值电阻率曲线对应的校正图版索引</param>
        /// <param name="Bounder">图版曲线参数左、右边界</param>
        /// <param name="chartlist">图版</param>
        /// <param name="param">图版曲线参数</param>
        /// <returns>目的层厚上的XYValue</returns>
        private XYValue getChartPoint(int Index, float Bounder, List<List<XYValue>> chartlist, float param)
        {
            XYValue result = new XYValue();
            List<XYValue> curve = new List<XYValue>();//曲线
            XYValue LeftPoint = new XYValue();//曲线上的左点
            XYValue RightPoint = new XYValue();//曲线上的右点
            try
            {
                for (int j = 0; j < chartlist[Index].Count; j++)
                {
                    if (chartlist[Index].ElementAt(j).ParaValue == Bounder)
                        curve.Add(chartlist[Index].ElementAt(j));
                }
                //Debug：打印曲线数据
                //ChartDataPrint(curve);
                if (curve.Count > 0)
                    Nearest2Point(curve, param, out LeftPoint, out RightPoint);
                if (LeftPoint.XValue != RightPoint.XValue)
                    result = InterPolation._InterPo.LagLinerInter(param, LeftPoint, RightPoint);
                else if (LeftPoint.XValue == RightPoint.XValue)
                    result = InterPolation._InterPo.LagLinerInter(param, LeftPoint, RightPoint, Bounder);
                else result = LeftPoint;
                curve.Clear();
                result.ParaValue = Bounder;
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "getChartPoint(）函数异常！");
                return result;
            }

        }
        /// <summary>
        /// （不同曲线 第二次插值）获取左、右某一个图版上测量点的乘系数列表
        /// 1.层厚在曲线上；
        /// 2.层厚曲线外，获取最近的2个目的层厚
        /// </summary>
        /// <param name="index">图版索引值</param>
        /// <param name="res">8条电阻率曲线之一</param>
        /// <param name="curveLeft">左曲线数据</param>
        /// <param name="curveRight">右曲线数据</param>
        /// <param name="chartlist">图版</param>
        public List<float> getOneCorrectList(int chartindex, List<float> res, List<XYValue> curveLeft, List<XYValue> curveRight, List<List<XYValue>> chartlist, float para)
        {
            //XYValue pointOnLeftChart = new XYValue();
            //XYValue pointOnRightChart = new XYValue();
            List<float> List_Factor = new List<float>();
            try
            {
                float factor = 0.0f;
                for (int j = 0; j < List_DEPTH.Count; j++)
                {

                    curveLeft[j].YValue = getChartPoint(chartindex, curveLeft[j].ParaValue, chartlist, para).YValue;
                    if (Math.Abs(curveLeft[j].YValue) > 1.2)
                        curveLeft[j].YValue = 1.2f;
                    else if (Math.Abs(curveLeft[j].YValue) < 0.8)
                        curveLeft[j].YValue = 0.8f;

                    curveRight[j].YValue = getChartPoint(chartindex, curveRight[j].ParaValue, chartlist, para).YValue;
                    if (Math.Abs(curveRight[j].YValue) > 1.2)
                        curveRight[j].YValue = 1.2f;
                    else if (Math.Abs(curveRight[j].YValue) < 0.8)
                        curveRight[j].YValue = 0.8f;
                    if (Math.Abs(curveLeft[j].XValue - curveRight[j].XValue) < 0.001f)
                    {
                        factor = InterPolation._InterPo.LagLinerInter(Tb, curveLeft[j], curveRight[j], res[j]).YValue;
                        List_Factor.Add(factor);
                    }
                    else
                    {
                        factor = InterPolation._InterPo.LagLinerInter(Tb, curveLeft[j], curveRight[j]).YValue;
                        List_Factor.Add(factor);
                    }
                }
                return List_Factor;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "getOneCorrectList()函数异常!");
                return List_Factor;
            }
        }


        /// <summary>
        /// （不同图版 第三次插值）获取某一个电阻率曲线的校正值列表
        /// </summary>
        /// <param name="OneRes"></param>
        private void GetOneResValue(List<float> OneRes,int chartindex,String method,float para)
        {
            //左曲线
            List<XYValue> left = new List<XYValue>();
            //右曲线
            List<XYValue> right = new List<XYValue>();
            //中曲线
            List<XYValue> mid = new List<XYValue>();
            //左图版上的乘因子列表
            List<float> LeftChartFactors = new List<float>();
            //右图版上的乘因子列表
            List<float> RightChartFactors = new List<float>();
            try
            {
                switch (CorMethod)
                {
                    case "围岩校正":
                        getResBounder(OneRes, out left, out right, out mid);
                        break;
                }
                LeftChartFactors = getOneCorrectList(chartindex,OneRes, left, right, ListLeft,para);
                left.Clear();
                right.Clear();
                mid.Clear();
                switch (CorMethod)
                {
                    case "围岩校正":
                        getResBounder(OneRes, out left, out right, out mid);
                        break;
                }
                RightChartFactors = getOneCorrectList(chartindex,OneRes, left, right, ListRight, para);
                left.Clear();
                right.Clear();
                mid.Clear();
                float result;
                switch (chartindex)
                {
                    case 0:
                        for (int i = 0; i < List_DEPTH.Count; i++)
                        {
                            result = OneRes[i] * InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, ParaChart, LeftChartFactors[i], RightChartFactors[i]);
                            if (result <= 3000 && result >= 0)
                                List_RACECHM_AC.Add(result);
                            else List_RACECHM_AC.Add(List_RACECHM[i]);
                        }
                        break;
                    case 1:
                        for (int i = 0; i < List_DEPTH.Count; i++)
                        {
                            result = OneRes[i] * InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, ParaChart, LeftChartFactors[i], RightChartFactors[i]);
                            if (result <= 3000 && result >= 0)
                                List_RACECLM_AC.Add(result);
                            else List_RACECLM_AC.Add(List_RACECLM[i]);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < List_DEPTH.Count; i++)
                        {
                            result = OneRes[i] * InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, ParaChart, LeftChartFactors[i], RightChartFactors[i]);
                            if (result <= 3000 && result >= 0)
                                List_RACECSHM_AC.Add(result);
                            else List_RACECSHM_AC.Add(List_RACECSHM[i]);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < List_DEPTH.Count; i++)
                        {
                            result = OneRes[i] * InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, ParaChart, LeftChartFactors[i], RightChartFactors[i]);
                            if (result <= 3000 && result >= 0)
                                List_RACECSLM_AC.Add(result);
                            else List_RACECSLM_AC.Add(List_RACECSLM[i]);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < List_DEPTH.Count; i++)
                        {
                            result = OneRes[i] * InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, ParaChart, LeftChartFactors[i], RightChartFactors[i]);
                            if (result <= 3000 && result >= 0)
                                List_RPCECHM_AC.Add(result);
                            else List_RPCECHM_AC.Add(List_RPCECHM[i]);
                        }
                        break;
                    case 5:
                        for (int i = 0; i < List_DEPTH.Count; i++)
                        {
                            result = OneRes[i] * InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, ParaChart, LeftChartFactors[i], RightChartFactors[i]);
                            if (result <= 3000 && result >= 0)
                                List_RPCECLM_AC.Add(result);
                            else List_RPCECLM_AC.Add(List_RPCECLM[i]);
                        }
                        break;
                    case 6:
                        for (int i = 0; i < List_DEPTH.Count; i++)
                        {
                            result = OneRes[i] * InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, ParaChart, LeftChartFactors[i], RightChartFactors[i]);
                            if (result <= 3000 && result >= 0)
                                List_RPCECSHM_AC.Add(result);
                            else List_RPCECSHM_AC.Add(List_RPCECSHM[i]);
                        }
                        break;
                    case 7:
                        for (int i = 0; i < List_DEPTH.Count; i++)
                        {
                            result = OneRes[i] * InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, ParaChart, LeftChartFactors[i], RightChartFactors[i]);
                            if (result <= 3000 && result >= 0)
                                List_RPCECSLM_AC.Add(result);
                            else List_RPCECSLM_AC.Add(List_RPCECSLM[i]);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 清空视电阻率左中右坐标临时数组
        /// </summary>
        private void ClearXY()
        {
            XLeft.Clear();
            YLeft.Clear();
            XRight.Clear();
            YRight.Clear();
            XMid.Clear();
            YMid.Clear();
        }
        /// <summary>
        /// 二分查找法索引最临近的两个XValue:Xleft和Xright 存入XLeft,XRight,XMid数组
        /// </summary>
        /// <param name="range">索引范围</param>
        /// <param name="target">测量值数组</param>
        private void Nearest2Point(List<XYValue> range,List<float> target)
        {
            int left = 0;
            int right = range.Count - 1;
            int mid = 0;
            for(int i=0;i<target.Count;i++)
            {
                while (left < right)
                {
                    mid = left + (right - left) / 2;
                    if (range[mid].ParaValue == target[i])
                    {
                        XMid[i] = range[mid].XValue;
                        YMid[i] = range[mid].YValue;
                        range[mid].ValueOnChart = true;
                        break;
                    }
                    else if (range[mid].ParaValue > target[i])
                    {
                        if ((right != mid) || (left != mid))
                            right = mid;
                        if(range[right-1]==range[left])
                        {
                            XLeft[i] = range[left].XValue;
                            XRight[i] = range[right].XValue;
                            YLeft[i] = range[left].YValue;
                            YRight[i] = range[right].YValue;
                            break;
                        }
                    }
                    else if (range[mid].ParaValue < target[i])
                    {
                        if ((right != mid) || (left != mid))
                            left = mid;
                        if (range[right - 1] == range[left])
                        {
                            XLeft[i] = range[left].XValue;
                            XRight[i] = range[right].XValue;
                            YLeft[i] = range[left].YValue;
                            YRight[i] = range[right].YValue;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 二分查找法索引图版曲线上最临近的两个点<XYValue>:Xleft和Xright 输出到LeftPoint,RightPoint
        /// </summary>
        /// <param name="range">索引范围</param>
        /// <param name="target">测量值</param>
        /// <param name="leftpoint">曲线左点</param>
        /// <param name="rightpoint">曲线右点</param>
        private void Nearest2Point(List<XYValue> range, float target,out XYValue LeftPoint,out XYValue RightPoint)
        {
            int left = 0;
            int right = range.Count-1;
            int mid = 0;
            LeftPoint = new XYValue();
            RightPoint = new XYValue();
            try
            {
                while (range.Count>0&&left < right)
                {
                    mid = left + (right - left) / 2;
                    if (range[mid].XValue == target)
                    {
                        LeftPoint = range[mid];
                        RightPoint = range[mid];
                        range[mid].ValueOnChart = true;
                        break;
                    }
                    else if (range[mid].XValue > target)
                    {
                        if ((right != mid) || (left != mid))
                            right = mid;
                        if (range[right - 1] == range[left])
                        {
                            break;
                        }
                    }
                    else if (range[mid].XValue < target)
                    {
                        if ((right != mid) || (left != mid))
                            left = mid;
                        if (range[right - 1] == range[left])
                        {
                            break;
                        }
                    }
                    else if (target < range[1].XValue && target < range[0].XValue)
                    {
                        left = 0;
                        right = 0;
                    }
                    else if (target > range[0].XValue && target > range[1].XValue)
                    {
                        left = range.Count - 1;
                        right = range.Count - 1;
                    }
                }
                LeftPoint = range[left];
                RightPoint = range[right];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\r\t Nearest2Point()函数异常！");
            }

        }

        public DataTable getCorDataTable()
        {
            try
            {
                dt_CorData.Columns.Add(new DataColumn("DEPTH"));
                dt_CorData.Columns.Add(new DataColumn("RACECHM_AC"));
                dt_CorData.Columns.Add(new DataColumn("RACECLM_AC"));
                dt_CorData.Columns.Add(new DataColumn("RACECSHM_AC"));
                dt_CorData.Columns.Add(new DataColumn("RACECSLM_AC"));
                dt_CorData.Columns.Add(new DataColumn("RPCECHM_AC"));
                dt_CorData.Columns.Add(new DataColumn("RPCECLM_AC"));
                dt_CorData.Columns.Add(new DataColumn("RPCECSHM_AC"));
                dt_CorData.Columns.Add(new DataColumn("RPCECSLM_AC"));
                for (int i = 0; i < List_DEPTH.Count; i++)
                {
                    DataRow _MergeRow = dt_CorData.NewRow();
                    object[] temp = new object[9];
                    temp[0] = (object)List_DEPTH[i];
                    temp[1] = (object)List_RACECHM_AC[i];
                    temp[2] = (object)List_RACECLM_AC[i];
                    temp[3] = (object)List_RACECSHM_AC[i];
                    temp[4] = (object)List_RACECSLM_AC[i];
                    temp[5] = (object)List_RPCECHM_AC[i];
                    temp[6] = (object)List_RPCECLM_AC[i];
                    temp[7] = (object)List_RPCECSHM_AC[i];
                    temp[8] = (object)List_RPCECSLM_AC[i];
                    dt_CorData.Rows.Add(temp);
                }
                return dt_CorData;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}