using MclApp.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MclApp.Data.Mappings
{
    public  class NarrativeMap : IEntityTypeConfiguration<Narrative>
    {
        public void Configure(EntityTypeBuilder<Narrative> builder)
        {
            builder.ToTable("Narratives");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).IsRequired();
        }
    }
}