namespace Vsoff.WC.Common.Modules.Commands.Types
{
    public class KeyboardCommand : ICommand
    {
        public string Keys { get; set; }

        public KeyboardCommand(string keys)
        {
            Keys = keys;
        }
    }
}
