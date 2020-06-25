using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Core;
using MclApp.Core.IdentityDomain.Group;
using MclApp.Core.IdentityDomain.Identity;

namespace UserIdentity.Services.AppManagement
{
    public interface IAppGroupService
    {
        Task<OutResult> AddNewGroup(AppGroup group);
        Task<List<AppGroup>> GetGroupByName(List<string> names);
        Task<List<AppGroup>> GetApplicationGroups(int applicationId);

        Task<OutResult> AddUserToGroup(AppUser user, AppGroup group);

        Task<AppGroup> GetGroupById(int id);
        Task<List<AppGroup>> GetGroupByName(string name);
        Task<ServiceResponse<IEnumerable<AppPermission>>> GetGroupPermissions(int id);
        Task<ServiceResponse<IEnumerable<AppUser>>> GetGroupUsers(int id);
        Task<OutResult> AddPermissionToGroup(int groupId, int permissionId);
        Task<OutResult> RemovePermissionFromGroup(int groupId, int permissionId);
    }
}