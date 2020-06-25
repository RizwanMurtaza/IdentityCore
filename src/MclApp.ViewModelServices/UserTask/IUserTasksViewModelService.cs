using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.ViewModelServices.ViewModels;

namespace MclApp.ViewModelServices.UserTask
{
    public interface IUserTasksViewModelService
    {
        Task<List<UserTasksViewModel>> GetAllTasksForUsers(string userId);
        Task<List<UserNarrativeViewModel>> GetNarrativeForUsers(string userId);
    }
}