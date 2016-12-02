using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWD_DataProcess
{
    /// <summary>
    /// WPR环境校正类
    /// </summary>
    public sealed class WPR
    {
        //DEPTH RACECHM RACECLM RACECSHM    RACECSLM    RPCECHM RPCECLM RPCECSHM    RPCECSLM
        private float depth=0.0f;
        private float racechm = 0.0f;
        private float raceclm=0.0f;
        private float racecshm = 0.0f;
        private float racecslm = 0.0f;
        private float rpcechm = 0.0f;
        private float rpceclm = 0.0f;
        private float rpcecshm = 0.0f;
        private float rpcecslm = 0.0f;

        /// <summary>
        /// 深度
        /// </summary>
        public float DEPTH
        {
            get { return depth; }
            set { DEPTH = value; }
        }
        /// <summary>
        /// 2Mhz长源距幅度比
        /// </summary>
        public float RACECHM
        {
            get { return racechm; }
            set { RACECHM = value; }
        }
        /// <summary>
        /// 400Khz长源距幅度比
        /// </summary>
        public float RACECLM
        {
            get { return raceclm; }
            set { RACECLM = value; }
        }
        /// <summary>
        /// 2Mhz短源距幅度比
        /// </summary>
        public float RACECSHM
        {
            get { return racecshm; }
            set { RACECSHM = value; }
        }
        /// <summary>
        /// 400Khz短源距幅度比
        /// </summary>
        public float RACECSLM
        {
            get { return racecslm; }
            set { RACECSLM = value; }
        }
        /// <summary>
        /// 2Mhz长源距相位差
        /// </summary>
        public float RPCECHM
        {
            get { return rpcechm; }
            set { RPCECHM = value; }
        }
        /// <summary>
        /// 400Khz短源距相位差
        /// </summary>
        public float RPCECLM
        {
            get { return rpceclm; }
            set { RPCECLM = value; }
        }
        /// <summary>
        /// 2Mhz短源距相位差
        /// </summary>
        public float RPCECSHM
        {
            get { return rpcecshm; }
            set { RPCECSHM = value; }
        }
        /// <summary>
        /// 400Khz短源距相位差
        /// </summary>
        public float RPCECSLM
        {
            get { return rpcecslm; }
            set { RPCECSLM = value; }
        }

    }
}
