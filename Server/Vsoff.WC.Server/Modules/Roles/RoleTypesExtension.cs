using System.Security.Claims;
using Vsoff.WC.Common.Enums;

namespace Vsoff.WC.Server.Modules.Roles
{
    public static class RoleTypesExtension
    {
        public static Claim ToClaim(this RoleTypes role)
        {
            return new Claim(ClaimsIdentity.DefaultRoleClaimType, ((int) role).ToString());
        }
    }
}