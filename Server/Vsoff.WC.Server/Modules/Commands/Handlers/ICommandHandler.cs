using System;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Commands;

namespace Vsoff.WC.Core.Modules.Commands.Handlers
{
    public interface ICommandHandler
    {
        Type CommandType { get; }
        void Handle(CommandInfo commandInfo);
    }
}