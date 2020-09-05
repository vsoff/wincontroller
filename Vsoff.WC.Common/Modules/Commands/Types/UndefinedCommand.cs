namespace Vsoff.WC.Common.Modules.Commands.Types
{
    public class UndefinedCommand : ICommand
    {
        public string Message { get; set; }

        public UndefinedCommand(string message)
        {
            Message = message;
        }
    }
}
