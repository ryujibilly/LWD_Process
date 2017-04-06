using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    public sealed class GDIR
    {
        public static readonly GDIR _gdir = new GDIR();

        private float mudRes;
        private float borehole;
        private float tb;
        private float sbr;

        /// <summary>
        /// 泥浆电阻率
        /// </summary>
        public float MudRes
        {
            get
            {
                return mudRes;
            }

            set
            {
                mudRes = value;
            }
        }
        /// <summary>
        /// 井眼尺寸
        /// </summary>
        public float Borehole
        {
            get
            {
                return borehole;
            }

            set
            {
                borehole = value;
            }
        }
        /// <summary>
        /// 目的层厚
        /// </summary>
        public float Tb
        {
            get
            {
                return tb;
            }

            set
            {
                tb = value;
            }
        }
        /// <summary>
        /// 围岩电阻率
        /// </summary>
        public float Sbr
        {
            get
            {
                return sbr;
            }

            set
            {
                sbr = value;
            }
        }
    }
}
