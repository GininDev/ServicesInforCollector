using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using ServicesInforCollector.Core.Components;
using ServicesInforCollector.Core.Entities;

namespace ServicesInforCollector.Core.Helpers
{
    public class ShellHelper
    {
        /// <summary>
        ///     Shell
        /// </summary>
        /// <param name="fName"></param>
        public static void NotepadFile(string fName)
        {
            string sPath = Environment.GetEnvironmentVariable("windir");
            Process.Start(string.Format("{0}{1}NotePad.exe", sPath, Path.DirectorySeparatorChar), fName);
        }


        /// <summary>
        ///     Shell
        /// </summary>
        /// <param name="objz"></param>
        public static void ExplorerPath(WmiServiceObj objz)
        {
            DirectoryInfo adi;
            FileHelper.GetShortDirAndSetDirInfo(objz.Path, out adi);
            string sPath = Environment.GetEnvironmentVariable("windir");
            Process.Start(string.Format("{0}{1}Explorer.exe", sPath, Path.DirectorySeparatorChar), adi.FullName);
        }


        public static bool SwitchServiceStatusTo(WmiServiceObj objBiz, SortableList<WmiServiceObj> arRef)
        {
            var sc = new ServiceController(objBiz.Name);
            if (sc.Status == ServiceControllerStatus.Running)
            {
                if (sc.CanStop)
                {
                    sc.Stop();
                    Console.WriteLine("Service {0} stoped", sc.ServiceName);
                    return true;
                }
            }
            else
            {
                foreach (WmiServiceObj abz in arRef.Where(abz => sc.ServiceName == abz.Name))
                {
                    if (abz.StartMode.ToLower() != "disabled")
                    {
                        sc.Start();
                        return true;
                    }
                    break;
                }
            }

            sc.Refresh();
            return false;
        }

        /// <summary>
        /// </summary>
        /// <param name="objz"></param>
        /// <param name="scs"></param>
        /// <param name="arRef">for check service startmode</param>
        /// <returns></returns>
        public static bool SwitchServiceStatusTo(WmiServiceObj objz, ServiceControllerStatus scs,
            SortableList<WmiServiceObj> arRef)
        {
            var sc = new ServiceController(objz.Name);

            switch (scs)
            {
                case ServiceControllerStatus.Running:
                    foreach (WmiServiceObj abz in arRef.Where(abz => sc.ServiceName == abz.Name))
                    {
                        if (abz.StartMode.ToLower() != "disabled")
                        {
                            sc.Start();
                            return true;
                        }
                        break;
                    }

                    break;
                case ServiceControllerStatus.Stopped:
                    if (sc.CanStop)
                    {
                        sc.Stop();
                        Console.WriteLine("Service {0} stoped", sc.ServiceName);
                        return true;
                    }
                    break;
            }

            sc.Refresh();
            return false;
        }

        public ManagementScope GlobalWmiHelper(string serverName, string adminUser,
            string adminUserPassword)
        {
            ManagementScope managementScope;
            try
            {
                var connectionOptions = new ConnectionOptions();
                string wmiPath;
                if (string.IsNullOrEmpty(serverName) ||
                    string.IsNullOrEmpty(adminUser) ||
                    string.IsNullOrEmpty(adminUserPassword))
                {
                    wmiPath = @"root\cimv2";
                }
                else
                {
                    connectionOptions.Username = adminUser;
                    connectionOptions.Password = adminUserPassword;
                    wmiPath = String.Format(
                        @"\\{0}\root\cimv2", serverName);
                }
                managementScope = new ManagementScope(wmiPath, connectionOptions);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    String.Format("WMI exception: {0}", ex.Message));
            }
            return managementScope;
        }

        /// <summary>
        /// </summary>
        /// <param name="objz"></param>
        /// <param name="cmdTag"></param>
        /// <returns></returns>
        public static bool ChangeServiceStartMode(WmiServiceObj objz, string cmdTag)
        {
            bool bRel;
            var sc = new ServiceController(objz.Name);
            string startupType = "";

            var myPath = new ManagementPath
            {
                Server = Environment.MachineName,
                NamespacePath = @"root\CIMV2",
                RelativePath = string.Format("Win32_Service.Name='{0}'", sc.ServiceName)
            };
            switch (cmdTag)
            {
                case "41":
                    startupType = "Manual";
                    break;
                case "42":
                    startupType = "Auto";
                    break;
                case "43":
                    startupType = "Disabled";
                    break;
            }
            using (var service = new ManagementObject(myPath))
            {
                ManagementBaseObject inputArgs = service.GetMethodParameters("ChangeStartMode");
                inputArgs["startmode"] = startupType;
                service.InvokeMethod("ChangeStartMode", inputArgs, null);
                bRel = true;
            }

            sc.Refresh();
            return bRel;
        }

        /// <summary>
        /// </summary>
        /// <param name="objz"></param>
        /// <param name="sMessage"></param>
        /// <returns></returns>
        public static bool DeleteService(WmiServiceObj objz, out string sMessage)
        {
            bool bRel;
            var sc = new ServiceController(objz.Name);

            var myPath = new ManagementPath
            {
                Server = Environment.MachineName,
                NamespacePath = @"root\CIMV2",
                RelativePath = string.Format("Win32_Service.Name='{0}'", sc.ServiceName)
            };

            using (var service = new ManagementObject(myPath))
            {
                ManagementBaseObject outParams = service.InvokeMethod("delete", null, null);
                sMessage = outParams["ReturnValue"].ToString();
                bRel = true;
            }

            sc.Refresh();
            return bRel;
        }
    }
}