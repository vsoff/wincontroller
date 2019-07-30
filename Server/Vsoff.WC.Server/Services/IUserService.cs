using System;
using System.Text;
using Vsoff.WC.Domain.Main;

namespace Vsoff.WC.Server.Services
{
    public interface IUserService
    {
        User GetUser(string userName);
        User GetUser(string userName, string passwordHash);
    }
}
