using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core.Domain;
using MclApp.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MclApp.Services.CyberScoreServices
{
    public class CyberScoreService : ICyberScoreService
    {
        private readonly IDbRepositoryPattern<CyberScore> _cyberScoreRepository;

        public CyberScoreService(IDbRepositoryPattern<CyberScore> cyberScoreRepository)
        {
            _cyberScoreRepository = cyberScoreRepository;
        }
        public async Task<List<CyberScore>> GetCyberScoreForUser(Guid userId)
        {
            return await _cyberScoreRepository.Table.Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<CyberScore> GetSingleCyberScoreForUser(Guid userId)
        {
            var result = _cyberScoreRepository.Table.Where(x => x.UserId == userId).OrderByDescending(x => x.Date);
            var singleRow = await result.AnyAsync();
            if (singleRow)
                return result.First();

            return new CyberScore();

        }

        public Task<int> GetAllTasksCountForUser(Guid userId)
        {
            return _cyberScoreRepository.Table.CountAsync(x => x.UserId == userId);
        }

        public async Task<double> CalculateCyberScoreForUser(Guid userId)
        {
            var total = await _cyberScoreRepository.Table.SumAsync(x => x.Score);
            return total;
        }



    }
}