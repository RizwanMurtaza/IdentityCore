using System;
using Microsoft.AspNetCore.Identity;

namespace UserIdentity.Core.Domain.Identity
{
    public class LoginDetails : IdentityUserLogin<int>, IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}