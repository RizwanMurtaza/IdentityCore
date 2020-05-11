using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class User: IdentityUser<int> , IAuditableEntity
    {
        public int ApplicationId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => string.Concat(FirstName, " ", LastName);

        public DateTime? LastLoginTime { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public bool IsActive { get; set; } 

        public int ActiveLanguageId { get; set; }
        
        public bool IsPrimary { get; set; }

        public virtual ICollection<UserPermission> Permission { get; set; }

        public virtual ICollection<GroupUser> Groups { get; set; }
        public virtual Application Application { get; set; }


        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}