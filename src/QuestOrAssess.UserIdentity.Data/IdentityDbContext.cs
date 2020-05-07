using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data
{
    public class QuestOrAssessIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
                                                    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLoginDetails, 
                                                    ApplicationRoleClaim, IdentityUserToken<int>>
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
