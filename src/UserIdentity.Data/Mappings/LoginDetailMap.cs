using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Core.Domain.Identity;

namespace UserIdentity.Data.Mappings
{
    public partial class LoginDetailMap : IEntityTypeConfiguration<LoginDetails>
    {
        public void Configure(EntityTypeBuilder<LoginDetails> builder)
        {
            builder.ToTable("LoginDetail");
            //builder.Property(x => x.LoginProvider).HasMaxLength(760);
        }
    }
}
