using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Client.Messengers;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Client.Modules.System;
using Vsoff.WC.Client.Modules.System.Services;
using Vsoff.WC.Core.Modules.Commands;

namespace Vsoff.WC.Client.Modules.Commands.Handlers
{
    public class ShutdownCommandHandler : CommandHandler<ShutdownCommand>
    {
        private readonly IMessenger _messenger;
        private readonly ISystemService _systemService;

        public ShutdownCommandHandler(
            ISystemService systemService,
            IMessenger messenger)
        {
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(ShutdownCommand command)
        {
            if (command.IsAbort)
            {
                _systemService.ShutdownAbort();
                _messenger.Send($"Выключение компьютера было приостановлено");
            }
            else
            {
                _systemService.Shutdown(command.Delay);
                _messenger.Send($"Компьютер будет выключен через {command.Delay}");
            }
        }
    }
}
