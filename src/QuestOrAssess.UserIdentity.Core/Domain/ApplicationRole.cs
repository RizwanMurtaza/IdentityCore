using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class ApplicationRole : IdentityRole<int> 
    {
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
}
