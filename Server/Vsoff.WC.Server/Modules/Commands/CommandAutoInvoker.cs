using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Server.Modules.Messengers;

namespace Vsoff.WC.Server.Modules.Commands
{
    public interface ICommandAutoInvoker
    {
    }

    public class CommandAutoInvoker : ICommandAutoInvoker
    {
        private readonly ICommandInvoker _commandInvoker;
        private readonly IMessenger _messenger;

        public CommandAutoInvoker(
            ICommandInvoker commandInvoker,
            IMessenger messenger)
        {
            _commandInvoker = commandInvoker ?? throw new ArgumentNullException(nameof(commandInvoker));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

            _messenger.OnCommand += OnCommand;
        }

        private void OnCommand(object sender, CommandEventArgs e) => _commandInvoker.InvokeCommand(e.Command);
    }
}