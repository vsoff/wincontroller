﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsoff.WC.Client.Modules.System
{
    public class SystemInfo
    {
        public TimeSpan SystemUptime { get; set; }
        public TimeSpan AppUptime { get; set; }
        public DateTime StartTime { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public bool IsAdminUser { get; set; }
        public string PublicIp { get; set; }
        public string MonitorResolution { get; set; }
        public string AppVersion { get; set; }
    }
}
