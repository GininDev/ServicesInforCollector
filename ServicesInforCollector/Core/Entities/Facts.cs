using System;
using GininDev.Common.DataTools.DynLoader.Core;

namespace ServicesInforCollector.Core.Entities
{

    [TiTable("Facts", "fid", Description = "电厂")]
    public class Facts
    {

        /// <summary>
        /// 字典项唯一标识
        /// </summary>
        [TiField("fId", FiContext = "", FiLength = 40, FiOrder = 0, FiType = TiType.Number)]
        public Int32 fId { get; set; }

        [TiField("fName", FiContext = "", FiOrder = 1, FiType = TiType.String)]
        public string fName { get; set; }

        [TiField("fAlias", FiContext = "", FiLength = 128, FiOrder = 2, FiType = TiType.String)]
        public string fAlias { get; set; }

        [TiField("fShort", FiContext = "", FiLength = 128, FiOrder = 3, FiType = TiType.String)]
        public string fShort { get; set; }

        [TiField("IsDeleted", FiContext = "", FiLength = 1, FiOrder = 4, FiType = TiType.String)]
        public string IsDeleted { get; set; }
    }
}
