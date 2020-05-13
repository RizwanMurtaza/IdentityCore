using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Core.Domain.Group;

namespace QuestOrAssess.UserIdentity.Data.Mappings
{
    public partial class AppGroupMap : IEntityTypeConfiguration<AppGroup>
    {
        public void Configure(EntityTypeBuilder<AppGroup> builder)
        {
            builder.ToTable("AppGroups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ApplicationId).IsRequired();

        }
    }
}