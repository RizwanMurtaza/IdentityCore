using System;
using System.Collections.Generic;
using MclApp.Core.IdentityDomain.Group;
using Microsoft.AspNetCore.Identity;

namespace MclApp.Core.IdentityDomain.Identity
{
    public class MclAppUser: IdentityUser<int> , IAuditableEntity
    {

        public int MclApplicationId { get; set; }
        public Guid MclUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => string.Concat(FirstName, " ", LastName);

        public DateTime? LastLoginTime { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public bool IsActive { get; set; } 

        public int ActiveLanguageId { get; set; }
        
        public bool IsPrimary { get; set; }

        public virtual ICollection<MclAppUserPermission> Permission { get; set; }

        public virtual ICollection<MclGroupUser> Groups { get; set; }
        public virtual MclApplication Application { get; set; }


        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}