using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core;
using MclApp.Core.IdentityDomain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Data;

namespace UserIdentity.Services.UserManagement
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<MclAppUser> _applicationUserManager;
        private readonly IdentityDbContext _dbContext;

        public AppUserService(UserManager<MclAppUser> applicationUserManager,
                                    IdentityDbContext dbContext,
                                    IHttpContextAccessor httpAccessor)
        {
            _applicationUserManager = applicationUserManager;
            _dbContext = dbContext;
        }

        public Task<MclAppUser> GetUser(int id)
        {
            return _applicationUserManager.FindByIdAsync(id.ToString());
        }
        public async Task<MclAppUser> GetUserByIdAsync(string userId)
        {
            return await _applicationUserManager.FindByIdAsync(userId);
        }

        public async Task<MclAppUser> GetUserByUserNameAsync(string userName)
        {
            return await _applicationUserManager.FindByNameAsync(userName);
        }

        public async Task<MclAppUser> GetUserByEmailAsync(string email)
        {
            return await _applicationUserManager.FindByEmailAsync(email);
        }

        public async Task<bool> NeedSeeding()
        {
            return await _applicationUserManager.Users.AnyAsync() ;
        }

        public async Task<ServiceResponse<MclAppUser>> AddUserAsync(MclAppUser user)
        {
            var result =  await _applicationUserManager.CreateAsync(user);
            if (!result.Succeeded) return new ServiceResponse<MclAppUser>().FailedResponse("failed to create user");
            var createdUser = await _applicationUserManager.FindByEmailAsync(user.Email);
            return new ServiceResponse<MclAppUser>().SuccessResponse(createdUser);
        }
        
        public async Task<ServiceResponse<MclAppUser>> CreateUserAsync(MclAppUser user, string password)
        {
            var result = await _applicationUserManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return new ServiceResponse<MclAppUser>().FailedResponse(result.Errors.ToString());
            
            user = await _applicationUserManager.FindByNameAsync(user.UserName);

            return new ServiceResponse<MclAppUser>().SuccessResponse(user);
        }
        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(MclAppUser user, IEnumerable<string> roles)
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

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(MclAppUser user)
        {
            var result = await _applicationUserManager.DeleteAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }




    }



}
