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
        public GammaUnit gu;
        public PipeSize DrillPipeSize;
        ///// <summary>
        ///// 钻铤尺寸
        ///// </summary>
        //public PipeSize DrillPipeSize
        //{
        //    get { return drillPipeSize; }
        //    set { DrillPipeSize = value; }
        //}

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



        internal void Start(double ct)
        {
            _Gamma.count =ct;
            cal_DrillPipeSize(gu);
            cal_CircleInterval(gu);
            cal_MudDensity(gu);
            if (UseWellDiameter)
                cal_WellDiameter(gu);
        }
        internal void cal_DrillPipeSize(GammaUnit gu)
        {
            switch (DrillPipeSize)
            {
                //4.75英寸
                case PipeSize.inch475:
                    {
                        if(gu.Equals(GammaUnit.CPS))
                            _Gamma.count += -1.1429 * PipeWallSize + 65.3166;
                        else if(gu.Equals(GammaUnit.API)) 
                            _Gamma.count+= -5.72052 * PipeWallSize + 326.9277;
                    }
                    break;
                //6.75英寸
                case PipeSize.inch675:
                    {
                        if(gu.Equals(GammaUnit.CPS))
                            _Gamma.count += -0.97318 * PipeWallSize + 156.93415;
                        else if(gu.Equals(GammaUnit.API))
                            _Gamma.count += -4.87106 * PipeWallSize + 785.50581;
                    }
                    break;
                //默认4.75英寸
                default:
                    {
                        if (gu.Equals(GammaUnit.CPS))
                            _Gamma.count += -1.1429 * PipeWallSize + 65.3166;
                        else if (gu.Equals(GammaUnit.API))
                            _Gamma.count += -5.72052 * PipeWallSize + 326.9277;
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
                    _Gamma.count += -0.63498 * CircleInterval + 0.68865;
                else if (gu.Equals(GammaUnit.API))
                    _Gamma.count += -3.17829 * CircleInterval + 3.4469;
            }
            if (DrillPipeSize == PipeSize.inch675)
            {
                if (gu.Equals(GammaUnit.CPS))
                    _Gamma.count += -0.41234 * CircleInterval + 0.16211;
                else if (gu.Equals(GammaUnit.API))
                    _Gamma.count += -2.06387 * CircleInterval + 0.81134;
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
                        _Gamma.count += 2.48586 * MudDensity - 2.54418;
                    else if (gu.Equals(GammaUnit.API))
                        _Gamma.count += 12.4425 * MudDensity - 12.73434;
                }
                if (BariteContainment)
                {
                    if (gu.Equals(GammaUnit.CPS))
                        _Gamma.count += 11.73873 * MudDensity - 0.61853;
                    else if (gu.Equals(GammaUnit.API))
                        _Gamma.count += 18.87944 * MudDensity - 9.8961;
                }
            }
            else if (DrillPipeSize == PipeSize.inch675)
            {
                if (!BariteContainment)
                {
                    if (gu.Equals(GammaUnit.CPS))
                        _Gamma.count += 10.79281 * MudDensity - 11.19341;
                    else if (gu.Equals(GammaUnit.API))
                        _Gamma.count += 54.02138 * MudDensity - 56.0266;
                }
                if (BariteContainment)
                {
                    if (gu.Equals(GammaUnit.CPS))
                        _Gamma.count += 3.77184 * MudDensity - 1.97707;
                    else if (gu.Equals(GammaUnit.API))
                        _Gamma.count += 58.75599 * MudDensity - 3.09597;
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
