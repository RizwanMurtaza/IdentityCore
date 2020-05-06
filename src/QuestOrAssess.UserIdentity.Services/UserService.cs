using System;
using System.Threading.Tasks;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Services.Interfaces;

namespace QuestOrAssess.UserIdentity.Services
{
    public class UserService : IUserService
    {
        //public UserRepository()
        //    : this(new MasterDbContext())
        //{
        //    base.DisposeContext = true;
        //}

        //public UserRepository(System.Data.Entity.DbContext context)
        //    : base(context)
        //{
        //}
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
