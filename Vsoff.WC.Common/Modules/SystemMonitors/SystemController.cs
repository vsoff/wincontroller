using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsoff.WC.Common.Modules.SystemMonitors
{
    public interface ISystemMonitor
    {
        SystemInfo GetSystemInfo();
    }

    public class SystemController : ISystemMonitor
    {
        private readonly DateTime _applicationStartTime;

        public SystemController()
        {
            _applicationStartTime = DateTime.Now;
        }

        public SystemInfo GetSystemInfo() => new SystemInfo
        {
            PublicIP = GetPublicIp(),
            StartTime = _applicationStartTime,
            MachineName = Environment.MachineName,
        };

        private string GetPublicIp()
        {
            string pubIp = string.Empty;

            try
            {
                pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
            }
            catch (Exception ex)
            {
            }

            return pubIp;
        }
    }
}
