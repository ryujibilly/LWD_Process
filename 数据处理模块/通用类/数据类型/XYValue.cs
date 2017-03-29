using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    public class XYValue
    {
        private float xvalue=-999.25f;
        private float yvalue=-999.25f;
        private float paraValue=-999.25f;
        /// <summary>
        /// X坐标
        /// </summary>
        public float XValue
        {
            get { return xvalue; }
            set { xvalue = value; }
        }
        /// <summary>
        /// Y坐标
        /// </summary>
        public float YValue
        {
            get { return yvalue; }
            set { yvalue = value; }
        }

        public XYValue() { }
        public XYValue(float x,float y)
        {
            XValue = x;
            YValue = y;
        }
        public XYValue(float x,float y,float pv)
        {
            XValue = x;
            YValue = y;
            ParaValue = pv;
        }
        public XYValue(float x,float y,float pv,Boolean voc)
        {
            XValue = x;
            YValue = y;
            ParaValue = pv;
            ValueOnChart = voc;
        }
        /// <summary>
        /// 测量值是否在校正图版曲线上
        /// </summary>
        private Boolean valueOnChart = false;
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
        /// 曲线参数
        /// </summary>
        public float ParaValue
        {
            get
            {
                return paraValue;
            }

            set
            {
                paraValue = value;
            }
        }
    }
}
