using Vsoff.WC.Domain.Auth;
using Vsoff.WC.Models.Auth;

namespace Vsoff.WC.Core.Mappers
{
    public static class AuthMapper
    {
        public static TokenModel ToModel(this Token data)
        {
            return new TokenModel
            {
                AccessToken = data.AccessToken,
                UserName = data.UserName,
                Expires = data.Expires
            };
        }
    }
}