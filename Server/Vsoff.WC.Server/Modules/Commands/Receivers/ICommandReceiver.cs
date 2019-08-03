namespace Vsoff.WC.Server.Modules.Commands
{
    /// <summary>
    /// Ретранслирует новые команды в приложение.
    /// </summary>
    public interface ICommandReceiver
    {
        void Start();
        void Stop();
    }
}