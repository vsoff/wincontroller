using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsoff.WC.Client.Messengers;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Client.Modules.System;
using Vsoff.WC.Client.Modules.System.Services;
using Vsoff.WC.Core.Modules.Commands;

namespace Vsoff.WC.Client.Modules.Commands.Handlers
{
    public class LockCommandHandler : CommandHandler<LockCommand>
    {
        private readonly IMessenger _messenger;
        private readonly ISystemService _systemService;

        public LockCommandHandler(
            ISystemService systemService,
            IMessenger messenger)
        {
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(LockCommand command)
        {
            _systemService.LockWorkStation();
            _messenger.Send($"Устройство было заблокировано");
        }
    }
}
