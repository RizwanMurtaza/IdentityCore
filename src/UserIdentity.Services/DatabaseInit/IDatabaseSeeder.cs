using System.Threading.Tasks;

namespace UserIdentity.Services.DatabaseInit
{
    public interface IDatabaseSeeder
    {
        Task<bool> InitializeDataBase();
    }
}