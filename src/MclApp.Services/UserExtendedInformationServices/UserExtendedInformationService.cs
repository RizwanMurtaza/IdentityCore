using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core.Domain;
using MclApp.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MclApp.Services.UserExtendedInformationServices
{
    public class UserExtendedInformationService : IUserExtendedInformationService
    {
        private readonly IDbRepositoryPattern<UserExtendedInformation> _userExtendedInformationRepo;

        public UserExtendedInformationService(IDbRepositoryPattern<UserExtendedInformation> taskRepository)
        {
            _userExtendedInformationRepo = taskRepository;
        }
        public async Task<UserExtendedInformation> GetUserExtendedInformation(Guid userId)
        {
            var result = await _userExtendedInformationRepo.Table.Where(x => x.UserId == userId).ToListAsync();
            if (result.Any())
            {
                return result.First();
            }
            return new UserExtendedInformation();
        }
       public async Task<bool> UpdateExtendedInformation(UserExtendedInformation info)
       {
           var result = await _userExtendedInformationRepo.Update(info);
           return result.IsValid ;
        }
    }
}
