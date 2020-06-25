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
        private readonly IIdentityDbRepository<MclApplication> _applicationRepository;
        public AppService(IIdentityDbRepository<MclApplication> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<ServiceResponse<MclApplication>> GetApplicationByKey(string key)
        {
            Guid.TryParse(key, out var appKey);

            var application = await _applicationRepository.Table.Where(x => x.ApplicationKey.Equals(appKey))
                .ToListAsync();
            return application.Any() ? new ServiceResponse<MclApplication>().SuccessResponse(application.First()) : new ServiceResponse<MclApplication>().SuccessWithNoResponse();
        }


        public async Task<ServiceResponse<MclApplication>> GetApplicationById(int id)
        {
            var application = await GetById(id);
            return application.Any() ? new ServiceResponse<MclApplication>().SuccessResponse(application.First()) : new ServiceResponse<MclApplication>().SuccessWithNoResponse();
        }
        public async Task<ServiceResponse<MclApplication>> GetApplicationByName(string name)
        {
            var result = await _applicationRepository.Table.Where(x => x.Description.ToLower().Equals(name))
                .ToListAsync();
            if (result.Any())
            {
                return new ServiceResponse<MclApplication>().SuccessResponse(result.First());
            }

            return new ServiceResponse<MclApplication>().SuccessWithNoResponse();
        }


        public async Task<ServiceResponse<IEnumerable<MclAppGroup>>> GetApplicationGroups(int id)
        {
            var application = await GetById(id);
            if (!application.Any()) 
                return new ServiceResponse<IEnumerable<MclAppGroup>>().SuccessWithNoResponse();
            
            var groups = application.First().ApplicationGroup;
            return groups.Any() ? new ServiceResponse<IEnumerable<MclAppGroup>>().SuccessResponse(groups) 
                                : new ServiceResponse<IEnumerable<MclAppGroup>>().SuccessWithNoResponse();
        }
        public async Task<ServiceResponse<IEnumerable<MclAppUser>>> GetApplicationUsers(int id)
        {
            var application = await GetById(id);
            if (!application.Any())
                return new ServiceResponse<IEnumerable<MclAppUser>>().SuccessWithNoResponse();

            var users = application.First().AppUsers;
            return users.Any() ? new ServiceResponse<IEnumerable<MclAppUser>>().SuccessResponse(users)
                : new ServiceResponse<IEnumerable<MclAppUser>>().SuccessWithNoResponse();
        }
        public async Task<OutResult> AddApplication(MclApplication app)
        {
            return await _applicationRepository.Insert(app);
        }
        public async Task<OutResult> UpdateApplication(MclApplication app)
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

      
        private async Task<List<MclApplication>> GetById(int id)
        {
            var data = _applicationRepository.Table.Where(x => x.Id == id).ToListAsync();
            return await data;

        }
    }
}
