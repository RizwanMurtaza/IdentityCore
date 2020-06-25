using MclApp.Core.IdentityDomain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserIdentity.Data.Mappings
{
    public partial class LoginDetailMap : IEntityTypeConfiguration<LoginDetails>
    {
        public void Configure(EntityTypeBuilder<LoginDetails> builder)
        {
            builder.ToTable("MclLoginDetail");
            //builder.Property(x => x.LoginProvider).HasMaxLength(760);
        }
    }
}
