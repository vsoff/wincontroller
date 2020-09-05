using Vsoff.WC.Common.Modules.Commands.Types;

namespace Vsoff.WC.Common.Modules.Commands.Converters
{
    public interface ICommandConverter
    {
        ICommand Convert(string text);
    }
}
