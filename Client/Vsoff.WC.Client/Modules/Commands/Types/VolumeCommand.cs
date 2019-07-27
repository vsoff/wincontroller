using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsoff.WC.Client.Modules.Commands.Types
{
    public class VolumeCommand : ICommand
    {
        public Command Type { get; set; }

        public VolumeCommand(Command commandType)
        {
            Type = commandType;
        }

        public enum Command
        {
            Undefined,
            VolumeDown,
            VolumeUp,
            Mute
        }
    }
}
