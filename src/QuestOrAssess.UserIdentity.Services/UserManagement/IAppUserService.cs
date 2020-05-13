using System.Collections.Generic;
using System.Threading.Tasks;
using QuestOrAssess.UserIdentity.Core.Domain.Identity;

namespace QuestOrAssess.UserIdentity.Services.UserManagement
{
    public interface IAppUserService
    {
        Task<AppUser> GetUser(int id);
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<AppUser> GetUserByUserNameAsync(string userName);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
        Task<(AppUser User, IEnumerable<string> Roles)?> GetUserAndRolesAsync(int userId);
        Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(AppUser user, IEnumerable<string> roles, string password);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(AppUser user);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(AppUser user, IEnumerable<string> roles);
        Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(AppUser user, string newPassword);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdatePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(string userId);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(AppUser user);
    }
}