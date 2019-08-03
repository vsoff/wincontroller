namespace Vsoff.WC.Server.Modules.Messengers
{
    public interface INotifier
    {
        void Notify(NotifyMessage msg);
    }
}
