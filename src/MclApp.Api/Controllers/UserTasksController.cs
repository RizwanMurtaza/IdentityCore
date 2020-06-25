using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Api.Controllers;
using MclApp.Core.Domain;
using MclApp.Services.NarrativeServices;
using MclApp.Services.UserTaskServices;
using MclApp.ViewModelServices;
using MclApp.ViewModelServices.UserTask;
using MclApp.ViewModelServices.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MclApp.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UserTasksController : BaseController
    {
        private readonly IUserTasksViewModelService _userTasksViewModelService;
        private readonly IUsersTasksService _usersTasksService;
        private readonly IUserExtendedInformationViewModelService _extendedInformationViewModelService;
        private readonly INarrativeService _narrativeService;

        public UserTasksController(IUserTasksViewModelService improvementTasksViewModelService, IUsersTasksService securityTaskService, INarrativeService narrativeService, IUserExtendedInformationViewModelService extendedInformationViewModelService)
        {
            _userTasksViewModelService = improvementTasksViewModelService;
            _usersTasksService = securityTaskService;
            _narrativeService = narrativeService;
            _extendedInformationViewModelService = extendedInformationViewModelService;
        }

        [HttpGet]
        public async Task<List<UserTasksViewModel>> GetAllUserTasks()
        {
            var data = await _userTasksViewModelService.GetAllTasksForUsers(this.BreachUser.Id);
            return data;
        }

        [HttpGet]
        public async Task<List<UserNarrativeViewModel>> GetUserNarratives()
        {
            var data = await _userTasksViewModelService.GetNarrativeForUsers(this.BreachUser.Id);
            return data;
        }
        [HttpGet]
        public async Task<bool> ToggleTaskStatus(string taskId)
        {
            if (!Guid.TryParse(taskId, out var id)) return false;

            var data = await _usersTasksService.GetTaskById(id);
            data.Status = data.Status == UserTaskStatus.No ? UserTaskStatus.Yes : UserTaskStatus.No;
            var result = await _usersTasksService.UpdateTask(data);
            return result;

        }
        [HttpGet]
        public async Task<int> GetTaskCount(UserTaskStatus status)
        {
            if (!Guid.TryParse(BreachUser.Id, out var id))
            {
                return 0;
            }
            var data = await _usersTasksService.GetTaskCount(id, status);
            return data;
        }

        [HttpGet]
        public async Task<UserExtendedInformationViewModel> GetExtendedInformation()
        {
            var data = await _extendedInformationViewModelService.GetUserExtendedInformation(BreachUser.Id);
            data.FirstName = BreachUser.FirstName;
            data.LastName = BreachUser.LastName;
            data.Email = BreachUser.Email;
            return data;
        }


    }
}
