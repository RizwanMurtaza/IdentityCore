
using System;
using Microsoft.AspNetCore.Identity;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class LoginDetails : IdentityUserLogin<int>, IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}