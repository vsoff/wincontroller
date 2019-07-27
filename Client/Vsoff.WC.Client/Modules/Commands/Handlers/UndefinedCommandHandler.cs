using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Client.Messengers;
using Vsoff.WC.Client.Modules.Commands.Types;

namespace Vsoff.WC.Client.Modules.Commands.Handlers
{
    public class UndefinedCommandHandler : CommandHandler<UndefinedCommand>
    {
        private readonly IMessenger _messenger;

        public UndefinedCommandHandler(IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public override void Handle(UndefinedCommand command)
        {
            if (!string.IsNullOrWhiteSpace(command.Message))
                _messenger.Send(command.Message);
        }
    }
}
