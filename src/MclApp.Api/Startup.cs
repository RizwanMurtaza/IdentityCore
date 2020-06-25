using System.Reflection;
using AutoMapper;
using MclApp.Api.Filters;
using MclApp.Api.Settings;
using MclApp.Services;
using MclApp.ViewModelServices.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using UserIdentity.Services;

namespace MclApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            services.AddAuthentication(x =>
            {
                x.AddScheme<UserApiAuthenticationHandler>(UserApiAuthenticationHandler.SchemeName, UserApiAuthenticationHandler.SchemeName);
                x.DefaultScheme = UserApiAuthenticationHandler.SchemeName;
            });

            services.AddAuthorization(opt =>
            {
                var apb = new AuthorizationPolicyBuilder(new[] { UserApiAuthenticationHandler.SchemeName });
                apb = apb.RequireAuthenticatedUser();
                opt.DefaultPolicy = apb.Build();
                opt.AddPolicy("Default", policy =>
                {
                    policy.AuthenticationSchemes.Add(UserApiAuthenticationHandler.SchemeName);
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        );
            });
            services.AddUserIdentityServices(Configuration);


            //services.AddAutoMapper(assembly[])
            services.AddMclAppServiceInjections(Configuration);
            
         
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (app.EnsureIdentityIsSeeded(true))
                {
                    app.EnsureMclAppIsSeeded(true);
                }
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
 

            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


             app.UseAuthentication();
             app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }
    }
}
