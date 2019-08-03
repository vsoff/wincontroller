﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Core.Modules.Commands.Types;

namespace Vsoff.WC.Client.Modules.Commands.Types
{
    public class VolumeCommand : Command
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