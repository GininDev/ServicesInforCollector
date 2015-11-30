using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using GininDev.Common.DataTools.DynLoader.Core;
using GininDev.Common.DataTools.DynLoader.Plugin;
using GininDev.Common.DataTools.DynLoader.Utils;
using GininDev.Common.DataTools.Helpers;
using GininDev.Common.DataTools.Helpers.ConfigTool;

namespace GininDev.Common.DataTools.DynLoader
{
    /// <summary>
    ///     动态数据构造器
    /// </summary>
    public class DataFactory
    {
        private static ItDataHelper _objDh;

        private static DataFactory _instance;
        private static ConnectionStringSettings _connString = null;
        private Dictionary<string, ConnectionStringSettings> _connPool = null; 

        private void InitConnectPool()
        {
            var arConns = ConfigurationManager.ConnectionStrings;
            if (arConns != null && arConns.Count > 0)
            {
                foreach (var conn in arConns)
                {
                    var objConn = (ConnectionStringSettings) conn;
                    if (_connPool == null)
                        _connPool = new Dictionary<string, ConnectionStringSettings>();
                    _connPool.Add(objConn.Name, objConn);
                }
                if (_connPool.ContainsKey("em"))
                {
                    _connString = _connPool["em"];
                }
                else
                {
                    if (_connPool.Keys.Count > 0)
                    {
                        var aFirstKey = _connPool.Keys.First();
                        _connString = _connPool[aFirstKey];
                    }
                }
            }
        }

        private DataFactory()
        {
            var sType = ConfigManager.GetConfig().NormalGet("DST");
            if (!string.IsNullOrEmpty(sType))
                _objDh = (ItDataHelper)Activator.CreateInstance(Type.GetType(sType));
            InitConnectPool();
        }

        public static DataFactory Instance
        {
            get { return _instance ?? (_instance = new DataFactory()); }
        }

        public bool InsertObject<T>(T comp)
        {
            TiTableAttribute ta = GetTableAttribute<T>();

            if (ta == null)
                return false;

            var sbNames = new List<string>();
            Type tpMain = typeof (T);
            PropertyInfo[] infos = tpMain.GetProperties();
            foreach (PropertyInfo t in infos)
            {
                object[] attrsFields = t.GetCustomAttributes(true);
                string sFname = string.Empty;
                foreach (TiFieldAttribute fa in attrsFields.OfType<TiFieldAttribute>())
                {
                    sFname = fa.Name;
                }

                if (string.IsNullOrEmpty(sFname))
                    continue;

                sbNames.Add(sFname);
            }

            var sbValues = new List<string>();
            foreach (PropertyInfo t in infos)
            {
                string sFtype = t.PropertyType.Name;

                if (string.IsNullOrEmpty(sFtype))
                    continue;

                PropertyInfo pTemp = tpMain.GetProperty(t.Name);
                if (pTemp == null)
                    continue;

                object oTemp = pTemp.GetGetMethod().Invoke(comp, null);
                switch (sFtype.ToLower())
                {
                    case "string":
                        oTemp = string.Format("'{0}'", oTemp);
                        break;
                    case "int":
                    case "int32":
                    case "int64":
                        oTemp = string.Format("{0}", oTemp);
                        break;
                    default:
                        oTemp = string.Format("'{0}'", oTemp);
                        break;
                }
                sbValues.Add(oTemp.ToString());
            }

            string s1 = string.Join(",", sbNames.ToArray());
            string s2 = string.Join(",", sbValues.ToArray());

            string sSql = string.Format(_objDh.GetSqlSchema(SqlSchemaType.Insert), ta.Name, s1, s2);

            bool bResult = false;
            if (_connString != null)
            {
                bResult = (_objDh.ExecuteNonQuery(_connString.ConnectionString, sSql, CommandType.Text) == 1);
            }

            return bResult;
        }

        /// <summary>
        ///     获取实体类上的自定义属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static TiTableAttribute GetTableAttribute<T>()
        {
            MemberInfo info = typeof (T);
            object[] attrs = info.GetCustomAttributes(true);

            return attrs.OfType<TiTableAttribute>().FirstOrDefault();
        }

