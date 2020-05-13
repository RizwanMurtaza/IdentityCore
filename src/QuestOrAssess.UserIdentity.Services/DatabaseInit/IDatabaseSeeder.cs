using System.Threading.Tasks;

namespace QuestOrAssess.UserIdentity.Services.DatabaseInit
{
    public interface IDatabaseSeeder
    {
        Task<bool> InitializeDataBase();
    }
}