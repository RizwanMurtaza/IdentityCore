using System.Threading.Tasks;

namespace UserIdentity.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<(bool, string)> Login(string userName, string password);
    }
}