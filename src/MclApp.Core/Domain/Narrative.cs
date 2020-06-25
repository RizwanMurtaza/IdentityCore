using System;
using System.Collections.Generic;
using System.Text;

namespace MclApp.Core.Domain
{
    public class Narrative : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual string Description { get; set; }
        

    }
}
