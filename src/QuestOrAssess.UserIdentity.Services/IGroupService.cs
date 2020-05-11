using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Services
{
    public interface IGroupService
    {
        OutResult AddNewGroup(Group group);
        Group GetGroupById(int id);
    }
}