using System;

namespace MclApp.Core.Domain
{
    public class ImprovementTask : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string TaskDescription { get; set; }
        public ImprovementTaskStatus Status { get; set; }
    }
}
