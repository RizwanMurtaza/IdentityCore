﻿using AutoMapper;

namespace MclApp.ViewModelServices.ViewModels
{
    public class ImprovementTasksViewModel
    {
     
        public string Id { get; set; }
        public string Date { get; set; }
        public string TaskDescription { get; set; }
        public string Status { get; set; }
    }

    public class ImprovementTasksViewModelMapping : Profile
    {
        public ImprovementTasksViewModelMapping()
        {
            CreateMap<Core.Domain.ImprovementTask, ImprovementTasksViewModel>()
                .ForMember(x => x.Date,
                    m => m.MapFrom(a => a.Date.ToShortDateString()));
        }
    }


   
}
