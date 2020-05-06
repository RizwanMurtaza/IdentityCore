using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data.Mappings
{
    public class ApplicationRoleClaimMap : IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {
            // still use default table name
            builder.ToTable("ApplicationRoleClaim");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.RoleId)
                .HasColumnName(nameof(ApplicationRoleClaim.RoleId));

            builder.HasOne(p => p.Role)
                .WithMany(r => r.RoleClaims)
                .HasForeignKey(p => p.RoleId)
                .IsRequired();
        }
    }
}
