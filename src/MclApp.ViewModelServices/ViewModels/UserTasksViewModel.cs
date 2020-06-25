using AutoMapper;

namespace MclApp.ViewModelServices.ViewModels
{
    public class UserTasksViewModel
    {

        public string Id { get; set; }
        public string Date { get; set; }
        public string TaskDescription { get; set; }
        public string Status { get; set; }
        public string DueDate { get; set; }
        public bool IsOverDue { get; set; }
      
    }

    public class UserTasksViewModelViewModelMapping : Profile
    {
        public UserTasksViewModelViewModelMapping()
        {
            CreateMap<Core.Domain.UserTask, UserTasksViewModel>()
                .ForMember(x => x.DueDate,
                    m => m.MapFrom(a => a.DueDate.ToShortDateString()))
                .ForMember(x => x.Date,
                    m => m.MapFrom(a => a.Date.ToShortDateString()));
        }
    }
}