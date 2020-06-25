using System;

namespace MclApp.Core.Domain
{
    public class UserExtendedInformation : BaseEntity
    {
        public Guid UserId { get; set; }
        public string ExternalWebsiteToScan { get; set; }
        public string ExternalEndPointsToScan { get; set; }
        public string DomainNameForScan { get; set; }
        public string OfficeTenant { get; set; }
        public string CompanyName { get; set; }
    }
}
