using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace GininDev.Common.DataTools.Helpers
{
    public class StringHelper
    {
        public static Regex RgName = new Regex("(?<aName>[\\w|\\s]+)", RegexHelper.Regopts);


        public static string GetJsonShortPath(string aCapt)
        {
            var sRev = ReverseString(aCapt);
            var iPos = 0;
            var bStart = false;
            var bS2 = false;
            var sRes = string.Empty;
            var arChars = sRev.ToCharArray();
            for (int i = 0; i < sRev.Length; i++)
            {
                if (arChars[i] == ']')
                {
                    iPos = i;
                    bStart = true;
                }
                else if (arChars[i] == '\'' && bStart && !bS2)
                {
                    iPos += 1;
                    bS2 = true;
                }
                else if (arChars[i] == '\'' && bStart && bS2)
                {
                    sRes = sRev.Substring(iPos + 1, i - 2); //not include '
                    break;
                }
                else if (!bStart && !bS2 && arChars[i] == '.')
                {
                    sRes = sRev.Substring(0, i);
                    break;
                }
                else
                {
                    sRes = sRev;
                }
            }
            var z = ReverseString(sRes);
            return z;
        }

        /// <summary>
        /// 字符串反转
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReverseString(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }  

        /// <summary>
        ///     串分割
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public static List<string> SplitString(string sName)
        {
            var arNames = new List<string>();

            MatchCollection mis = RgName.Matches(sName);
            if (mis.Count <= 0) return arNames;
            for (int i = 0; i < mis.Count; i++)
            {
                string sCmd = mis[i].Result("${aName}");
                arNames.Add(sCmd);
            }
            return arNames;
        }

        /// <summary>
        ///     去掉扩展名，
        ///     主要为避开部分服务路径中带参数的情况
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public static string SplitByString(string sPath)
        {
            int iPos = sPath.IndexOf(".exe", StringComparison.InvariantCultureIgnoreCase);
            if (iPos >= 0)
            {
                sPath = sPath.Substring(0, iPos + 4);
            }
            return sPath;
        }

        /// <summary>
        ///     序列化对象
        /// </summary>
        /// <param name="objA"></param>
        /// <returns></returns>
        public static string SeriaString(object objA)
        {
            var sw = new StringWriter();
            var jser = new JsonSerializer { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = Formatting.Indented;
                jser.Serialize(jsonWriter, objA);
            }
            string sers = sw.ToString();
            return sers;
        }
    }
}