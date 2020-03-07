using System;
using System.Text;
using Vsoff.WC.Common.Enums;
using Vsoff.WC.Domain.Main;

namespace Vsoff.WC.Server.Services
{
    public interface IAccountService
    {
        Account GetAccount(int accountId, RoleType? role = null);
        Account GetAccount(string login, string passwordHash = null);
        Account GetUserAccountByTelegramId(int telegramId);
    }
}