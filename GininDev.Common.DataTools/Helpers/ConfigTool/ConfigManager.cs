namespace GininDev.Common.DataTools.Helpers.ConfigTool
{
    public class ConfigManager
    {
        public static AppConfig GetConfig()
        {
            return AppConfig.Instance();
        }

        public static IConfig GetConfig(ConfigTypes ct)
        {
            IConfig icResult;
            switch (ct)
            {
                case ConfigTypes.TemplatesConfig:
                    icResult = TemplatesConfig.Instance();

                    break;
                default:
                    icResult = AppConfig.Instance();

                    break;
            }

            return icResult;
        }
    }
}