using System;
using System.Collections.Generic;

namespace UserIdentity.Core.Domain.Group
{
    public class AppGroup : IAuditableEntity
    {
        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
        public virtual ICollection<GroupUser> UsersInGroup { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}