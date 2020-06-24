using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using UserIdentity.Core.Domain;
using UserIdentity.Core.Domain.Defaults;
using UserIdentity.Core.Domain.Group;
using UserIdentity.Core.Domain.Identity;
using UserIdentity.Services.AppManagement;
using UserIdentity.Services.UserManagement;
using UserIdentity.ViewModels.UserManagement.Users;

namespace UserIdentity.Services.DatabaseInit
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
        private readonly IUserServices _userServices;

        public DatabaseSeeder(IAppService appService, IAppGroupService groupService, IAppPermissionService permissionService, IAppUserService appUserService, IUserServices userServices)
        {
            _appService = appService;
            _groupService = groupService;
            _permissionService = permissionService;
            _appUserService = appUserService;
            _userServices = userServices;
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
                Name = SystemDefaultGroups.SuperAdmin.ToString(),
                Description = "Group Super Admin",
                ApplicationId = app.Object.Id
            };
            var admin = new AppGroup()
            {
                Name = SystemDefaultGroups.Admin.ToString(),
                Description = "Group for Admin",
                ApplicationId = app.Object.Id
            };
            var viewer = new AppGroup()
            {
                Name = SystemDefaultGroups.DefaultUser.ToString(),
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
                .RuleFor(o => o.EmailConfirmed, true)
                .RuleFor(o => o.ActiveLanguageId, 2)
             
                .RuleFor(o => o.ApplicationId, app.Object.Id)
                .RuleFor(o => o.Application, app.Object)
                .RuleFor(o => o.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(o => o.UserName, (f, usr) => usr.Email)
                .RuleFor(o => o.IsActive, f => true);

            var users = fakeUsers.Generate(20);
            var createdUsers = new List<AppUser>();
            foreach (var appUser in users)
            {
               // appUser.Group = new List<string>();
             //   appUser.Group.Add(SystemDefaultGroups.SuperAdmin.ToString());
                var user = await _appUserService.CreateUserAsync(appUser,"rizwan321");
                if (user.Success)
                { 
                    createdUsers.Add(user.Object);
                }
            }

            await InitalizeSuperAdminUser();
            return createdUsers;
        }

        public async Task<bool> InitalizeSuperAdminUser()
        {
            var app = await _appService.GetApplicationByName(DefaultApplication);
            var superAdmin = new CreateUserRequest();
            superAdmin.FirstName = "Rizwan";
            superAdmin.LastName = "Murtaza";
            superAdmin.Email = "rizwan.murtaza@live.com";
            superAdmin.Username = "rizwan.murtaza@live.com";
            superAdmin.Password = "rizwan321";
            superAdmin.ApplicationKey = app.Object.ApplicationKey.ToString();
            superAdmin.Group = new List<string>();
            superAdmin.Group.Add(SystemDefaultGroups.SuperAdmin.ToString());
            var result=  await _userServices.CreateAccount(superAdmin);
            return result.Success;
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

