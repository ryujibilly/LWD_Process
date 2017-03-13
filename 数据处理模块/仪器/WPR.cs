using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace LWD_DataProcess
{
    /// <summary>
    /// WPR环境校正类
    /// </summary>
    public class WPR
    {
        private WPR() { }
        /// <summary>
        /// 单例模式对象
        /// </summary>
        public readonly static WPR _wpr = new WPR();
        //DEPTH   RACECHM   RACECLM   RACECSHM    RACECSLM    RPCECHM   RPCECLM   RPCECSHM    RPCECSLM
        #region 字段
        private float depth=0.0f;
        private float racechm = 0.0f;
        private float raceclm=0.0f;
        private float racecshm = 0.0f;
        private float racecslm = 0.0f;
        private float rpcechm = 0.0f;
        private float rpceclm = 0.0f;
        private float rpcecshm = 0.0f;
        private float rpcecslm = 0.0f;
        private float rm = 0.0f;
        private float hd = 0.0f;
        private String toolsize = "6.75";
        private float[] RmRange = { 0.05f, 0.1f, 0.2f, 0.5f, 1, 2 };
        private float[] sbrRange = { 0.2f, 0.5f, 1, 2, 5, 10 };
        private float[] tbRange = { 0.5f, 1, 2, 4, 6, 8, 10, 15, 20, 30, 40, 50 };
        private float sbr = 0.0f;
        private float bedthickness = 0.0f;
        private String corMethod = "";
        private String chartPara = "";
        private String[] chartnameleft = new String[8];
        private String[] chartnameright = new String[8];
        private String[] chartnamemid = new String[8];
        private Boolean paraOnChart = false;
        private float xleft = -999.25f;
        private float xright = -999.25f;
        private float xmid = -999.25f;
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

        #endregion

        #region 属性
        /// <summary>
        /// 视电阻率左侧X坐标
        /// </summary>
        public float XLeft
        {
            get { return xleft; }
            set { xleft = value; }
        }
        /// <summary>
        /// 视电阻率X坐标
        /// </summary>
        public float XMid
        {
            get { return xmid; }
            set { xmid = value; }
        }
        /// <summary>
        /// 视电阻率左侧X坐标
        /// </summary>
        public float XRight
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
            get { return bedthickness; }
            set { bedthickness = value; }
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
        /// PCECHM队列
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
        /// PCECHM表
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
                switch (rawCurveNames[i])
                {
                    case "RACECHM":
                        ChartPrefix[i] = ToolSize + "_2M_R36A_";
                        break;
                    case "RACECLM":
                        ChartPrefix[i] = ToolSize + "_400K_R36A_";
                        break;
                    case "RACECSHM":
                        ChartPrefix[i] = ToolSize + "_2M_R22.5A_";
                        break;
                    case "RACECSLM":
                        ChartPrefix[i] = ToolSize + "_400K_R22.5A_";
                        break;
                    case "RPCECHM":
                        ChartPrefix[i] = ToolSize + "_2M_R36P_";
                        break;
                    case "RPCECLM":
                        ChartPrefix[i] = ToolSize + "_400K_R36P_";
                        break;
                    case "RPCECSHM":
                        ChartPrefix[i] = ToolSize + "_2M_R22.5P_";
                        break;
                    case "RPCECSLM":
                        ChartPrefix[i] = ToolSize + "_400K_R22.5P_";
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
                    ChartNameMid[i] = ChartPrefix[i] + this.CorMethod + MidParaString;
                ParaOnChart = true;
            }
            //该参数有左右边界,前8个位左边界图版，后8个位右边界图版
            else if(para==-999.25f)
            {
                for (int i = 0; i < 8; i++)
                {
                    ChartNameLeft[i] = ChartPrefix[i] + this.CorMethod + LeftParaString;
                }
                for (int i = 0; i < 8; i++)
                {
                    ChartNameRight[i] = ChartPrefix[i] + this.CorMethod + RightParaString;
                }
                ParaOnChart = false;
            }
        }
        /// <summary>
        /// 获取泥浆电阻率左右边界
        /// </summary>
        /// <param name="mudRes"></param>
        public void getRmBounder()
        {
            for (int i = 0; i < RmRange.Length-1; i++)
            {
                if ((RmRange[i] < Rm && RmRange[i + 1] > Rm) ||
                    (RmRange[i] < Rm && RmRange[i + 1] < Rm) ||
                    (RmRange[i] > Rm && RmRange[i + 1] > Rm))
                {
                    RmLeft = RmRange[i];
                    LeftParaString = "Rm=" + RmLeft.ToString();
                    RmRight = RmRange[i + 1];
                    RightParaString = "Rm=" + RmRight.ToString();
                }
                else if (Rm == RmRange[i])
                {
                    RmMid = Rm;
                    MidParaString = "Rm=" + RmMid.ToString();
                }
                else if (Rm == RmRange[i + 1])
                {
                    RmMid = Rm;
                    MidParaString = "Rm=" + RmMid.ToString();
                }
            }
        }
        /// <summary>
        /// 获取围岩电阻率左右边界:给SBRLeft\SBRRight\SBRMid赋值；给ParaString赋值
        /// 1.参数在中间，左右边界即左右边界；
        /// 2.参数在边界外，左右边界代表反向延长线左右前后两点
        /// </summary>
        public void getSbrBounder()
        {
            for(int i=0;i<sbrRange.Length-1;i++)
            {
                if ((sbrRange[i] < SBR && sbrRange[i + 1] > SBR) ||
                    (sbrRange[i] < SBR && sbrRange[i + 1] < SBR) ||
                    (sbrRange[i] > SBR && sbrRange[i + 1] > SBR))
                {
                    SBRLeft = sbrRange[i];
                    LeftParaString = "SBR=" + SBRLeft.ToString();
                    SBRRight = sbrRange[i + 1];
                    RightParaString = "SBR=" + SBRRight.ToString();
                }
                else if (SBR == sbrRange[i])
                {
                    SBRMid = SBR;
                    MidParaString = "SBR=" + RmMid.ToString();
                }
                else if (SBR == sbrRange[i + 1])
                {
                    SBRMid = SBR;
                    MidParaString = "SBR=" + RmMid.ToString();
                }
            }
        }
        /// <summary>
        /// 获取电阻率左右点X坐标:
        /// 1.视电阻率在图版内，左右X坐标代表插值左右X坐标；
        /// 2.视电阻率在图版外，左右边界代表反向延长线左右前后两点
        /// </summary>
        public void getXBounder()
        {
            for (int i = 0; i < sbrRange.Length - 1; i++)
            {
                if ((sbrRange[i] < SBR && sbrRange[i + 1] > SBR) ||
                    (sbrRange[i] < SBR && sbrRange[i + 1] < SBR) ||
                    (sbrRange[i] > SBR && sbrRange[i + 1] > SBR))
                {
                    SBRLeft = sbrRange[i];
                    LeftParaString = "SBR=" + SBRLeft.ToString();
                    SBRRight = sbrRange[i + 1];
                    RightParaString = "SBR=" + SBRRight.ToString();
                }
                else if (SBR == sbrRange[i])
                {
                    SBRMid = SBR;
                    MidParaString = "SBR=" + RmMid.ToString();
                }
                else if (SBR == sbrRange[i + 1])
                {
                    SBRMid = SBR;
                    MidParaString = "SBR=" + RmMid.ToString();
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
        /// 获取图版上的校正系数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public float GetFactor(float value)
        {
            return factor;
        }
        /// <summary>
        /// 匹配图版
        /// </summary>
        /// <param name="rawcurvenames">曲线名称数组</param>
        public void MatchChart(String[] rawcurvenames)
        {
            float paramid = -999.25f;
            switch(CorMethod)
            {
                case "介电常数": 
                    break;
                case "井眼校正": getRmBounder();paramid = RmMid;
                    break;
                case "围岩校正": getSbrBounder();paramid = SBRMid;
                    break;
                case "侵入校正": 
                    break;
                case "各向异性":
                    break;
                default:
                    break;
            }
            SetChartPrefix(rawcurvenames);
            SetChartPostfix(paramid);
        }
        /// <summary>
        /// 定位电阻率测量值所在图版位置：
        /// </summary>
        public void PositionXValue(float XValue)
        {

        }
    }
}
