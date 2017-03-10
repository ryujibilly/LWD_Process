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
    public sealed class WPR
    {
        public static WPR _wpr = new WPR();
        //DEPTH   RACECHM   RACECLM   RACECSHM    RACECSLM    RPCECHM   RPCECLM   RPCECSHM    RPCECSLM
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
        
        /// <summary>
        /// 8条曲线，对应的图版名前缀数组，长度为8
        /// </summary>
        private String[] ChartPrefix = new String[8];

        /// <summary>
        /// 8条曲线，对应的图版名后缀数组，长度为8
        /// </summary>
        private String[] ChartPostfix = new String[8];
        ///// <summary>
        ///// 8条曲线对应的图版全名数组
        ///// </summary>
        //private String[] ChartName = new String[8];
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
        /// 围岩电阻率Ω.m
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
        /// 坐标List
        /// </summary>
        public List<Coordinates> co=new List<Coordinates>();



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
        public void SetChartPostfix()
        {

        }

        public String[] GetChartName(String[] chartname)
        {
            for (int i = 0; i < 8; i++)
                chartname[i] = ChartPrefix[i] + ChartPostfix[i];
            return chartname;
        }
        /// <summary>
        /// 获取泥浆电阻率左右边界
        /// </summary>
        /// <param name="mudRes"></param>
        public void getRmRange()
        {
            for (int i = 0; i < RmRange.Length-1; i++)
            {
                if (RmRange[i] < Rm && RmRange[i + 1] > Rm)
                {
                    RmLeft = RmRange[i];
                    RmRight = RmRange[i + 1];
                }
                else if (Rm == RmRange[i])
                    RmMid = Rm;
                else if (Rm == RmRange[i + 1])
                    RmMid = Rm;
            }
        }
        /// <summary>
        /// 获取围岩电阻率左右边界
        /// </summary>
        public void getSbrRange()
        {
            for(int i=0;i<sbrRange.Length-1;i++)
            {
                if (sbrRange[i] < SBR && sbrRange[i + 1] > SBR)
                {
                    SBRLeft = sbrRange[i];
                    SBRRight = sbrRange[i + 1];
                }
                else if (SBR == sbrRange[i])
                    SBRMid = SBR;
                else if (SBR == sbrRange[i + 1])
                    SBRMid = SBR;
            }
        }
        /// <summary>
        /// 剔除超差
        /// </summary>
        public void FiltOverproof()
        {

        }
    }
}
