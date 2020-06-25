using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Core.Domain;

namespace MclApp.Services.ImprovementTaskServices
{
    public interface IImprovementTasksService
    {
        Task<List<ImprovementTask>> GetAllTasksForUser(Guid userId);
        Task<int> GetAllOpenTasksCountForUser(Guid userId);
        Task<ImprovementTask> GetTaskById(Guid taskId);
        Task<bool> UpdateTask(ImprovementTask task);
        Task<int> GetTaskCount(Guid userId , ImprovementTaskStatus status);
    }
}