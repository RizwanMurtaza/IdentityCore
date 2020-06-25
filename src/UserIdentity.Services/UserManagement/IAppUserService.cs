using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Core;
using MclApp.Core.IdentityDomain.Identity;

namespace UserIdentity.Services.UserManagement
{
    public interface IAppUserService
    {
        Task<MclAppUser> GetUser(int id);
        Task<MclAppUser> GetUserByIdAsync(string userId);
        Task<MclAppUser> GetUserByUserNameAsync(string userName);
        Task<MclAppUser> GetUserByEmailAsync(string email);
        Task<ServiceResponse<MclAppUser>> AddUserAsync(MclAppUser user);
        Task<IList<string>> GetUserRolesAsync(MclAppUser user);
        Task<(MclAppUser User, IEnumerable<string> Roles)?> GetUserAndRolesAsync(int userId);
        Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(MclAppUser user, IEnumerable<string> roles, string password);
        Task<ServiceResponse<MclAppUser>> CreateUserAsync(MclAppUser user, string password);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(MclAppUser user);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(MclAppUser user, IEnumerable<string> roles);
        Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(MclAppUser user, string newPassword);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdatePasswordAsync(MclAppUser user, string currentPassword, string newPassword);
        Task<bool> CheckPasswordAsync(MclAppUser user, string password);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(string userId);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(MclAppUser user);
    }
}