using System.Collections.Generic;
using System.Security.Claims;

namespace UserIdentity.ViewModels.Authentication.Claims
{
    public class BreachIdentity : ClaimsIdentity
    {
        public BreachIdentity(TokenBreachUser user) : base("Password")
        {
            User = user;
        }

        public BreachIdentity(TokenBreachUser user, IEnumerable<Claim> claims) : base(claims, "Password")
        {
            User = user;
        }

        public TokenBreachUser User { get; }
    }
}