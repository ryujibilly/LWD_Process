using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LWD_DataProcess.Decoder
{
    /// <summary>
    /// 解码器类-Singleton
    /// </summary>
    sealed class Decoder
    {
        Decoder() { }
        public static readonly Decoder _decoder=new Decoder();
    }
}
