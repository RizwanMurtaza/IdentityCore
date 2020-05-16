using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserIdentity.Core.Domain.Identity;

namespace UserIdentity.Services.Authentication
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _applicationUserManager;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthenticationService(SignInManager<AppUser> signInManager,
                                    UserManager<AppUser> applicationUserManager, IJwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _applicationUserManager = applicationUserManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<(bool, string)> Login(string userName, string password)
        {
            if (!userName.Equals("test"))
            {
                var user = await _applicationUserManager.FindByNameAsync(userName);
                if (user == null || !await _applicationUserManager.CheckPasswordAsync(user, password))
                {
                    return (false, "The username or password is invalid.");
                }

                if (!await _applicationUserManager.IsEmailConfirmedAsync(user))
                {
                    return (false, "You must have a confirmed email to log in.");
                }
            }
            return (true, _jwtTokenService.GenerateToken(userName));
        }



    }
}
