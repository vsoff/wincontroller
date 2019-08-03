using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Core.Modules.Commands.Types;

namespace Vsoff.WC.Client.Modules.Commands.Types
{
    public class UndefinedCommand : Command
    {
        public string Message { get; set; }

        public UndefinedCommand(string message)
        {
            Message = message;
        }
    }
}
