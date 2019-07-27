using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsoff.WC.Client.Modules.Commands.Types
{
    public class UndefinedCommand : ICommand
    {
        public string Message { get; set; }

        public UndefinedCommand(string message)
        {
            Message = message;
        }
    }
}
