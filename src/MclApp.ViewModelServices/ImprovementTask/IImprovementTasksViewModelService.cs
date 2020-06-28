using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.ViewModelServices.ViewModels;

namespace MclApp.ViewModelServices.ImprovementTask
{
    public interface IImprovementTasksViewModelService
    {
        Task<List<ImprovementTasksViewModel>> GetImprovementTasksForUsers(string userId);
    }
}