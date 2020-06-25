using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core;
using MclApp.Core.IdentityDomain;
using MclApp.Core.IdentityDomain.Group;
using MclApp.Core.IdentityDomain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Data.Repository;

namespace UserIdentity.Services.AppManagement
{
   public class AppGroupService : IAppGroupService
   {
       private readonly IIdentityDbRepository<AppGroup> _groupRepository;
       private readonly IIdentityDbRepository<GroupUser> _groupUserRepository;
        private readonly RoleManager<AppPermission> _permissionManager;
       private readonly IIdentityDbRepository<GroupPermission> _groupPermissionRepository;
       private readonly UserManager<AppUser> _applicationUserManager;

        public AppGroupService(IIdentityDbRepository<AppGroup>group, 
            IIdentityDbRepository<Application> applicationRepository, 
            RoleManager<AppPermission> roleManager, IIdentityDbRepository<GroupPermission> groupPermissionRepository, UserManager<AppUser> applicationUserManager, IIdentityDbRepository<GroupUser> groupUserRepository)
        {
            _groupRepository = group;
            _permissionManager = roleManager;
            _groupPermissionRepository = groupPermissionRepository;
            _applicationUserManager = applicationUserManager;
            _groupUserRepository = groupUserRepository;
        }
       public async Task<OutResult> AddNewGroup(AppGroup group)
       {
           return await _groupRepository.Insert(group);
       }
       public async Task<AppGroup> GetGroupById(int id)
       {
           var group = await GetById(id);
           return @group.Any() ? @group.First() : null;
       }
       public async Task<List<AppGroup>> GetApplicationGroups(int applicationId)
       {
           var group = await _groupRepository.Table.Where(x => x.ApplicationId == applicationId).ToListAsync();
           return group;
           
       }

        public async Task<List<AppGroup>> GetGroupByName(string name)
       {
          return await _groupRepository.Table.Where(x=>x.Name.ToLower().Equals(name.ToLower())).ToListAsync();
       }
       public async Task<List<AppGroup>> GetGroupByName(List<string> names)
       {
           return await _groupRepository.Table.Where(x =>names.Contains(x.Name.ToLower())).ToListAsync();
       }

       public async Task<OutResult> AddUserToGroup(AppUser user , AppGroup group)
       {
           var groupUser = new GroupUser()
           {
               UserId = user.Id,
               GroupId = group.Id,
               User = user,
               Group = group
           };
           return await _groupUserRepository.Insert(groupUser);
       }
        
       public async Task<ServiceResponse<IEnumerable<AppPermission>>> GetGroupPermissions(int id)
       {
           var group = await GetById(id);
            if (!@group.Any()) 
               return new ServiceResponse<IEnumerable<AppPermission>>().SuccessWithNoResponse();
           var singleGroup = @group.First();
           if (singleGroup.GroupPermissions.Any())
           {
               return new ServiceResponse<IEnumerable<AppPermission>>().SuccessResponse(
                   singleGroup.GroupPermissions.Select(x => x.Permission));
           }
           return new ServiceResponse<IEnumerable<AppPermission>>().SuccessWithNoResponse();
       }
       public async Task<ServiceResponse<IEnumerable<AppUser>>> GetGroupUsers(int id)
       {
           var group = await GetById(id);
           if (!@group.Any())
               return new ServiceResponse<IEnumerable<AppUser>>().SuccessWithNoResponse();
           var singleGroup = @group.First();
           if (singleGroup.UsersInGroup.Any())
           {
               return new ServiceResponse<IEnumerable<AppUser>>().SuccessResponse(
                   singleGroup.UsersInGroup.Select(x => x.User));
           }
           return new ServiceResponse<IEnumerable<AppUser>>().SuccessWithNoResponse();
       }

       public async Task<OutResult> AddPermissionToGroup(int groupId, int permissionId)
       {
           var group = await GetById(groupId);
           if (!group.Any())
           {
                return OutResult.Failed("Group not found");
           }
           var permission = _permissionManager.Roles.Where(x => x.Id == permissionId);
           if (!permission.Any())
           {
               return OutResult.Failed("Permission not found");
           }

           var groupToAdd = group.First();
           var permissionToAdd = permission.First();
           
           var groupPermission = new GroupPermission()
           {
               Group = groupToAdd,
               Permission = permissionToAdd,
               GroupId = groupId,
               PermissionId = permissionId
           };
           if (groupToAdd.GroupPermissions.Contains(groupPermission))
               return OutResult.Failed("Group Already have Permission");

           
           var permissionResult =  await _groupPermissionRepository.Insert(groupPermission);
           var groupUsers = groupToAdd.UsersInGroup.Select(x=>x.User);
           foreach (var user in groupUsers)
           {
               if (!await _applicationUserManager.IsInRoleAsync(user, permissionToAdd.Name))
               {
                   await _applicationUserManager.AddToRoleAsync(user, permissionToAdd.Name);
               }
           }
           return OutResult.Success_Updated();

       }

       public async Task<OutResult> RemovePermissionFromGroup(int groupId, int permissionId)
       {
           var group = await GetById(groupId);
           if (!group.Any())
           {
               return OutResult.Failed("Group not found");
           }
           var permission = _permissionManager.Roles.Where(x => x.Id == permissionId);
           if (!permission.Any())
           {
               return OutResult.Failed("Permission not found");
           }

           var groupToAdd = group.First();
           var permissionToAdd = permission.First();

           var groupPermission = new GroupPermission()
           {
               Group = groupToAdd,
               Permission = permissionToAdd,
               GroupId = groupId,
               PermissionId = permissionId
           };
           if (!groupToAdd.GroupPermissions.Contains(groupPermission))
               return OutResult.Failed("Group Permission Not Exist");

           var permissionResult = await _groupPermissionRepository.Delete(groupPermission);
           var groupUsers = groupToAdd.UsersInGroup.Select(x => x.User);
           foreach (var user in groupUsers)
           {
               if (!await _applicationUserManager.IsInRoleAsync(user, permissionToAdd.Name))
               {
                   await _applicationUserManager.RemoveFromRoleAsync(user, permissionToAdd.Name);
               }
           }
           return OutResult.Success_Updated();

       }
        private async Task<List<AppGroup>> GetById(int id)
       {
           return await _groupRepository.Table.Where(x => x.Id == id).ToListAsync();
       }


    }
}
