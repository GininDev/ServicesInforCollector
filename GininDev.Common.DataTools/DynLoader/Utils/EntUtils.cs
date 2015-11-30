using System;
using System.Collections.Generic;
using System.Reflection;
using GininDev.Common.DataTools.Helpers;

namespace GininDev.Common.DataTools.DynLoader.Utils
{
    /// <summary>
    ///     转换名值字典到实体类
    /// </summary>
    public class EntUtils
    {
        public static T ConvertDict2Ent<T>(Type tpMain, IDictionary<string, string> sbNames,
            IDictionary<string, string> sbValues, IEnumerable<PropertyInfo> infos)
            where T : class, new()
        {
            var obj = new T();
            foreach (PropertyInfo t in infos)
            {
                string sFtype = t.PropertyType.Name;

                if (String.IsNullOrEmpty(sFtype))
                    continue;

                PropertyInfo pTemp = tpMain.GetProperty(t.Name);
                if (pTemp == null)
                    continue;

                object oTemp = null;
                switch (sFtype.ToLower())
                {
                    case "string":
                        if (sbValues != null) oTemp = sbValues[sbNames[t.Name]];
                        break;
                    case "int":
                    case "int32":
                    case "int64":
                        if (sbValues != null) oTemp = ConvertData.ToInt32(sbValues[sbNames[t.Name]]);
                        break;
                    case "double":
                        if (sbValues != null) oTemp = ConvertData.ToDouble(sbValues[sbNames[t.Name]]);
                        break;
                    case "boolean":
                    case "bool":
                        oTemp = (sbValues != null &&
                                 sbValues[sbNames[t.Name]].Equals("true", StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case "datetime":
                        if (sbValues != null) oTemp = ConvertData.ToDateTime(sbValues[sbNames[t.Name]]);
                        break;
                    default:
                        if (sbValues != null) oTemp = sbValues[sbNames[t.Name]];
                        break;
                }

                pTemp.GetSetMethod().Invoke(obj, new[] {oTemp});
            }
            return obj;
        }
    }
}