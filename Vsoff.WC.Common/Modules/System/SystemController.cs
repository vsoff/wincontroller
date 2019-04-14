using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Vsoff.WC.Common.Modules.System
{
    public interface ISystemController
    {
        string MachineName { get; }

        void ExecuteCmd(string commandName, string arguments);
        SystemInfo GetSystemInfo();
        void Shutdown(TimeSpan delay);
        void ShutdownAbort();
    }

    public class SystemController : ISystemController
    {
        public string MachineName => Environment.MachineName;

        private readonly DateTime _applicationStartTime;

        public SystemController()
        {
            _applicationStartTime = DateTime.Now;
        }

        public SystemInfo GetSystemInfo()
        {
            string pubIp = string.Empty;

            try
            {
                pubIp = new WebClient().DownloadString("https://api.ipify.org");
            }
            catch (Exception ex)
            {
            }

            return new SystemInfo
            {
                PublicIP = pubIp,
                StartTime = _applicationStartTime,
                IsAdminUser = IsUserAdministrator(),
                MachineName = MachineName,
            };
        }

        public void Shutdown(TimeSpan delay) => ExecuteCmd("shutdown", $"/s /t {(int)delay.TotalSeconds}");

        public void ShutdownAbort() => ExecuteCmd("shutdown", $"/a");

        public void ExecuteCmd(string commandName, string arguments)
        {
            var psi = new ProcessStartInfo(commandName, arguments);
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
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
    }
}
