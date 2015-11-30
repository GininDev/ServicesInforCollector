using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace GininDev.Common.DataTools.Helpers.ConfigTool
{
    public class AppConfig : IConfig
    {

        private const string DataFileKey = "DataFile";

        private static readonly AppConfig instance = new AppConfig();

        private AppConfig()
        {
            DataFile = ConfigurationManager.AppSettings[DataFileKey];
        }
       
        public string DataFile { get; set; }

        public string NormalGet(string sKey)
        {
            return ConfigurationManager.AppSettings[sKey];
        }

        public static AppConfig Instance()
        {
            return instance;
        }
    }
}