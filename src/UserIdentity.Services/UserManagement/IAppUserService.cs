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
        Task<bool> NeedSeeding();
        Task<ServiceResponse<MclAppUser>> AddUserAsync(MclAppUser user);
        Task<ServiceResponse<MclAppUser>> CreateUserAsync(MclAppUser user, string password);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(MclAppUser user);
    }
}