using System;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core;
using MclApp.Core.IdentityDomain.Identity;
using Microsoft.AspNetCore.Identity;

namespace UserIdentity.Services.AppManagement
{
    public class AppPermissionService : IAppPermissionService
    {
        private readonly RoleManager<AppPermission> _permissionManager;
        private readonly UserManager<AppUser> _applicationUserManager;
        private readonly IAppGroupService _groupService;
        public AppPermissionService(RoleManager<AppPermission> permissionManager, UserManager<AppUser> applicationUserManager, IAppGroupService groupService)
        {
            _permissionManager = permissionManager;
            _applicationUserManager = applicationUserManager;
            _groupService = groupService;
        }

        public async Task<OutResult> AddPermission(string name, string description)
        {
            var permissionExists = await _permissionManager.FindByNameAsync(name);
            if (permissionExists!= null)
            {
                return OutResult.Error_AlreadyExists();
            }
            var newPermission = new AppPermission()
            {
                Name = name,
                Description = description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            var result = await _permissionManager.CreateAsync(newPermission);
            return result.Succeeded ? OutResult.Success_Created() : OutResult.Failed("Failed to add Permission");
        }
        public async Task<OutResult> RemovePermission(string name)
        {
            var permissionExists = await _permissionManager.FindByNameAsync(name);
            if (permissionExists == null)
            {
                return OutResult.Failed("Permission not Exists");
            }

            var groupWithPermissions = permissionExists.GroupPermissions.Select(x => x.Group);

            foreach (var group in groupWithPermissions)
            {
                await _groupService.RemovePermissionFromGroup(group.Id, permissionExists.Id);
            }

            return OutResult.Success_Deleted();
        }






    }
}
