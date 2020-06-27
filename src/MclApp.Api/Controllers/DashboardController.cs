using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Api.Controllers;
using MclApp.ViewModelServices;
using MclApp.ViewModelServices.UserTask;
using MclApp.ViewModelServices.ViewModels;
using MclApp.ViewModelServices.Vulnerabilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.ViewModels.Dashboard;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace MclApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class DashBoardController : BaseController
    {

        private readonly IDashboardViewModelService _dashboardViewModelService;
        private readonly IVulnerabilitiesViewModelService _vulnerabilitiesService;
        private readonly IUserTasksViewModelService _userTasksViewModelService;



        public DashBoardController(IDashboardViewModelService dashboardViewModelService, IVulnerabilitiesViewModelService vulnerabilitiesService, IUserTasksViewModelService userTasksViewModelService)
        {
            _dashboardViewModelService = dashboardViewModelService;
            _vulnerabilitiesService = vulnerabilitiesService;
            _userTasksViewModelService = userTasksViewModelService;
        }
        
        public async Task<DashboardViewModel> GetDashBoardData([FromBody] string userId)
        {
            var data =  await _dashboardViewModelService.GetDashBoardData(this.BreachUser.MclUserId);
            return data;
        }
       
        public async Task<DashboardViewModel> GetVulnuriblities()
        {
            var data = await _dashboardViewModelService.GetDashBoardData(BreachUser.MclUserId);
            return data;
        }

        public async Task <List<VulnerabilitiesPieChartData>> GetVulnerabilitiesPieChartData()
        {
            var data = await _dashboardViewModelService.GetChartData(BreachUser.MclUserId);
            return data;
        }
        public async Task<List<CyberVulnerabilityViewModel>> GetVulnerabilityTableData()
        {
            var data = await _vulnerabilitiesService.GetAllVulnerabilitiesForUsers(this.BreachUser.MclUserId);
            return data;
        }
        public async Task<List<UserTasksViewModel>> GetAllUserTasks()
        {
            var data = await _userTasksViewModelService.GetAllTasksForUsers(this.BreachUser.MclUserId);
            return data;
        }
        public async Task<List<UserNarrativeViewModel>> GetUserNarratives()
        {
            var data = await _userTasksViewModelService.GetNarrativeForUsers(this.BreachUser.MclUserId);
            return data;
        }




    }
}
