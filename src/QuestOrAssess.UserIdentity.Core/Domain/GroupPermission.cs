using System;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class GroupPermission : IAuditableEntity
    {
        public int Id { get; set; }

        public int PermissionId { get; set; }
        public int GroupId { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual Group Group { get; set; }



        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
