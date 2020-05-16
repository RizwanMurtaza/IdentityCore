using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Core;
using UserIdentity.Core.Domain.Identity;
using UserIdentity.Data;

namespace UserIdentity.Services.UserManagement
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _applicationUserManager;
        private readonly QuestOrAssessIdentityDbContext _dbContext;

        public AppUserService(UserManager<AppUser> applicationUserManager,
                                    QuestOrAssessIdentityDbContext dbContext,
                                    IHttpContextAccessor httpAccessor)
        {
            _applicationUserManager = applicationUserManager;
            _dbContext = dbContext;
        }

        public Task<AppUser> GetUser(int id)
        {
            return _applicationUserManager.FindByIdAsync(id.ToString());
        }
        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            return await _applicationUserManager.FindByIdAsync(userId);
        }

        public async Task<AppUser> GetUserByUserNameAsync(string userName)
        {
            return await _applicationUserManager.FindByNameAsync(userName);
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _applicationUserManager.FindByEmailAsync(email);
        }

        public async Task<ServiceResponse<AppUser>> AddUserAsync(AppUser user)
        {
            var result =  await _applicationUserManager.CreateAsync(user);
            if (!result.Succeeded) return new ServiceResponse<AppUser>().FailedResponse("failed to create user");
            var createdUser = await _applicationUserManager.FindByEmailAsync(user.Email);
            return new ServiceResponse<AppUser>().SuccessResponse(createdUser);
        }
        public async Task<IList<string>> GetUserRolesAsync(AppUser user)
        {
            return await _applicationUserManager.GetRolesAsync(user);
        }

        public async Task<(AppUser User, IEnumerable<string> Roles)?> GetUserAndRolesAsync(int userId)
        {
            var user = await _dbContext.Users
                .Include(u => u.Permission)
                .Where(u => u.Id == userId)
                .SingleOrDefaultAsync();

            if (user == null)
                return null;

            var userRoleIds = user.Permission.Select(r => r.RoleId).ToList();

            var roles = await _dbContext.Roles
                .Where(r => userRoleIds.Contains(r.Id))
                .Select(r => r.Name)
                .ToArrayAsync();

            return (user, roles);
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(AppUser user, IEnumerable<string> roles, string password)
        {
            var result = await _applicationUserManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));


            user = await _applicationUserManager.FindByNameAsync(user.UserName);

            try
            {
                result = await this._applicationUserManager.AddToRolesAsync(user, roles.Distinct());
            }
            catch
            {
                await DeleteUserAsync(user);
                throw;
            }

            if (!result.Succeeded)
            {
                await DeleteUserAsync(user);
                return (false, result.Errors.Select(e => e.Description).ToArray());
            }

            return (true, new List<string>());
        }


        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(AppUser user)
        {
            return await UpdateUserAsync(user, null);
        }


        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(AppUser user, IEnumerable<string> roles)
        {
            var result = await _applicationUserManager.UpdateAsync(user);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));


            if (roles == null) return (true, new string[] { });
            {
                var userRoles = await _applicationUserManager.GetRolesAsync(user);

                var rolesToRemove = userRoles.Except(roles).ToArray();
                var rolesToAdd = roles.Except(userRoles).Distinct().ToArray();

                if (rolesToRemove.Any())
                {
                    result = await _applicationUserManager.RemoveFromRolesAsync(user, rolesToRemove);
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description));
                }

                if (!rolesToAdd.Any()) return (true, new string[] { });
                {
                    result = await _applicationUserManager.AddToRolesAsync(user, rolesToAdd);
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description));
                }
            }

            return (true, new string[] { });
        }


        public async Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(AppUser user, string newPassword)
        {
            var resetToken = await _applicationUserManager.GeneratePasswordResetTokenAsync(user);

            var result = await _applicationUserManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            return (true, new string[] { });
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdatePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            var result = await _applicationUserManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            return (true, new string[] { });
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            if (!await _applicationUserManager.CheckPasswordAsync(user, password))
            {
                if (!_applicationUserManager.SupportsUserLockout)
                    await _applicationUserManager.AccessFailedAsync(user);

                return false;
            }

            return true;
        }
        public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(string userId)
        {
            var user = await _applicationUserManager.FindByIdAsync(userId);

            if (user != null)
                return await DeleteUserAsync(user);

            return (true, new string[] { });
        }


        public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(AppUser user)
        {
            var result = await _applicationUserManager.DeleteAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }




    }



}
