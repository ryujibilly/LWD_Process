using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    public class XYValue
    {
        private float xvalue;
        private float yvalue;
        public float XValue
        {
            get { return xvalue; }
            set { yvalue = value; }
        }
        public float YValue
        {
            get { return yvalue; }
            set { yvalue = value; }
        }

        public XYValue() { }
        public XYValue(float x,float y)
        {
            xvalue = x;
            yvalue = y;
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

    }
}
