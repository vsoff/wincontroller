using Vsoff.WC.Server.Modules.Menu;

namespace Vsoff.WC.Server.Services
{
    public interface IAccountContextService
    {
        AccountContext GetAccountContext(int accountId);
        void SetAccountCurrentMenu(int accountId, MenuType menuType);
        void SetAccountCurrentMachineId(int accountId, int machineId);
    }
}