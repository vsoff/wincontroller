using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Common.Modules.Commands.Types;

namespace Vsoff.WC.Common.Modules.Commands.Converters
{
    public interface ICommandConverter
    {
        ICommand Convert(string text);
    }
}