        /// <summary>
        ///     根据主键获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sGuid"></param>
        /// <returns></returns>
        public T LoadObject<T>(string sGuid) where T : class, new()
        {
            string sKeyValue = sGuid;

            TiTableAttribute ta = GetTableAttribute<T>();

            if (ta == null)
                return null;

            if (string.IsNullOrEmpty(ta.Name) || string.IsNullOrEmpty(ta.KeyField))
                return null;

            var sbNames = new Dictionary<string, string>();
            Type tpMain = typeof (T);
            PropertyInfo[] infos = tpMain.GetProperties();
            foreach (PropertyInfo t in infos)
            {
                object[] attrsFields = t.GetCustomAttributes(true);
                string sFname = string.Empty;
                foreach (TiFieldAttribute fa in attrsFields.OfType<TiFieldAttribute>())
                {
                    sFname = fa.Name;
                }

                if (string.IsNullOrEmpty(sFname))
                    continue;

                sbNames.Add(t.Name, sFname);
            }

            Dictionary<string, string> sbValues = null;
            string sSql = string.IsNullOrEmpty(sKeyValue)
                ? string.Format(_objDh.GetSqlSchema(SqlSchemaType.SelectByKey), ta.Name, "'1'", 1)
                : string.Format(_objDh.GetSqlSchema(SqlSchemaType.SelectByKey), ta.Name, ta.KeyField, sKeyValue);
            DbDataReader dtr = null;
            if (_connString != null)
            {
                dtr = _objDh.ExecuteReader(_connString.ConnectionString, sSql, CommandType.Text);
            }
            while (dtr.Read())
            {
                sbValues = sbNames.Values.ToDictionary(sbName => sbName, sbName => ConvertData.ToString(dtr[sbName]));
            }

            if (sbValues != null && sbValues.Count == 0)
                return null;

            var obj = EntUtils.ConvertDict2Ent<T>(tpMain, sbNames, sbValues, infos);

            return obj;
        }

        /// <summary>
        ///     根据条件获取对象集合数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sFilter"></param>
        /// <returns></returns>
        public DataTable LoadObjectsTable<T>(string sFilter) where T : class, new()
        {
            TiTableAttribute tba = GetTableAttribute<T>();
            if (tba == null)
                return null;

            if (string.IsNullOrEmpty(tba.Name) || string.IsNullOrEmpty(tba.KeyField))
                return null;

            var sbNames = new Dictionary<string, string>();
            Type tpMain = typeof (T);
            PropertyInfo[] infos = tpMain.GetProperties();
            foreach (PropertyInfo t in infos)
            {
                object[] attrsFields = t.GetCustomAttributes(true);
                string sFname = string.Empty;
                foreach (TiFieldAttribute fa in attrsFields.OfType<TiFieldAttribute>())
                {
                    sFname = fa.Name;
                }

                if (string.IsNullOrEmpty(sFname))
                    continue;

                sbNames.Add(t.Name, sFname);
            }

            string sSql = string.IsNullOrEmpty(sFilter)
                ? string.Format(_objDh.GetSqlSchema(SqlSchemaType.Select), tba.Name, "1=1")
                : string.Format(_objDh.GetSqlSchema(SqlSchemaType.Select), tba.Name, sFilter);
            DataSet dsr = null;
            if (_connString != null)
            {
                dsr = _objDh.ExecuteDataSet(_connString.ConnectionString, sSql, CommandType.Text);
            }
            if (dsr == null || dsr.Tables.Count < 1)
                return null;

            DataTable dtr = dsr.Tables[0];

            return dtr;
        }

        /// <summary>
        ///     根据条件获取对象集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sFilter"></param>
        /// <returns></returns>
        public List<T> LoadObjects<T>(string sFilter) where T : class, new()
        {
            TiTableAttribute tba = GetTableAttribute<T>();
            if (tba == null)
                return null;

            if (string.IsNullOrEmpty(tba.Name) || string.IsNullOrEmpty(tba.KeyField))
                return null;

            var sbNames = new Dictionary<string, string>();
            Type tpMain = typeof (T);
            PropertyInfo[] infos = tpMain.GetProperties();
            foreach (PropertyInfo t in infos)
            {
                object[] attrsFields = t.GetCustomAttributes(true);
                string sFname = string.Empty;
                foreach (TiFieldAttribute fa in attrsFields.OfType<TiFieldAttribute>())
                {
                    sFname = fa.Name;
                }

                if (string.IsNullOrEmpty(sFname))
                    continue;

                sbNames.Add(t.Name, sFname);
            }

            string sSql = string.IsNullOrEmpty(sFilter)
                ? string.Format(_objDh.GetSqlSchema(SqlSchemaType.Select), tba.Name, "1=1")
                : string.Format(_objDh.GetSqlSchema(SqlSchemaType.Select), tba.Name, sFilter);
            DataSet dsr = null;
            if (_connString != null)
            {
                dsr = _objDh.ExecuteDataSet(_connString.ConnectionString, sSql, CommandType.Text);
            }
            if (dsr == null || dsr.Tables.Count < 1)
                return null;

            DataTable dtr = dsr.Tables[0];
            List<T> list = (from DataRow row in dtr.Rows
                select
                    sbNames.Values.ToDictionary(sbName => sbName, sbName => ConvertData.ToString(row[sbName]))
                into sbValues
                where sbValues.Count != 0
                select EntUtils.ConvertDict2Ent<T>(tpMain, sbNames, sbValues, infos)).ToList();
            return dtr.Rows.Count == 0 ? null : list;
        }

