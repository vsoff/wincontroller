using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsoff.WC.Common.Modules.Commands.Types
{
    public class VolumeCommand : ICommand
    {
        public CommandType Type { get; set; }

        public VolumeCommand(CommandType commandType)
        {
            Type = commandType;
        }

        public enum CommandType
        {
            Undefined,
            VolumeDown,
            VolumeUp,
            Mute
        }
    }
}
