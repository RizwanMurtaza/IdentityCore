using System;
using System.Collections.Generic;
using System.Text;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class GroupUser : IAuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
