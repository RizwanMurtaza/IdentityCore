namespace UserIdentity.ViewModels.Authentication.Login
{
    public class TwoFaLoginResponse
    {
        public bool IsCodeValid { get; set; }
        public LoginResponse LoginResponse { get; set; }


    }
}
