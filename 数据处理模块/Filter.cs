using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    public class Filter
    {
        static double MediumValue = 0.0d;
        public static Filter _Filter = new Filter();
        #region 中位平均滤波
        /// <summary>
        /// 中位值平均滤波法
        /// </summary>
        /// <param name="slist">输入列表</param>
        /// <param name="flist">输出列表</param>
        /// <returns>滤波成功</returns>
        public bool MidAvr(SlotList<Slot> slist, SlotList<Slot> flist,int indexOfparas)
        {
            double[] temp=new double[slist.Count];
            for (int i = 0; i < slist.Count;i++ )
                temp[i] = ((Slot)(slist.Values)).Paras[indexOfparas];
            MediumValue= MediumAverage(temp);
            return true;
        }
        /// <summary>
        /// 求中位平均值
        /// </summary>
        /// <param name="colomn">求和参数数组</param>
        /// <returns>返回中位平均值</returns>
        internal double MediumAverage(double[] colomn)
        {
            int count,i, j;
            double sum = 0.0d,temp=0.0d;
            //double[] newColomn = new Double[colomn.Length];
            for(j=0;j<colomn.Length-1;j++)
                for(i=0;i<colomn.Length-j;i++)
                {
                    if(colomn[i]>colomn[i+1])
                    {
                        temp = colomn[i];
                        colomn[i] = colomn[i + 1];
                        colomn[i + 1] = temp;
                    }
                }
            for (count = 1; count < colomn.Length-1; count++)
            {
                sum += colomn[count];
            }
            return sum / (colomn.Length - 2);
        }
        #endregion

        #region Kalman滤波

        #endregion

        #region
        #endregion

        #region
        #endregion
    }
}
