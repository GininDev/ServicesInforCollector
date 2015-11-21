using System.IO;
using System.Text.RegularExpressions;

namespace ServicesInforCollector.Core.Helpers
{
    public class XmlHelper
    {
        public const string RecPath = "/Config/Properties[@name='Application']/Property[@name='Protocol_Transfer']";
        public const string SenPath = "/Config/Properties[@name='Application']/Property[@name='Signal']";
        public const string LogPath = "/Config/Properties[@name='Application']/Property[@name='Logger']";

        public const string TcpAddressPath = "/Config/Properties[@name='win_sockets_tcp']/Property[@name='Address']";
        public const string TcpPortPath = "/Config/Properties[@name='win_sockets_tcp']/Property[@name='Port']";

        public const string SenServerNamePath = "/Config/Properties[@name='DataDest']/Property[@name='ServerName']";
        public const string SenPortPath = "/Config/Properties[@name='DataDest']/Property[@name='Port']";

        /// <summary>
        ///     ContentCheck for xml
        /// </summary>
        /// <param name="aFull"></param>
        /// <returns></returns>
        public static bool CheckIsXml(string aFull)
        {
            bool bIsXml = false;

            string sContent = FileHelper.ReadDataFile(aFull);
            var fi = new FileInfo(aFull);
            if (fi.Extension.ToLower() != ".cfg" && fi.Extension.ToLower() != ".xml") return false;

            Match mi = RegexHelper.RegxXml.Match(sContent);
            if (mi.Success)
            {
                bIsXml = true;
            }
            return bIsXml;
        }
    }
}