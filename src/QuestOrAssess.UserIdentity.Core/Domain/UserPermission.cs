using System;
using Microsoft.AspNetCore.Identity;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class UserPermission : IdentityUserRole<int> , IAuditableEntity
    {

        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Permission Permission { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}