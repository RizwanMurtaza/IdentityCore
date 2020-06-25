using System.Net.Mime;
using MclApp.Core.IdentityDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserIdentity.Data.Mappings
{
    public partial class ApplicationMap : IEntityTypeConfiguration<MclApplication>
    {
        public void Configure(EntityTypeBuilder<MclApplication> builder)
        {
            builder.ToTable("MclApplications");
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ApplicationGroup).WithOne(x => x.Application);
        }
    }
}