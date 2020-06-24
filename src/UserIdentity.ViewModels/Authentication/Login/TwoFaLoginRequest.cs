namespace UserIdentity.ViewModels.Authentication.Login
{
    public class TwoFaLoginRequest
    {
        public string Code { get; set; }
        public string Username { get; set; }
        public bool RememberMachine { get; set; }
        public bool RememberMe { get; set; }
    }
}