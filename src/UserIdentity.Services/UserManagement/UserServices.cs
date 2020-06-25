using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core.IdentityDomain.Defaults;
using MclApp.Core.IdentityDomain.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using UserIdentity.Services.AppManagement;
using UserIdentity.Services.Email;
using UserIdentity.ViewModels.Authentication.Login;
using UserIdentity.ViewModels.UserManagement.Users;

namespace UserIdentity.Services.UserManagement
{
    public class UserServices : IUserServices
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _applicationUserManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IAppGroupService _groupService;
        private readonly IAppService _appService;

		public UserServices(UserManager<AppUser> applicationUserManager, IHostingEnvironment hostingEnvironment, IEmailService emailService, IAppGroupService groupService, IAppService appService)
        {
            _applicationUserManager = applicationUserManager;
            _hostingEnvironment = hostingEnvironment;
            _emailService = emailService;
            _groupService = groupService;
            _appService = appService;
        }
        public async Task<CreateUserResponse> CreateAccount(CreateUserRequest request)
		{
            var user = await _applicationUserManager.FindByEmailAsync(request.Username);

            var appId = await _appService.GetApplicationByKey(request.ApplicationKey);
            if (!appId.HasData)
            {
               return CreateUserResponse.Fail("Trying to create user from unknown source ");
            }
            if (user == null)
            {
                
				user = new AppUser()
				{
					Email = request.Username,
					UserName = request.Username,
					FirstName = request.FirstName,
					LastName = request.LastName,
					PhoneNumber = request.PhoneNumber,
                    ApplicationId = appId.Object.Id,
                    LockoutEnabled = true,
                    EmailConfirmed = true
				};
				var identityResult = await _applicationUserManager.CreateAsync(user,request.Password);

				if (!identityResult.Succeeded)
				{
				 return CreateUserResponse.Fail(identityResult.Errors.First().Description);
				}
            }
            else
            {
                return CreateUserResponse.Fail("Email already exist in our system");
            }

            if (!user.EmailConfirmed)
            {
                return CreateUserResponse.Fail("Account already exist, email address is not confirmed yet.");
            }

			//TODO Add user to group 

            var newlyCreatedUser = await _applicationUserManager.FindByEmailAsync(request.Email);
            var applicationGroups = await _groupService.GetApplicationGroups(appId.Object.Id);
            if (request.Group == null || !request.Group.Any())
            {
                if (applicationGroups!= null && applicationGroups.Any())
                {
                    request.Group = new List<string>();
                    var defaultGroup = applicationGroups.First(x => x.Name.Equals(SystemDefaultGroups.DefaultUser.ToString()));
                    if (defaultGroup != null)
                    {
                        await _groupService.AddUserToGroup(newlyCreatedUser, defaultGroup);
                    }
                }
                
            }
            else if (request.Group != null && request.Group.Any())
            {
                var userGroups =  applicationGroups.Where(x => request.Group.Contains(x.Name));
                if (userGroups.Any())
                {
                    foreach (var group in userGroups)
                    {
                        await _groupService.AddUserToGroup(newlyCreatedUser, group);
                    }
                }
               
            }

            if (user.EmailConfirmed) return CreateUserResponse.Succeed("Account created successfully");


            var token = await _applicationUserManager.GenerateEmailConfirmationTokenAsync(newlyCreatedUser);
            var url = $"{request.ConfirmationUrl}?token={EncodeToken(token)}&username={request.Username}";
            var name = newlyCreatedUser.FullName;

            var filePath = _hostingEnvironment.ContentRootPath;

            var emailTemplate = System.IO.Path.Combine(filePath, "Template", "Emails", "CreateAccount.html");
            var emailFile = await System.IO.File.ReadAllTextAsync(emailTemplate);
            var emailContent = ReplaceEmail(emailFile,
                new Dictionary<string, string> {["name"] = name, ["url"] = url});

            var wrapperTemplate = System.IO.Path.Combine(filePath, "Template", "Emails", "_Email_Wrapper.html");
            var wrapperFile = await System.IO.File.ReadAllTextAsync(wrapperTemplate);
            var result = ReplaceEmail(wrapperFile,
                new Dictionary<string, string> {["emailContent"] = emailContent});


            var emailMessage = new EmailMessage
            {
                To = request.Username,
                Subject = "FindYourData.io Account Creation",
                HtmlContent = result
            };
            await _emailService.SendEmailAsync(emailMessage);

            return  CreateUserResponse.Succeed("Account confirmation email sent, please check your email");
        }
        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _applicationUserManager.FindByEmailAsync(request.Username);

            if (user == null)
            {
                return ForgotPasswordResponse.Fail("Our system don't recognize your email.");
            }
            try
            { 
                
                var token = await _applicationUserManager.GeneratePasswordResetTokenAsync(user);

            var url = $"{request.ConfirmationUrl}?token={EncodeToken(token)}&username={request.Username}";

            var filePath = _hostingEnvironment.ContentRootPath;

            var emailTemplate = System.IO.Path.Combine(filePath, "Template", "Emails", "ForgotPassword.html");
            var emailFile = System.IO.File.ReadAllText(emailTemplate);
            var emailContent = ReplaceEmail(emailFile, new Dictionary<string, string> { ["url"] = url });

            var wrapperTemplate = System.IO.Path.Combine(filePath, "Template", "Emails", "_Email_Wrapper.html");
            var wrapperFile = System.IO.File.ReadAllText(wrapperTemplate);
            var result = ReplaceEmail(wrapperFile, new Dictionary<string, string> { ["emailContent"] = emailContent });

            var emailMessage = new EmailMessage
            {
                To = request.Username,
                Subject = "FindYourData.io Password Reset",
                HtmlContent = result
            };
  
                await _emailService.SendEmailAsync(emailMessage);
            }
            catch (Exception e)
            {
                return ForgotPasswordResponse.Fail("Failed to send email, please try later!");
            }

            return ForgotPasswordResponse.Succeed();
            
        }
        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return ChangePasswordResponse.Fail("Error while changing your password");


                throw new Exception("Can only change password of the logged in user");
            }
            var user = await _applicationUserManager.FindByEmailAsync(email);
            var passwordChanged = await _applicationUserManager.ChangePasswordAsync(user, request.CurrentPassword, request.Password);

            if (passwordChanged.Succeeded)
            {
                return ChangePasswordResponse.Succeed("Password changed successfully");
            }

            return ChangePasswordResponse.Fail("Fail to change password please try again later");
        }


        private string EncodeToken(string token)
        {
            return token.Replace('+', '-');
        }
        private string ReplaceEmail(string email, Dictionary<string, string> replacements)
        {
            foreach (var replacement in replacements)
            {
                email = email.Replace($"[{replacement.Key}]", replacement.Value);
            }
            return email;
        }
	}
}
