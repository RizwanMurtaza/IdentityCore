using MclApp.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MclApp.Data.Mappings
{
    public class UserTaskMap : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.ToTable("UserTasks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TaskDescription).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.DueDate).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}