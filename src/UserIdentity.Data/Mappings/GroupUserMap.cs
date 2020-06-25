using MclApp.Core.IdentityDomain.Group;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserIdentity.Data.Mappings
{
    public partial class GroupUserMap : IEntityTypeConfiguration<GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder.ToTable("GroupUsers");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Group).WithMany(x => x.UsersInGroup)
                .HasForeignKey(x => x.GroupId).OnDelete(deleteBehavior: DeleteBehavior.NoAction);
            builder.HasOne(x => x.User).WithMany(x => x.Groups)
                .HasForeignKey(x => x.UserId).OnDelete(deleteBehavior: DeleteBehavior.NoAction);
        }
    }
}