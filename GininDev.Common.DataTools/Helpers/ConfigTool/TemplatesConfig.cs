using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GininDev.Common.DataTools.Helpers.ConfigTool
{
    public class TemplatesConfig : IConfig
    {
        private const string SmsTemplateCacheKey = "obj_TemplatePage_Key";

        private const string PDetailTemplateCacheKey = "obj_PDetailTemplatePage_Key";

        private static readonly TemplatesConfig instance = new TemplatesConfig();

        private TemplatesConfig()
        {
            InitData();
        }

        public string SmsTemplate { get; set; }
        public string PDetailTemplate { get; set; }

        public static TemplatesConfig Instance()
        {
            return instance;
        }

        private void InitData()
        {
            var oResult = InitSmsTemplate();
            SmsTemplate = oResult;
        }

        private static string InitSmsTemplate()
        {
            var sTplStream = string.Empty;
            if (string.IsNullOrEmpty(sTplStream))
            {
                var sBasePath = string.Format("{0}\\Config\\Templates\\{1}",
                                                 Application.StartupPath, AppConfig.Instance().DataFile);
                if (File.Exists(sBasePath))
                {
                    sTplStream = FileHelper.ReadDataFile(sBasePath);
                }
                else
                {
                    sTplStream = "";
                }
            }
            var oResult = sTplStream;
            return oResult;
        }

        public string NormalGet(string sKey)
        {
            throw new System.NotImplementedException();
        }
    }
}