using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MclApp.Core.Domain;

namespace MclApp.Services.NarrativeServices
{
    public interface INarrativeService
    {
        Task<List<Narrative>> GetAllNarrativesForUser(Guid userId);
    }
}