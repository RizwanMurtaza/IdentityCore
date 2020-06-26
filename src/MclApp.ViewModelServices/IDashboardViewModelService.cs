using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.ViewModelServices.ViewModels;
using UserIdentity.ViewModels.Dashboard;

namespace MclApp.ViewModelServices
{
    public interface IDashboardViewModelService
    {
        Task<DashboardViewModel> GetDashBoardData(string userId);
        Task<List<VulnerabilitiesPieChartData>> GetChartData(string userId);
    }
}