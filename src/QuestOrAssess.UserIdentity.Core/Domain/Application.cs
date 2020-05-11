using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class Application :IAuditableEntity
    {
        public int Id { get; set; }
        public Guid ApplicationKey { get; set; }
        public string Description { get; set; }
        public ICollection<Group> ApplicationGroup { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
