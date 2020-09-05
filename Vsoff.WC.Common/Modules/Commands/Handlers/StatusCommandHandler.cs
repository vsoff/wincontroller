using System;
using System.Text;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands.Types;
using Vsoff.WC.Common.Modules.System;
using Vsoff.WC.Common.Modules.System.Services;
using Vsoff.WC.Core.Common;

namespace Vsoff.WC.Common.Modules.Commands.Handlers
{
    public class StatusCommandHandler : CommandHandler<StatusCommand>
    {
        private readonly IUserMonitoringService _userMonitoringService;
        private readonly ISystemService _systemService;
        private readonly IMessenger _messenger;

        public StatusCommandHandler(
            IUserMonitoringService userMonitoringService,
            ISystemService systemService,
            IMessenger messenger)
        {
            _userMonitoringService = userMonitoringService ?? throw new ArgumentNullException(nameof(userMonitoringService));
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(StatusCommand command)
        {
            SystemInfo info = _systemService.GetSystemInfo();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($" ======= System info ======= ");
            sb.AppendLine($"* Sys time: {DateTime.Now}");
            sb.AppendLine($"* User activity: {_userMonitoringService.GetLastActivityTime()}");
            sb.AppendLine($"* App start in: {info.StartTime}");
            sb.AppendLine($"* App uptime: {info.AppUptime}");
            sb.AppendLine($"* Sys uptime: {info.SystemUptime}");
            sb.AppendLine($"* MachineName: {info.MachineName}");
            sb.AppendLine($"* UserName: {info.UserName}");
            sb.AppendLine($"* Admin: {(info.IsAdminUser ? "Yes" : "No")}");
            sb.AppendLine($"* IP: {info.PublicIp}");
            sb.AppendLine($"* Monitor: {info.MonitorResolution}");
            sb.AppendLine($"* AppVersion: {info.AppVersion}");
            sb.AppendLine($" =========================== ");

            _messenger.Send(new NotifyMessage
            {
                Text = sb.ToString()
            });
        }
    }
}
