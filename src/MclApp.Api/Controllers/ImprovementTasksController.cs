using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Api.Controllers;
using MclApp.Core.Domain;
using MclApp.Services.ImprovementTaskServices;
using MclApp.ViewModelServices.ImprovementTask;
using MclApp.ViewModelServices.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MclApp.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ImprovementTasksController : BaseController
    {
        private readonly IImprovementTasksViewModelService _improvementTasksViewModelService;
        private readonly IImprovementTasksService _improvementTaskService;

        public ImprovementTasksController(IImprovementTasksViewModelService tasksViewModelService, IImprovementTasksService securityTaskService)
        {
            _improvementTasksViewModelService = tasksViewModelService;
            _improvementTaskService = securityTaskService;
        }

        [HttpGet]
        public async Task<List<ImprovementTasksViewModel>> GetImprovementTasks()
        {
            var data = await _improvementTasksViewModelService.GetAllTasksForUsers(this.BreachUser.Id);
            return data;
        }

        [HttpGet]
        public async Task<bool> ToggleTaskStatus(string taskId)
        {
            if (!Guid.TryParse(taskId, out var id)) return false;

            var data = await _improvementTaskService.GetTaskById(id);
            data.Status = data.Status == ImprovementTaskStatus.Completed ? ImprovementTaskStatus.Open : ImprovementTaskStatus.Completed;
            var result = await _improvementTaskService.UpdateTask(data);
            return result;

        }
        [HttpGet]
        public async Task<int> GetTaskCount(ImprovementTaskStatus status)
        {
            if (!Guid.TryParse(BreachUser.Id, out var id))
            {
                return 0;
            }
            var data = await _improvementTaskService.GetTaskCount(id, status);
            return data;
        }

    }
}
