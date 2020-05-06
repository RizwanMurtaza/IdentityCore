using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data.Mappings
{
    public partial class ApplicationUserRoleMap : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("ApplicationUserRoles");

            builder.HasOne(p => p.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(p => p.RoleId)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(p => p.UserId)
                .IsRequired();
        }
    }
}
