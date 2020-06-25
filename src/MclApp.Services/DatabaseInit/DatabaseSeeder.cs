//using System;
//using System.Threading.Tasks;
//using MclApp.Core.Domain;
//using MclApp.Data.Repository;

using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MclApp.Core.Domain;
using MclApp.Data.Repository;

namespace MclApp.Services.DatabaseInit
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        public  Guid UserId = new Guid("d66ae70b-1f2c-4ea4-a4d7-0644ad1053f3");
        private readonly IDbRepositoryPattern<ImprovementTask> _improvementTaskRepo;
        private readonly IDbRepositoryPattern<CyberScore> _cyberScoreRepo;
        private readonly IDbRepositoryPattern<CyberVulnerability> _cyberVulnerabilityRepo;
        private readonly IDbRepositoryPattern<UserTask> _userTasksRepo;
        private readonly IDbRepositoryPattern<Narrative> _narrativesRepo;
        private readonly IDbRepositoryPattern<UserExtendedInformation> _userExtendedInfoRepo;

        public DatabaseSeeder(IDbRepositoryPattern<ImprovementTask> securityTaskRepo,
            IDbRepositoryPattern<CyberScore> cyberScoreRepo,
            IDbRepositoryPattern<CyberVulnerability> cyberVulnerabilityRepo, IDbRepositoryPattern<UserTask> userTasksRepo, IDbRepositoryPattern<Narrative> narrativesRepo, IDbRepositoryPattern<UserExtendedInformation> userExtendedInfoRepo)
        {
            _improvementTaskRepo = securityTaskRepo;
            _cyberScoreRepo = cyberScoreRepo;
            _cyberVulnerabilityRepo = cyberVulnerabilityRepo;
            _userTasksRepo = userTasksRepo;
            _narrativesRepo = narrativesRepo;
            _userExtendedInfoRepo = userExtendedInfoRepo;
        }

        public bool InitializeDataBase()
        {
            InsertScores();
            InsertTasks();
            InsertVul();
            InsertUserTasks();
            InsertNarratives();
            AddExtendedInformation();
            return true;

        }


        private void InsertScores()
        {
            var allScoreForUser = _cyberScoreRepo.Table.Where(x => x.UserId == UserId);
          
            if(allScoreForUser.Any())
                return;

            var data = DummyData.ScoreData();
            foreach (var score in data)
            {
                _cyberScoreRepo.Insert(score);
            }
        }

        private void InsertTasks()
        {
            var allScoreForUser = _improvementTaskRepo.Table.Where(x => x.UserId == UserId);
            if (allScoreForUser.Any())
                return;

            var data = DummyData.GetTasksData();
            foreach (var task in data)
            {
                _improvementTaskRepo.Insert(task);
            }
        }

        private void InsertVul()
        {
            var allScoreForUser = _cyberVulnerabilityRepo.Table.Where(x => x.UserId == UserId);
            if (allScoreForUser.Any())
                return;

            var data = DummyData.GetVulnerabilities();
            foreach (var vul in data)
            {
                _cyberVulnerabilityRepo.Insert(vul);
            }
        }


        private void InsertUserTasks()
        {
            var allScoreForUser = _userTasksRepo.Table.Where(x => x.UserId == UserId);
            if (allScoreForUser.Any())
                return;

            var data = DummyData.GetUserTasksData();
            foreach (var vul in data)
            {
                _userTasksRepo.Insert(vul);
            }
        }

        private void InsertNarratives()
        {
            var narratives = _narrativesRepo.Table.Where(x => x.UserId == UserId);
            if (narratives.Any())
                return;
            var data = DummyData.GetNarratives();
            foreach (var vul in data)
            {
                _narrativesRepo.Insert(vul);
            }
        }

        private void AddExtendedInformation()
        {
            var narratives = _userExtendedInfoRepo.Table.Where(x => x.UserId == UserId);
            if (narratives.Any())
                return;

            var extendedInformation = new UserExtendedInformation()
            {
                CompanyName = "My New Company",
                CreatedDate = DateTime.Now,
                DomainNameForScan = "www.gmail.com",
                ExternalEndPointsToScan = "www.gmail.com/accounts",
                OfficeTenant = "Microsoft 365",
                ExternalWebsiteToScan = "www.facebook.com",
                UserId = UserId,
                LastModifiedDate = DateTime.Now
            };
           
                _userExtendedInfoRepo.Insert(extendedInformation);
           
        }

    }



}






