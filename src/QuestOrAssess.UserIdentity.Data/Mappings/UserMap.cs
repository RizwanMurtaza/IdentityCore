using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data.Mappings
{
    public partial class UserMap : IEntityTypeConfiguration<User> 
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("ApplicationUser");
            builder.HasKey(x => x.Id);
        }
    }
}
