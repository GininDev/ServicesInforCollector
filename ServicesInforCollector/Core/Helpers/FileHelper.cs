using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ServicesInforCollector.Core.Helpers
{
    public class FileHelper
    {
        // string result = RgFullPath.Replace(InputText,MyRegexReplace);
        // string[] results = MyRegex.Split(InputText);
        // Match m = RgFullPath.Match(InputText);
        // MatchCollection ms = MyRegex.Matches(InputText);
        // bool IsMatch = RgFullPath.IsMatch(InputText);
        // string[] GroupNames = RgFullPath.GetGroupNames();
        // int[] GroupNumbers = RgFullPath.GetGroupNumbers();

        public static Regex RgFullPath =
            new Regex(
                "(?<fullcmd>\"?(?<cmd>[a-z|A-Z|0-9|_|-|:|\\\\| |.|-]+\\.\\w+)\"?)" +
                "(?<pars>\\s+[-|/|:]?(?<argname>[a-z|A|Z|0-9]+))*",
                RegexOptions.Multiline
                | RegexOptions.RightToLeft
                | RegexOptions.CultureInvariant
                | RegexOptions.Compiled
                );

        /// <summary>
        ///     获取短文件名并检查其存在
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="di"></param>
        /// <returns></returns>
        public static string GetShortNameAndSetDirInfo(string sPath, out DirectoryInfo di)
        {
            if (sPath == null)
            {
                di = null;
                return string.Empty;
            }
            Match mi = RgFullPath.Match(sPath);
            if (!mi.Success && File.Exists(sPath))
            {
                di = null;
                return string.Empty;
            }


            sPath = sPath.TrimStart('"');
            sPath = sPath.TrimEnd('"');
            sPath = StringHelper.SplitByString(sPath);

            if (!string.IsNullOrEmpty(sPath))
            {
                var fi = new FileInfo(sPath);
                if (!File.Exists(sPath))
                    di = Directory.Exists(sPath) ? new DirectoryInfo(sPath) : fi.Directory;
                else
                    di = fi.Directory;

                string sShort = Path.GetFileNameWithoutExtension(fi.Name);

                return sShort;
            }
            di = null;
            return null;
        }


        /// <summary>
        ///     获取短文件名并检查其存在
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="di"></param>
        /// <returns></returns>
        public static string GetShortDirAndSetDirInfo(string sPath, out DirectoryInfo di)
        {
            Match mi = RgFullPath.Match(sPath);
            if (!mi.Success && File.Exists(sPath))
            {
                di = null;
                return string.Empty;
            }

            sPath = sPath.TrimStart('"');
            sPath = sPath.TrimEnd('"');
            sPath = StringHelper.SplitByString(sPath);

            var fi = new FileInfo(sPath);
            if (!File.Exists(sPath))
                di = Directory.Exists(sPath) ? new DirectoryInfo(sPath) : fi.Directory;
            else
                di = fi.Directory;

            string sShort = fi.FullName;

            return sShort;
        }

        public static StringWriter JsonWriter(Dictionary<string, Dictionary<string, object>> arResu)
        {
            var sw = new StringWriter();
            var jser = new JsonSerializer {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = Formatting.Indented;
                jser.Serialize(jsonWriter, arResu);
            }
            return sw;
        }

        /// <summary>
        ///     输出数据文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void WriteDataFile(string fileName, string content)
        {
            StreamWriter writer = null;
            try
            {
                if (File.Exists(fileName)) return;

                writer = new StreamWriter(fileName, false, Encoding.Default);
                writer.Write(content);
                writer.Flush();
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public static string ReadDataFile(string fileName)
        {
            string re = string.Empty;
            StreamReader reader = null;
            try
            {
                if (!File.Exists(fileName)) return re;

                reader = new StreamReader(fileName, Encoding.Default);
                re = reader.ReadToEnd();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return re;
        }
    }
}