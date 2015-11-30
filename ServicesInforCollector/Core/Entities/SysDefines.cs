using System;
using GininDev.Common.DataTools.DynLoader.Core;

namespace ServicesInforCollector.Core.Entities
{

    [TiTable("SysDefines", "sid", Description = "系统定义")]
    public class SysDefines
    {

        /// <summary>
        /// 字典项唯一标识
        /// </summary>
        [TiField("sId", FiContext = "", FiLength = 40, FiOrder = 0, FiType = TiType.Number)]
        public Int32 sId { get; set; }

        [TiField("sName", FiContext = "", FiOrder = 1, FiType = TiType.String)]
        public string sName { get; set; }

        [TiField("sAlias", FiContext = "", FiLength = 128, FiOrder = 2, FiType = TiType.String)]
        public string sAlias { get; set; }

        [TiField("sShort", FiContext = "", FiLength = 128, FiOrder = 3, FiType = TiType.String)]
        public string sShort { get; set; }

        [TiField("sFull", FiContext = "", FiLength = 128, FiOrder = 3, FiType = TiType.String)]
        public string sFull { get; set; }

        [TiField("IsDeleted", FiContext = "", FiLength = 1, FiOrder = 4, FiType = TiType.String)]
        public string IsDeleted { get; set; }
    }
}
