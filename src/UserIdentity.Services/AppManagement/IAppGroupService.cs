using System.Collections.Generic;
using System.Threading.Tasks;
using UserIdentity.Core;
using UserIdentity.Core.Domain;
using UserIdentity.Core.Domain.Group;
using UserIdentity.Core.Domain.Identity;

namespace UserIdentity.Services.AppManagement
{
    public interface IAppGroupService
    {
        Task<OutResult> AddNewGroup(AppGroup group);
        Task<AppGroup> GetGroupById(int id);
        Task<ServiceResponse<IEnumerable<AppPermission>>> GetGroupPermissions(int id);
        Task<ServiceResponse<IEnumerable<AppUser>>> GetGroupUsers(int id);
        Task<OutResult> AddPermissionToGroup(int groupId, int permissionId);
        Task<OutResult> RemovePermissionFromGroup(int groupId, int permissionId);
    }
}