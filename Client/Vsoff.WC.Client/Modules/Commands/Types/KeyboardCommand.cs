using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsoff.WC.Client.Modules.Commands.Types
{
    public class KeyboardCommand : ICommand
    {
        public string Keys { get; set; }

        public KeyboardCommand(string keys)
        {
            Keys = keys;
        }
    }
}
