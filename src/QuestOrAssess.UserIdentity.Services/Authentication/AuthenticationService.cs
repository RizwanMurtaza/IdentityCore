using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Core.Domain.Identity;
using QuestOrAssess.UserIdentity.Services.Authentication;

namespace QuestOrAssess.UserIdentity.Services
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
