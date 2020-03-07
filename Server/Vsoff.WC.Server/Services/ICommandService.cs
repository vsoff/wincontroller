using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Server.Modules.Commands;

namespace Vsoff.WC.Server.Services
{
    public interface ICommandService
    {
        Guid Add(UserCommand userCommand);
        Command[] GetCommands(int machineId);
    }
}