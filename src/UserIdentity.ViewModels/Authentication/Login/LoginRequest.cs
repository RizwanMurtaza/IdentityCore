namespace UserIdentity.ViewModels.Authentication.Login
{
    public class LoginRequest
    {
        public string ApplicationKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }



    }
}
