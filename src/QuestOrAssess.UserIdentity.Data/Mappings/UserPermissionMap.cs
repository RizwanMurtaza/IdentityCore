using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data.Mappings
{
    public partial class UserPermissionMap : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermissions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoleId).HasColumnName("PermissionId");

            builder.HasOne(x=>x.Permission).WithMany(x=>x.UserPermissions)
                .HasForeignKey(y => y.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User).WithMany(x => x.Permission)
                .HasForeignKey(y => y.UserId)
                .OnDelete(DeleteBehavior.NoAction);

          

        }
    }
}
