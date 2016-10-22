using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    /// <summary>
    /// SLOT数据类型：中间数据(“深度”为Key值，且唯一)
    /// </summary>
    public class Slot
    {
        //日期/时间
        private String dateTime;
        public String DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }
        //Key值，井深
        private String wellDepth;
        public String WellDepth
        {
            get { return wellDepth; }
            set { wellDepth = value; }
        }
        //参数数组
        private Double [] paras;
        public Double[] Paras
        {
            get { return paras; }
            set { paras = value; }
        }
        //钻进状态
        private DrillStatus drillStatus;
        public DrillStatus DrillStatus
        {
            get { return drillStatus; }
            set { drillStatus = value; }
        }
        /// <summary>
        /// 空初始化
        /// </summary>
        public Slot()
        {

        }
        /// <summary>
        /// 初始化1:完全初始化(平行化)
        /// </summary>
        /// <param name="datetime">日期/时间</param>
        /// <param name="welldepth">Key值，井深</param>
        /// <param name="parameters">参数数组</param>
        /// <param name="drillstatus">钻进状态</param>
        public Slot(String datetime,String welldepth,Double[] parameters,DrillStatus drillstatus)
        {
            this.dateTime = datetime;
            this.wellDepth = welldepth;
            this.paras = parameters;
            this.drillStatus = drillstatus;
        }
        /// <summary>
        /// 初始化2：深度-测量值(Key-Value模式)
        /// </summary>
        /// <param name="welldepth">Key值，井深</param>
        /// <param name="parameters">参数数组</param>
        public Slot(String welldepth, Double[] parameters)
        {
            this.dateTime = "-/-";
            this.wellDepth = welldepth;
            this.paras = parameters;
            this.drillStatus = DrillStatus.Unknown;
        }
        //public static readonly Slot _slot=new Slot();
    }
    
}
