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
        private readonly IUserService _userService;

        public AuthorizationController(
            IServerAuthorizationService serverAuthorizationService,
            IUserService userService)
        {
            _serverAuthorizationService = serverAuthorizationService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public object Token([FromBody] LoginRequestModel loginRequest)
        {
            User user = _userService.GetUser(loginRequest.Username, loginRequest.Password);
            if (user == null)
                return null;

            Token token = _serverAuthorizationService.GenerateToken(user);
            return token.ToModel();
        }
    }
}