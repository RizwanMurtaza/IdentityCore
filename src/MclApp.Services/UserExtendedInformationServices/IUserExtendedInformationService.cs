using System;
using System.Threading.Tasks;
using MclApp.Core.Domain;

namespace MclApp.Services.UserExtendedInformationServices
{
    public interface IUserExtendedInformationService
    {
        Task<UserExtendedInformation> GetUserExtendedInformation(Guid userId);
        Task<bool> UpdateExtendedInformation(UserExtendedInformation info);
    }
}