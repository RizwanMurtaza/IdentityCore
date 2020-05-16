using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Core.Domain.Group;

namespace UserIdentity.Data.Mappings
{
    public partial class GroupPermissionMap : IEntityTypeConfiguration<GroupPermission>
    {
        public void Configure(EntityTypeBuilder<GroupPermission> builder)
        {
            builder.ToTable("GroupPermissions");
            builder.HasKey(x => x.Id);


            builder.HasOne(x=>x.Group)
                .WithMany(x => x.GroupPermissions)
                .HasForeignKey(y => y.PermissionId)
                .OnDelete(DeleteBehavior.NoAction); ;
            builder.HasOne(x=>x.Permission)
                .WithMany(x => x.GroupPermissions).HasForeignKey(y => y.GroupId)
                .OnDelete(DeleteBehavior.NoAction); ;


        }
    }
}