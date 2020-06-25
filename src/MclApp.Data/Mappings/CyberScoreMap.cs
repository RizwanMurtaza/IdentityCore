using MclApp.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MclApp.Data.Mappings
{
    public  class CyberScoreMap : IEntityTypeConfiguration<CyberScore>
    {
        public void Configure(EntityTypeBuilder<CyberScore> builder)
        {
            builder.ToTable("CyberScores");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Score).IsRequired();
            builder.Property(x => x.TargetScore).IsRequired();
        }
    }
}