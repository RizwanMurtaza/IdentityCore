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
        Task<ServiceResponse<MclApplication>> GetApplicationByKey(string key);
        Task<ServiceResponse<MclApplication>> GetApplicationById(int id);
        Task<ServiceResponse<IEnumerable<MclAppGroup>>> GetApplicationGroups(int id);
        Task<ServiceResponse<IEnumerable<MclAppUser>>> GetApplicationUsers(int id);
        Task<OutResult> AddApplication(MclApplication app);
        Task<OutResult> UpdateApplication(MclApplication app);
        Task<OutResult> DeleteApplication(int id);
        Task<ServiceResponse<MclApplication>> GetApplicationByName(string name);

    }
}