using System.Threading.Tasks;
using UserIdentity.ViewModels.Authentication.Login;

namespace UserIdentity.Services.Authentication
{
    public interface ITwoFactorAuthenticationService
    {
        Task<bool> IsActive(string userId);
        Task<AuthenticatorViewModel> GetAuthenticatorForUser(string userId);
        Task<bool> Disable2FaForUser(string userId);
        Task<bool> Enable2FaForUser(string userId);
        Task<Activate2FaAuthentication> ActivateAuthenticatorForUser(string activationCode, string userId);
    }
}