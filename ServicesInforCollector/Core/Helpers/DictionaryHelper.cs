using System.Management;
using Newtonsoft.Json.Linq;

namespace ServicesInforCollector.Core.Helpers
{
    internal class DictionaryHelper
    {
        /// <summary>
        ///     字典安全值
        /// </summary>
        /// <param name="mo"></param>
        /// <param name="pName"></param>
        /// <returns></returns>
        public static string MboSafeValue(ManagementBaseObject mo, string pName)
        {
            string sR = string.Empty;

            if (mo[pName] != null)
                sR = mo[pName].ToString();

            return sR;
        }

        public static string MboSafeValue(JObject mo, string pName)
        {
            string sR = string.Empty;

            if (mo[pName] != null)
                sR = mo[pName].ToString();

            return sR;
        }
    }
}