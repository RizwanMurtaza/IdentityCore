using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data.Mappings
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
