using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Core.Modules.Commands.Types;

namespace Vsoff.WC.Server.Modules.Commands.Types
{
    public class SetMachineCommand : Command
    {
        public Guid MachineId { get; set; }
    }
}