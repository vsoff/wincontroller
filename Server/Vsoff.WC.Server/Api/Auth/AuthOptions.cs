using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Vsoff.WC.Server.Api.Auth
{
    // TODO: Вынести в конфиг.
    public static class AuthOptions
    {
        /// <summary>
        /// Ключ шифрования.
        /// </summary>
        private const string Key = "mysupersecret_secretkey!123";

        /// <summary>
        /// Издатель токена.
        /// </summary>
        public const string Issuer = "MyAuthServer";

        /// <summary>
        /// Потребитель токена.
        /// </summary>
        public const string Audience = "http://localhost:51884/";

        /// <summary>
        /// Время жизни токена.
        /// </summary>
        public static readonly TimeSpan Lifetime = TimeSpan.FromSeconds(20);

        /// <summary>
        /// Возвращает симметричный ключ шифрования.
        /// </summary>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}