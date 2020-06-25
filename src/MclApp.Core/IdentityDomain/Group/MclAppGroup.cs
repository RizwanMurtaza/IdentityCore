using System;
using System.Collections.Generic;

namespace MclApp.Core.IdentityDomain.Group
{
    public class MclAppGroup : IAuditableEntity
    {
        public int ApplicationId { get; set; }
        public virtual MclApplication Application { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<MclGroupPermission> GroupPermissions { get; set; }
        public virtual ICollection<MclGroupUser> UsersInGroup { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}