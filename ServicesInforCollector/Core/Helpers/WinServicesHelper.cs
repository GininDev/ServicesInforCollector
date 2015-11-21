using System;
using System.Globalization;
using System.Management;
using System.Windows.Forms;

namespace ServicesInforCollector.Core.Helpers
{
    internal class WinServicesHelper
    {
        #region Enum

        public enum OnError
        {
            UserIsNotNotified = 0,
            UserIsNotified = 1,
            SystemRestartedLastGoodConfiguraion = 2,
            SystemAttemptStartWithGoodConfiguration = 3
        }

        public enum ReturnValue
        {
            Success = 0,
            NotSupported = 1,
            AccessDenied = 2,
            DependentServicesRunning = 3,
            InvalidServiceControl = 4,
            ServiceCannotAcceptControl = 5,
            ServiceNotActive = 6,
            ServiceRequestTimeout = 7,
            UnknownFailure = 8,
            PathNotFound = 9,
            ServiceAlreadyRunning = 10,
            ServiceDatabaseLocked = 11,
            ServiceDependencyDeleted = 12,
            ServiceDependencyFailure = 13,
            ServiceDisabled = 14,
            ServiceLogonFailure = 15,
            ServiceMarkedForDeletion = 16,
            ServiceNoThread = 17,
            StatusCircularDependency = 18,
            StatusDuplicateName = 19,
            StatusInvalidName = 20,
            StatusInvalidParameter = 21,
            StatusInvalidServiceAccount = 22,
            StatusServiceExists = 23,
            ServiceAlreadyPaused = 24,
            ServiceNotFound = 25
        }

        public enum ServiceState
        {
            Running,
            Stopped,
            Paused,
            StartPending,
            StopPending,
            PausePending,
            ContinuePending
        }

        public enum ServiceType : uint
        {
            KernelDriver = 0x1,
            FileSystemDriver = 0x2,
            Adapter = 0x4,
            RecognizerDriver = 0x8,
            OwnProcess = 0x10,
            ShareProcess = 0x20,
            Interactive = 0x100
        }

        public enum StartMode
        {
            Boot = 0,
            System = 1,
            Automatic = 2,
            Manual = 3,
            Disabled = 4
        }

        #endregion Enum

