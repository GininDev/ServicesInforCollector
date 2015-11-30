using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using GininDev.Common.DataTools.Helpers;
using Newtonsoft.Json;

namespace GininDev.Common.DataTools.Entities
{
    public class WmiServiceObj
    {
        [JsonConstructor]
        public WmiServiceObj(Int32 iOrder, string sName, string sDispName, string sCapt, string sDesc, string sStartMode,
            string sStarted, string sState, string sStatus, string sPath)
        {
            DirectoryInfo di;
            string sShort = FileHelper.GetShortNameAndSetDirInfo(sPath, out di);
            Dictionary<string, object> arConfigs = ConfigHelper.GetConfigs(sPath);

            OrderNo = iOrder;
            Name = sName;
            ExeName = sShort;
            DispName = sDispName;
            Capt = sCapt;
            Desc = sDesc;
            StartMode = sStartMode;
            Started = sStarted;
            State = sState;
            Status = sStatus;
            Path = sPath;
            ConfigList = arConfigs;
        }

        public WmiServiceObj(ManagementBaseObject mobj, int iOrder, IEnumerable<string> arExclude)
        {
            if (mobj == null)
                return;

            string sName = DictionaryHelper.MboSafeValue(mobj, "Name");
            string sDispName = DictionaryHelper.MboSafeValue(mobj, "DisplayName");
            string sCapt = DictionaryHelper.MboSafeValue(mobj, "Caption");
            string sDesc = DictionaryHelper.MboSafeValue(mobj, "Description");
            string sStartMode = DictionaryHelper.MboSafeValue(mobj, "StartMode");
            string sStarted = DictionaryHelper.MboSafeValue(mobj, "Started");
            string sState = DictionaryHelper.MboSafeValue(mobj, "State");
            string sStatus = DictionaryHelper.MboSafeValue(mobj, "Status");
            string sPath = DictionaryHelper.MboSafeValue(mobj, "PathName");

            DirectoryInfo di;
            string sShort = FileHelper.GetShortNameAndSetDirInfo(sPath, out di);
            if (arExclude != null)
            {
                if (arExclude.Any(ex => ex == sShort))
                {
                    BSkipReadConfig = true;
                }
            }

            if (BSkipReadConfig) return;

            Dictionary<string, object> arConfigs = ConfigHelper.GetConfigs(sPath);

            OrderNo = iOrder;
            Name = sName;
            ExeName = sShort;
            DispName = sDispName;
            Capt = sCapt;
            Desc = sDesc;
            StartMode = sStartMode;
            Started = sStarted;
            State = sState;
            Status = sStatus;
            Path = sPath;
            ConfigList = arConfigs;
        }

        [DisplayName("OrderNo")]
        /// <summary>
        /// 顺序
        /// </summary>
        public Int32 OrderNo { get; set; }

        /// <summary>
        ///     服务显示名
        /// </summary>
        public string DispName { get; set; }

        /// <summary>
        ///     服务名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     服务路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     服务描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        ///     服务启动类型
        /// </summary>
        public string StartMode { get; set; }

        /// <summary>
        ///     是否已启动
        /// </summary>
        public string Started { get; set; }

        /// <summary>
        ///     服务状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///     可用状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Caption属性通常值与Displayname同
        /// </summary>
        public string Capt { get; set; }

        /// <summary>
        ///     短名称
        /// </summary>
        public string ExeName { get; set; }

        /// <summary>
        ///     不读取配置
        /// </summary>
        public bool BSkipReadConfig { get; set; }


        public Dictionary<string, object> ConfigList { get; set; }
    }
}