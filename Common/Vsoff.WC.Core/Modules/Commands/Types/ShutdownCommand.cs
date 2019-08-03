using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Core.Modules.Commands.Types;

namespace Vsoff.WC.Client.Modules.Commands.Types
{
    public class ShutdownCommand : Command
    {
        /// <summary>
        /// Задержка до выключения устройства.
        /// </summary>
        public TimeSpan Delay { get; set; }

        /// <summary>
        /// Указывает, является ли данная команда прерыванием выключения устройства.
        /// </summary>
        public bool IsAbort { get; set; }
    }
}
