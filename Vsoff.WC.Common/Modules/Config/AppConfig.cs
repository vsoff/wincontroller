﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsoff.WC.Common.Modules.Config
{
    public class AppConfig : ICloneable
    {
        public long AdminId { get; set; }
        public string ProxyIp { get; set; }
        public string TelegramToken { get; set; }

        public AppConfig()
        {
            AdminId = -1;
            ProxyIp = "127.0.0.1:443";
            TelegramToken = "000000:AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        }

        public AppConfig GetClone() => (AppConfig)Clone();
        public object Clone() => MemberwiseClone();
    }
}
