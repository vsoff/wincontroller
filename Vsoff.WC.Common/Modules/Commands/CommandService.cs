using System;
using System.Collections.Generic;
using System.Linq;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands.Handlers;
using Vsoff.WC.Common.Modules.Commands.Types;

namespace Vsoff.WC.Common.Modules.Commands
{
    public interface ICommandService
    {
        void InvokeCommand(ICommand command);
    }

    public class CommandService : ICommandService
    {
        private readonly IMessenger _messenger;

        private readonly Dictionary<Type, ICommandHandler> _commandHandlersMap;

        public CommandService(
            ICommandHandler[] commandHandlers,
            IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

            _commandHandlersMap = commandHandlers.ToDictionary(key => key.CommandType, val => val);
        }

        public void InvokeCommand(ICommand command)
        {
            Type t = command.GetType();

            if (!_commandHandlersMap.ContainsKey(t))
                throw new KeyNotFoundException($"Нет зарегистрированного обработчика команды с типом `{t}`");

            try
            {
                _commandHandlersMap[t].Handle(command);
            }
            catch (Exception ex)
            {
                _messenger.Send($"Во время выполнения команды типа {t.Name} произошло исключение");
            }
        }
    }
}
