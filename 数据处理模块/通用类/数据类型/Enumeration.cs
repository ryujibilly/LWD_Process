using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    /// <summary>
    /// 仪器类型
    /// </summary>
    public enum Ins_Type { GDIR, WPR, GRT, CGR };
    /// <summary>
    /// 插值计算类型
    /// </summary>
    public enum COI_Type { Linear, Akima, ThreeTimes, Kriging, None };//Calculus of Interpolation
    /// <summary>
    /// 滤波算法类型 Filter Algorithm1.中位值平均滤波2.卡尔曼滤波3.加权递推平均滤波4.限幅平均滤波
    /// </summary>
    public enum FA_Type { MidAvr, Kaman, WeiAvr, AmpLimit, None };
    /// <summary>
    /// 钻进状态
    /// </summary>
    public enum DrillStatus { Unknown, Start, Drilling, PullUp, PushDown, Stop, HoldOn }//未知、开始、钻进、下放、上提、静止、悬停
    /// <summary>
    /// 从原始数据文件中分离出的数据类型
    /// </summary>
    public enum PipeSize { inch475, inch675 }
    /// <summary>
    /// 计数率单位
    /// </summary>
    public enum GammaUnit { API, CPS }
    /// <summary>
    /// 测井方式:随钻测井,钻后测
    /// </summary>
    public enum LogMethod { LWD,LAD}
    /// <summary>
    /// 列类型
    /// </summary>
    public enum ColType { Text, Integer }
}
