using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using QuestOrAssess.UserIdentity.Core.Domain;
using QuestOrAssess.UserIdentity.Core.Domain.Group;
using QuestOrAssess.UserIdentity.Core.Domain.Identity;
using QuestOrAssess.UserIdentity.Services.AppManagement;
using QuestOrAssess.UserIdentity.Services.UserManagement;

namespace QuestOrAssess.UserIdentity.Services.DatabaseInit
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        public string DefaultApplication = "QuizSystem";
        public string SuperAdmin = "Super Admin";
        public string Admin = "Admin";
        public string Viewer = "Viewer";
        private readonly IAppService _appService;
        private readonly IAppGroupService _groupService;
        private readonly IAppPermissionService _permissionService;
        private readonly IAppUserService _appUserService;

        public DatabaseSeeder(IAppService appService, IAppGroupService groupService, IAppPermissionService permissionService, IAppUserService appUserService)
        {
            _appService = appService;
            _groupService = groupService;
            _permissionService = permissionService;
            _appUserService = appUserService;
        }

        public async Task<bool> InitializeDataBase()
        {
            var appCreated = await InitializeApplication();
            var groupCreated = false;
            if (appCreated)
            {
                groupCreated = await InitializeGroup();
            }

            var permissionCreated = false;
            if (groupCreated)
            {
                permissionCreated = await InitializePermissions();
            }

            if (permissionCreated)
            {
                var users = await InitializeUsers();
            }

            return permissionCreated;
        }
        private async Task<bool> InitializeApplication()
        {
            var app = new Application()
            {
                ApplicationKey = Guid.NewGuid(),
                Description = DefaultApplication,
            };
            var result =  await _appService.AddApplication(app);
            return result.IsValid;

        }
        private async Task<bool> InitializeGroup()
        {
            var app = await _appService.GetApplicationByName(DefaultApplication);
            if (!app.HasData)
            {
                return false;
            }
            var superAdmin = new AppGroup()
            {
                Name = SuperAdmin,
                Description = "Group for Super Admin",
                ApplicationId = app.Object.Id
            };
            var admin = new AppGroup()
            {
                Name = Admin,
                Description = "Group for Admin",
                ApplicationId = app.Object.Id
            };
            var viewer = new AppGroup()
            {
                Name = Viewer,
                Description = "Group for Viewer",
                ApplicationId = app.Object.Id
            };

            await _groupService.AddNewGroup(superAdmin);
            await _groupService.AddNewGroup(admin);
            await _groupService.AddNewGroup(viewer);
            
            return true;
        }
        private async Task<List<AppUser>> InitializeUsers()
        {

            var app = await _appService.GetApplicationByName(DefaultApplication);
            if (!app.HasData)
            {
             return new List<AppUser>();
            }
            var fakeUsers = new Faker<AppUser>()
                .RuleFor(o => o.UpdatedAt, f => f.Date.Recent(100))
                .RuleFor(o => o.PhoneNumber, f => f.Person.Phone)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.ActiveLanguageId, 2)
                .RuleFor(o => o.UserName, f => f.Name.FirstName())
                .RuleFor(o => o.PasswordHash, "rizwan321")
                .RuleFor(o => o.ApplicationId, app.Object.Id)
                .RuleFor(o => o.Application, app.Object)
                .RuleFor(o => o.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(o => o.IsActive, f => true);

            var users = fakeUsers.Generate(100);
            var createdUsers = new List<AppUser>();
            foreach (var appUser in users)
            {
                var user = await _appUserService.AddUserAsync(appUser);
                if (user.Success)
                { 
                    createdUsers.Add(user.Object);
                }
            }

            return createdUsers;
        }

        private async Task<bool> InitializePermissions()
        {
            foreach (var permissionName in Enum.GetNames(typeof(AppPermissions)))
            {
                await _permissionService.AddPermission(permissionName, permissionName);
            }
            return true;
        }


    }

}
