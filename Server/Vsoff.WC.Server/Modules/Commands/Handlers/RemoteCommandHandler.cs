using System;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Commands.Handlers;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Messengers;
using Vsoff.WC.Server.Services;

namespace Vsoff.WC.Server.Modules.Commands.Handlers
{
    /// <summary>
    /// Передаёт выполнение команды на удалённое устройство.
    /// </summary>
    public class RemoteCommandHandler<TCommand> : CommandHandler<TCommand> where TCommand : Command
    {
        private readonly ICommandService _commandService;
        private readonly IMessenger _messenger;

        public RemoteCommandHandler(
            ICommandService commandService,
            IMessenger messenger)
        {
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        protected override void Handle(CommandInfo commandInfo, TCommand command)
        {
            if (commandInfo.MachineId == Guid.Empty)
            {
                _messenger.Send(new NotifyMessage("Не указана машина, для которой передаётся команда"));
                return;
            }

            _commandService.Add(commandInfo);
        }
    }
}