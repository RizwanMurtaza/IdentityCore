using MclApp.Core.IdentityDomain.Identity;
using UserIdentity.ViewModels.Authentication.Claims;

namespace UserIdentity.Services.Helper
{
    public static class HelperService
    {
        public static TokenBreachUser ToUser(MclAppUser user)
        {
            return new TokenBreachUser
            {
                Username = user.UserName,
                Email = user.Email,
                Id = user.Id.ToString(),
                IsActive = user.IsActive,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
