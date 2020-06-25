using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MclApp.Core.IdentityDomain.Identity;
using Microsoft.AspNetCore.Identity;
using UserIdentity.ViewModels.Authentication.Login;

namespace UserIdentity.Services.Authentication
{
    public class TwoFactorAuthenticationService : ITwoFactorAuthenticationService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _applicationUserManager;
        private readonly UrlEncoder _urlEncoder;
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public TwoFactorAuthenticationService(SignInManager<AppUser> signInManager, UserManager<AppUser> applicationUserManager, UrlEncoder urlEncoder)
        {
            _signInManager = signInManager;
            _applicationUserManager = applicationUserManager;
            _urlEncoder = urlEncoder;
        }
        public async Task<bool> IsActive(string userId)
        {
            var user = await _applicationUserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            return user.TwoFactorEnabled;
        }


        public async Task<AuthenticatorViewModel> GetAuthenticatorForUser(string userId)
        {
            var user = await _applicationUserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AuthenticatorViewModel();
            }
            var model = new AuthenticatorViewModel();
            await LoadSharedKeyAndQrCodeUriAsync(user, model);
            return model;
        }

        public async Task<bool> Disable2FaForUser(string userId)
        {
            var user = await _applicationUserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            var disable2FaResult = await _applicationUserManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2FaResult.Succeeded)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> Enable2FaForUser(string userId)
        {
            var user = await _applicationUserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            var disable2FaResult = await _applicationUserManager.SetTwoFactorEnabledAsync(user, true);
            if (!disable2FaResult.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<Activate2FaAuthentication> ActivateAuthenticatorForUser(string activationCode, string userId)
        {
            var  returnModel = new Activate2FaAuthentication();
            var user = await _applicationUserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new Activate2FaAuthentication();
            }
            var verificationCode = activationCode.Replace(" ", string.Empty).Replace("-", string.Empty);
            var isValidToken = await _applicationUserManager.VerifyTwoFactorTokenAsync(user, _applicationUserManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (isValidToken)
            {
                await _applicationUserManager.SetTwoFactorEnabledAsync(user, true);
                var recoveryCodes = await _applicationUserManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                returnModel.IsCodeValid = true;
                returnModel.RecoveryKey = recoveryCodes.ToList();

                return returnModel;
            }
            returnModel.IsCodeValid = false;
            return returnModel;
        }

        private async Task LoadSharedKeyAndQrCodeUriAsync(AppUser user, AuthenticatorViewModel model)
        {
            var unformattedKey = await _applicationUserManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _applicationUserManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _applicationUserManager.GetAuthenticatorKeyAsync(user);
            }

            model.SharedKey = FormatKey(unformattedKey);
            model.AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey);
            model.Success = true;
        }
        private static string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            var currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenticatorUriFormat,
                _urlEncoder.Encode("Melius Cyber"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }



    }
}
