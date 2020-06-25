using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MclApp.Core.Domain;
using MclApp.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MclApp.Services.NarrativeServices
{
    public class UsersNarrativeService : INarrativeService
    {
        private readonly IDbRepositoryPattern<Narrative> _narrativeRepository;

        public UsersNarrativeService(IDbRepositoryPattern<Narrative> taskRepository)
        {
            _narrativeRepository = taskRepository;
        }
        public async Task<List<Narrative>> GetAllNarrativesForUser(Guid userId)
        {
            return await _narrativeRepository.Table.Where(x => x.UserId == userId).ToListAsync();
        }

    }

}
