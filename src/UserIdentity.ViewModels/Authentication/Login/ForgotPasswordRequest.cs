namespace UserIdentity.ViewModels.Authentication.Login
{
    public class ForgotPasswordRequest
    {
        public string Username { get; set; }
        public string ConfirmationUrl { get; set; }
    }
}