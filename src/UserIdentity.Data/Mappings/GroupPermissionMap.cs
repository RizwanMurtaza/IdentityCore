using MclApp.Core.IdentityDomain.Group;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserIdentity.Data.Mappings
{
    public partial class GroupPermissionMap : IEntityTypeConfiguration<MclGroupPermission>
    {
        public void Configure(EntityTypeBuilder<MclGroupPermission> builder)
        {
            builder.ToTable("MclGroupPermissions");
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