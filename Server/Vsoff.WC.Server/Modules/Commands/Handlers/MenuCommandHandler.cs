using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Core.Modules.Commands.Handlers;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Commands.Types;
using Vsoff.WC.Server.Modules.Messengers;

namespace Vsoff.WC.Server.Modules.Commands.Handlers
{
    public class MenuCommandHandler : CommandHandler<MenuCommand>
    {
        private readonly IMessenger _messenger;

        public MenuCommandHandler(IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        protected override void Handle(UserCommand userCommand, MenuCommand command)
        {
            _messenger.Send(userCommand.UserId, command.Menu);
            //throw new NotImplementedException();
        }
    }
}