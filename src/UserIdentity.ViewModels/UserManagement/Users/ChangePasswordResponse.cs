namespace UserIdentity.ViewModels.UserManagement.Users
{
    public class ChangePasswordResponse
    {

        public bool Success { get; set; }
        public string Message { get; set; }

        public static ChangePasswordResponse Succeed(string message)
        {
            return new ChangePasswordResponse
            {
                Success = true,
                Message = message
            };
        }
        public static ChangePasswordResponse Fail(string reason)
        {
            return new ChangePasswordResponse { Success = false, Message = reason };
        }
    }
}