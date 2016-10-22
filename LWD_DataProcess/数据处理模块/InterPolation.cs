using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    /// <summary>
    /// 插值算法类
    /// </summary>
    public class InterPolation
    {
        #region 属性
        /// <summary>
        /// 最小键值
        /// </summary>
        public double keyMax { get; set; }
        /// <summary>
        /// 最大键值
        /// </summary>
        public double keyMin { get; set; }
        /// <summary>
        /// 插值间隔
        /// </summary>
        public double Interval { get; set; }
        /// <summary>
        /// 新插值点数
        /// </summary>
        public int points { get; set; }
        /// <summary>
        /// AIP列表键值数组
        /// </summary>
        public double[] AIPKeys { get; set; }
        /// <summary>
        /// 测量值的参数列数
        /// </summary>
        public int ParaLength { get; set; }
        #endregion

        /// <summary>
        /// 插值算法的单例模式
        /// </summary>
        public static InterPolation _InterPo = new InterPolation();
        public InterPolation() { }
        /// <summary>
        /// 完整参数初始化
        /// </summary>
        /// <param name="minKey">最小深度</param>
        /// <param name="maxKey">最大深度</param>
        /// <param name="interval">插值深度间隔</param>
        /// <param name="paraLength">测量值参数个数</param>
        public InterPolation(double minKey,double maxKey,double interval,int paraLength)
        {
            ParaLength = paraLength;
            keyMin = minKey;
            keyMax = maxKey;
            Interval = interval;
        }

        /// <summary>
        /// 线性插值算法(拉格朗日插值法原型，已知参考点为2个点时)
        /// </summary>
        /// <param name="slist">带入赋值后的SlotList列表</param>
        /// <param name="aiplist">带入未赋值的AIPList列表，运算中赋值</param>
        /// <returns>是否完成插值</returns>
        public bool LinearInterPolation(SlotList<Slot> slist,SlotList<Slot> aiplist)
        {
            int i = 0, j = 0, k = 0;
            double tempKey = 0.0;
            slist = slist._SlotList;
            Slot minValue = new Slot();
            Slot maxValue = new Slot();
            Slot Value = new Slot();
            bool bRet = false;
            try
            {

                //double tempSKey=0.0;// SList 列表键值标记位
                //double tempAKey=0.0;//AIPList列表键值标记位
                //构建AIPKeys数组，作为插值列表AIPList的键值列
                CreateAIPKeys();
                if (points > 0)
                {
                    //AIP列表首部
                    slist.TryGetValue(keyMin, out minValue);//获取minValue
                    aiplist.Add(keyMin, minValue);//插入minValue到AIPList
                    System.Diagnostics.Trace.WriteLine(++k + "\t :aiplist当前键::TKey->" + keyMin.ToString());
                    tempKey = keyMin;
                    //获取AIP列表的插值部分键值对
                    while ((i < slist._SlotList.Count) && (j < points))
                    {
                        //Yj=Lagrange(Xi,Xi+1,Xj,Yi,Yi+1);
                        //Xi=slist.Keys[i],Xi+1=slist.Keys[i+1],X=AIPKeys[j+1]
                        //Yi=slist.Values[i],Yi+1=slist.Values[i+1]

                        //AIPList键<SList键，AIPKeys键后移，插入AIP列表
                        if (tempKey != AIPKeys[j + 1] && AIPKeys[j + 1] < slist.Keys[i + 1])
                        {
                            tempKey = AIPKeys[j + 1];
                            Value.Paras = Lagrange(slist.Keys[i], slist.Keys[i + 1], AIPKeys[j + 1],
                                          slist.Values[i].Paras, slist.Values[i + 1].Paras);
                            aiplist.Add(AIPKeys[j + 1], Value);
                            System.Diagnostics.Trace.WriteLine(++k+"\t :aiplist当前键::TKey->" + AIPKeys[j + 1].ToString());

                            j++;
                            continue;
                        }
                        //AIPList键=>SList键，SList的Keys后移，不插入AIP列表
                        else if (tempKey != AIPKeys[j + 1] && AIPKeys[j + 1] >= slist._SlotList.Keys[i + 1])
                        {
                            tempKey = slist.Keys[i + 1];
                            Value.Paras = Lagrange(slist.Keys[i], slist.Keys[i + 1], AIPKeys[j + 1],
                                          slist.Values[i].Paras, slist.Values[i + 1].Paras);
                            aiplist.Add(slist.Keys[i + 1], slist.Values[i + 1]);
                            System.Diagnostics.Trace.WriteLine(++k + "\t :aiplist当前键::TKey->" + slist.Keys[i + 1].ToString());

                            i++;
                            continue;
                        }
                        else break;
                    }
                    //AIP列表末尾
                    slist.TryGetValue(keyMax, out maxValue);
                    aiplist.Add(keyMax, maxValue);
                    System.Diagnostics.Trace.WriteLine(++k + "\t :aiplist当前键::TKey->" + keyMax.ToString());
                    bRet = true;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("\t :aiplist当前键::TKey->" + slist.Keys[i + 1].ToString() + "\r\n" + ex.ToString());
                bRet=false;
                DataStruct.ReadyToPrint = false;
            }
            DataStruct.ReadyToPrint = true;
            return bRet;
        }
        /// <summary>
        /// 拉格朗日（在N=1时为线性）插值多项式
        /// </summary>
        /// <param name="x0">键i</param>
        /// <param name="x1">键i+1</param>
        /// <param name="x">插值点键j</param>
        /// <param name="para0">值i</param>
        /// <param name="para1">值i+1</param>
        /// <returns>插值点j</returns>
        private double[] Lagrange(double x0, double x1, double x, double[] para0, double[] para1)//,out double[] para
        {
            double[] para = new double[ParaLength];
            try
            {
                for (int i = 0; i < ParaLength; i++)
                {
                    para[i] = Math.Round(para0[i] * ((x - x1) / (x0 - x1)) + para1[i] * ((x - x0) / (x1 - x0)),3);
                }
                return para;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 键值数组初始化
        /// </summary>
        public void CreateAIPKeys()
        {
            points = (int)Math.Floor((keyMax - keyMin) / Interval);
            AIPKeys = new double[points + 2];
            AIPKeys[0] = keyMin;
            for (int i = 0; i < points; i++)
                AIPKeys[i + 1] =Math.Round(AIPKeys[i] + 0.1d,3);
            AIPKeys[points + 1] = keyMax;
        }
        public bool Akima(SlotList<Slot> slist, SlotList<Slot> aiplist)
        {
            return false;
        }
        public bool ThreeTimes(SlotList<Slot> slist, SlotList<Slot> aiplist)
        {
            return false;
        }
        public bool Kriging(SlotList<Slot> slist,SlotList<Slot> aiplist)
        {
            return false;
        }


    }
}
