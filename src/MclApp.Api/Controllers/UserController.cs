using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Services.Authentication;
using UserIdentity.Services.UserManagement;
using UserIdentity.ViewModels.Authentication.Login;
using UserIdentity.ViewModels.UserManagement.Users;

namespace MclApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserServices _newUserServices;
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;
        public UserController(IUserServices newUserServices, ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _newUserServices = newUserServices;
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<CreateUserResponse> CreateNewUser(CreateUserRequest model)
        {
            return await _newUserServices.CreateAccount(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ForgotPasswordResponse> ForgotPassword([FromBody]ForgotPasswordRequest model)
        {
            return await _newUserServices.ForgotPassword(model);
        }


        [HttpPost]
        [Authorize]
        public async Task<ChangePasswordResponse> ChangePassword([FromBody] ChangePasswordRequest model)
        {
            var user = BreachUser;
            if (user == null)
            {
                return ChangePasswordResponse.Fail("Server error in changing password");
            }
            return await _newUserServices.ChangePassword(model, user.Email);
        }
        [Authorize]
        public async Task<bool> CheckAuthenticatorStatus()
        {
            var user = BreachUser;
            if (user == null)
            {
                return false;
            }
            return  await _twoFactorAuthenticationService.IsActive(user.Id);
        }
        [Authorize]
        public async Task<AuthenticatorViewModel> ShowAuthenticator()
        {
            var user = BreachUser;
            if (user == null)
            {
                return new AuthenticatorViewModel() { Success = false };
            }
            return await _twoFactorAuthenticationService.GetAuthenticatorForUser(user.Id);

        }


        [Authorize]
        [HttpPost]
        public async Task<Activate2FaAuthentication> VerifyAuthenticator([FromBody]string code)
        {
            var user = BreachUser;
            if (user == null)
            {
                return new Activate2FaAuthentication() { IsCodeValid = false };
            }
            var sts = await _twoFactorAuthenticationService.Enable2FaForUser(user.Id);
            if (sts)
            {
                return await _twoFactorAuthenticationService.ActivateAuthenticatorForUser(code,user.Id);
            }

            return new Activate2FaAuthentication() { IsCodeValid = false };
        }

        [Authorize]
        public async Task<bool> DisableAuthenticator()
        {
            var user = BreachUser;
            if (user == null)
            {
                return false;
            }
            var sts = await _twoFactorAuthenticationService.Disable2FaForUser(user.Id);
            return true;
        }





        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return Ok("Hello there");
        }
    }
}