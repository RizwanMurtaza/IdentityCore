using MclApp.Core.IdentityDomain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserIdentity.Data.Mappings
{
    public partial class AppUserMap : IEntityTypeConfiguration<MclAppUser> 
    {
        public void Configure(EntityTypeBuilder<MclAppUser> builder)
        {
            builder.ToTable("MclAppUsers");
            builder.HasKey(x => x.Id);
        }
    }
}
