using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace LWD_DataProcess
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public sealed class CommonData
    {
        static string[] sep= { "="};
        /// <summary>
        /// 静态CommonData单例
        /// </summary>
        public static readonly CommonData _CD = new CommonData();
        /// <summary>
        /// 原始队列最小深度
        /// </summary>
        public double SMinDep { get; set; }
        /// <summary>
        /// 原始队列最大深度
        /// </summary>
        public double SMaxDep { get; set; }
        /// <summary>
        /// Slist深度差
        /// </summary>
        public double SDInterval { get; set; }
        /// <summary>
        /// 插值队列最小深度
        /// </summary>
        public double AMinDep { get; set; }
        /// <summary>
        /// 插值队列的最大深度
        /// </summary>
        public double AMaxDep { get; set; }
        /// <summary>
        /// AIPList深度差
        /// </summary>
        public double AInterval { get; set; }
        /// <summary>
        /// 时深匹配后的原始数据列表
        /// </summary>
        //public static SlotList<Slot> SList { get; set; }
        public SlotList<Slot> SList = new SlotList<Slot>();
        /// <summary>
        /// 等间隔插值后的数据列表
        /// </summary>
        //public static SlotList<Slot> AIPList { get; set; }//AfterInterPolationList
        public SlotList<Slot> AIPList = new SlotList<Slot>();
        /// <summary>
        /// 滤波后的数据列表
        /// </summary>
        public SlotList<Slot> FSList = new SlotList<Slot>();

        /// <summary>
        /// 图版索引关键字集合
        /// </summary>
        public static String[] ChartIndex { get; set; }
        /// <summary>
        /// 参数表达式字符串
        /// </summary>
        public static ConcurrentQueue<String> ChartParaExpression { get; set; }
        /// <summary>
        /// 图版参数名称
        /// </summary>
        public static ConcurrentQueue<String> ChartPara { get; set; }
        /// <summary>
        /// 图版参数数值
        /// </summary>
        public static ConcurrentQueue<String> ParaValue { get; set; }
        /// <summary>
        /// X坐标字符串
        /// </summary>
        public static ConcurrentQueue<String> Xvalue { get; set; }
        /// <summary>
        /// Y坐标字符串
        /// </summary>
        public static ConcurrentQueue<String> Yvalue { get; set; }
        /// <summary>
        /// 同步锁
        /// </summary>
        private object SListObj = new object();
        private object AIPListObj = new object();
        private object listObj { get; set; }

        private CommonData()
        {
            ChartParaExpression = new ConcurrentQueue<string>();
            ChartPara = new ConcurrentQueue<string>();
            ParaValue = new ConcurrentQueue<string>();
            Xvalue = new ConcurrentQueue<string>();
            Yvalue = new ConcurrentQueue<string>();
        }

        /// <summary>
        /// 获取列表中的一个深度为dep的元素
        /// </summary>
        /// <returns></returns>
        public Slot GetSListItem(SlotList<Slot> list,Double dep)
        {
            Slot slotRet = null;
            if (list.Equals(SList))
                listObj = SListObj;
            if (list.Equals(AIPList))
                listObj = AIPListObj;  
            lock (listObj)
            {
                if (list.Count > 0)
                {
                    slotRet = list[list.IndexOfKey(dep)];
                    return slotRet;
                }
                else return null;
            }
        }
        /// <summary>
        /// 向列表添加元素
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="item">元素</param>
        /// <returns>是否添加成功(同步操作可能等待)</returns>
        public bool SaveList(SlotList<Slot> list, Slot item)
        {
            bool bRet = false;
            if (list.Equals(SList))
                listObj = SListObj;
            if (list.Equals(AIPList))
                listObj = AIPListObj;
            lock (listObj)
            {
                list.Add(int.Parse(item.WellDepth), item);
                bRet = true;
                return bRet;
            }
        }
        public static void getSKeyMinMax()
        {
            try
            {
                DataStruct.SKeysMin = _CD.SList._SlotList.Keys[0];
                DataStruct.SKeysMax = _CD.SList._SlotList.Keys[_CD.SList._SlotList.Count - 1];
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        public Boolean getChartParaExpresion()
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Boolean getXValue()
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Boolean getYValue()
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public void getParaValue(String paraExpresion)
        {
            String[] para_value = new String[2];
            try
            {
                para_value=paraExpresion.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                ChartPara.Enqueue(para_value[0]);
                ChartPara.Enqueue(para_value[1]);
            }
            catch (Exception)
            {
                Debug.WriteLine("获取参数表达式"+para_value[0]+"="+para_value[1]);
            }

        }
    }
}
