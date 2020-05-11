using System.Threading.Tasks;

namespace QuestOrAssess.UserIdentity.Services
{
    public interface IAuthenticationService
    {
        Task<(bool, string)> Login(string userName, string password);
    }
}