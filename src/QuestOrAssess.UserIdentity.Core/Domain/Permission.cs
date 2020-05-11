using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class Permission : IdentityRole<int> , IAuditableEntity
    {
        public int ApplicationId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        //public virtual ICollection<User> Users { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
