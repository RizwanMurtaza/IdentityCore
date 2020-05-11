using System.Collections.Generic;
using System.Threading.Tasks;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Services
{
    public interface IApplicationUserService
    {
        Task<User> GetUser(int id);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> GetUserByEmailAsync(string email);
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<(User User, IEnumerable<string> Roles)?> GetUserAndRolesAsync(int userId);
        Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(User user, IEnumerable<string> roles, string password);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(User user);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(User user, IEnumerable<string> roles);
        Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(User user, string newPassword);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdatePasswordAsync(User user, string currentPassword, string newPassword);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(string userId);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(User user);
        Task<bool> TestCanDeleteRoleAsync(int roleId);
        Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(string roleName);
        Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(Permission role);
    }
}