using System.Linq;
using System.Security.Claims;

namespace UserIdentity.ViewModels.Authentication.Claims
{
    public class BreachPrincipal : ClaimsPrincipal
    {
        public BreachPrincipal(BreachIdentity identity) : base(identity)
        {
        }

        public override bool IsInRole(string role)
        {
            return Identity is BreachIdentity bi
                ? bi.User.Roles.Any(x => x == role)
                : Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == role);
        }
    }
}
