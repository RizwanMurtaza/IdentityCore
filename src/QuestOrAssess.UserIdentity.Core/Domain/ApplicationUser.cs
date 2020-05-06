using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class ApplicationUser: IdentityUser<int> 
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => string.Concat(FirstName, " ", LastName);

        public DateTime? LastLoginTime { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public bool IsActive { get; set; } 

        public int ActiveLanguageId { get; set; }
        
        public bool IsPrimary { get; set; }

        public byte[] ProfilePicture { get; set; }


        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationUserClaim> UserClaims { get; set; }


    }
}