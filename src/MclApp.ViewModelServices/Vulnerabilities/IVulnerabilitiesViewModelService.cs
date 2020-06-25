using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.ViewModelServices.ViewModels;

namespace MclApp.ViewModelServices.Vulnerabilities
{
    public interface IVulnerabilitiesViewModelService
    {
        Task<List<CyberVulnerabilityViewModel>> GetAllVulnerabilitiesForUsers(string userId);
    }
}