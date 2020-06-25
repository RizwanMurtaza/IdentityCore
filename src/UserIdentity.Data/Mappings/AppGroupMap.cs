using MclApp.Core.IdentityDomain.Group;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserIdentity.Data.Mappings
{
    public partial class AppGroupMap : IEntityTypeConfiguration<MclAppGroup>
    {
        public void Configure(EntityTypeBuilder<MclAppGroup> builder)
        {
            builder.ToTable("MclAppGroups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ApplicationId).IsRequired();

        }
    }
}