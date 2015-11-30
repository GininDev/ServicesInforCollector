using System;

namespace GininDev.Common.DataTools.DynLoader.Core
{
    /// <summary>
    ///     表自定属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class TiTableAttribute : Attribute
    {
        private readonly string _keyField;
        private readonly string _name;

        public TiTableAttribute(string name)
        {
            _name = name;
        }

        public TiTableAttribute(string name, string keyField)
        {
            _name = name;
            _keyField = keyField;
        }

        public virtual string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 关键字字段
        /// </summary>
        public virtual string KeyField
        {
            get { return _keyField; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 连接名
        /// </summary>
        public string ConnName { get; set; }
    }
}