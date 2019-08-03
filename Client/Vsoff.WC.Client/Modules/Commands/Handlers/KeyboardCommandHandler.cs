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
    public class KeyboardCommandHandler : CommandHandler<KeyboardCommand>
    {
        private readonly IMessenger _messenger;
        private readonly ISystemService _systemService;

        public KeyboardCommandHandler(
            ISystemService systemService,
            IMessenger messenger)
        {
            _systemService = systemService ?? throw new ArgumentNullException(nameof(systemService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(KeyboardCommand command)
        {
            try
            {
                SendKeys.SendWait(command.Keys);
            }
            catch (Exception ex)
            {
                _messenger.Send($"Не удалось выполнить нажатие клавиш.\nОшибка: {ex.Message}");
            }
        }
    }
}
