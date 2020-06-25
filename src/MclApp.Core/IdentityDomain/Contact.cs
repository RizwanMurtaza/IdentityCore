using System;
using MclApp.Core.IdentityDomain.Identity;

namespace MclApp.Core.IdentityDomain
{
    public class Contact : IAuditableEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => string.Concat(FirstName, " ", LastName);
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public bool IsPrimary { get; set; }
        public virtual MclAppUser User { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
