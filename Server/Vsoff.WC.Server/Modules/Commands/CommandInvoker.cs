using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Commands.Handlers;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Domain.Main;

namespace Vsoff.WC.Server.Modules.Commands
{
    public interface ICommandInvoker
    {
        void InvokeCommand(UserCommand userCommand);
    }

    public class CommandInvoker : ICommandInvoker
    {
        private readonly ConcurrentDictionary<Type, ICommandHandler> _commandHandlersMap;

        public CommandInvoker(ICommandHandler[] commandHandlers)
        {
            if (commandHandlers == null) throw new ArgumentNullException(nameof(commandHandlers));
            var dict = commandHandlers.ToDictionary(key => key.CommandType, val => val);
            _commandHandlersMap = new ConcurrentDictionary<Type, ICommandHandler>(dict);
        }

        public void InvokeCommand(UserCommand userCommand)
        {
            Type t = userCommand.Command.GetType();

            if (!_commandHandlersMap.TryGetValue(t, out var handler))
                throw new KeyNotFoundException($"Нет зарегистрированного обработчика команды с типом `{t}`");

            handler.Handle(userCommand);
        }
    }
}