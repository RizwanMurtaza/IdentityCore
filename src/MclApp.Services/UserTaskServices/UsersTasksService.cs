using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core.Domain;
using MclApp.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MclApp.Services.UserTaskServices
{
    public class UsersTasksService : IUsersTasksService
    {
        private readonly IDbRepositoryPattern<UserTask> _taskRepository;

        public UsersTasksService(IDbRepositoryPattern<UserTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<List<UserTask>> GetAllTasksForUser(Guid userId)
        {
          return await _taskRepository.Table.Where(x => x.UserId == userId).ToListAsync();
        }
       public async Task<UserTask> GetTaskById(Guid taskId)
        {

            var result = await _taskRepository.Table.Where(x => x.Id == taskId).ToListAsync();

            return result.Any() ? result.First() : new UserTask();
        }
        public async Task<bool> UpdateTask(UserTask task)
        {
            var result = await _taskRepository.Update(task);
            return result.IsValid;
        }
        public async Task<int> GetTaskCount(Guid userId , UserTaskStatus status)
        {
            var result = await _taskRepository.Table.CountAsync(x => x.UserId
                == userId && x.Status == status);

            return result;
        }
        public async Task<int> GetTaskCount(Guid userId)
        {
            var result = await _taskRepository.Table.CountAsync(x => x.UserId
                == userId );

            return result;
        }
    }
}
