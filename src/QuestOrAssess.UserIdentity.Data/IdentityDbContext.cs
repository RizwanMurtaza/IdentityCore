using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data
{
    public class QuestOrAssessIdentityDbContext : IdentityDbContext<User, Permission, int,
                                                    IdentityUserClaim<int>, UserPermission, LoginDetails, 
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
