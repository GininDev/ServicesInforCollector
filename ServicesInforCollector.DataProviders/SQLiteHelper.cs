﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using GininDev.Common.DataTools.DynLoader.Plugin;

namespace ServicesInforCollector.DataProviders
{
    /// <summary>
    /// 本类为SQLite数据库帮助静态类,使用时只需直接调用即可,无需实例化
    /// </summary>
    public class SqLiteHelper : ItDataHelper
    {
        public SqLiteHelper()
        {

        }

        #region ExecuteNonQuery
        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>所受影响的行数</returns>
        public  int ExecuteNonQuery(string connectionString, SQLiteCommand cmd)
        {
            SQLiteCommand mycmd = cmd;

            int result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(mycmd, con, ref trans, true, mycmd.CommandType, mycmd.CommandText);
                try
                {
                    result = mycmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>所受影响的行数</returns>
        public  int ExecuteNonQuery(string connectionString, string commandText, CommandType commandType)
        {
            int result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>所受影响的行数</returns>
        public  int ExecuteNonQuery(string connectionString, string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            SQLiteParameter[] myCmdParms = cmdParms;

            int result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public  object ExecuteScalar(string connectionString, SQLiteCommand cmd)
        {
            SQLiteCommand myCmd = cmd;

            object result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(myCmd, con, ref trans, true, myCmd.CommandType, myCmd.CommandText);
                try
                {
                    result = myCmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public  object ExecuteScalar(string connectionString, string commandText, CommandType commandType)
        {
            object result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>查询所得的第1行第1列数据</returns>
        public  object ExecuteScalar(string connectionString, string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            SQLiteParameter[] myCmdParms = cmdParms;

            object result = 0;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, ref trans, true, commandType, commandText);
                try
                {
                    result = cmd.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return result;
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>SqlDataReader对象</returns>
        public  DbDataReader ExecuteReader(string connectionString, SQLiteCommand cmd)
        {
            SQLiteCommand myCmd = cmd;

            DbDataReader reader = null;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");

            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteTransaction trans = null;
            PrepareCommand(myCmd, con, ref trans, false, myCmd.CommandType, myCmd.CommandText);
            try
            {
                reader = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }

        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>SqlDataReader对象</returns>
        public  DbDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType)
        {
            DbDataReader reader = null;
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");
            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException("commandText");

            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }

        /// <summary>
        /// 执行数据库查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>SqlDataReader对象</returns>
        public  DbDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            SQLiteParameter[] myCmdParms = cmdParms;

            DbDataReader reader = null;
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText, myCmdParms);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <returns>DataSet对象</returns>
        public  DataSet ExecuteDataSet(string connectionString,SQLiteCommand  cmd)
        {
            SQLiteCommand myCmd = cmd;

            DataSet ds = new DataSet();
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteTransaction trans = null;
            PrepareCommand(myCmd, con, ref trans, false, myCmd.CommandType, myCmd.CommandText);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(myCmd);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myCmd.Connection != null)
                {
                    if (myCmd.Connection.State == ConnectionState.Open)
                    {
                        myCmd.Connection.Close();
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <returns>DataSet对象</returns>
        public  DataSet ExecuteDataSet(string connectionString, string commandText, CommandType commandType)
        {
            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            DataSet ds = new DataSet();
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 执行数据库查询，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandText">执行语句或存储过程名</param>
        /// <param name="commandType">执行类型</param>
        /// <param name="cmdParms">SQL参数对象</param>
        /// <returns>DataSet对象</returns>
        public DataSet ExecuteDataSet(string connectionString, string commandText, CommandType commandType, params SQLiteParameter[] cmdParms)
        {
            SQLiteParameter[] myCmdParms = cmdParms;

            if (connectionString == null || connectionString.Length == 0)
                throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            DataSet ds = new DataSet();
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, ref trans, false, commandType, commandText, myCmdParms);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return ds;
        }
        #endregion

        /// <summary>
        /// 通用分页查询方法
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="strColumns">查询字段名</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="strOrder">排序条件</param>
        /// <param name="pageSize">每页数据数量</param>
        /// <param name="currentIndex">当前页数</param>
        /// <param name="recordOut">数据总量</param>
        /// <returns>DataTable数据表</returns>
        public  DataTable SelectPaging(string connString, string tableName, string strColumns, string strWhere, string strOrder, int pageSize, int currentIndex, out int recordOut)
        {
            DataTable dt = new DataTable();
            recordOut = Convert.ToInt32(ExecuteScalar(connString, string.Format("select count(*) from {0}", tableName), CommandType.Text));
            string pagingTemplate = "select {0} from {1} where {2} order by {3} limit {4} offset {5} ";
            int offsetCount = (currentIndex - 1) * pageSize;
            string commandText = String.Format(pagingTemplate, strColumns, tableName, strWhere, strOrder, pageSize.ToString(), offsetCount.ToString());
            using (DbDataReader reader = ExecuteReader(connString, commandText, CommandType.Text))
            {
                if (reader != null)
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }

        /// <summary>
        /// 预处理Command对象,数据库链接,事务,需要执行的对象,参数等的初始化
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="conn">Connection对象</param>
        /// <param name="trans">Transcation对象</param>
        /// <param name="useTrans">是否使用事务</param>
        /// <param name="cmdType">SQL字符串执行类型</param>
        /// <param name="cmdText">SQL Text</param>
        /// <param name="cmdParms">SQLiteParameters to use in the command</param>
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, ref SQLiteTransaction trans, bool useTrans, CommandType cmdType, string cmdText, params SQLiteParameter[] cmdParms)
        {
            SQLiteParameter[] myCmdParms = cmdParms;

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (useTrans)
            {
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = trans;
            }


            cmd.CommandType = cmdType;

            if (myCmdParms != null)
            {
                foreach (SQLiteParameter parm in myCmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #region ItDataHelper 成员

        /// <summary>
        /// 获取业务处理语句
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string GetSqlSchema(SqlSchemaType cmd)
        {
            var sResult = string.Empty;
            switch (cmd)
            {
                case SqlSchemaType.Insert:
                    sResult = SQLiteSchema.STemp;
                    break;
                case SqlSchemaType.Update:
                    sResult = SQLiteSchema.SUpdateByKeySql;
                    break;
                case SqlSchemaType.Delte:
                    sResult = SQLiteSchema.SDeleteByKeySql;
                    break;
                case SqlSchemaType.Select:
                    sResult = SQLiteSchema.SSelectSql;
                    break;
                case SqlSchemaType.SelectByKey:
                    sResult = SQLiteSchema.SDeleteByKeySql;
                    break;
            }
            return sResult;
        }

        #endregion
    }
}