        /// <summary>
        ///     修改数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comp"></param>
        /// <returns></returns>
        public bool UpdateObject<T>(T comp)
        {
            TiTableAttribute ta = GetTableAttribute<T>();


            if (string.IsNullOrEmpty(ta.Name) || string.IsNullOrEmpty(ta.KeyField))
                return false;

            var sbNames = new Dictionary<string, string>();
            var sbRevNames = new Dictionary<string, string>();
            Type tpMain = typeof (T);
            PropertyInfo[] infos = tpMain.GetProperties();
            foreach (PropertyInfo t in infos)
            {
                object[] attrsFields = t.GetCustomAttributes(true);
                string sFname = string.Empty;
                foreach (TiFieldAttribute fa in attrsFields.OfType<TiFieldAttribute>())
                {
                    sFname = fa.Name;
                }

                if (string.IsNullOrEmpty(sFname))
                    continue;

                sbNames.Add(sFname, t.Name);
                sbRevNames.Add(t.Name, sFname);
            }

            string sKeyProp = sbNames[ta.KeyField];
            PropertyInfo pTemp = tpMain.GetProperty(sKeyProp);
            if (pTemp == null)
                return false;

            object oTemp = pTemp.GetGetMethod().Invoke(comp, null);
            string sKeyValue = ConvertData.ToString(oTemp);

            var sbValues = new List<string>();
            foreach (PropertyInfo t in infos)
            {
                string sFtype = t.PropertyType.Name;

                if (string.IsNullOrEmpty(sFtype))
                    continue;

                PropertyInfo paTemp = tpMain.GetProperty(t.Name);
                if (paTemp == null)
                    continue;

                if (t.Name.Equals(sKeyProp, StringComparison.CurrentCultureIgnoreCase))
                    continue;

                object orTemp = paTemp.GetGetMethod().Invoke(comp, null);
                switch (sFtype.ToLower())
                {
                    case "string":
                        oTemp = string.Format("{0}='{1}'", sbRevNames[t.Name], orTemp);
                        break;
                    case "int":
                    case "int32":
                    case "int64":
                        oTemp = string.Format("{0}={1}", sbRevNames[t.Name], orTemp);
                        break;
                    default:
                        oTemp = string.Format("{0}='{1}'", sbRevNames[t.Name], orTemp);
                        break;
                }
                sbValues.Add(oTemp.ToString());
            }
            string s1 = string.Join(",", sbValues.ToArray());

            string sSql = string.Format(_objDh.GetSqlSchema(SqlSchemaType.Update), ta.Name, s1, ta.KeyField, sKeyValue);

            bool bResult = false;
            if (_connString != null)
            {
                bResult = _objDh.ExecuteNonQuery(_connString.ConnectionString, sSql, CommandType.Text) == 1;
            }

            return bResult;
        }

        /// <summary>
        ///     删除数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comp"></param>
        /// <returns></returns>
        public bool DeleteObject<T>(T comp)
        {
            TiTableAttribute ta = GetTableAttribute<T>();


            if (string.IsNullOrEmpty(ta.Name) || string.IsNullOrEmpty(ta.KeyField))
                return false;

            var sbNames = new Dictionary<string, string>();
            Type tpMain = typeof (T);
            PropertyInfo[] infos = tpMain.GetProperties();
            foreach (PropertyInfo t in infos)
            {
                object[] attrsFields = t.GetCustomAttributes(true);
                string sFname = string.Empty;
                foreach (TiFieldAttribute fa in attrsFields.OfType<TiFieldAttribute>())
                {
                    sFname = fa.Name;
                }

                if (string.IsNullOrEmpty(sFname))
                    continue;

                sbNames.Add(sFname, t.Name);
            }

            string sKeyProp = sbNames[ta.KeyField];
            PropertyInfo pTemp = tpMain.GetProperty(sKeyProp);
            if (pTemp == null)
                return false;

            object oTemp = pTemp.GetGetMethod().Invoke(comp, null);
            string sKeyValue = ConvertData.ToString(oTemp);

            string sSql = string.Format(_objDh.GetSqlSchema(SqlSchemaType.Delte), ta.Name, ta.KeyField, sKeyValue);

            bool bResult = false;
            if (_connString != null)
            {
                bResult = (_objDh.ExecuteNonQuery(_connString.ConnectionString, sSql, CommandType.Text) == 1);
            }

            return bResult;
        }
    }
}