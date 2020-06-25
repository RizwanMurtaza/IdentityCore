using System;
using Microsoft.AspNetCore.Identity;

namespace MclApp.Core.IdentityDomain.Identity
{
    public class MclAppUserPermission : IdentityUserRole<int> , IAuditableEntity
    {

        public int Id { get; set; }
        public virtual MclAppUser User { get; set; }
        public virtual MclAppPermission Permission { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}