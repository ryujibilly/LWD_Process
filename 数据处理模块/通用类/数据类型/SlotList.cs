using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    /// <summary>
    /// 重载SortedList可排序列表类
    /// </summary>
    /// <typeparam name="Slot">中间数据结构体</typeparam>
    public sealed class SlotList<Slot> : SortedList<Double, Slot>
    {
        public static SlotList<Slot> _slotList = new SlotList<Slot>();
        public SlotList<Slot> _SlotList
        {
            get { return _slotList; }
            set { _slotList = value; }
        }
        private Slot svalue;
        public Slot Svalue
        {
            get { return Svalue; }
            set { svalue = value; }
        }
        /// <summary>
        /// 获取相应Key的Value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Slot getValue(Double key)
        {
            return _slotList[key];
        }
    }
}
