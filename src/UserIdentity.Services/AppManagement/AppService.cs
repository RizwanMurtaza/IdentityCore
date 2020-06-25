using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core;
using MclApp.Core.IdentityDomain;
using MclApp.Core.IdentityDomain.Group;
using MclApp.Core.IdentityDomain.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Data.Repository;

namespace UserIdentity.Services.AppManagement
{
    public class AppService : IAppService
    {
        private readonly IDbRepositoryPattern<Application> _applicationRepository;
        public AppService(IDbRepositoryPattern<Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<ServiceResponse<Application>> GetApplicationByKey(string key)
        {
            Guid.TryParse(key, out var appKey);

            var application = await _applicationRepository.Table.Where(x => x.ApplicationKey.Equals(appKey))
                .ToListAsync();
            return application.Any() ? new ServiceResponse<Application>().SuccessResponse(application.First()) : new ServiceResponse<Application>().SuccessWithNoResponse();
        }


        public async Task<ServiceResponse<Application>> GetApplicationById(int id)
        {
            var application = await GetById(id);
            return application.Any() ? new ServiceResponse<Application>().SuccessResponse(application.First()) : new ServiceResponse<Application>().SuccessWithNoResponse();
        }
        public async Task<ServiceResponse<Application>> GetApplicationByName(string name)
        {
            var result = await _applicationRepository.Table.Where(x => x.Description.ToLower().Equals(name))
                .ToListAsync();
            if (result.Any())
            {
                return new ServiceResponse<Application>().SuccessResponse(result.First());
            }

            return new ServiceResponse<Application>().SuccessWithNoResponse();
        }


        public async Task<ServiceResponse<IEnumerable<AppGroup>>> GetApplicationGroups(int id)
        {
            var application = await GetById(id);
            if (!application.Any()) 
                return new ServiceResponse<IEnumerable<AppGroup>>().SuccessWithNoResponse();
            
            var groups = application.First().ApplicationGroup;
            return groups.Any() ? new ServiceResponse<IEnumerable<AppGroup>>().SuccessResponse(groups) 
                                : new ServiceResponse<IEnumerable<AppGroup>>().SuccessWithNoResponse();
        }
        public async Task<ServiceResponse<IEnumerable<AppUser>>> GetApplicationUsers(int id)
        {
            var application = await GetById(id);
            if (!application.Any())
                return new ServiceResponse<IEnumerable<AppUser>>().SuccessWithNoResponse();

            var users = application.First().AppUsers;
            return users.Any() ? new ServiceResponse<IEnumerable<AppUser>>().SuccessResponse(users)
                : new ServiceResponse<IEnumerable<AppUser>>().SuccessWithNoResponse();
        }
        public async Task<OutResult> AddApplication(Application app)
        {
            return await _applicationRepository.Insert(app);
        }
        public async Task<OutResult> UpdateApplication(Application app)
        {
            return await _applicationRepository.Update(app);
        }
        public async Task<OutResult> DeleteApplication(int id)
        {
            var app = await GetById(id);
            if (!app.Any()) return OutResult.Error_TryingToDeleteNull();
            
            await _applicationRepository.Delete(app);
            return OutResult.Success_Deleted();
        }

      
        private async Task<List<Application>> GetById(int id)
        {
            var data = _applicationRepository.Table.Where(x => x.Id == id).ToListAsync();
            return await data;

        }
    }
}
