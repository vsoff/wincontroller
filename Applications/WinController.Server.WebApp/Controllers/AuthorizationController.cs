using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vsoff.WC.Core.Mappers;
using Vsoff.WC.Domain.Auth;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Models.Auth;
using Vsoff.WC.Server.Api.Auth;
using Vsoff.WC.Server.Services;

namespace WinController.Server.WebApp.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IServerAuthorizationService _serverAuthorizationService;
        private readonly IAccountService _accountService;

        public AuthorizationController(
            IServerAuthorizationService serverAuthorizationService,
            IAccountService accountService)
        {
            _serverAuthorizationService = serverAuthorizationService;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public object Token([FromBody] LoginRequestModel loginRequest)
        {
            Account account = _accountService.GetAccount(loginRequest.Username, loginRequest.Password);
            if (account == null)
                return null;

            Token token = _serverAuthorizationService.GenerateToken(account);
            return token.ToModel();
        }
    }
}