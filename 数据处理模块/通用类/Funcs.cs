using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace LWD_DataProcess
{
    /// <summary>
    /// 扩展基础操作-Singleton
    /// </summary>
    sealed class Funcs
    {
        public static readonly Funcs _funcs = new Funcs();
        /// <summary>
        /// 字符串数组打印
        /// </summary>
        /// <param name="receive">要打印的字符串数组</param>
        public void print(String[] receive)
        {
            try
            {
                int c = 0;
                for (int i = 0; i < receive.Length; i++)
                {
                    System.Diagnostics.Trace.Write(string.Format(@receive[i]) + '\t');
                    c++;
                    if (c == receive.Length)
                    {
                        System.Diagnostics.Trace.Write("\n");
                        c = 0;
                    }
                }
                System.Diagnostics.Trace.Write("\r\n");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString()+"\r\n");
            }
        }
        /// <summary>
        /// 二维字符串数组打印
        /// </summary>
        /// <param name="receive">要打印的字符串数组</param>
        public void print(int row,String[] receive)
        {
            try
            {
                System.Diagnostics.Trace.Write(string.Format(@receive[row-1]) + '\t' + "\r\n");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString() + "\r\n");
            }
        }
        /// <summary>
        /// 字符串打印
        /// </summary>
        /// <param name="receive">要打印的字符串</param>
        public void print(String receive)
        {
            try
            {
                if (receive != "")
                {
                    System.Diagnostics.Trace.Write(string.Format(@receive) + '\t');
                    System.Diagnostics.Trace.Write("\r\n");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString() + "\r\n");
            }
        }
        /// <summary>
        /// HexString 转 byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(String s)
        {
            try
            {
                if (s != null)
                {
                    char[] seperater = { ' ' };
                    String[] str = s.Split(seperater, StringSplitOptions.RemoveEmptyEntries);
                    byte[] b = new byte[str.LongLength];
                    for (long i = 0; i < str.LongLength; i++)
                    {
                        b[i] = byte.Parse(str[i], System.Globalization.NumberStyles.AllowHexSpecifier);
                    }
                    return b;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// byte[] 转 HexString
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            try
            {
                StringBuilder sb = new StringBuilder(data.Length * 3);
                foreach (byte b in data)
                    sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
                return sb.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }
        //判断是否是0~9数表示的字符串 的正则表达式
        public static Boolean IsNumStr(String str)
        {
            String pattern = @"^[+-]?(?!0\d)\d+(\.[0-9]+)?$";//^[+-]?([1-9][0-9]*|0)(\.[0-9]+)?$
            return Regex.IsMatch(str, pattern);
        }
        /// <summary>
        /// 是否中文字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>boolean</returns>
        public static Boolean IsChineseStr(String str)
        {
            String pattern = @"[\u4e00-\u9fa5]";
            return Regex.IsMatch(str, pattern);
        }
        /// <summary>
        /// 是否由26个英文字母组成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsEnglishStr(String str)
        {
            String pattern = @"^[A-Za-z]";
            return Regex.IsMatch(str, pattern);
        }
        /// <summary>
        /// 是否参数表达式（含"="）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsParameterExpression(String str)
        {
            if (str.Contains("="))
                return true;
            else return false;
        }
        /// <summary>
        /// 科学记数法正则表达式
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是/否</returns>
        public static Boolean IsScienceNumber(String str)
        {
            String pattern = @"\d+\.\d+([Ee]-?\d+)?";
            return Regex.IsMatch(str, pattern);
        }
        //public static Boolean IsScienceNumber(String str)
        //{
        //    if (str.Contains("e+")|| str.Contains("e-"))
        //        return true;
        //    else return false;
        //}
        /// <summary>
        /// 科学计数法转换成小数点计数法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String convertScienceNumber(String str)
        {
            String result = null;
            String[] temp = null;
            temp = str.Split(DataStruct.ScienceNumSign, StringSplitOptions.RemoveEmptyEntries);
            result = (Funcs.StringToDouble(temp[0]) * Math.Pow(10, Funcs.StringToDouble(temp[1]))).ToString("0.000000");
            return result;
        }
        public static int GetRows(string FilePath)
        {
            String[] temp = null;
            using (StreamReader read = new StreamReader(FilePath, Encoding.Default))
            {
                temp = read.ReadToEnd().Split(DataStruct.rn, StringSplitOptions.RemoveEmptyEntries);
                return temp.Length;
            }
        }
        public static int GetRows_U(string FilePath)
        {
            using (StreamReader read = new StreamReader(FilePath, Encoding.Default))
            {
                return read.ReadToEnd().Split('\n').Length;
            }
        }
        public static String DoubleArrayToString(Double[] array,int start,int end)
        {
            String str = null;
            for (int i = start; i < end; i++)
                str += Math.Round(array[i], 3)+"\t";
            return str;
        }
        public static Double StringToDouble(String str)
        {
            Double d = 0.0d;
            d = Convert.ToDouble(str);
            return d;
        }
    }
}
