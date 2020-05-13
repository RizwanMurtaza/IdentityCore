using System;
using QuestOrAssess.UserIdentity.Core.Domain.Identity;

namespace QuestOrAssess.UserIdentity.Core.Domain.Group
{
    public class GroupPermission : IAuditableEntity
    {
        public int Id { get; set; }

        public int PermissionId { get; set; }
        public int GroupId { get; set; }
        public virtual AppPermission Permission { get; set; }
        public virtual AppGroup Group { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
