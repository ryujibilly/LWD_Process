using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;

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
        private float[] tbRange = { 0.5f, 1, 2, 4, 6, 8, 10, 15, 20, 30, 40, 50 };
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
        private float curveParaLeft = -999.25f;
        private float curveParaRight = -999.25f;
        private float curveParaMid = -999.25f;

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
        /// 当前校正类型对应的左边界图版名集
        /// </summary>
        private String[] CurChartLeft = new String[8];
        /// <summary>
        /// 当前校正类型对应的右边界图版名集
        /// </summary>
        private String[] CurChartRight = new String[8];
        /// <summary>
        /// 当前校正类型对应的唯一界图版名集
        /// </summary>
        private String[] CurChartMid = new String[8];

        /// <summary>
        /// 8条曲线，对应的图版名前缀数组，长度为8
        /// </summary>
        private String[] ChartPrefix = new String[8];

        /// <summary>
        /// 8条曲线，对应的图版名后缀，后缀名相同
        /// </summary>
        private String ChartPostfix = "";
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

        /// <summary>
        /// 围岩层厚左边界
        /// </summary>
        private float TbLeft = 0.0f;
        /// <summary>
        /// 围岩层厚刚好在图版上
        /// </summary>
        private float TbMid = -999.25f;
        /// <summary>
        /// 围岩层厚右边界
        /// </summary>
        private float TbRight = 0.0f;

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

        private float factor = 0.0f;
        /// <summary>
        /// 校正系数
        /// </summary>
        public float Factor
        {
            get { return factor; }
            set { factor = value; }
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
        /// 插值处理后的校正系数
        /// </summary>
        public List<float> InterY
        {
            get
            {
                return intery;
            }

            set
            {
                intery = value;
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

        public float[] TbRange
        {
            get
            {
                return tbRange;
            }

            set
            {
                tbRange = value;
            }
        }
        /// <summary>
        /// 左曲线参数
        /// </summary>
        public float CurveParaLeft
        {
            get
            {
                return curveParaLeft;
            }

            set
            {
                curveParaLeft = value;
            }
        }
        /// <summary>
        /// 曲线参数
        /// </summary>
        public float CurveParaMid
        {
            get
            {
                return curveParaMid;
            }

            set
            {
                curveParaMid = value;
            }
        }
        /// <summary>
        /// 右曲线参数
        /// </summary>
        public float CurveParaRight
        {
            get
            {
                return curveParaRight;
            }

            set
            {
                curveParaRight = value;
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
                    LeftParaString = "Rm=" + RmLeft.ToString();
                    RmRight = RmRange[i + 1];
                    ParaRight = RmRight;
                    RightParaString = "Rm=" + RmRight.ToString();
                    break;
                }
                else if (Rm == RmRange[i])
                {
                    RmMid = Rm;
                    MidParaString = "Rm=" + RmMid.ToString();
                    ParaMid = RmMid;
                    break;
                }
                else if (Rm == RmRange[i + 1])
                {
                    RmMid = Rm;
                    MidParaString = "Rm=" + RmMid.ToString();
                    ParaMid = RmMid;
                    break;
                }
                else if(RmRange[i] < Rm && RmRange[i + 1] < Rm)
                {
                    RmLeft = RmRange[i];
                    ParaLeft = RmLeft;
                    LeftParaString = "Rm=" + RmLeft.ToString();
                    RmRight = RmRange[i + 1];
                    ParaRight = RmRight;
                    RightParaString = "Rm=" + RmRight.ToString();
                }
                else if (RmRange[i] > Rm && RmRange[i + 1] > Rm)
                {
                    RmLeft = RmRange[i];
                    ParaLeft = RmLeft;
                    LeftParaString = "Rm=" + RmLeft.ToString();
                    RmRight = RmRange[i + 1];
                    ParaRight = RmRight;
                    RightParaString = "Rm=" + RmRight.ToString();
                    break;
                }
            }
        }
        /// <summary>
        /// 获取围岩校正围岩电阻率左右边界:给SBRLeft\SBRRight\SBRMid赋值；给ParaString赋值
        /// 1.参数在中间，左右边界即左右边界；
        /// 2.参数在边界外，左右边界代表反向延长线左右前后两点
        /// </summary>
        public void getSbrBounder()
        {
            for(int i=0;i<sbrRange.Length-2;i++)
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
                else if(SBR>sbrRange[i]&&SBR>sbrRange[i+1])
                {
                    SBRLeft = sbrRange[i];
                    ParaLeft = SBRLeft;
                    LeftParaString = "SBR=" + SBRLeft.ToString("F1");
                    SBRRight = sbrRange[i + 1];
                    ParaRight = SBRRight;
                    RightParaString = "SBR=" + SBRRight.ToString("F1");
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
            }
        }

        /// <summary>
        /// 获取围岩层厚左右边界:给TbLeft\TbRight\TbMid赋值
        /// 1.参数在中间，左右边界即左右边界；
        /// 2.参数在边界外，左右边界代表反向延长线左右前后两点
        /// </summary>
        public void getTbBounder()
        {
            for (int i = 0; i < TbRange.Length - 2; i++)
            {
                if ((TbRange[i] < Tb && TbRange[i + 1] > Tb))
                {
                    TbLeft = TbRange[i];
                    CurveParaLeft = TbLeft;
                    TbRight = TbRange[i + 1];
                    CurveParaRight = TbRight;
                    break;
                }
                else if (Tb == TbRange[i])
                {
                    TbMid = Tb;
                    CurveParaMid = TbMid;
                    break;
                }
                else if (Tb == TbRange[i + 1])
                {
                    TbMid = Tb;
                    CurveParaMid = TbMid;
                    break;
                }
                else if (Tb > TbRange[i] && Tb > TbRange[i + 1])
                {
                    TbLeft = TbRange[i];
                    CurveParaLeft = TbLeft;
                    TbRight = TbRange[i + 1];
                    CurveParaRight = TbRight;
                }
                else if (Tb < TbRange[i] && Tb < TbRange[i + 1])
                {
                    TbLeft = TbRange[i];
                    CurveParaLeft = TbLeft;
                    TbRight = TbRange[i + 1];
                    CurveParaRight = TbRight;
                    break;
                }
            }
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
                case "井眼校正": getRmBounder();ParaMid = RmMid;
                    break;
                case "围岩校正": getSbrBounder();getTbBounder(); ParaMid = SBRMid;
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

        public void CorrectFlow(String method)
        {
            switch(method)
            {
                case "介电常数":
                    break;
                case "井眼校正":
                    GetChartXYValueList(RmLeft, RmRight, RmMid);
                    break;
                case "围岩校正":
                    GetChartXYValueList(TbLeft, TbRight, TbMid);
                    break;
                case "侵入校正":
                    break;
                case "各向异性":
                    break;
                default:
                    break;
            }//输入校正类型、校正参数，返回 图版XY值列表之列表 
            GetACValue();
            //获取测量值在对应图版上临近的两个点Xleft和Xright,校正系数Yleft,YRigth
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
                XYValue tempxy = new XYValue();
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
                    tempxy.XValue = float.Parse(dt.Rows[i].ItemArray[0].ToString());
                    tempxy.YValue = float.Parse(dt.Rows[i].ItemArray[1].ToString());
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
        /// 获取图版XY值列表之列表
        /// </summary>
        public void GetChartXYValueList(float pvleft,float pvright,float pvmid)
        {
            try
            {
                if (!ParaOnChart)//参数不在图版上
                    for (int i = 0; i < 8; i++)
                    {
                        //左边界 
                        ListLeft.Add(GetXYList(ChartNameLeft[i], pvleft));
                        //右边界
                        ListRight.Add(GetXYList(ChartNameRight[i], pvright));
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
        /// 获取测量值 插值处理后的校正数据 List_RACECHM=>List_RACECHM_AC...
        /// </summary>
        private void GetACValue()
        {
            try
            {
                float[] leftValue = new float[List_DEPTH.Count];
                float[] rightValue = new float[List_DEPTH.Count];
                //外循环 8条曲线 : j
                for (int j = 0; j < 8; j++)
                {
                    switch (j)
                    {
                        case 0://RACECHM
                            Nearest2Point(ListLeft[j], List_RACECHM);//左图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RACECHM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RACECHM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RACECHM[i], YLeft[i], YRight[i]);
                                }
                            }
                            Nearest2Point(ListRight[j], List_RACECHM);//右图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RACECHM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RACECHM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RACECHM[i], YLeft[i], YRight[i]);
                                }
                            }
                            for (int i = 0; i < List_DEPTH.Count; i++)
                                List_RACECHM_AC.Add(InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, List_RACECHM[i], leftValue[i], rightValue[i]));
                            break;
                        case 1://RACECLM
                            Nearest2Point(ListLeft[j], List_RACECLM);//左图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RACECLM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RACECLM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RACECLM[i], YLeft[i], YRight[i]);
                                }
                            }
                            Nearest2Point(ListRight[j], List_RACECLM);//右图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RACECLM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RACECLM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RACECLM[i], YLeft[i], YRight[i]);
                                }
                            }
                            for (int i = 0; i < List_DEPTH.Count; i++)
                                List_RACECLM_AC.Add(InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, List_RACECLM[i], leftValue[i], rightValue[i]));
                            break;
                        case 2://RACECSHM
                            Nearest2Point(ListLeft[j], List_RACECSHM);//左图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RACECSHM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RACECSHM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RACECSHM[i], YLeft[i], YRight[i]);
                                }
                            }
                            Nearest2Point(ListRight[j], List_RACECSHM);//右图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RACECSHM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RACECSHM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RACECSHM[i], YLeft[i], YRight[i]);
                                }
                            }
                            for (int i = 0; i < List_DEPTH.Count; i++)
                                List_RACECSHM_AC.Add(InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, List_RACECSHM[i], leftValue[i], rightValue[i]));
                            break;
                        case 3://RACECSLM
                            Nearest2Point(ListLeft[j], List_RACECSLM);//左图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RACECSLM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RACECSLM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RACECSLM[i], YLeft[i], YRight[i]);
                                }
                            }
                            Nearest2Point(ListRight[j], List_RACECSLM);//右图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RACECSLM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RACECSLM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RACECSLM[i], YLeft[i], YRight[i]);
                                }
                            }
                            for (int i = 0; i < List_DEPTH.Count; i++)
                                List_RACECSLM_AC.Add(InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, List_RACECSLM[i], leftValue[i], rightValue[i]));
                            break;
                        case 4://RPCECHM
                            Nearest2Point(ListLeft[j], List_RPCECHM);//左图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RPCECHM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RPCECHM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RPCECHM[i], YLeft[i], YRight[i]);
                                }
                            }
                            Nearest2Point(ListRight[j], List_RPCECHM);//右图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RPCECHM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RPCECHM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RPCECHM[i], YLeft[i], YRight[i]);
                                }
                            }
                            for (int i = 0; i < List_DEPTH.Count; i++)
                                List_RPCECHM_AC.Add(InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, List_RPCECHM[i], leftValue[i], rightValue[i]));
                            break;
                        case 5://RPCECLM
                            Nearest2Point(ListLeft[j], List_RPCECLM);//左图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RPCECLM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RPCECLM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RPCECLM[i], YLeft[i], YRight[i]);
                                }
                            }
                            Nearest2Point(ListRight[j], List_RPCECLM);//右图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RPCECLM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RPCECLM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RPCECLM[i], YLeft[i], YRight[i]);
                                }
                            }
                            for (int i = 0; i < List_DEPTH.Count; i++)
                                List_RPCECLM_AC.Add(InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, List_RPCECLM[i], leftValue[i], rightValue[i]));
                            break;
                        case 6://RPCECSHM
                            Nearest2Point(ListLeft[j], List_RPCECSHM);//左图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RPCECSHM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RPCECSHM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RPCECSHM[i], YLeft[i], YRight[i]);
                                }
                            }
                            Nearest2Point(ListRight[j], List_RPCECSHM);//右图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RPCECSHM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RPCECSHM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RPCECSHM[i], YLeft[i], YRight[i]);
                                }
                            }
                            for (int i = 0; i < List_DEPTH.Count; i++)
                                List_RPCECSHM_AC.Add(InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, List_RPCECSHM[i], leftValue[i], rightValue[i]));
                            break;
                        case 7://RPCECSLM
                            Nearest2Point(ListLeft[j], List_RPCECSLM);//左图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RPCECSLM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListLeft[j].ElementAt(i).ValueOnChart)
                                {
                                    leftValue[i] = List_RPCECSLM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RPCECSLM[i], YLeft[i], YRight[i]);
                                }
                            }
                            Nearest2Point(ListRight[j], List_RPCECSLM);//右图版
                            //内循环 记录条数 : i
                            for (int i = 0; i < List_DEPTH.Count; i++)
                            {
                                //测量值在图版曲线上 
                                if (ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RPCECSLM[i] * YMid[i];
                                }
                                //测量值部在图版曲线上 
                                else if (!ListRight[j].ElementAt(i).ValueOnChart)
                                {
                                    rightValue[i] = List_RPCECSLM[i] *
                                        InterPolation._InterPo.LagLinerInter(XLeft[i], XRight[i], List_RPCECSLM[i], YLeft[i], YRight[i]);
                                }
                            }
                            for (int i = 0; i < List_DEPTH.Count; i++)
                                List_RPCECSLM_AC.Add(InterPolation._InterPo.LagLinerInter(ParaLeft, ParaRight, List_RPCECSLM[i], leftValue[i], rightValue[i]));
                            break;
                    }
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
                    if (range[mid].XValue == target[i])
                    {
                        XMid[i] = range[mid].XValue;
                        YMid[i] = range[mid].YValue;
                        range[mid].ValueOnChart = true;
                        break;
                    }
                    else if (range[mid].XValue > target[i])
                    {
                        if ((right != mid) || (left != mid))
                            right = mid;
                        else
                        {
                            XLeft[i] = range[left].XValue;
                            XRight[i] = range[right].XValue;
                            YLeft[i] = range[left].YValue;
                            YRight[i] = range[right].YValue;
                            break;
                        }
                    }
                    else if (range[mid].XValue < target[i])
                    {
                        if ((right != mid) || (left != mid))
                            left = mid;
                        else
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
    }
}
