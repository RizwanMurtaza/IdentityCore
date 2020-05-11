using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data.Mappings
{
    public partial class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("PermissionId");
            builder.Property(x => x.ApplicationId).IsRequired();
           // builder.HasMany<UserPermission>().WithOne(x => x.Permission).HasForeignKey(x => x.RoleId);

        }
    }
}
