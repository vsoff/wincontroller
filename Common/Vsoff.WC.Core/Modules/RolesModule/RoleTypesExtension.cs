using System.Security.Claims;
using Vsoff.WC.Common.Enums;

namespace Vsoff.WC.Core.Modules.RolesModule
{
    public static class RoleTypesExtension
    {
        public static Claim ToClaim(this RoleTypes role)
        {
            return new Claim(ClaimsIdentity.DefaultRoleClaimType, ((int) role).ToString());
        }
    }
}