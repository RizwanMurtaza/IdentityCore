using System;
using System.Collections.Generic;
using System.Text;

namespace MclApp.Core.Domain
{
    public class UserTask : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string TaskDescription { get; set; }
        public UserTaskStatus Status { get; set; }
        public virtual bool IsOverDue => this.Status == UserTaskStatus.No &&  DueDate.Date <= DateTime.Now.Date;

    }

    public enum UserTaskStatus
    {
        No = 0,
        Yes= 1
    }
}
