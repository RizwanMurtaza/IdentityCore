using System;
using System.Threading.Tasks;
using AutoMapper;
using MclApp.Services.CyberScoreServices;
using MclApp.Services.CyberVulnerabilityServices;
using MclApp.Services.ImprovementTaskServices;
using MclApp.Services.UserTaskServices;
using MclApp.ViewModelServices.ViewModels;

namespace MclApp.ViewModelServices
{
    public class DashboardViewModelService : IDashboardViewModelService
    {
        private readonly ICyberScoreService _cyberScoreService;
        private readonly ICyberVulnerabilityService _cyberVulnerabilityService;
        private readonly IImprovementTasksService _improvementTaskService;
        private readonly IUsersTasksService _usersTasksService;
        private readonly IMapper _mapper;
        public DashboardViewModelService(ICyberScoreService cyberScoreService, ICyberVulnerabilityService cyberVulnerabilityService, IImprovementTasksService securityTaskService, IMapper mapper, IUsersTasksService usersTasksService)
        {
            _cyberScoreService = cyberScoreService;
            _cyberVulnerabilityService = cyberVulnerabilityService;
            _improvementTaskService = securityTaskService;
            _mapper = mapper;
            _usersTasksService = usersTasksService;
        }

        public async Task<DashboardViewModel> GetDashBoardData(string userId)
        {
            Guid.TryParse(userId, out var userIdGuid);
            var model = new DashboardViewModel();
            
            var scoreData = await _cyberScoreService.GetSingleCyberScoreForUser(userIdGuid);
        
            model.TargetScore = (scoreData.TargetScore * 100).ToString("0.0");
            model.CurrentCyberScore = scoreData.ScoreString;
            model.CurrentCyberValue = (scoreData.Score * 100).ToString("0.0");
            var improvementTaskForUser = await _improvementTaskService.GetAllOpenTasksCountForUser(userIdGuid);
            model.ImprovementTasks = improvementTaskForUser.ToString();

            model.CurrentCyberScoreForChart = (scoreData.TargetScore * 100).ToString("0.0");

            if (scoreData.Score >= scoreData.TargetScore)
            {
                model.GoalScoreTracker = "On track";
            }
            else
            {
                model.GoalScoreTracker =
                    ((scoreData.TargetScore * 100) - (scoreData.Score * 100)).ToString("0.0");
            }

            var userTasks = await _usersTasksService.GetTaskCount(userIdGuid);
            model.UserTasks = userTasks.ToString();

            var actions = 0;
            model.Actions = actions.ToString();

          
            return model;
        }

    }
   
}
