using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Core.Domain;

namespace MclApp.Services.UserTaskServices
{
    public interface IUsersTasksService
    {
        Task<List<UserTask>> GetAllTasksForUser(Guid userId);
        Task<UserTask> GetTaskById(Guid taskId);
        Task<bool> UpdateTask(UserTask task);
        Task<int> GetTaskCount(Guid userId , UserTaskStatus status);
        Task<int> GetTaskCount(Guid userId);
    }
}