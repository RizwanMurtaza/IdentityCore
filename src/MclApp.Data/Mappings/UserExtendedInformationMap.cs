using MclApp.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MclApp.Data.Mappings
{
    public  class UserExtendedInformationMap : IEntityTypeConfiguration<UserExtendedInformation>
    {
        public void Configure(EntityTypeBuilder<UserExtendedInformation> builder)
        {
            builder.ToTable("UserExtendedInformation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}