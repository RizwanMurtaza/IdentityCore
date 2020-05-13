using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuestOrAssess.UserIdentity.Core.Domain.Identity;

namespace QuestOrAssess.UserIdentity.Data
{
    public sealed class QuestOrAssessIdentityDbContext : IdentityDbContext<AppUser, AppPermission, int,
                                                    IdentityUserClaim<int>, AppUserPermission, LoginDetails, 
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public int CurrentUserId { get; set; }
        public QuestOrAssessIdentityDbContext(DbContextOptions<QuestOrAssessIdentityDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
