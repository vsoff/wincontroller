using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Vsoff.WC.Common.Enums;
using Vsoff.WC.Server.Modules.Roles;

namespace Vsoff.WC.Server.Api.Auth.Attributes
{
    public class RoleAttribute : TypeFilterAttribute
    {
        public RoleAttribute(RoleTypes role) : base(typeof(RoleRequirementFilter))
        {
            Arguments = new object[] {role.ToClaim()};
        }
    }

    public class RoleRequirementFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RoleRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            if (!hasClaim || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}