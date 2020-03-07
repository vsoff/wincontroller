using System;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Commands.Handlers;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Commands.Types;
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

        protected override void Handle(UserCommand userCommand, UndefinedCommand command)
        {
            if (!string.IsNullOrWhiteSpace(command.Message))
                _messenger.Send(userCommand.UserId, command.Message);
        }
    }

    public class SetMachineCommandHandler : CommandHandler<SetMachineCommand>
    {
        private readonly IMessenger _messenger;

        public SetMachineCommandHandler(IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        protected override void Handle(UserCommand userCommand, SetMachineCommand command)
        {
            throw new NotImplementedException();
        }
    }
}