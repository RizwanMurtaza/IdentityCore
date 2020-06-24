using System.Collections.Generic;
using System.Security.Claims;
using UserIdentity.ViewModels.Authentication.Claims;

namespace UserIdentity.Services.Jwt
{
    public interface IJwtTokenService
    {
        string CreateToken(TokenBreachUser user, List<Claim> claims);
        string CreateToken(TokenBreachUser user, IEnumerable<string> roles);
        BreachPrincipal ReadToken(string token, string expectedIssuer, string expectedAudience);
    }
}