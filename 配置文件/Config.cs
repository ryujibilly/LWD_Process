using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace LWD_DataProcess
{
    /// <summary>
    /// 配置的信息类
    /// </summary>
    public class ConfigInfo
    {
        /// <summary>
        /// 默认文件夹路径
        /// </summary>
        public string FoldBrowserPath { get; set; }
        /// <summary>
        /// 深度间隔
        /// </summary>
        public Double DepthInterval { get; set; }
        /// <summary>
        /// 插值算法
        /// </summary>
        public COI_Type CT { get; set; }
        /// <summary>
        /// 滤波算法
        /// </summary>
        public FA_Type FT { get; set; }
        /// <summary>
        /// 仪器类型
        /// </summary>
        public Ins_Type IT { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string DeviceSN = "00000000";
        /// <summary>
        /// 网络密钥
        /// </summary>
        public string NetKey = "00000000";
        /// <summary>
        /// 配置信息是否有效
        /// </summary>
        public bool IsValue = false;
        ///// <summary>
        ///// 单个数据库文件路径
        ///// </summary>
        //public String DB_PATH { get; set; }
        /// <summary>
        /// 井信息数据库文件路径
        /// </summary>
        public String DBPath_Well { get; set; }
        /// <summary>
        /// 图版配置数据库文件路径
        /// </summary>
        public String DBPath_CorrectionChart { get; set; }
    }

    /// <summary>
    /// 配置类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 全局配置信息-单例模式
        /// </summary>
        public static ConfigInfo CfgInfo = new ConfigInfo();

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig()
        {
            bool bIsSave = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);

                //默认文件夹
                XmlElement eleFoldBrowserPath = doc.CreateElement("FoldBrowserPath");
                XmlText txtFoldBrowserPath = doc.CreateTextNode(CfgInfo.FoldBrowserPath);

                //深度间隔
                XmlElement eleDepthInterval = doc.CreateElement("DepthInterval");
                XmlText txtDepthInterval = doc.CreateTextNode(CfgInfo.DepthInterval.ToString());

                //插值算法
                XmlElement eleCT = doc.CreateElement("CT");
                XmlText txtCT = doc.CreateTextNode(CfgInfo.CT.ToString());

                //滤波算法
                XmlElement eleFT = doc.CreateElement("FT");
                XmlText txtFT = doc.CreateTextNode(CfgInfo.FT.ToString());

                //仪器类型
                XmlElement eleIT = doc.CreateElement("IT");
                XmlText txtIT = doc.CreateTextNode(CfgInfo.IT.ToString());

                ////数据库文件地址
                //XmlElement eleDB_PATH = doc.CreateElement("DB_PATH");
                //XmlText txtDB_PATH = doc.CreateTextNode(CfgInfo.DB_PATH.ToString());

                //测井存档数据库文件地址
                XmlElement eleDBPath_Well = doc.CreateElement("DBPath_Well");
                XmlText txtDBPath_Well = doc.CreateTextNode(CfgInfo.DBPath_Well.ToString());

                //环境校正图版数据库文件地址
                XmlElement eleDBPath_CorrectionChart = doc.CreateElement("DBPath_CorrectionChart");
                XmlText txtDBPath_CorrectionChart = doc.CreateTextNode(CfgInfo.DBPath_CorrectionChart.ToString());

                XmlNode newElem = doc.CreateNode("element", "config", "");

                newElem.AppendChild(eleFoldBrowserPath);
                newElem.LastChild.AppendChild(txtFoldBrowserPath);

                newElem.AppendChild(eleDepthInterval);
                newElem.LastChild.AppendChild(txtDepthInterval);

                newElem.AppendChild(eleCT);
                newElem.LastChild.AppendChild(txtCT);

                newElem.AppendChild(eleFT);
                newElem.LastChild.AppendChild(txtFT);

                newElem.AppendChild(eleIT);
                newElem.LastChild.AppendChild(txtIT);

                //newElem.AppendChild(eleDB_PATH);
                //newElem.LastChild.AppendChild(txtDB_PATH);

                newElem.AppendChild(eleDBPath_Well);
                newElem.LastChild.AppendChild(txtDBPath_Well);

                newElem.AppendChild(eleDBPath_CorrectionChart);
                newElem.LastChild.AppendChild(txtDBPath_CorrectionChart);


                XmlElement root = doc.CreateElement("config");
                root.AppendChild(newElem);
                doc.AppendChild(root);
                doc.Save("NodeSettings.xml");
                bIsSave = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                bIsSave = false;
            }
            return bIsSave;
        }

        /// <summary>
        /// 获取配置信息 
        /// </summary>
        /// <returns></returns>
        public static bool GetConfig()
        {
            bool bIsGet = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("config.xml");

                CfgInfo.FoldBrowserPath = doc.SelectSingleNode("//FoldBrowserPath").InnerText;
                CfgInfo.DepthInterval = Double.Parse(doc.SelectSingleNode("//DepthInterval").InnerText);
                CfgInfo.IT = ToIns_Type(doc.SelectSingleNode("//IT").InnerText);
                CfgInfo.CT = ToCOI_Type(doc.SelectSingleNode("//CT").InnerText);
                CfgInfo.FT = ToFA_Type(doc.SelectSingleNode("//FT").InnerText);
                //CfgInfo.DB_PATH = doc.SelectSingleNode("//DB_PATH").InnerText;
                CfgInfo.DBPath_Well = doc.SelectSingleNode("//DBPath_Well").InnerText;
                CfgInfo.DBPath_CorrectionChart = doc.SelectSingleNode("//DBPath_CorrectionChart").InnerText;

                XmlDocument xmldoc = new XmlDocument();
                if (File.Exists("NodeSettings.xml"))
                {
                    xmldoc.Load("NodeSettings.xml");
                    XmlNodeList xmlnode = xmldoc.SelectSingleNode("Settings").ChildNodes;
                    foreach (XmlElement element in xmlnode)
                    {
                        CfgInfo.DeviceSN = element.Attributes["deviceSN"].Value;
                        CfgInfo.NetKey = element.Attributes["netKey"].Value;
                        //CfgInfo.DB_PATH = element.Attributes["dbPath"].Value;
                        CfgInfo.DBPath_Well = element.Attributes["dbPath_Well"].Value;
                        CfgInfo.DBPath_CorrectionChart = element.Attributes["dbPath_CorrectionChart"].Value;
                        Properties.Settings.Default.DBPath_ChartInfo = CfgInfo.DBPath_CorrectionChart;
                        Properties.Settings.Default.DBPath_WellInfo = CfgInfo.DBPath_Well;
                        break;
                    }
                }
                bIsGet = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                bIsGet = false;          
            }
            return bIsGet;
        }
        internal static Ins_Type ToIns_Type(String str)
        {
            /*
             * *居中伽马
                感应电阻率
                电磁波电阻率
             * */

            Ins_Type it = Ins_Type.CGR;
            switch(str)
            {
                case "居中伽马": it= Ins_Type.CGR;
                    break;
                case "感应电阻率": it= Ins_Type.GDIR;
                    break;
                case "电磁波电阻率": it= Ins_Type.WPR;
                    break;
                default: it= Ins_Type.CGR;
                    break;
            }
            return it;   
        }
        /**
            1.中位值平均滤波
            2.卡尔曼滤波
            3.加权递推平均滤波
            4.限幅滤波
         */
        internal static FA_Type ToFA_Type(String str)
        {

            FA_Type ft = FA_Type.None;
            switch (str)
            {
                case "1.中位值平均滤波": ft = FA_Type.MidAvr;
                    break;
                case "2.卡尔曼滤波": ft = FA_Type.Kaman;
                    break;
                case "3.加权递推平均滤波": ft = FA_Type.WeiAvr;
                    break;
                case "4.限幅滤波": ft = FA_Type.AmpLimit;
                    break;
                default: ft = FA_Type.MidAvr;
                    break;
            }
            return ft;
        }
        /*
         * 1.线性插值
           2.Akima插值
           3.三次样条插值
           4.Kriging插值
         */
        internal static COI_Type ToCOI_Type(String str)

        {
            COI_Type ct = COI_Type.Linear;
            switch (str)
            {
                case "1.线性插值": ct = COI_Type.Linear;
                    break;
                case "2.Akima插值": ct = COI_Type.Akima;
                    break;
                case "3.三次样条插值": ct = COI_Type.ThreeTimes;
                    break;
                case "4.Kriging插值": ct = COI_Type.Kriging;
                    break;
                default: ct = COI_Type.Linear;
                    break;
            }
            return ct;
        }

    }
}
