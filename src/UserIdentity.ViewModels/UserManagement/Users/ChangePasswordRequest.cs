using System;
using System.Collections.Generic;
using System.Text;

namespace UserIdentity.ViewModels.UserManagement.Users
{
    public class ChangePasswordRequest
    {

        public string CurrentPassword { get; set; }
        public string  Password { get; set; }
        public string  ConfirmPassword { get; set; }
    }
}
