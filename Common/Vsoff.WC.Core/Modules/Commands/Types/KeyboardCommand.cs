using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Core.Modules.Commands.Types;

namespace Vsoff.WC.Client.Modules.Commands.Types
{
    public class KeyboardCommand : Command
    {
        public string Keys { get; set; }

        public KeyboardCommand(string keys)
        {
            Keys = keys;
        }
    }
}
