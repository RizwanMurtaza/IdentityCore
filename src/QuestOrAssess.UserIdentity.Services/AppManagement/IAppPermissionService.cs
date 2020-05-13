using System.Threading.Tasks;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Services.AppManagement
{
    public interface IAppPermissionService
    {
        Task<OutResult> AddPermission(string name, string description);
        Task<OutResult> RemovePermission(string name);
    }
}