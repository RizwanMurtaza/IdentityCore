using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Api.Controllers;
using MclApp.ViewModelServices;
using MclApp.ViewModelServices.ViewModels;
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

        public DashBoardController(IDashboardViewModelService dashboardViewModelService)
        {
            _dashboardViewModelService = dashboardViewModelService;
        }
        
        public async Task<DashboardViewModel> GetDashBoardData([FromBody] string userId)
        {
            var data =  await _dashboardViewModelService.GetDashBoardData(this.BreachUser.MclUserId);
            return data;
        }
       
        public async Task<DashboardViewModel> GetVulnuriblities()
        {
            var data = await _dashboardViewModelService.GetDashBoardData(BreachUser.Id);
            return data;
        }

        public async Task <List<VulnerabilitiesPieChartData>> GetVulnerabilitiesPieChartData()
        {
            var data = await _dashboardViewModelService.GetChartData(BreachUser.Id);
            return data;
        }



    }
}
