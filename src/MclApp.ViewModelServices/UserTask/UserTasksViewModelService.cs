using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MclApp.Services.NarrativeServices;
using MclApp.Services.UserTaskServices;
using MclApp.ViewModelServices.ViewModels;

namespace MclApp.ViewModelServices.UserTask
{
    public class UserTasksViewModelService : IUserTasksViewModelService
    {
        private readonly IUsersTasksService _usersTasksService;
        private readonly INarrativeService _narrativeService;
        private readonly IMapper _mapper;

        public UserTasksViewModelService(IUsersTasksService securityTaskService, IMapper mapper, INarrativeService narrativeService)
        {
            _usersTasksService = securityTaskService;
            _mapper = mapper;
            _narrativeService = narrativeService;
        }

        public async Task<List<UserTasksViewModel>> GetAllTasksForUsers(string userId)
        {

            Guid.TryParse(userId, out var id);
            var allTasks = await _usersTasksService.GetAllTasksForUser(id);
            var modelToReturn = _mapper.Map<List<UserTasksViewModel>>(allTasks);
            return modelToReturn.OrderByDescending(x=>x.Status).ToList();
        }

        public async Task<List<UserNarrativeViewModel>> GetNarrativeForUsers(string userId)
        {
            Guid.TryParse(userId, out var id);
            var allTasks = await _narrativeService.GetAllNarrativesForUser(id);
            var modelToReturn = _mapper.Map<List<UserNarrativeViewModel>>(allTasks);
            return modelToReturn;
        }

        //public async Task<List<SecurityTaskViewModel>> MarkTaskAsCompleted(string taskId)
        //{

        //    Guid.TryParse(userId, out var id);
        //    var allTasks = await _securityTaskService.GetAllTasksForUser(id);
        //    var modelToReturn = _mapper.Map<List<SecurityTaskViewModel>>(allTasks);
        //    return modelToReturn.OrderByDescending(x => x.Status).ToList();
        //}


    }
}
