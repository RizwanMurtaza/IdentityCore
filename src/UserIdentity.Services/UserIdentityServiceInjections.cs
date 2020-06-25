using System.Linq;
using System.Reflection;
using System.Text;
using MclApp.Core.IdentityDomain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserIdentity.Data;
using UserIdentity.Data.Repository;
using UserIdentity.Services.Email;
using UserIdentity.Services.Jwt;

namespace UserIdentity.Services
{
    public static class UserIdentityServiceInjections
    {
        public static IServiceCollection AddUserIdentityServices(this IServiceCollection services,
            IConfiguration configuration, bool useSqlServer = true)
        {
            var assemblyName = typeof(IdentityDbContext).Assembly.GetName().Name;
            if (useSqlServer)
            {
                var dbConnectionString = configuration.GetConnectionString("SqlServerConnectionString");
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseSqlServer(dbConnectionString,
                        optionsBuilder =>
                            optionsBuilder.MigrationsAssembly(assemblyName)
                    ));
            }
            else
            {
                var dbConnectionString = configuration.GetConnectionString("MySqlServerConnectionString");
                services.AddDbContext<IdentityDbContext>(options =>
                {
                    options.UseMySql(dbConnectionString,
                        optionsBuilder =>
                            optionsBuilder.MigrationsAssembly(assemblyName)
                    );
                });
            }

            services.AddScoped(typeof(IIdentityDbRepository<>), typeof(IdentityDbRepository<>));
            
            
            
            var key = Encoding.ASCII.GetBytes(configuration["jwtTokenSecret"]);
            var audience = configuration["jwtAudience"];
            var jwtExpiryInDays = int.Parse(configuration["jwtExpiryInDays"]);
            services.AddTransient<IJwtTokenService>(a => new JwtTokenService(key, audience, a.GetService<IHttpContextAccessor>(), jwtExpiryInDays));
            var breachApiUrl = configuration["BreachApi:Url"];
            var breachApiEmail = configuration["BreachApi:Email"];
            var breachApiPassword = configuration["BreachApi:Password"];
           
            if (configuration.GetSection("SendGrid").Exists())
            {
                var sendGridApiKey = configuration["SendGrid:ApiKey"];
                var sendGridToOverride = configuration["SendGrid:ToOverride"];
                services.AddTransient<IEmailService>(a => new EmailService(sendGridApiKey, sendGridToOverride));
            }
            else if (configuration.GetSection("SmtpServer").Exists())
            {
                var toOverride = configuration["SmtpServer:ToOverride"];
                var ipAddress = configuration["SmtpServer:IpAddress"];
                var username = configuration["SmtpServer:Username"];
                var password = configuration["SmtpServer:Password"];
                services.AddTransient<IEmailService>(a => new EmailService(ipAddress, username, password, toOverride));
            }

           //services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddIdentity<AppUser, AppPermission>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<IdentityDbContext>()
                .AddUserManager<UserManager<AppUser>>().AddDefaultTokenProviders();

            Register(services);
            return services;
        }

        public static void Register(IServiceCollection services)
        {
            var thisAssembly = Assembly.GetAssembly(typeof(UserIdentityServiceInjections));
            var namespaces = new[]
            {
                "UserIdentity.Services.AppManagement",
                "UserIdentity.Services.UserManagement",
                "UserIdentity.Services.DatabaseInit",
                "UserIdentity.Services.Authentication"
            };

            if (thisAssembly == null) return;
            var typesToRegister = thisAssembly.GetTypes().Where(x => namespaces.Contains(x.Namespace)).ToList();

            foreach (var typeToRegister in typesToRegister)
            {
                var typeInterfaces = typeToRegister.GetInterfaces();

                foreach (var typesInterface in typeInterfaces)
                {
                    services.AddScoped(typesInterface, typeToRegister);
                }
            }
        }
    }
}
