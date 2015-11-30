using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace GininDev.Common.DataTools.CustTypes
{
    /// <summary>
    ///     Extended generic list, which allows to sort by property name
    /// </summary>
    public class SortableList<TItemType> : List<TItemType>
    {
        public SortableList()
        {
        }

        public SortableList(IEnumerable<TItemType> collection)
            : base(collection)
        {
        }

        public SortableList<TItemType> SortByProperty(string propertyName, SortOrder direction)
        {
            if (direction == SortOrder.None)
                return this;

            ParameterExpression param = Expression.Parameter(typeof (TItemType), "item");
            Expression<Func<TItemType, object>> mySortExpression =
                Expression.Lambda<Func<TItemType, object>>(
                    Expression.Convert(Expression.Property(param, propertyName), typeof (object)), param);

            return direction == SortOrder.Ascending
                ? new SortableList<TItemType>(this.AsQueryable().OrderBy(mySortExpression))
                : new SortableList<TItemType>(this.AsQueryable().OrderByDescending(mySortExpression));
        }
    }
}