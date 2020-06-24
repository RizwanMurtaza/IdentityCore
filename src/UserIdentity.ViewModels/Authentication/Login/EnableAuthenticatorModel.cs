using System.Collections.Generic;

namespace UserIdentity.ViewModels.Authentication.Login
{
    public class Activate2FaAuthentication
    {
        public string Code { get; set; }
        public List<string> RecoveryKey { get; set; }
        public bool IsCodeValid { get; set; }
    }
}