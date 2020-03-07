using System;

namespace Vsoff.WC.Server.Modules.Commands
{
    public class CommandEventArgs : EventArgs
    {
        public UserCommand Command { get; set; }

        public CommandEventArgs()
        {
        }

        public CommandEventArgs(UserCommand command)
        {
            Command = command;
        }
    }
}