namespace Vsoff.WC.Common.Modules.Commands.Types
{
    public class VolumeCommand : ICommand
    {
        public Command Type { get; set; }

        public VolumeCommand(Command commandType)
        {
            Type = commandType;
        }

        public enum Command
        {
            Undefined,
            VolumeDown,
            VolumeUp,
            Mute
        }
    }
}
