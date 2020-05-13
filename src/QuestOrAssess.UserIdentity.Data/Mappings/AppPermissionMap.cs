using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Core.Domain.Identity;

namespace QuestOrAssess.UserIdentity.Data.Mappings
{
    public partial class AppPermissionMap : IEntityTypeConfiguration<AppPermission>
    {
        public void Configure(EntityTypeBuilder<AppPermission> builder)
        {
            builder.ToTable("AppPermissions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("PermissionId");
            builder.Property(x => x.ApplicationId).IsRequired();
           // builder.HasMany<UserPermission>().WithOne(x => x.Permission).HasForeignKey(x => x.RoleId);

        }
    }
}
