using System;
using System.Threading.Tasks;
using AutoMapper;
using MclApp.Services.UserExtendedInformationServices;
using MclApp.ViewModelServices.ViewModels;

namespace MclApp.ViewModelServices
{
    public interface IUserExtendedInformationViewModelService
    {
        Task<UserExtendedInformationViewModel> GetUserExtendedInformation(string userId);
    }

    public class UserExtendedInformationViewModelService : IUserExtendedInformationViewModelService
    {

        private readonly IUserExtendedInformationService _userExtendedInformationService;
        private readonly IMapper _mapper;

        public UserExtendedInformationViewModelService(IUserExtendedInformationService userExtendedInformationService, IMapper mapper)
        {
            _userExtendedInformationService = userExtendedInformationService;
            _mapper = mapper;
        }

        public async Task<UserExtendedInformationViewModel> GetUserExtendedInformation(string userId)
        {
            Guid.TryParse(userId, out var id);
            var allTasks = await _userExtendedInformationService.GetUserExtendedInformation(id);
            var modelToReturn = _mapper.Map<UserExtendedInformationViewModel>(allTasks);
            return modelToReturn;
        }











    }
}
