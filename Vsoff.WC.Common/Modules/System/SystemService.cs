using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Vsoff.WC.Common.Modules.System
{
    public interface ISystemService
    {
        string ApplicationFolder { get; }
        string MachineName { get; }

        Process ExecuteCmd(string commandName, string arguments);
        void Shutdown(TimeSpan delay);
        void LockWorkStation();
        SystemInfo GetSystemInfo();
        void ShutdownAbort();
    }

    public class SystemService : ISystemService
    {
        public string ApplicationFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string MachineName => Environment.MachineName;

        private readonly DateTime _applicationStartTime;

        public SystemService()
        {
            _applicationStartTime = DateTime.Now;
        }

        public SystemInfo GetSystemInfo()
        {

            return new SystemInfo
            {
                PublicIP = GetIpAddress(),
                StartTime = _applicationStartTime,
                IsAdminUser = IsUserAdministrator(),
                MachineName = MachineName,
                UserName = Environment.UserName,
                AppUptime = DateTime.Now - _applicationStartTime,
                SystemUptime = GetSystemUptime(),
                MonitorResolution = $"{Screen.PrimaryScreen.WorkingArea.Width}x{Screen.PrimaryScreen.WorkingArea.Height}",
                AppVersion = GetAppVersion()
            };
        }

        public void Shutdown(TimeSpan delay) => ExecuteCmd("shutdown", $"/s /t {(int)delay.TotalSeconds}");

        public void ShutdownAbort() => ExecuteCmd("shutdown", "/a");

        public void LockWorkStation() => ExecuteCmd("rundll32.exe", "user32.dll, LockWorkStation");

        public Process ExecuteCmd(string commandName, string arguments)
        {
            var psi = new ProcessStartInfo(commandName, arguments);
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            return Process.Start(psi);
        }

        #region Вспомогательные методы

        private string GetAppVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        }

        private string GetIpAddress()
        {
            string pubIp = string.Empty;

            try
            {
                pubIp = new WebClient().DownloadString("https://api.ipify.org");
            }
            catch (Exception ex)
            {
            }

            return pubIp;
        }

        private TimeSpan GetSystemUptime()
        {
            using (var uptime = new PerformanceCounter("System", "System Up Time"))
            {
                uptime.NextValue();
                return TimeSpan.FromSeconds(uptime.NextValue());
            }
        }

        private bool IsUserAdministrator()
        {
            bool isAdmin = false;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);//Is Admin
            }
            //Is not admin
            catch (UnauthorizedAccessException ex) { }
            catch (Exception ex) { }

            return isAdmin;
        }

        #endregion
    }
}
