using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Core.Modules.Configs;

namespace Vsoff.WC.Client.Modules.Config
{
    public class AppConfig : ConfigBase
    {
        public long AdminId { get; set; }
        public string ProxyIp { get; set; }
        public string TelegramToken { get; set; }
        public TimeSpan UserActivitySessionDuration { get; set; }

        public AppConfig()
        {
            AdminId = -1;
            ProxyIp = "127.0.0.1:443";
            TelegramToken = "000000:AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            UserActivitySessionDuration = TimeSpan.FromMinutes(5);
        }
    }
}