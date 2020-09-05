namespace Vsoff.WC.Common.Modules.Commands.Types
{
    public class AutorunCommand : ICommand
    {
        public Command Type { get; set; }

        public AutorunCommand(Command commandType)
        {
            Type = commandType;
        }

        public enum Command
        {
            Undefined,
            AddAutorun,
            RemoveAutorun
        }
    }
}
