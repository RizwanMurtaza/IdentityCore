using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Core;
using MclApp.Core.IdentityDomain;
using MclApp.Core.IdentityDomain.Group;
using MclApp.Core.IdentityDomain.Identity;

namespace UserIdentity.Services.AppManagement
{
    public interface IAppService
    {
        Task<ServiceResponse<Application>> GetApplicationByKey(string key);
        Task<ServiceResponse<Application>> GetApplicationById(int id);
        Task<ServiceResponse<IEnumerable<AppGroup>>> GetApplicationGroups(int id);
        Task<ServiceResponse<IEnumerable<AppUser>>> GetApplicationUsers(int id);
        Task<OutResult> AddApplication(Application app);
        Task<OutResult> UpdateApplication(Application app);
        Task<OutResult> DeleteApplication(int id);
        Task<ServiceResponse<Application>> GetApplicationByName(string name);

    }
}