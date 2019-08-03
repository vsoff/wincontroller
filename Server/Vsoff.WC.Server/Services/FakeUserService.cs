using System;
using System.Collections.Generic;
using System.Linq;
using Vsoff.WC.Common.Enums;
using Vsoff.WC.Core.Modules.Configs;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Configs;

namespace Vsoff.WC.Server.Services
{
    public class FakeUserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User {Login = "admin@gmail.com", Password = "12345", Role = RoleTypes.Admin},
            new User {Login = "qwerty", Password = "55555", Role = RoleTypes.User}
        };

        public FakeUserService(IConfigService<ServerConfig> scs)
        {
            var config = scs.GetConfig();
            _users.Add(new User
            {
                Login = "admin",
                Password = Guid.NewGuid().ToString("N"),
                Role = RoleTypes.Admin,
                TelegramId = config.AdminTelegramId
            });
        }

        public User GetUser(string userName) => _users.First(x => x.Login == userName);

        public User GetUser(string userName, string passwordHash) =>
            _users.FirstOrDefault(x => x.Login == userName && x.Password == passwordHash);

        public User GetUserByTelegramId(int telegramId) => _users.FirstOrDefault(x => x.TelegramId == telegramId);
    }
}