using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public sealed class CommonData
    {
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
        /// 同步锁
        /// </summary>
        private object SListObj = new object();
        private object AIPListObj = new object();
        private object listObj { get; set; }

        private CommonData()
        {
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
    }
}
