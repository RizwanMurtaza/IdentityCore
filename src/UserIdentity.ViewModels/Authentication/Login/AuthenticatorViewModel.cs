namespace UserIdentity.ViewModels.Authentication.Login
{
    public class AuthenticatorViewModel
    {
        public string SharedKey { get; set; }
        public string AuthenticatorUri { get; set; }
        public bool Success { get; set; }
    }
}