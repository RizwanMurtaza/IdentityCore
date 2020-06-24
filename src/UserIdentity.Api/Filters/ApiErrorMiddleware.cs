using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MclApp.API.Auth
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ApiErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _debug;
        public ApiErrorMiddleware(RequestDelegate next, bool debug)
        {
            _next = next;
            _debug = debug;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!_debug || !httpContext.Request.Path.ToString().ToLower().Contains("/api/"))
            {
                await _next(httpContext);
                return;
            }

            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
                {
                    httpContext.Response.StatusCode = 200;
                    httpContext.Response.Clear();
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        ex.Message,
                        ex.StackTrace
                    }));
                }
            }
            return;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ApiErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiErrorMiddleware(this IApplicationBuilder builder, bool debug)
        {
            return builder.UseMiddleware<ApiErrorMiddleware>(debug);
        }
    }
}
