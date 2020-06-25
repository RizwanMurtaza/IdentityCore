using System.Threading.Tasks;
using UserIdentity.ViewModels.Authentication.Login;

namespace UserIdentity.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<TwoFaLoginResponse> LoginWith2Fa(TwoFaLoginRequest request);
    }
}