using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Core.Modules.Commands.Types;

namespace Vsoff.WC.Client.Modules.Commands.Types
{
    public class AutorunCommand : Command
    {
        public Command Type { get; set; }

        public AutorunCommand(Command commandType)
        {
            Type = commandType;
        }

        public enum Command
        {
            Undefined,
            AddAutorun,
            RemoveAutorun
        }
    }
}
