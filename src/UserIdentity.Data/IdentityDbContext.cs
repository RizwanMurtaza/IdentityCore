using System.Reflection;
using MclApp.Core.IdentityDomain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserIdentity.Data
{
    public sealed class IdentityDbContext : IdentityDbContext<MclAppUser, MclAppPermission, int,
                                                    IdentityUserClaim<int>, MclAppUserPermission, LoginDetails, 
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public int CurrentUserId { get; set; }
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
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
