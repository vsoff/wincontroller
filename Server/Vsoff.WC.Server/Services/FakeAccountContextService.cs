using System.Collections.Concurrent;
using Vsoff.WC.Server.Modules.Menu;

namespace Vsoff.WC.Server.Services
{
    public class FakeAccountContextService : IAccountContextService
    {
        private readonly object _syncLocker;
        private readonly ConcurrentDictionary<int, AccountContext> _memory;

        public FakeAccountContextService()
        {
            _syncLocker = new object();
            _memory = new ConcurrentDictionary<int, AccountContext>();
        }

        private AccountContext GetOrCreateAccountContext(int accountId)
        {
            lock (_syncLocker)
            {
                if (!_memory.ContainsKey(accountId))
                    _memory[accountId] = new AccountContext
                    {
                        CurrentMenuType = MenuType.Undefined,
                        MachineId = null
                    };
                return _memory[accountId];
            }
        }

        public AccountContext GetAccountContext(int accountId) => GetOrCreateAccountContext(accountId);

        public void SetAccountCurrentMenu(int accountId, MenuType menuType)
            => GetOrCreateAccountContext(accountId).CurrentMenuType = menuType;

        public void SetAccountCurrentMachineId(int accountId, int machineId)
            => GetOrCreateAccountContext(accountId).MachineId = machineId;
    }
}