using System;
using System.Collections.Concurrent;
using System.Linq;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Server.Modules.Commands;

namespace Vsoff.WC.Server.Services
{
    public class FakeCommandService : ICommandService
    {
        private readonly ConcurrentDictionary<Guid, UserCommand> _memory;

        public FakeCommandService()
        {
            _memory = new ConcurrentDictionary<Guid, UserCommand>();
        }

        public Guid Add(UserCommand userCommand)
        {
            var id = Guid.NewGuid();
            userCommand.Id = id;
            _memory[id] = userCommand;
            return id;
        }

        public Command[] GetCommands(int machineId)
            => _memory.Where(x => x.Value.MachineId == machineId).Select(x => x.Value.Command).ToArray();
    }
}