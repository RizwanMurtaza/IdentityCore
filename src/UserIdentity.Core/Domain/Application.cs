using System;
using System.Collections.Generic;
using UserIdentity.Core.Domain.Group;
using UserIdentity.Core.Domain.Identity;

namespace UserIdentity.Core.Domain
{
    public class Application : IAuditableEntity
    {
        public int Id { get; set; }
        public Guid ApplicationKey { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
       
        public ICollection<AppGroup> ApplicationGroup { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
