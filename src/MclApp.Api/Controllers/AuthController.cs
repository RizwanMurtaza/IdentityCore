using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Services.Authentication;
using UserIdentity.Services.Helper;
using UserIdentity.ViewModels.Authentication.Login;

namespace MclApp.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<LoginResponse> Login([FromBody]LoginRequest model)
        {
            return await _authenticationService.Login(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<TwoFaLoginResponse> LoginWith2Fa([FromBody] TwoFaLoginRequest request)
        {
            return await _authenticationService.LoginWith2Fa(request);
        }

        private bool IsLoginViewModelValid(LoginRequest model)
        {
            if (HelperMethodService<LoginRequest>.IsAnyNullOrEmpty(model))
            {
                return false;
            }
            if (!Guid.TryParse(model.ApplicationKey, out var applicationKey))
            {
                return false;
            }
            return true;
        }
    }
}
