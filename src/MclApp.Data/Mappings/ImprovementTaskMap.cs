using MclApp.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MclApp.Data.Mappings
{
    public class ImprovementTaskMap : IEntityTypeConfiguration<ImprovementTask>
    {
        public void Configure(EntityTypeBuilder<ImprovementTask> builder)
        {
            builder.ToTable("ImprovementTasks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TaskDescription).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}