using Microsoft.AspNetCore.Authorization;

namespace MclApp.Api.Filters
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
