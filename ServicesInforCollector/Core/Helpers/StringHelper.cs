using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ServicesInforCollector.Core.Helpers
{
    public class StringHelper
    {
        public static Regex RgName = new Regex("(?<aName>[\\w|\\s]+)", RegexHelper.Regopts);

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
    }
}