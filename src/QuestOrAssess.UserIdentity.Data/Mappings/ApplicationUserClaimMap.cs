using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data.Mappings
{
    public partial class ApplicationUserClaimMap : IEntityTypeConfiguration<ApplicationUserClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
        {
            builder.ToTable("ApplicationUserClaims");
            
            builder.Property(p => p.UserId)
                .HasColumnName(nameof(ApplicationUserClaim.UserId));

            builder.HasOne(p => p.User)
                .WithMany(u => u.UserClaims)
                .HasForeignKey(p => p.UserId)
                .IsRequired();
        }
    }
}
