﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsoff.WC.Common.Modules.SystemMonitors
{
    public class SystemInfo
    {
        public DateTime StartTime { get; set; }
        public TimeSpan Uptime => DateTime.Now - StartTime;
        public string MachineName { get; set; }
        public string PublicIP { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Информация о системе:");
            sb.AppendLine();
            sb.AppendLine($"* Текущее время: {DateTime.Now}");
            sb.AppendLine($"* Время запуска: {StartTime}");
            sb.AppendLine($"* Время работы: {Uptime}");
            sb.AppendLine($"* Имя машины: {MachineName}");
            sb.AppendLine($"* IP машины: {PublicIP}");

            return sb.ToString();
        }
    }
}
