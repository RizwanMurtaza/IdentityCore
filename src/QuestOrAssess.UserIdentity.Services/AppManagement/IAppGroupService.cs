using System.Collections.Generic;
using System.Threading.Tasks;
using QuestOrAssess.UserIdentity.Core;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Core.Domain.Group;
using QuestOrAssess.UserIdentity.Core.Domain.Identity;

namespace QuestOrAssess.UserIdentity.Services.AppManagement
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