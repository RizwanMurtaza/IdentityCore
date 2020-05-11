using System.Linq;
using QuestOrAssess.UserIdentity.Core;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Data.Repository;

namespace QuestOrAssess.UserIdentity.Services.ApplicationManagement
{
    public class ApplicationService
    {
        private readonly IDbRepositoryPattern<Application> _applicationRepository;
        public ServiceResponse<Application> ReturnObject { get; set; }
        public ApplicationService(IDbRepositoryPattern<Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public ServiceResponse<Application> GetApplicationById(int id)
        {
            var application = GetById(id);
            return application.Any() ? ReturnObject.SuccessResponse(application.First()) : ReturnObject.SuccessWithNoResponse();
        }
        public OutResult AddApplication(Application app)
        {
            return _applicationRepository.Insert(app);
        }
        public OutResult UpdateApplication(Application app)
        {
            return _applicationRepository.Update(app);
        }
        public OutResult DeleteApplication(int id)
        {
            var app = GetById(id);
            if (!app.Any()) return OutResult.Error_TryingToDeleteNull();
            _applicationRepository.Delete(app);
            return OutResult.Success_Deleted();

        }



        private IQueryable<Application> GetById(int id)
        {
            return _applicationRepository.Table.Where(x => x.Id == id);

        }
    }
}
