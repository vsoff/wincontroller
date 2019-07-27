using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Client.Modules.Commands.Types;

namespace Vsoff.WC.Client.Modules.Commands.Handlers
{
    public interface ICommandHandler
    {
        Type CommandType { get; }
        void Handle(ICommand command);
    }

    public abstract class CommandHandler<TCommand> : ICommandHandler where TCommand : ICommand
    {
        public Type CommandType => typeof(TCommand);

        public void Handle(ICommand command)
        {
            if (command.GetType() != CommandType)
                throw new ArgumentException($"Тип команды {command.GetType()}, а должен быть {CommandType}");

            Handle((TCommand)command);
        }

        public virtual void Handle(TCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
