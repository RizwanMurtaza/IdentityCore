using System.Threading.Tasks;
using UserIdentity.Core.Domain;

namespace UserIdentity.Services.AppManagement
{
    public interface IAppPermissionService
    {
        Task<OutResult> AddPermission(string name, string description);
        Task<OutResult> RemovePermission(string name);
    }
}