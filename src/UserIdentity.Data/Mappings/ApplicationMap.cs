using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Core.Domain;

namespace UserIdentity.Data.Mappings
{
    public partial class ApplicationMap : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("Applications");
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ApplicationGroup).WithOne(x => x.Application);
        }
    }
}