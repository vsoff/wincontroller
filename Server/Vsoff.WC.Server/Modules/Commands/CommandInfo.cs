using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Domain.Main;

namespace Vsoff.WC.Server.Modules.Commands
{
    public class CommandInfo
    {
        public Guid CommandId { get; set; }
        public Guid MachineId { get; set; }
        public User User { get; set; }
        public Command Command { get; set; }

        public CommandInfo()
        {
        }

        public CommandInfo(User user, Command command)
        {
            User = user;
            Command = command;
        }
    }
}