using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Client.Modules.Commands.Types;

namespace Vsoff.WC.Client.Modules.Commands.Converters
{
    public interface ICommandConverter
    {
        ICommand Convert(string text);
    }
}
