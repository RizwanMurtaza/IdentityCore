using System.Reflection;
using MclApp.Data.DataContext;
using MclApp.Data.Repository;
using MclApp.Services.CyberScoreServices;
using MclApp.ViewModelServices;
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
    }
}
