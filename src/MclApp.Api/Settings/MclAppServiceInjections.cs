using System.Linq;
using System.Reflection;
using AutoMapper;
using MclApp.Data.DataContext;
using MclApp.Data.Repository;
using MclApp.Services.CyberScoreServices;
using MclApp.ViewModelServices;
using MclApp.ViewModelServices.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MclApp.Api.Settings
{
    public static class MclAppServiceInjections
    {
        public static IServiceCollection AddMclAppServiceInjections(this IServiceCollection services,
            IConfiguration configuration, bool useSqlServer = true)
        {
            var assemblyName = typeof(MclAppDataObjectContext).Assembly.GetName().Name;
            if (useSqlServer)
            {
                var dbConnectionString = configuration.GetConnectionString("BreachAppConnection");
                services.AddDbContext<MclAppDataObjectContext>(options =>
                    options.UseSqlServer(dbConnectionString,
                        optionsBuilder =>
                            optionsBuilder.MigrationsAssembly(assemblyName)
                    ));
            }
            else
            {
                var dbConnectionString = configuration.GetConnectionString("MySqlServerConnectionString");
                services.AddDbContext<MclAppDataObjectContext>(options =>
                {
                    options.UseMySql(dbConnectionString,
                        optionsBuilder =>
                            optionsBuilder.MigrationsAssembly(assemblyName)
                    );
                });
            }

            services.AddScoped(typeof(IDbRepositoryPattern<>), typeof(DbRepositoryPattern<>));
            var servicesAssembly = Assembly.GetAssembly(typeof(CyberScoreService));
            var viewModelAssembly = Assembly.GetAssembly(typeof(DashboardViewModelService));
            
            services.AddAutoMapper(typeof(VulnerabilityViewModel));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(CyberVulnerabilityViewModelMapping)));
            
            Register(services, servicesAssembly);
            Register(services, viewModelAssembly);
            return services;
        }

        public static void Register(IServiceCollection services, Assembly thisAssembly)
        {
            if (thisAssembly == null) return;
            var typesToRegister = thisAssembly.GetTypes();

            foreach (var typeToRegister in typesToRegister)
            {
                var typeInterfaces = typeToRegister.GetInterfaces();

                foreach (var typesInterface in typeInterfaces)
                {
                    services.AddScoped(typesInterface, typeToRegister);
                }
            }
        }
        public static void Register(IServiceCollection services)
        {
            var thisAssembly = Assembly.GetAssembly(typeof(MclAppServiceInjections));
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
