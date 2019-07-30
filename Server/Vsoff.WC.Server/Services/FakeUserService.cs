using System.Collections.Generic;
using System.Linq;
using Vsoff.WC.Common.Enums;
using Vsoff.WC.Domain.Main;

namespace Vsoff.WC.Server.Services
{
    public class FakeUserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User {Login = "admin@gmail.com", Password = "12345", Role = RoleTypes.Admin},
            new User {Login = "qwerty", Password = "55555", Role = RoleTypes.User}
        };

        public User GetUser(string userName)
        {
            return _users.First(x => x.Login == userName);
        }

        public User GetUser(string userName, string passwordHash)
        {
            return _users.FirstOrDefault(x => x.Login == userName && x.Password == passwordHash);
        }
    }
}