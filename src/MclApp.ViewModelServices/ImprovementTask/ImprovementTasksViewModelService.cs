using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MclApp.Services.ImprovementTaskServices;
using MclApp.ViewModelServices.ViewModels;

namespace MclApp.ViewModelServices.ImprovementTask
{
    public class ImprovementTasksViewModelService : IImprovementTasksViewModelService
    {
        private readonly IImprovementTasksService _securityTaskService;
        private readonly IMapper _mapper;

        public ImprovementTasksViewModelService(IImprovementTasksService securityTaskService, IMapper mapper)
        {
            _securityTaskService = securityTaskService;
            _mapper = mapper;
        }

        public async Task<List<ImprovementTasksViewModel>> GetImprovementTasksForUsers(string userId)
        {

            Guid.TryParse(userId, out var id);
            var allTasks = await _securityTaskService.GetAllTasksForUser(id);
            var modelToReturn = _mapper.Map<List<ImprovementTasksViewModel>>(allTasks);
            return modelToReturn.OrderByDescending(x=>x.Status).ToList();
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
