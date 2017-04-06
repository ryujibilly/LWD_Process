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

        /// <summary>
        /// WPR泥浆电阻率
        /// </summary>
        public float WPR_MudRes { get; set; }
        /// <summary>
        /// WPR井眼尺寸
        /// </summary>
        public float WPR_Borehole { get; set; }
        /// <summary>
        /// WPR校正围岩电阻率
        /// </summary>
        public float WPR_SBR { get; set; }
        /// <summary>
        /// WPR校正目的层厚
        /// </summary>
        public float WPR_Tb { get; set; }
        /// <summary>
        /// GDIR泥浆电阻率
        /// </summary>
        public float GDIR_MudRes { get; set; }
        /// <summary>
        /// GDIR井眼尺寸
        /// </summary>
        public float GDIR_Borehole { get; set; }
        /// <summary>
        /// GDIR围岩电阻率
        /// </summary>
        public float GDIR_SBR { get; set; }
        /// <summary>
        /// GDIR目的层厚
        /// </summary>
        public float GDIR_Tb { get; set; }
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

                //WPR泥浆电阻率
                XmlElement eleWPR_MudRes = doc.CreateElement("WPR_MudRes");
                XmlText txtWPR_MudRes = doc.CreateTextNode(CfgInfo.WPR_MudRes.ToString("F3"));

                //WPR井眼尺寸
                XmlElement eleWPR_Borehole = doc.CreateElement("WPR_Borehole");
                XmlText txtWPR_Borehole = doc.CreateTextNode(CfgInfo.WPR_Borehole.ToString("F3"));

                //WPR围岩电阻率
                XmlElement eleWPR_SBR = doc.CreateElement("WPR_SBR");
                XmlText txtWPR_SBR = doc.CreateTextNode(CfgInfo.WPR_SBR.ToString("F3"));

                //WPR目的层厚
                XmlElement eleWPR_Tb = doc.CreateElement("WPR_Tb");
                XmlText txtWPR_Tb = doc.CreateTextNode(CfgInfo.WPR_Tb.ToString("F3"));

                //GDIR目的层厚
                XmlElement eleGDIR_Tb = doc.CreateElement("GDIR_Tb");
                XmlText txtGDIR_Tb = doc.CreateTextNode(CfgInfo.GDIR_Tb.ToString("F3"));

                //GDIR围岩电阻率
                XmlElement eleGDIR_SBR = doc.CreateElement("GDIR_SBR");
                XmlText txtGDIR_SBR = doc.CreateTextNode(CfgInfo.GDIR_SBR.ToString("F3"));

                //GDIR泥浆电阻率
                XmlElement eleGDIR_MudRes = doc.CreateElement("GDIR_MudRes");
                XmlText txtGDIR_MudRes = doc.CreateTextNode(CfgInfo.GDIR_MudRes.ToString("F3"));

                //GDIR井眼尺寸
                XmlElement eleGDIR_Borehole = doc.CreateElement("GDIR_Borehole");
                XmlText txtGDIR_Borehole = doc.CreateTextNode(CfgInfo.GDIR_Borehole.ToString("F3"));

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

                newElem.AppendChild(eleDBPath_Well);
                newElem.LastChild.AppendChild(txtDBPath_Well);

                newElem.AppendChild(eleDBPath_CorrectionChart);
                newElem.LastChild.AppendChild(txtDBPath_CorrectionChart);

                //WPR 校正参数
                newElem.AppendChild(eleWPR_SBR);
                newElem.LastChild.AppendChild(txtWPR_SBR);

                newElem.AppendChild(eleWPR_Tb);
                newElem.LastChild.AppendChild(txtWPR_Tb);

                newElem.AppendChild(eleWPR_Borehole);
                newElem.LastChild.AppendChild(txtWPR_Borehole);

                newElem.AppendChild(eleWPR_MudRes);
                newElem.LastChild.AppendChild(txtWPR_MudRes);
                //GDIR校正参数
                newElem.AppendChild(eleGDIR_SBR);
                newElem.LastChild.AppendChild(txtGDIR_SBR);

                newElem.AppendChild(eleGDIR_Tb);
                newElem.LastChild.AppendChild(txtGDIR_Tb);

                newElem.AppendChild(eleGDIR_Borehole);
                newElem.LastChild.AppendChild(txtGDIR_Borehole);

                newElem.AppendChild(eleGDIR_MudRes);
                newElem.LastChild.AppendChild(txtGDIR_MudRes);


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
                CfgInfo.WPR_SBR = float.Parse( doc.SelectSingleNode("//WPR_SBR").InnerText);
                CfgInfo.WPR_Tb = float.Parse(doc.SelectSingleNode("//WPR_Tb").InnerText);
                CfgInfo.WPR_Borehole = float.Parse(doc.SelectSingleNode("//WPR_Borehole").InnerText);
                CfgInfo.WPR_MudRes = float.Parse(doc.SelectSingleNode("//WPR_MudRes").InnerText);
                CfgInfo.GDIR_SBR = float.Parse(doc.SelectSingleNode("//GDIR_SBR").InnerText);
                CfgInfo.GDIR_Tb = float.Parse(doc.SelectSingleNode("//GDIR_Tb").InnerText);
                CfgInfo.GDIR_Borehole = float.Parse(doc.SelectSingleNode("//GDIR_Borehole").InnerText);
                CfgInfo.GDIR_MudRes = float.Parse(doc.SelectSingleNode("//GDIR_MudRes").InnerText);

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
                        //WPR 校正参数
                        Properties.Settings.Default.SBR = (decimal)CfgInfo.WPR_SBR;
                        Properties.Settings.Default.Tb = (decimal)CfgInfo.WPR_Tb;
                        Properties.Settings.Default.WPR_Borehole =CfgInfo.WPR_Borehole.ToString("F3");
                        Properties.Settings.Default.WPR_MudRes = (decimal)CfgInfo.WPR_Tb;
                        //GDIR 校正参数
                        Properties.Settings.Default.GDIR_SBR = (decimal)CfgInfo.GDIR_SBR;
                        Properties.Settings.Default.GDIR_Tb = (decimal)CfgInfo.GDIR_Tb;
                        Properties.Settings.Default.GDIR_BoreHole = CfgInfo.GDIR_Borehole.ToString("F3");
                        Properties.Settings.Default.GDIR_MudRes = (decimal)CfgInfo.GDIR_MudRes;
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
