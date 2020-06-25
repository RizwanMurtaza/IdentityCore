using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Core.Domain.Identity;
using UserIdentity.Services.Helper;
using UserIdentity.Services.Jwt;
using UserIdentity.ViewModels.Authentication.Claims;
using UserIdentity.ViewModels.Authentication.Login;

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
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, request.RememberMe, true);

            if (result.RequiresTwoFactor)
            {
                return LoginResponse.TwoFactorAuthenticationEnabled();
            }
            if (result.IsLockedOut)
            {
                return LoginResponse.LockedOut();
            }
            if (result.Succeeded)
            {
                var user = _applicationUserManager.Users.Include(y=>y.Groups).First(x => x.UserName.ToLower().Equals(request.Username));
                var roles = await _applicationUserManager.GetRolesAsync(user);
                var groups = user.Groups.Select(x => x.Group.Name);
                var token = _jwtTokenService.CreateToken(HelperService.ToUser(user), roles);

                return LoginResponse.Success( token, GetResponseUser(user, token));
            }
            else
            {
                return LoginResponse.Failure("Invalid Username or Password");
            }
        }
        public async Task<TwoFaLoginResponse> LoginWith2Fa(TwoFaLoginRequest request)
        {

            var user = await _applicationUserManager.FindByEmailAsync(request.Username);
            if (user == null)
            {
                return new TwoFaLoginResponse() { IsCodeValid = false };

            }
            var authenticatorCode = request.Code.Replace(" ", string.Empty).Replace("-", string.Empty);
            var result = await _applicationUserManager.VerifyTwoFactorTokenAsync(user, new IdentityOptions().Tokens.AuthenticatorTokenProvider, authenticatorCode);

            if (!result) return new TwoFaLoginResponse() {IsCodeValid = false};
            
            user = _applicationUserManager.Users.Include(y => y.Groups).First(x => x.UserName.ToLower().Equals(request.Username));
            var roles = await _applicationUserManager.GetRolesAsync(user);
            var groups = user.Groups.Select(x => x.Group.Name);
            var token = _jwtTokenService.CreateToken(HelperService.ToUser(user), roles);
            return new TwoFaLoginResponse()
            {
                IsCodeValid = true,
                LoginResponse = LoginResponse.Success(token: token, GetResponseUser(user, token))
            };


        }



        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }
        private static ResponseUser GetResponseUser(AppUser user, string token )
        {
            return new ResponseUser
            {
                UserName = user.UserName,
                Email = user.Email,
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                Lastname = user.LastName,
                Token = token
            };
        }





    }
}
