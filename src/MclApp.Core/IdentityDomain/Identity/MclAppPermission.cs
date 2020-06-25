using System;
using System.Collections.Generic;
using MclApp.Core.IdentityDomain.Group;
using Microsoft.AspNetCore.Identity;

namespace MclApp.Core.IdentityDomain.Identity
{
    public class MclAppPermission : IdentityRole<int> , IAuditableEntity
    {
        public int MclApplicationId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<MclGroupPermission> GroupPermissions { get; set; }
        public virtual ICollection<MclAppUserPermission> UserPermissions { get; set; }
        //public virtual ICollection<User> Users { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
