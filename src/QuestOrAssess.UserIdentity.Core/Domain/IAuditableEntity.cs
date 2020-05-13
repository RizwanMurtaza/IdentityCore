using System;
using System.Collections.Generic;
using System.Text;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public interface IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    } 
}
