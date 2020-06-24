using System;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserIdentity.Services.Authentication;
using UserIdentity.Services.Jwt;

namespace UserIdentity.Api.Filters
{
    public class UserApiAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string SchemeName = "UserAPIAuthScheme";

        public UserApiAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> om,
            ILoggerFactory lf,
            UrlEncoder ue,
            ISystemClock sc)
            : base(om, lf, ue, sc)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var headerValue = Request.Headers[AuthConstants.MvcToApiHeaderKey].ToString();
                var jwtTokenManager = (IJwtTokenService)Context.RequestServices.GetService(typeof(IJwtTokenService));
                // This server should have issued the token
                var expectedIssuer = Context.Request.Scheme + "://" + Context.Request.Host.ToString() + "/";
                var config = (IConfiguration)Context.RequestServices.GetService(typeof(IConfiguration));
                var expectedAudience = config["jwtAudience"];
                var gasPrincipal = jwtTokenManager.ReadToken(headerValue, expectedIssuer, expectedAudience);
                if (gasPrincipal == null)
                {
                    return AuthenticateResult.Fail("Couldn't read user from token");
                }
                // set the current context user
                var authenticationTicket = new AuthenticationTicket(gasPrincipal, SchemeName);
                Context.User = gasPrincipal;
                return AuthenticateResult.Success(authenticationTicket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}