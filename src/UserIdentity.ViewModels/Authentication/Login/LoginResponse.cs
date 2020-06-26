namespace UserIdentity.ViewModels.Authentication.Login
{
    public partial class LoginResponse
    {
        public string AuthMessage { get; set; }
        public string AuthToken { get; set; }
        public bool IsAccountLocked { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool RequireTwoFactorAuthentication { get; set; }
        public ResponseUser ResponseUser { get; set; }
        public static LoginResponse Success(string token, ResponseUser user ,  string reason = "")
        {
            return new LoginResponse
            {
                IsAuthenticated = true,
                AuthToken= token,
                ResponseUser = user
            };
        }
        public static LoginResponse LockedOut(string reason = "")
        {
            return new LoginResponse { IsAccountLocked = true, IsAuthenticated = false };
        }
        public static LoginResponse Failure(string reason = "")
        {
            return new LoginResponse { IsAuthenticated = false, AuthMessage = reason};
        }
        public static LoginResponse TwoFactorAuthenticationEnabled(string reason = "")
        {
            return new LoginResponse() {IsAuthenticated = false, RequireTwoFactorAuthentication = true};
        }

    }
    public class ResponseUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string MclUserId { get; set; }
    }


}
