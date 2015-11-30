using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInforCollector.DataProviders
{
    /// <summary>
    /// 查询语句模板
    /// </summary>
    public class SQLiteSchema
    {
        public const string SSelUser = "Select * from tblUsers where usrId = '{0}' and IsDeleted = '0'";
        public const string STemp = "Insert into {0} ({1}, IsDeleted) values ({2},'0')";
        public const string SSelectByKeySql = "Select * from {0} where {1}='{2}' and IsDeleted = '0'";
        public const string SSelectSql = "Select * from {0} where {1} and IsDeleted = '0'";
        public const string SUpdateByKeySql = "Update {0} Set {1} where {2}='{3}' and IsDeleted = '0'";
        public const string SDeleteByKeySql = "Update {0} Set IsDeleted = '1' where {1}='{2}'";
    }
}
