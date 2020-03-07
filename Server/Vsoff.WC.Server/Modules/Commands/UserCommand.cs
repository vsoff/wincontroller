using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Domain.Main;

namespace Vsoff.WC.Server.Modules.Commands
{
    public class UserCommand
    {
        public Guid Id { get; set; }
        public int? MachineId { get; set; }
        public int UserId { get; set; }
        public Command Command { get; set; }

        public UserCommand()
        {
        }

        public UserCommand(int userId, int? machineId, Command command)
        {
            UserId = userId;
            MachineId = machineId;
            Command = command;
        }
    }
}