using System;
using System.Collections.Generic;
using System.Linq;
using Vsoff.WC.Common.Enums;
using Vsoff.WC.Core.Modules.Configs;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Configs;

namespace Vsoff.WC.Server.Services
{
    public class FakeAccountService : IAccountService
    {
        private readonly List<Account> _users = new List<Account>
        {
            new Account {Id = 2, Login = "admin@gmail.com", Password = "12345", Role = RoleType.Admin},
            new Account {Id = 3, Login = "qwerty", Password = "55555", Role = RoleType.User}
        };

        public FakeAccountService(IConfigService<ServerConfig> scs)
        {
            var config = scs.GetConfig();
            _users.Add(new Account
            {
                Id = 1,
                Login = "admin",
                Password = Guid.NewGuid().ToString("N"),
                Role = RoleType.Admin,
                TelegramId = config.AdminTelegramId
            });
        }

        public Account GetAccount(int accountId, RoleType? role = null)
            => _users.FirstOrDefault(x => x.Id == accountId && x.Role == role);

        public Account GetAccount(string login, string passwordHash = null)
        {
            return passwordHash == null
                ? _users.FirstOrDefault(x => x.Login == login)
                : _users.FirstOrDefault(x => x.Login == login && x.Password == passwordHash);
        }

        public Account GetUserAccountByTelegramId(int telegramId)
            => _users.FirstOrDefault(x => x.TelegramId == telegramId);
    }
}