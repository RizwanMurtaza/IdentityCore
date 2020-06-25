using System;

namespace MclApp.Core.Domain
{
    //dpo.cyberscore with fields Id,score,date,target score.
    public class CyberScore : BaseEntity
    {
        public Guid UserId { get; set; }
        public double Score { get; set; }
        public DateTime Date { get; set; }
        public double TargetScore { get; set; }
        public virtual string ScoreString
        {
            get
            {
                if (this.Score < 2.5)
                    return "Very Poor";
                if (this.Score > 2.5 && this.Score <= 5.0)
                    return "Poor";
                if (this.Score > 5 && this.Score <= 7.5)
                    return "Average";
                if (this.Score > 7.5)
                    return "Excellent";
                return "Unknown";
            }
        }

    }
}
