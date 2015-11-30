using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace GininDev.Common.DataTools.DynLoader.Plugin
{
    public interface ItDataHelper
    {
        #region ExecuteNonQuery

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>所受影响的行数</returns>
        int ExecuteNonQuery(string connectionString, string commandText, CommandType commandType);

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        object ExecuteScalar(string connectionString, string commandText, CommandType commandType);

        #endregion

        #region ExecuteReader

        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>SqlDataReader对象</returns>
        DbDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType);

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>DataSet对象</returns>
        DataSet ExecuteDataSet(string connectionString, string commandText, CommandType commandType);

        #endregion

        /// <summary>
        /// 得到对应数据库类型的语句模板
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        string GetSqlSchema(SqlSchemaType cmd);
    }
}
