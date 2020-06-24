using System.Text;

namespace UserIdentity.ViewModels.Authentication.Login
{
    public class ForgotPasswordResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static ForgotPasswordResponse Fail(string reason)
        {
            return new ForgotPasswordResponse { Success = false, Message = reason };
        }
        public static ForgotPasswordResponse Succeed(string message ="")
        {
            return new ForgotPasswordResponse
            {
                Success = true,
                Message = "Password reset email sent."
                
            };
        }
    }
}
