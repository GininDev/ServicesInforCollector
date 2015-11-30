using System;

namespace GininDev.Common.DataTools.DynLoader.Core
{
    /// <summary>
    ///     字段自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class TiFieldAttribute : Attribute
    {
        private readonly string _name;

        public TiFieldAttribute(string name)
        {
            _name = name;
        }

        public virtual string Name
        {
            get { return _name; }
        }

        /// <summary>
        ///     数据类型
        /// </summary>
        public TiType FiType { get; set; }

        /// <summary>
        ///     数据长度
        /// </summary>
        public Int32 FiLength { get; set; }

        /// <summary>
        ///     顺序
        /// </summary>
        public Int32 FiOrder { get; set; }

        /// <summary>
        ///     上下文,通常存储属性真实名称
        /// </summary>
        public string FiContext { get; set; }

        /// <summary>
        ///     公式值.若为真则不需要填写
        /// </summary>
        public bool IsFormula { get; set; }
    }
}