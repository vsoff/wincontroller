using System;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Commands;

namespace Vsoff.WC.Core.Modules.Commands.Handlers
{
    public abstract class CommandHandler<TCommand> : ICommandHandler where TCommand : Command
    {
        public Type CommandType => typeof(TCommand);

        public void Handle(UserCommand userCommand)
        {
            Type t = userCommand.Command.GetType();
            if (t != CommandType)
                throw new ArgumentException($"Тип команды {t}, а должен быть {CommandType}");

            Handle(userCommand, (TCommand) userCommand.Command);
        }

        protected abstract void Handle(UserCommand userCommand, TCommand command);
    }
}