using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace GininDev.Common.DataTools.Helpers
{
    public class ConfigHelper
    {
        /// <summary>
        ///     忽略收集的属性
        /// </summary>
        private static readonly List<string> ArSkipProp;

        /// <summary>
        ///     属性拼接
        /// </summary>
        private static readonly List<string> ArJoinProp;

        /// <summary>
        ///     确定每个应用按文件名即key进行匹配(true)配置文件，
        ///     还是(false)配置文件按default中配置的名字轮询
        /// </summary>
        private static readonly bool UseKeyTair;

        /// <summary>
        ///     配置文件字典
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, string>> DicConfigs;

        /// <summary>
        ///     Ini文件主节名
        /// </summary>
        private static readonly string STypeNode = string.Empty;

        /// <summary>
        ///     常用配置文件名
        /// </summary>
        private static readonly Dictionary<string, string> DicDefaults;

        /// <summary>
        ///     忽略处理的文件夹
        /// </summary>
        private static readonly List<string> ArSkipFolders;


        static ConfigHelper()
        {
            string sSkipProp = ConfigReader("skipProp");
            ArSkipProp = StringHelper.SplitString(sSkipProp);

            string sJoinProp = ConfigReader("joinProp");
            ArJoinProp = StringHelper.SplitString(sJoinProp);

            UseKeyTair = ConfigReader("getConfigByKey") == "1";

            DicConfigs = new Dictionary<string, Dictionary<string, string>>();
            foreach (string ask in ConfigurationManager.AppSettings.Keys)
            {
                if (!ask.StartsWith("key")) continue;
                object dicV = JsonConvert.DeserializeObject(ConfigurationManager.AppSettings[ask],
                    typeof (Dictionary<string, string>));
                DicConfigs[ask.Substring(3)] = (Dictionary<string, string>) dicV;
            }

            STypeNode = ConfigReader("typeNode");

            string sdefaultConfigs = ConfigReader("defaultConfigs");
            object dicD = JsonConvert.DeserializeObject(sdefaultConfigs,
                typeof (Dictionary<string, string>));
            DicDefaults = (Dictionary<string, string>) dicD;

            string sKipFolders = ConfigReader("skipFolders");
            ArSkipFolders = StringHelper.SplitString(sKipFolders);
        }

        /// <summary>
        ///     Simple Read Appsettings
        /// </summary>
        /// <param name="kName"></param>
        /// <returns></returns>
        public static string ConfigReader(string kName)
        {
            string sDefaultName = ConfigurationManager.AppSettings[kName];
            return sDefaultName;
        }

        /// <summary>
        ///     穷举配置文件
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetDirConfigs(DirectoryInfo di)
        {
            Dictionary<string, object> arConfigs = GetConfigs(di.FullName);

            DirectoryInfo[] arChild = di.GetDirectories();
            if (arChild.Length <= 0) return arConfigs;

            foreach (
                DirectoryInfo aChi in from aChi in arChild let exiFlag = StringMatch(aChi) where !exiFlag select aChi)
            {
                if (arConfigs == null) arConfigs = new Dictionary<string, object>();
                arConfigs[aChi.Name] = GetDirConfigs(aChi);
            }
            return arConfigs;
        }

        /// <summary>
        ///     忽略特殊文件夹
        /// </summary>
        /// <param name="aChi"></param>
        /// <returns></returns>
        private static bool StringMatch(FileSystemInfo aChi)
        {
            return ArSkipFolders.Any(asf => aChi.Name.IndexOf(asf, StringComparison.Ordinal) > -1);
        }

        /// <summary>
        ///     获取配置信息
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetConfigs(string sPath)
        {
            Dictionary<string, object> arConfigs = null;
            DirectoryInfo di;
            string sShortName = FileHelper.GetShortNameAndSetDirInfo(sPath, out di);

            if (UseKeyTair)
            {
                //for 1
                if (di != null && di.Exists) arConfigs = GetConfigByKeyPair(sShortName, di);
            }
            else
            {
                //for 0
                foreach (string cv in DicDefaults.Values)
                {
                    if (cv.IndexOf('*') >= 0)
                    {
                        if (di == null || !di.Exists) continue;
                        FileInfo[] arFs = di.GetFiles(cv);
                        string cv1 = cv;
                        arConfigs = arFs.Aggregate(arConfigs,
                            (current, aFs) => ReadConfigFile(aFs.FullName, current, cv1));
                    }
                    else
                    {
                        //Build filename
                        if (di == null || !di.Exists) continue;
                        string aFull = string.Format("{0}{1}{2}", di.FullName, Path.DirectorySeparatorChar, cv);
                        if (!File.Exists(aFull)) continue;

                        arConfigs = ReadConfigFile(aFull, arConfigs, cv);
                    }
                }
            }

            return arConfigs;
        }

        private static Dictionary<string, object> ReadConfigFile(string aFull, Dictionary<string, object> arConfigs,
            string cv)
        {
            bool bIsXml = XmlHelper.CheckIsXml(aFull);

            if (bIsXml)
            {
                try
                {
                    var objFi = new FileInfo(aFull);
                    string dBase = objFi.DirectoryName;

                    var document = new XmlDocument();
                    document.Load(aFull);
                    XmlNode objR = document.SelectSingleNode(XmlHelper.RecPath);
                    string s1 = objR.Attributes["value"].Value;

                    XmlNode objS = document.SelectSingleNode(XmlHelper.SenPath);
                    string s2 = objS.Attributes["value"].Value;

                    if (s1 == "tcp_client")
                    {
                        string nesPath = string.Format("transfer{0}win32_socket", Path.DirectorySeparatorChar);

                        string rConfig = string.Format("{0}{1}{2}{1}win32_socket_pto.cfg", dBase,
                            Path.DirectorySeparatorChar, nesPath);

                        var objXr = new XmlDocument();
                        objXr.Load(rConfig);
                        XmlNode objXri = objXr.SelectSingleNode(XmlHelper.TcpAddressPath);
                        string sIp = objXri.Attributes["value"].Value;

                        XmlNode objXrp = objXr.SelectSingleNode(XmlHelper.TcpPortPath);
                        string sPort = objXrp.Attributes["value"].Value;

                        var arRec = new Dictionary<string, string> {{"Address", sIp}, {"Port", sPort}};
                        if (arConfigs == null)
                            arConfigs = new Dictionary<string, object>();

                        arConfigs.Add("win32_socket_pto.cfg", arRec);
                    }

                    string dstPath = string.Empty;

                    var arS2 = s2.Split(',');
                    foreach (var as2 in arS2)
                    {
                        dstPath = string.Format("{0}{1}Signal{1}{2}", dBase, Path.DirectorySeparatorChar, as2);
                        var dstDi = new DirectoryInfo(dstPath);
                        FileInfo[] arDlls = dstDi.GetFiles("*.dll");
                        if (arDlls != null && arDlls.Any())
                        {
                            FileInfo aDll = arDlls[0];
                            string dllName = aDll.FullName;
                            string dllCfgName = dllName.Replace(".dll", ".cfg");

                            var objXs = new XmlDocument();
                            objXs.Load(dllCfgName);
                            XmlNode objXsi = objXs.SelectSingleNode(XmlHelper.SenServerNamePath);
                            string sIp = objXsi.Attributes["value"].Value;

                            XmlNode objXsp = objXs.SelectSingleNode(XmlHelper.SenPortPath);
                            string sPort = objXsp.Attributes["value"].Value;

                            XmlNode objDdt = objXs.SelectSingleNode(XmlHelper.SenDataDestType);
                            string sDestType = objDdt.Attributes["value"].Value;

                            XmlNode objDir = objXs.SelectSingleNode(XmlHelper.SenDirectory);
                            string sDir = objDir.Attributes["value"].Value;

                            var arRec = new Dictionary<string, string> { { "DestType", sDestType }, { "ServerName", sIp }, { "Port", sPort }, { "Directory", sDir } };
                            if (arConfigs == null)
                                arConfigs = new Dictionary<string, object>();

                            arConfigs.Add(aDll.Name, arRec);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                var objId = new IniDocumentHelper(aFull, Encoding.GetEncoding("GB2312"));
                string[] obj = objId.SectionNames;
                if (obj.Length <= 0 || !obj.Contains(STypeNode, StringComparer.OrdinalIgnoreCase)) return arConfigs;

                string[] arKeys = SmartSection(objId);
                string sVirt;
                Dictionary<int, Dictionary<string, string>> sVals = ReadProperties(objId, arKeys, out sVirt);

                if (arConfigs == null)
                    arConfigs = new Dictionary<string, object>();

                if (!arConfigs.ContainsKey(cv))
                    arConfigs.Add(cv, JsonConvert.SerializeObject(sVals));

                if (!string.IsNullOrEmpty(sVirt))
                    arConfigs.Add("Virt_" + cv, sVirt);
            }

            return arConfigs;
        }

        private static Dictionary<string, object> GetConfigByKeyPair(string sShortName, FileSystemInfo di)
        {
            Dictionary<string, object> arConfigs = null;
            foreach (
                Dictionary<string, string>.ValueCollection objTarConfigs in
                    from ck in DicConfigs.Keys
                    where ck == sShortName || sShortName.StartsWith(ck)
                    select DicConfigs[ck].Values)
            {
                foreach (string otc in objTarConfigs)
                {
                    //Build filename
                    string aFull = string.Format("{0}{1}{2}", di.FullName, Path.DirectorySeparatorChar, otc);
                    if (!File.Exists(aFull)) continue;

                    var objId = new IniDocumentHelper(aFull, Encoding.GetEncoding("GB2312"));
                    string[] obj = objId.SectionNames;
                    if (obj.Length <= 0 || !obj.Contains(STypeNode, StringComparer.OrdinalIgnoreCase)) continue;

                    string[] arKeys = SmartSection(objId);
                    string sVirt;
                    Dictionary<int, Dictionary<string, string>> sVals = ReadProperties(objId, arKeys, out sVirt);

                    if (arConfigs == null)
                        arConfigs = new Dictionary<string, object>();

                    arConfigs.Add(otc,
                        string.Format("{0}/{1}:{2}", otc, arKeys, JsonConvert.SerializeObject(sVals)));

                    if (!string.IsNullOrEmpty(sVirt))
                        arConfigs.Add(string.Format("Virt_{0}", otc), sVirt);
                }

                break;
            }
            return arConfigs;
        }

        /// <summary>
        ///     智能获取SectionName
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        private static string[] SmartSection(IniDocumentHelper objId)
        {
            List<string> arKeys = null;
            //string sKey = string.Empty;

            string oCount = objId[STypeNode, "Count"];
            string sType = objId[STypeNode, "RemoteType"];

            if (objId.SectionNames.Contains(sType, StringComparer.OrdinalIgnoreCase))
            {
                if (sType != null)
                {
                    if (arKeys == null)
                        arKeys = new List<string>();
                    arKeys.Add(sType);
                }
            }
            else
            {
                if (oCount == null) return arKeys.ToArray();

                int iCount = Convert.ToInt32(oCount);
                for (int i = 1; i < iCount + 1; i++)
                {
                    string sKey = sType + i;
                    if (!objId.SectionNames.Contains(sKey, StringComparer.OrdinalIgnoreCase)) continue;

                    if (arKeys == null)
                        arKeys = new List<string>();
                    arKeys.Add(sKey);
                }
            }
            return arKeys.ToArray();
        }

        /// <summary>
        ///     读取Section属性
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="sKey"></param>
        /// <param name="sVirt">形成的虚拟串</param>
        /// <returns></returns>
        private static Dictionary<int, Dictionary<string, string>> ReadProperties(IniDocumentHelper objId,
            string[] arKeys, out string sVirt)
        {
            Dictionary<int, Dictionary<string, string>> sVals = null;
            sVirt = string.Empty;

            List<string> arOut = null;
            for (int i = 0; i < arKeys.Length; i++)
            {
                string sKey = arKeys[i];
                NameValueCollection z = objId[sKey];
                List<string> arVirt = null;

                if (sVals == null)
                    sVals = new Dictionary<int, Dictionary<string, string>>();

                Dictionary<string, string> arDicPair = null;
                foreach (string tmz in z.Keys)
                {
                    if (arDicPair == null)
                        arDicPair = new Dictionary<string, string>();

                    if (!ArSkipProp.Contains(tmz))
                        arDicPair[tmz] = z[tmz]; //set values here

                    if (!ArJoinProp.Contains(tmz)) continue;
                    if (arVirt == null)
                        arVirt = new List<string>();
                    arVirt.Add(z[tmz]);
                }
                sVals.Add(i, arDicPair);

                if (arVirt != null && arVirt.Count > 0)
                {
                    string aVirt = string.Join(":", arVirt.ToArray());
                    if (arOut == null)
                        arOut = new List<string>();

                    arOut.Add(aVirt);
                }
            }
            if (arOut != null)
                sVirt = string.Join(";", arOut.ToArray());

            return sVals;
        }
    }

    public class ConfigPair
    {
        public string Key { get; set; }
        public Dictionary<string, string> Configs { get; set; }
    }
}