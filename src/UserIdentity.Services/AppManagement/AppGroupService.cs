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
       private readonly IIdentityDbRepository<MclAppGroup> _groupRepository;
       private readonly IIdentityDbRepository<MclGroupUser> _groupUserRepository;
        private readonly RoleManager<MclAppPermission> _permissionManager;
       private readonly IIdentityDbRepository<MclGroupPermission> _groupPermissionRepository;
       private readonly UserManager<MclAppUser> _applicationUserManager;

        public AppGroupService(IIdentityDbRepository<MclAppGroup>group, 
            IIdentityDbRepository<MclApplication> applicationRepository, 
            RoleManager<MclAppPermission> roleManager, IIdentityDbRepository<MclGroupPermission> groupPermissionRepository, UserManager<MclAppUser> applicationUserManager, IIdentityDbRepository<MclGroupUser> groupUserRepository)
        {
            _groupRepository = group;
            _permissionManager = roleManager;
            _groupPermissionRepository = groupPermissionRepository;
            _applicationUserManager = applicationUserManager;
            _groupUserRepository = groupUserRepository;
        }
       public async Task<OutResult> AddNewGroup(MclAppGroup group)
       {
           return await _groupRepository.Insert(group);
       }
       public async Task<MclAppGroup> GetGroupById(int id)
       {
           var group = await GetById(id);
           return @group.Any() ? @group.First() : null;
       }
       public async Task<List<MclAppGroup>> GetApplicationGroups(int applicationId)
       {
           var group = await _groupRepository.Table.Where(x => x.ApplicationId == applicationId).ToListAsync();
           return group;
           
       }

        public async Task<List<MclAppGroup>> GetGroupByName(string name)
       {
          return await _groupRepository.Table.Where(x=>x.Name.ToLower().Equals(name.ToLower())).ToListAsync();
       }
       public async Task<List<MclAppGroup>> GetGroupByName(List<string> names)
       {
           return await _groupRepository.Table.Where(x =>names.Contains(x.Name.ToLower())).ToListAsync();
       }

       public async Task<OutResult> AddUserToGroup(MclAppUser user , MclAppGroup group)
       {
           var groupUser = new MclGroupUser()
           {
               UserId = user.Id,
               GroupId = group.Id,
               User = user,
               Group = group
           };
           return await _groupUserRepository.Insert(groupUser);
       }
        
       public async Task<ServiceResponse<IEnumerable<MclAppPermission>>> GetGroupPermissions(int id)
       {
           var group = await GetById(id);
            if (!@group.Any()) 
               return new ServiceResponse<IEnumerable<MclAppPermission>>().SuccessWithNoResponse();
           var singleGroup = @group.First();
           if (singleGroup.GroupPermissions.Any())
           {
               return new ServiceResponse<IEnumerable<MclAppPermission>>().SuccessResponse(
                   singleGroup.GroupPermissions.Select(x => x.Permission));
           }
           return new ServiceResponse<IEnumerable<MclAppPermission>>().SuccessWithNoResponse();
       }
       public async Task<ServiceResponse<IEnumerable<MclAppUser>>> GetGroupUsers(int id)
       {
           var group = await GetById(id);
           if (!@group.Any())
               return new ServiceResponse<IEnumerable<MclAppUser>>().SuccessWithNoResponse();
           var singleGroup = @group.First();
           if (singleGroup.UsersInGroup.Any())
           {
               return new ServiceResponse<IEnumerable<MclAppUser>>().SuccessResponse(
                   singleGroup.UsersInGroup.Select(x => x.User));
           }
           return new ServiceResponse<IEnumerable<MclAppUser>>().SuccessWithNoResponse();
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
           
           var groupPermission = new MclGroupPermission()
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

           var groupPermission = new MclGroupPermission()
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
        private async Task<List<MclAppGroup>> GetById(int id)
       {
           return await _groupRepository.Table.Where(x => x.Id == id).ToListAsync();
       }


    }
}
