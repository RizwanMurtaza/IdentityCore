using System;
using AutoMapper;
using MclApp.Core.Domain;

namespace MclApp.ViewModelServices.ViewModels
{
    public class UserNarrativeViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }

    }

    public class UserNarrativeViewModelMapping : Profile
    {
        public UserNarrativeViewModelMapping()
        {
            CreateMap<Narrative, UserNarrativeViewModel>();
        }
    }

    public class UserExtendedInformationViewModel
    {
        public Guid UserId { get; set; }
        public string ExternalWebsiteToScan { get; set; }
        public string ExternalEndPointsToScan { get; set; }
        public string DomainNameForScan { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string OfficeTenant { get; set; }
        
        public string LastName { get; set; }
        public string Email { get; set; }
    }


    public class UserExtendedInformationViewModelMapping : Profile
    {
        public UserExtendedInformationViewModelMapping()
        {
            CreateMap<UserExtendedInformation, UserExtendedInformationViewModel>();
        }
    }




}
