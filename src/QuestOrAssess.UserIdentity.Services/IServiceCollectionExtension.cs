using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Data;
using QuestOrAssess.UserIdentity.Data.Repository;
using QuestOrAssess.UserIdentity.Services.ApplicationManagement;

namespace QuestOrAssess.UserIdentity.Services
{
    public static class UserIdentityDependencyInjection
    {
        public static IServiceCollection AddUserIdentityServices(this IServiceCollection services, IConfiguration configuration, bool useSqlServer = true)
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
            services.AddIdentity<User, Permission>().AddEntityFrameworkStores<QuestOrAssessIdentityDbContext>().AddUserManager<UserManager<User>>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IGroupService, GroupService>();




            return services;
        }
    }
}
