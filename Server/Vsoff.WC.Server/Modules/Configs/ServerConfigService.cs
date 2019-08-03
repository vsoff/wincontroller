using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Core.Modules.Configs;

namespace Vsoff.WC.Server.Modules.Configs
{
    public class ServerConfigService : ConfigService<ServerConfig>
    {
        protected override string ConfigName => "ServerConfig.json";
    }
}