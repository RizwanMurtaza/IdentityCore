using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Core;
using MclApp.Core.IdentityDomain.Group;
using MclApp.Core.IdentityDomain.Identity;

namespace UserIdentity.Services.AppManagement
{
    public interface IAppGroupService
    {
        Task<OutResult> AddNewGroup(MclAppGroup group);
        Task<List<MclAppGroup>> GetGroupByName(List<string> names);
        Task<List<MclAppGroup>> GetApplicationGroups(int applicationId);

        Task<OutResult> AddUserToGroup(MclAppUser user, MclAppGroup group);

        Task<MclAppGroup> GetGroupById(int id);
        Task<List<MclAppGroup>> GetGroupByName(string name);
        Task<ServiceResponse<IEnumerable<MclAppPermission>>> GetGroupPermissions(int id);
        Task<ServiceResponse<IEnumerable<MclAppUser>>> GetGroupUsers(int id);
        Task<OutResult> AddPermissionToGroup(int groupId, int permissionId);
        Task<OutResult> RemovePermissionFromGroup(int groupId, int permissionId);
    }
}