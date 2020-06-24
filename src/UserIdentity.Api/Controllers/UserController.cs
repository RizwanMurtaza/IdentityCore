using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Services.UserManagement;
using UserIdentity.ViewModels.Authentication.Login;
using UserIdentity.ViewModels.UserManagement.Users;

namespace UserIdentity.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserServices _newUserServices;
        public UserController(IUserServices newUserServices)
        {
            _newUserServices = newUserServices;
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



        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return Ok("Hello there");
        }
    }
}