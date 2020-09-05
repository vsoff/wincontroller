using System;

namespace Vsoff.WC.Common.Modules.Commands.Types
{
    public class ShutdownCommand : ICommand
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
