using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vsoff.WC.Domain.Auth;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Roles;
using Vsoff.WC.Server.Services;

namespace Vsoff.WC.Server.Api.Auth
{
    public interface IServerAuthorizationService
    {
        Token GenerateToken(User user, TimeSpan? tokenLifeTime = null);

        void BlockToken(string accessToken);
    }

    public class ServerAuthorizationService : IServerAuthorizationService
    {
        public ServerAuthorizationService()
        {
        }

        public void BlockToken(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Token GenerateToken(User user, TimeSpan? tokenLifeTime = null)
        {
            var identity = GetIdentity(user);
            if (identity == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;
            var expires = now.Add(tokenLifeTime ?? AuthOptions.Lifetime);

            var creds = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: expires,
                signingCredentials: creds);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new Token
            {
                AccessToken = token,
                UserName = identity.Name,
                Expires = expires
            };
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    user.Role.ToClaim(),
                };

                return new ClaimsIdentity(claims, "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            }

            return null;
        }
    }
}