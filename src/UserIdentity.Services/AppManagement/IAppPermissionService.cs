using System.Threading.Tasks;
using MclApp.Core;

namespace UserIdentity.Services.AppManagement
{
    public interface IAppPermissionService
    {
        Task<OutResult> AddPermission(string name, string description);
        Task<OutResult> RemovePermission(string name);
    }
}