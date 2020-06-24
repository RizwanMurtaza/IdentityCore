using System.Threading.Tasks;
using UserIdentity.ViewModels.Authentication.Login;
using UserIdentity.ViewModels.UserManagement.Users;

namespace UserIdentity.Services.UserManagement
{
    public interface IUserServices
    {
        Task<CreateUserResponse> CreateAccount(CreateUserRequest request);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request);
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, string email);
    }
}