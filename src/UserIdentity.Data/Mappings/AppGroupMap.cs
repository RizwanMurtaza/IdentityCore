using MclApp.Core.IdentityDomain.Group;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserIdentity.Data.Mappings
{
    public partial class AppGroupMap : IEntityTypeConfiguration<AppGroup>
    {
        public void Configure(EntityTypeBuilder<AppGroup> builder)
        {
            builder.ToTable("AppGroups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ApplicationId).IsRequired();

        }
    }
}