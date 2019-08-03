using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Client.Modules.System;
using Vsoff.WC.Client.Modules.System.Services;
using Vsoff.WC.Core.Modules.Configs;

namespace Vsoff.WC.Client.Modules.Config
{
    public class AppConfigService : ConfigService<AppConfig>
    {
        protected override string ConfigName => "config.json";
    }
}