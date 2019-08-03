using System;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Commands.Handlers;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Messengers;

namespace Vsoff.WC.Server.Modules.Commands.Handlers
{
    public class UndefinedCommandHandler : CommandHandler<UndefinedCommand>
    {
        private readonly IMessenger _messenger;

        public UndefinedCommandHandler(IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        protected override void Handle(CommandInfo commandInfo, UndefinedCommand command)
        {
            if (!string.IsNullOrWhiteSpace(command.Message))
                _messenger.Send(new NotifyMessage(command.Message));
        }
    }
}