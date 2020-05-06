using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data.Mappings
{
    public partial class ApplicationUserLoginDetailMap : IEntityTypeConfiguration<ApplicationUserLoginDetails>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserLoginDetails> builder)
        {
            builder.ToTable("ApplicationUserLoginDetail");

        }
    }
}
