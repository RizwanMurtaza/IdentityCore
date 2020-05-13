using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuestOrAssess.UserIdentity.Core.Domain.Identity;
using QuestOrAssess.UserIdentity.Data;
using QuestOrAssess.UserIdentity.Data.Repository;

namespace QuestOrAssess.UserIdentity.Services
{
    public static class UserIdentityServiceInjections
    {
        public static IServiceCollection AddUserIdentityServices(this IServiceCollection services,
            IConfiguration configuration, bool useSqlServer = true)
        {
            var assemblyName = typeof(QuestOrAssessIdentityDbContext).Assembly.GetName().Name;
            if (useSqlServer)
            {
                var dbConnectionString = configuration.GetConnectionString("SqlServerConnectionString");
                services.AddDbContext<QuestOrAssessIdentityDbContext>(options =>
                    options.UseSqlServer(dbConnectionString,
                        optionsBuilder =>
                            optionsBuilder.MigrationsAssembly(assemblyName)
                    ));
            }
            else
            {
                var dbConnectionString = configuration.GetConnectionString("MySqlServerConnectionString");
                services.AddDbContext<QuestOrAssessIdentityDbContext>(options =>
                {
                    options.UseMySql(dbConnectionString,
                        optionsBuilder =>
                            optionsBuilder.MigrationsAssembly(assemblyName)
                    );
                });
            }

            services.AddScoped(typeof(IDbRepositoryPattern<>), typeof(DbRepositoryPattern<>));
            services.Configure<JwtOptions>(configuration.GetSection("jwt"));
            services.AddIdentity<AppUser, AppPermission>().AddEntityFrameworkStores<QuestOrAssessIdentityDbContext>()
                .AddUserManager<UserManager<AppUser>>();
            Register(services);
            return services;
        }

        public static void Register(IServiceCollection services)
        {
            var thisAssembly = Assembly.GetAssembly(typeof(UserIdentityServiceInjections));
            var namespaces = new[]
            {
                "QuestOrAssess.UserIdentity.Services.AppManagement",
                "QuestOrAssess.UserIdentity.Services.UserManagement",
                "QuestOrAssess.UserIdentity.Services.Authentication",
                "QuestOrAssess.UserIdentity.Services.DatabaseInit"
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
