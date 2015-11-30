using System;
using GininDev.Common.DataTools.DynLoader.Core;

namespace ServicesInforCollector.Core.Entities
{

    [TiTable("Dicts", "did", Description = "字典")]
    public class Dicts
    {

        /// <summary>
        /// 字典项唯一标识
        /// </summary>
        [TiField("dId", FiContext = "", FiLength = 40, FiOrder = 0, FiType = TiType.Number)]
        public Int32 dId { get; set; }

        [TiField("dicName", FiContext = "", FiOrder = 1, FiType = TiType.String)]
        public string dName { get; set; }

        [TiField("dicValue", FiContext = "", FiLength = 128, FiOrder = 2, FiType = TiType.String)]
        public string dValue { get; set; }

        [TiField("IsDeleted", FiContext = "", FiLength = 1, FiOrder = 3, FiType = TiType.String)]
        public string IsDeleted { get; set; }
    }
}
