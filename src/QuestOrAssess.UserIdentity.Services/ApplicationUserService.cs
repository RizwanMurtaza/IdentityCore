using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace QuestOrAssess.UserIdentity.Services
{


    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<User> _applicationUserManager;
        private readonly RoleManager<Permission> _roleManager;
        private readonly QuestOrAssessIdentityDbContext _dbContext;
        private readonly SignInManager<User> _signInManager;

        public ApplicationUserService(UserManager<User> applicationUserManager,
                                    RoleManager<Permission> roleManager, 
                                    QuestOrAssessIdentityDbContext dbContext,
                                    IHttpContextAccessor httpAccessor, SignInManager<User> signInManager)
        {
            _applicationUserManager = applicationUserManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _signInManager = signInManager;
            // dbContext.CurrentUserId = httpAccessor.HttpContext?.User.FindFirst("userId")?.Value?.Trim();
        }

        public Task<User> GetUser(int id)
        {
            return _applicationUserManager.FindByIdAsync(id.ToString());
        }
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _applicationUserManager.FindByIdAsync(userId);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _applicationUserManager.FindByNameAsync(userName);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _applicationUserManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _applicationUserManager.GetRolesAsync(user);
        }

        public async Task<(User User, IEnumerable<string> Roles)?> GetUserAndRolesAsync(int userId)
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

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(User user, IEnumerable<string> roles, string password)
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


        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(User user)
        {
            return await UpdateUserAsync(user, null);
        }


        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(User user, IEnumerable<string> roles)
        {
            var result = await _applicationUserManager.UpdateAsync(user);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));


            if (roles != null)
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

                if (rolesToAdd.Any())
                {
                    result = await _applicationUserManager.AddToRolesAsync(user, rolesToAdd);
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description));
                }
            }

            return (true, new string[] { });
        }


        public async Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(User user, string newPassword)
        {
            var resetToken = await _applicationUserManager.GeneratePasswordResetTokenAsync(user);

            var result = await _applicationUserManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            return (true, new string[] { });
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdatePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var result = await _applicationUserManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            return (true, new string[] { });
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
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


        public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(User user)
        {
            var result = await _applicationUserManager.DeleteAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }
        //public async Task<(bool Succeeded, string[] Errors)> UpdateRoleAsync(ApplicationRole role, IEnumerable<string> claims)
        //{
        //    if (claims != null)
        //    {
        //        string[] invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null).ToArray();
        //        if (invalidClaims.Any())
        //            return (false, new[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) });
        //    }


        //    var result = await _roleManager.UpdateAsync(role);
        //    if (!result.Succeeded)
        //        return (false, result.Errors.Select(e => e.Description).ToArray());


        //    if (claims != null)
        //    {
        //        var roleClaims = (await _roleManager.GetClaimsAsync(role)).Where(c => c.Type == ClaimConstants.Permission);
        //        var roleClaimValues = roleClaims.Select(c => c.Value).ToArray();

        //        var claimsToRemove = roleClaimValues.Except(claims).ToArray();
        //        var claimsToAdd = claims.Except(roleClaimValues).Distinct().ToArray();

        //        if (claimsToRemove.Any())
        //        {
        //            foreach (string claim in claimsToRemove)
        //            {
        //                result = await _roleManager.RemoveClaimAsync(role, roleClaims.Where(c => c.Value == claim).FirstOrDefault());
        //                if (!result.Succeeded)
        //                    return (false, result.Errors.Select(e => e.Description).ToArray());
        //            }
        //        }

        //        if (claimsToAdd.Any())
        //        {
        //            foreach (string claim in claimsToAdd)
        //            {
        //                result = await _roleManager.AddClaimAsync(role, new Claim(ClaimConstants.Permission, ApplicationPermissions.GetPermissionByValue(claim)));
        //                if (!result.Succeeded)
        //                    return (false, result.Errors.Select(e => e.Description).ToArray());
        //            }
        //        }
        //    }

        //    return (true, new string[] { });
        //}


        public async Task<bool> TestCanDeleteRoleAsync(int roleId)
        {
            return !await _dbContext.UserRoles.Where(r => r.RoleId == roleId).AnyAsync();
        }


        public async Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
                return await DeleteRoleAsync(role);

            return (true, new string[] { });
        }

        public async Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(Permission role)
        {
            var result = await _roleManager.DeleteAsync(role);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }




    }



}
