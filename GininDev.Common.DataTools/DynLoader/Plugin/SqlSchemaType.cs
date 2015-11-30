using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GininDev.Common.DataTools.DynLoader.Plugin
{
    /// <summary>
    /// 语句类型
    /// </summary>
    public enum SqlSchemaType
    {
        /// <summary>
        /// 插入语句
        /// </summary>
        Insert,
        /// <summary>
        /// 按主键查询语句
        /// </summary>
        SelectByKey,
        /// <summary>
        /// 查询语句
        /// </summary>
        Select,
        /// <summary>
        /// 修改语句
        /// </summary>
        Update,
        /// <summary>
        /// 删除语句
        /// </summary>
        Delte
    }
}
