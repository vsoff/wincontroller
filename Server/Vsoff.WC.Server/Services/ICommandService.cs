using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Server.Modules.Commands;

namespace Vsoff.WC.Server.Services
{
    public interface ICommandService
    {
        Guid Add(CommandInfo commandInfo);
        Command[] GetCommands(Guid machineId);
    }

    public class FakeCommandService : ICommandService
    {
        private readonly ConcurrentDictionary<Guid, CommandInfo> _memory;

        public FakeCommandService()
        {
            _memory = new ConcurrentDictionary<Guid, CommandInfo>();
        }

        public Guid Add(CommandInfo commandInfo)
        {
            var id = Guid.NewGuid();
            commandInfo.CommandId = id;
            _memory[id] = commandInfo;
            return id;
        }

        public Command[] GetCommands(Guid machineId)
            => _memory.Where(x => x.Value.MachineId == machineId).Select(x => x.Value.Command).ToArray();
    }
}