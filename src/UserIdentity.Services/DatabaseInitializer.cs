using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserIdentity.Data;
using UserIdentity.Services.DatabaseInit;

namespace UserIdentity.Services
{
    public static class DatabaseInitializer
    {
        public static bool EnsureDatabaseIsSeeded(this IApplicationBuilder applicationBuilder, bool autoMigrateDatabase)
        {
            var serviceScopes = applicationBuilder.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope();

            var seedService = serviceScopes.ServiceProvider.GetService<IDatabaseSeeder>();
            var context = serviceScopes.ServiceProvider.GetService<QuestOrAssessIdentityDbContext>();
            if (autoMigrateDatabase)
            {
                context.Database.Migrate();
            }

            return seedService.InitializeDataBase().Result;

        }

    }
}
