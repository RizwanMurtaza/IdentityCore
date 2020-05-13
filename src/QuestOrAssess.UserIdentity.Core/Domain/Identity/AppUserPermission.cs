using System;
using Microsoft.AspNetCore.Identity;

namespace QuestOrAssess.UserIdentity.Core.Domain.Identity
{
    public class AppUserPermission : IdentityUserRole<int> , IAuditableEntity
    {

        public int Id { get; set; }
        public virtual AppUser User { get; set; }
        public virtual AppPermission Permission { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}