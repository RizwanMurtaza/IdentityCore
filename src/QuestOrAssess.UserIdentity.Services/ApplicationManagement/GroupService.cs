using System.Linq;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Data.Repository;

namespace QuestOrAssess.UserIdentity.Services.ApplicationManagement
{
   public class GroupService : IGroupService
   {
       private readonly IDbRepositoryPattern<Group> _groupRepository;

        public GroupService(IDbRepositoryPattern<Group>group, IDbRepositoryPattern<Application> applicationRepository)
        {
            _groupRepository = group;
        }

       public OutResult AddNewGroup(Group group)
       {
           return _groupRepository.Insert(group);
       }

       public Group GetGroupById(int id)
       {
           var group = _groupRepository.Table.Where(x => x.Id == id);
           return @group.Any() ? @group.First() : null;
       }

   }
}
