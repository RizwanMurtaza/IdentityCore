using System.Threading.Tasks;
using MclApp.ViewModelServices.ViewModels;

namespace MclApp.ViewModelServices
{
    public interface IDashboardViewModelService
    {
        Task<DashboardViewModel> GetDashBoardData(string userId);
    }
}