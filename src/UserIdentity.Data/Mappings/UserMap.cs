using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Core.Domain.Identity;

namespace UserIdentity.Data.Mappings
{
    public partial class AppUserMap : IEntityTypeConfiguration<AppUser> 
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.HasKey(x => x.Id);
        }
    }
}
