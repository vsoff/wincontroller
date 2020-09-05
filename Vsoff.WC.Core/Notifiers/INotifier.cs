using Vsoff.WC.Core.Common;

namespace Vsoff.WC.Core.Notifiers
{
    public interface INotifier
    {
        void Notify(NotifyMessage msg);
    }
}
