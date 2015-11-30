using System;
using GininDev.Common.DataTools.DynLoader.Core;

namespace ServicesInforCollector.Core.Entities
{

    [TiTable("Bizs", "bid", Description = "业务系统")]
    public class Bizs
    {

        /// <summary>
        /// 字典项唯一标识
        /// </summary>
        [TiField("bId", FiContext = "", FiLength = 40, FiOrder = 0, FiType = TiType.Number)]
        public Int32 bId { get; set; }

        [TiField("bName", FiContext = "", FiOrder = 1, FiType = TiType.String)]
        public string bName { get; set; }

        [TiField("bAlias", FiContext = "", FiLength = 128, FiOrder = 2, FiType = TiType.String)]
        public string bAlias { get; set; }

        [TiField("IsDeleted", FiContext = "", FiLength = 1, FiOrder = 3, FiType = TiType.String)]
        public string IsDeleted { get; set; }
    }
}
