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

        public void Handle(CommandInfo commandInfo)
        {
            Type t = commandInfo.Command.GetType();
            if (t != CommandType)
                throw new ArgumentException($"Тип команды {t}, а должен быть {CommandType}");

            Handle(commandInfo, (TCommand) commandInfo.Command);
        }

        protected abstract void Handle(CommandInfo commandInfo, TCommand command);
    }
}