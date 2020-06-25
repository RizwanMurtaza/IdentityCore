using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Core.Domain;

namespace MclApp.Services.CyberScoreServices
{
    public interface ICyberScoreService
    {
        Task<List<CyberScore>> GetCyberScoreForUser(Guid userId);
        Task<CyberScore> GetSingleCyberScoreForUser(Guid userId);
        Task<int> GetAllTasksCountForUser(Guid userId);
        Task<double> CalculateCyberScoreForUser(Guid userId);
    }
}