using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands.Types;
using Vsoff.WC.Common.Modules.System;

namespace Vsoff.WC.Common.Modules.Commands.Handlers
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
