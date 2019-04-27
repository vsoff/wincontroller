using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands.Types;
using Vsoff.WC.Common.Modules.System;
using Vsoff.WC.Core.Common;

namespace Vsoff.WC.Common.Modules.Commands.Handlers
{
    public class StatusCommandHandler : CommandHandler<StatusCommand>
    {
        private readonly IMessenger _messenger;
        private readonly ISystemService _systemService;

        public StatusCommandHandler(
            ISystemService systemService,
            IMessenger messenger)
        {
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(StatusCommand command)
        {
            SystemInfo info = _systemService.GetSystemInfo();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($" ======= System info ======= ");
            sb.AppendLine($"* Sys time: {DateTime.Now}");
            sb.AppendLine($"* App start in: {info.StartTime}");
            sb.AppendLine($"* App uptime: {info.AppUptime}");
            sb.AppendLine($"* Sys uptime: {info.SystemUptime}");
            sb.AppendLine($"* MachineName: {info.MachineName}");
            sb.AppendLine($"* UserName: {info.UserName}");
            sb.AppendLine($"* Admin: {(info.IsAdminUser ? "Yes" : "No")}");
            sb.AppendLine($"* IP: {info.PublicIP}");
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
