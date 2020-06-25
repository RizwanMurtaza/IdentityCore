using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MclApp.Services.CyberVulnerabilityServices;
using MclApp.ViewModelServices.ViewModels;

namespace MclApp.ViewModelServices.Vulnerabilities
{
    public class VulnerabilitiesViewModelService : IVulnerabilitiesViewModelService
    {

        private readonly ICyberVulnerabilityService _cyberVulnerabilityService;
        private readonly IMapper _mapper;

        public VulnerabilitiesViewModelService(ICyberVulnerabilityService cyberVulnerabilityService, IMapper mapper)
        {
            _cyberVulnerabilityService = cyberVulnerabilityService;
            _mapper = mapper;
        }

        public async Task<List<CyberVulnerabilityViewModel>> GetAllVulnerabilitiesForUsers(string userId)
        {
            Guid.TryParse(userId, out var id);
            var allTasks = await _cyberVulnerabilityService.GetCyberVulnerabilityForUser(id);
            var modelToReturn = _mapper.Map<List<CyberVulnerabilityViewModel>>(allTasks);
            return modelToReturn;
        }



    }
}
