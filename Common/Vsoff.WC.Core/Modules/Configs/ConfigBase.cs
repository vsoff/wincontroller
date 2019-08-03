using System;
using System.Collections.Generic;
using System.Text;

namespace Vsoff.WC.Core.Modules.Configs
{
    public abstract class ConfigBase : ICloneable
    {
        public object Clone() => MemberwiseClone();
    }
}