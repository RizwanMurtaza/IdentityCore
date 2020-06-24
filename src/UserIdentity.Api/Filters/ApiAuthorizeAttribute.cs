using Microsoft.AspNetCore.Authorization;

namespace UserIdentity.Api.Filters
{
	public class ApiAuthorizeAttribute : AuthorizeAttribute
	{
		public ApiAuthorizeAttribute() : base()
		{
			AuthenticationSchemes = UserApiAuthenticationHandler.SchemeName;
			Policy = "Default";
		}
	}
}
