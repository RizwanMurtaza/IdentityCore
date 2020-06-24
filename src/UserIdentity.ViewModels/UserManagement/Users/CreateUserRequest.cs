using System;
using System.Collections.Generic;

namespace UserIdentity.ViewModels.UserManagement.Users
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ConfirmationUrl { get; set; }
        public bool CanLogIn { get; set; }
        public  List<string> Group { get; set; }
        public string ApplicationKey { get; set; }

    }
}
