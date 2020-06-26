using System;
using System.Collections.Generic;

namespace UserIdentity.ViewModels.Authentication.Claims
{
    public class TokenBreachUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string MclUserId { get; set; }
        public bool IsActive { get; set; }
        public string[] Roles { get; set; }
        public List<string> UserGroups { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public string NationalInsuranceNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        

    }
}