        public static ReturnValue InstallService(string svcName, string svcDispName, string svcPath, ServiceType svcType,
            OnError errHandle, StartMode svcStartMode, bool interactWithDesktop, string svcStartName, string svcPassword,
            string loadOrderGroup, string[] loadOrderGroupDependencies, string[] svcDependencies)
        {
            var mc = new ManagementClass("Win32_Service");
            ManagementBaseObject inParams = mc.GetMethodParameters("create");
            inParams["Name"] = svcName;
            inParams["DisplayName"] = svcDispName;
            inParams["PathName"] = svcPath;
            inParams["ServiceType"] = svcType;
            inParams["ErrorControl"] = errHandle;
            inParams["StartMode"] = svcStartMode.ToString();
            inParams["DesktopInteract"] = interactWithDesktop;
            inParams["StartName"] = svcStartName;
            inParams["StartPassword"] = svcPassword;
            inParams["LoadOrderGroup"] = loadOrderGroup;
            inParams["LoadOrderGroupDependencies"] = loadOrderGroupDependencies;
            inParams["ServiceDependencies"] = svcDependencies;

            try
            {
                ManagementBaseObject outParams = mc.InvokeMethod("create", inParams, null);

                return (ReturnValue) Enum.Parse(typeof (ReturnValue), outParams["ReturnValue"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ReturnValue UninstallService(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    ManagementBaseObject outParams = service.InvokeMethod("delete", null, null);

                    return (ReturnValue) Enum.Parse(typeof (ReturnValue), outParams["ReturnValue"].ToString());
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Trim() == "not found" || ex.GetHashCode() == 41149443)
                        return ReturnValue.ServiceNotFound;
                    throw ex;
                }
            }
        }

        public static ReturnValue StartService(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    ManagementBaseObject outParams = service.InvokeMethod("StartService", null, null);

                    return (ReturnValue) Enum.Parse(typeof (ReturnValue), outParams["ReturnValue"].ToString());
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Trim() == "not found" || ex.GetHashCode() == 41149443)
                        return ReturnValue.ServiceNotFound;
                    throw ex;
                }
            }
        }

        public static ReturnValue StopService(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    ManagementBaseObject outParams = service.InvokeMethod("StopService", null, null);

                    return (ReturnValue) Enum.Parse(typeof (ReturnValue), outParams["ReturnValue"].ToString());
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Trim() == "not found" || ex.GetHashCode() == 41149443)
                        return ReturnValue.ServiceNotFound;
                    throw ex;
                }
            }
        }

        public static ReturnValue ResumeService(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    ManagementBaseObject outParams = service.InvokeMethod("ResumeService", null, null);

                    return (ReturnValue) Enum.Parse(typeof (ReturnValue), outParams["ReturnValue"].ToString());
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Trim() == "not found" || ex.GetHashCode() == 41149443)
                        return ReturnValue.ServiceNotFound;
                    throw ex;
                }
            }
        }

        public static ReturnValue PauseService(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    ManagementBaseObject outParams = service.InvokeMethod("PauseService", null, null);

                    return (ReturnValue) Enum.Parse(typeof (ReturnValue), outParams["ReturnValue"].ToString());
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Trim() == "not found" || ex.GetHashCode() == 41149443)
                        return ReturnValue.ServiceNotFound;
                    throw ex;
                }
            }
        }

        public static ReturnValue ChangeStartMode(string svcName, StartMode startMode)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                ManagementBaseObject inParams = service.GetMethodParameters("ChangeStartMode");
                inParams["StartMode"] = startMode.ToString();
                try
                {
                    ManagementBaseObject outParams = service.InvokeMethod("ChangeStartMode", inParams, null);

                    return (ReturnValue) Enum.Parse(typeof (ReturnValue), outParams["ReturnValue"].ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static bool IsServiceInstalled(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    ManagementBaseObject outParams = service.InvokeMethod("InterrogateService", null, null);

                    return true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Trim() == "not found" || ex.GetHashCode() == 41149443)
                        return false;
                    throw ex;
                }
            }
        }

        public static ServiceState GetServiceState(string svcName)
        {
            var toReturn = ServiceState.Stopped;

            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    string state = service.Properties["State"].Value.ToString().Trim();
                    switch (state)
                    {
                        case "Running":
                            toReturn = ServiceState.Running;
                            break;
                        case "Stopped":
                            toReturn = ServiceState.Stopped;
                            break;
                        case "Paused":
                            toReturn = ServiceState.Paused;
                            break;
                        case "Start Pending":
                            toReturn = ServiceState.StartPending;
                            break;
                        case "Stop Pending":
                            toReturn = ServiceState.StopPending;
                            break;
                        case "Continue Pending":
                            toReturn = ServiceState.ContinuePending;
                            break;
                        case "Pause Pending":
                            toReturn = ServiceState.PausePending;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return toReturn;
        }

        public static bool CanStop(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    return bool.Parse(service.Properties["AcceptStop"].Value.ToString());
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool CanPauseAndContinue(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    return bool.Parse(service.Properties["AcceptPause"].Value.ToString());
                }
                catch
                {
                    return false;
                }
            }
        }

        public static int GetProcessId(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    return int.Parse(service.Properties["ProcessId"].Value.ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static string GetPath(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    return service.Properties["PathName"].Value.ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }


        public static bool ShowProperties(string svcName)
        {
            string objPath = string.Format("Win32_Service.Name='{0}'", svcName);
            using (var service = new ManagementObject(new ManagementPath(objPath)))
            {
                try
                {
                    foreach (PropertyData a in service.SystemProperties)
                        MessageBox.Show(PropData2String(a));
                    foreach (PropertyData a in service.Properties)
                        MessageBox.Show(PropData2String(a));
                    if (service.Properties["State"].Value.ToString().Trim() == "Running")
                        return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        private static string PropData2String(PropertyData pd)
        {
            string toReturn = string.Format("Name: {0}{1}", pd.Name, Environment.NewLine);
            toReturn += string.Format("IsArray: {0}{1}", pd.IsArray.ToString(), Environment.NewLine);
            toReturn += string.Format("IsLocal: {0}{1}", pd.IsLocal.ToString(), Environment.NewLine);
            toReturn += string.Format("Origin: {0}{1}", pd.Origin, Environment.NewLine);
            toReturn += string.Format("CIMType: {0}{1}", pd.Type.ToString(), Environment.NewLine);
            if (pd.Value != null)
                toReturn += string.Format("Value: {0}{1}", pd.Value.ToString(), Environment.NewLine);
            else
                toReturn += string.Format("Value is null{0}", Environment.NewLine);
            int i = 0;
            foreach (QualifierData qd in pd.Qualifiers)
            {
                toReturn += string.Format("\tQualifier[{0}]IsAmended: {1}{2}", i.ToString(CultureInfo.InvariantCulture),
                    qd.IsAmended.ToString(), Environment.NewLine);
                toReturn += string.Format("\tQualifier[{0}]IsLocal: {1}{2}", i.ToString(CultureInfo.InvariantCulture),
                    qd.IsLocal.ToString(), Environment.NewLine);
                toReturn += string.Format("\tQualifier[{0}]IsOverridable: {1}{2}",
                    i.ToString(CultureInfo.InvariantCulture), qd.IsOverridable.ToString(), Environment.NewLine);
                toReturn += string.Format("\tQualifier[{0}]Name: {1}{2}", i.ToString(CultureInfo.InvariantCulture),
                    qd.Name, Environment.NewLine);
                toReturn += string.Format("\tQualifier[{0}]PropagatesToInstance: {1}{2}",
                    i.ToString(CultureInfo.InvariantCulture), qd.PropagatesToInstance.ToString(), Environment.NewLine);
                toReturn += string.Format("\tQualifier[{0}]PropagatesToSubclass: {1}{2}",
                    i.ToString(CultureInfo.InvariantCulture), qd.PropagatesToSubclass.ToString(), Environment.NewLine);
                if (qd.Value != null)
                    toReturn += string.Format("\tQualifier[{0}]Value: {1}{2}", i.ToString(CultureInfo.InvariantCulture),
                        qd.Value.ToString(), Environment.NewLine);
                else
                    toReturn += string.Format("\tQualifier[{0}]Value is null{1}",
                        i.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
                i++;
            }
            return toReturn;
        }
    }
}