using MclApp.Core.IdentityDomain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserIdentity.Data.Mappings
{
    public partial class AppPermissionMap : IEntityTypeConfiguration<MclAppPermission>
    {
        public void Configure(EntityTypeBuilder<MclAppPermission> builder)
        {
            builder.ToTable("MclAppPermissions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("PermissionId");
            builder.Property(x => x.MclApplicationId).IsRequired();
           // builder.HasMany<UserPermission>().WithOne(x => x.Permission).HasForeignKey(x => x.RoleId);

        }
    }
}
