using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using UserIdentity.ViewModels.Authentication.Claims;

namespace MclApp.Api.Filters
{
	public class ApiAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
	{

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
		{
			if (context.User == null || !context.User.Claims.Any())
			{
				return Task.CompletedTask;
			}
			if (requirement.AllowedRoles.Any(x => ((BreachIdentity)context.User.Identity).Claims
				// For some reason the JWT seems to be trimming the ROLE claim name down from a namespace to "role"
				.Any(y => (y.Type == ClaimTypes.Role || y.Type.ToLower() == "role") && y.Value == x)))
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}
			return Task.CompletedTask;
		}
	}
}
