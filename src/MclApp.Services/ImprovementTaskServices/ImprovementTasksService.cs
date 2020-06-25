using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core.Domain;
using MclApp.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MclApp.Services.ImprovementTaskServices
{
    public class ImprovementTasksService : IImprovementTasksService
    {
        private readonly IDbRepositoryPattern<ImprovementTask> _taskRepository;

        public ImprovementTasksService(IDbRepositoryPattern<ImprovementTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<List<ImprovementTask>> GetAllTasksForUser(Guid userId)
        {
          return await _taskRepository.Table.Where(x => x.UserId == userId).ToListAsync();
        }
        public Task<int> GetAllOpenTasksCountForUser(Guid userId)
        {
            return _taskRepository.Table.CountAsync(x => x.UserId == userId && x.Status == ImprovementTaskStatus.Open);
        }
        public async Task<ImprovementTask> GetTaskById(Guid taskId)
        {

            var result = await _taskRepository.Table.Where(x => x.Id == taskId).ToListAsync();

            return result.Any() ? result.First() : new ImprovementTask();
        }
        public async Task<bool> UpdateTask(ImprovementTask task)
        {
            var result = await _taskRepository.Update(task);
            return result.IsValid;
        }
        public async Task<int> GetTaskCount(Guid userId , ImprovementTaskStatus status)
        {
            var result = await _taskRepository.Table.CountAsync(x => x.UserId
                == userId && x.Status == status);

            return result;
        }
    }
}
