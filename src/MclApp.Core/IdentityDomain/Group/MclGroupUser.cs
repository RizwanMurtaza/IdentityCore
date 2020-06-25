﻿using System;
using MclApp.Core.IdentityDomain.Identity;

namespace MclApp.Core.IdentityDomain.Group
{
    public class MclGroupUser : IAuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public virtual MclAppUser User { get; set; }
        public virtual MclAppGroup Group { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}