using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    /// <summary>
    /// 伽马校正类-Singleton
    /// </summary>
    public sealed class Gamma
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        public static readonly Gamma _Gamma = new Gamma();

        private double cps=0.0d;
        /// <summary>
        /// CPS参数
        /// </summary>
        public double CPS
        {
            get { return cps; }
            set { CPS = value; }
        }
        private double api=0.0d;
        /// <summary>
        /// API参数
        /// </summary>
        public double API
        {
            get { return api; }
            set { API = value; }
        }
        /// <summary>
        /// 计数率
        /// </summary>
        public double count { get; set; }
        /// <summary>
        /// 伽马单位
        /// </summary>
        public GammaUnit gu;
        /// <summary>
        /// 井眼尺寸/钻铤尺寸
        /// </summary>
        public PipeSize DrillPipeSize;
        /// <summary>
        /// 钻铤壁厚校正因子
        /// </summary>
        public double Factor_pws= 0.0d;
        /// <summary>
        /// 环空间隔校正因子
        /// </summary>
        public double Factor_ci = 0.0d;
        /// <summary>
        /// 泥浆密度校正因子
        /// </summary>
        public double Factor_md = 0.0d;
        ///// <summary>
        ///// 钻铤尺寸
        ///// </summary>

        public double PipeWallSize;
        ///// <summary>
        ///// 钻铤壁厚
        ///// </summary>
        //public double PipeWallSize
        //{
        //    get { return pipeWallSize; }
        //    set { PipeWallSize = value; }
        //}
        public double CircleInterval;
        ///// <summary>
        ///// 环空尺寸
        ///// </summary>
        //public double CircleInterval
        //{
        //    get { return circleInterval; }
        //    set { CircleInterval = value; }
        //}

        public Boolean BariteContainment;
        ///// <summary>
        ///// 是否含重晶石
        ///// </summary>
        //public Boolean BariteContainment
        //{
        //    get { return bariteContainment; }
        //    set { BariteContainment = value; }
        //}

        public double MudDensity;
        ///// <summary>
        ///// 泥浆密度
        ///// </summary>
        //public double MudDensity
        //{
        //    get { return mudDensity; }
        //    set { MudDensity = value; }
        //}
        public Boolean UseWellDiameter=false;
        ///// <summary>
        ///// 是否计算偏心井径
        ///// </summary>
        //public Boolean UseWellDiameter
        //{
        //    get { return useWellDiameter; }
        //    set { UseWellDiameter = value; }
        //}
        public double WellDiameter;
        ///// <summary>
        ///// 偏心井径
        ///// </summary>
        //public double WellDiameter
        //{
        //    get { return wellDiameter; }
        //    set { WellDiameter = value; }
        //}


        internal double getFactors()
        {
            if (Factor_pws != 1)
                cal_DrillPipeSize(gu);
            else Factor_pws = 1;
            if (Factor_ci != 1)
                cal_CircleInterval(gu);
            else Factor_ci = 1;
            cal_MudDensity(gu);
            return 1 / (Factor_pws * Factor_md * Factor_ci);
        }

        internal void Start(double ct)
        {
            _Gamma.count = ct/(Factor_pws * Factor_md * Factor_ci) ;//乘因子
        }
        /// <summary>
        /// 钻铤壁厚（内径）
        /// </summary>
        /// <param name="gu"></param>
        internal void cal_DrillPipeSize(GammaUnit gu)
        {
            switch (DrillPipeSize)
            {
                //4.75英寸
                case PipeSize.inch475:
                    {
                        if(gu.Equals(GammaUnit.CPS))
                            _Gamma.Factor_pws = -1.1429 * PipeWallSize + 65.3166;
                        else if(gu.Equals(GammaUnit.API)) 
                            _Gamma.Factor_pws = 0.01507 * PipeWallSize +0.1387;
                    }
                    break;
                //6.75英寸
                case PipeSize.inch675:
                    {
                        if(gu.Equals(GammaUnit.CPS))
                            _Gamma.Factor_pws = -0.97318 * PipeWallSize + 156.93415;
                        else if(gu.Equals(GammaUnit.API))
                            _Gamma.Factor_pws = 0.01283 * PipeWallSize - 1.06928;
                    }
                    break;
                //默认4.75英寸
                default:
                    {
                        if (gu.Equals(GammaUnit.CPS))
                            _Gamma.Factor_pws = -1.1429 * PipeWallSize + 65.3166;
                        else if (gu.Equals(GammaUnit.API))
                            _Gamma.Factor_pws = 0.01507 * PipeWallSize + 0.1387;
                    }
                    break;
            }
        }
        /// <summary>
        /// 环空间隙校正
        /// </summary>
        internal void cal_CircleInterval(GammaUnit gu)
        {
            if (DrillPipeSize  == PipeSize.inch475)
            {
                if (gu.Equals(GammaUnit.CPS))
                    _Gamma.Factor_ci = -0.63498 * CircleInterval + 0.68865;
                else if (gu.Equals(GammaUnit.API))
                    _Gamma.Factor_ci = 0.00794 * CircleInterval + 0.98826;
            }
            if (DrillPipeSize == PipeSize.inch675)
            {
                if (gu.Equals(GammaUnit.CPS))
                    _Gamma.Factor_ci = -0.41234 * CircleInterval + 0.16211;
                else if (gu.Equals(GammaUnit.API))
                    _Gamma.Factor_ci = 0.006 * CircleInterval + 1.00138;
            }
        }
        /// <summary>
        /// 泥浆密度校正
        /// </summary>
        internal void cal_MudDensity(GammaUnit gu)
        {
            if (DrillPipeSize == PipeSize.inch475)
            {
                if (!BariteContainment)
                {
                    if (gu.Equals(GammaUnit.CPS))
                        _Gamma.Factor_md = 2.48586 * MudDensity - 2.54418;
                    else if (gu.Equals(GammaUnit.API))
                        _Gamma.Factor_md = -0.03205 * MudDensity +1.03285;//
                }
                if (BariteContainment)
                {
                    if (gu.Equals(GammaUnit.CPS))
                        _Gamma.Factor_md = 11.73873 * MudDensity - 0.61853;
                    else if (gu.Equals(GammaUnit.API))
                        _Gamma.Factor_md = -0.04933 * MudDensity + 1.02718;//
                }
            }
            else if (DrillPipeSize == PipeSize.inch675)
            {
                if (!BariteContainment)
                {
                    if (gu.Equals(GammaUnit.CPS))
                        _Gamma.Factor_md = 10.79281 * MudDensity - 11.19341;
                    else if (gu.Equals(GammaUnit.API))
                        _Gamma.Factor_md = -0.16027 * MudDensity + 1.16554;//
                }
                if (BariteContainment)
                {
                    if (gu.Equals(GammaUnit.CPS))
                        _Gamma.Factor_md = 3.77184 * MudDensity - 1.97707;
                    else if (gu.Equals(GammaUnit.API))
                        _Gamma.Factor_md = -0.15719 * MudDensity + 0.96335;//
                }
            }
        }
        internal void cal_WellDiameter(GammaUnit gu)
        {
            if (gu.Equals(GammaUnit.CPS))
                _Gamma.count += -0.23393 * Math.Pow(WellDiameter, 3) + 0.63014 *
                            Math.Pow(WellDiameter, 2) - 0.67427 * WellDiameter - 0.64317;
            else if (gu.Equals(GammaUnit.API))
                _Gamma.count += -1.17091 * Math.Pow(WellDiameter, 3) + 3.15405 *
                            Math.Pow(WellDiameter, 2) - 3.37494 * WellDiameter + 3.21909;
        }
    }
}
