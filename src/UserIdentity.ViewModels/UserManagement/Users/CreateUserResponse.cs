namespace UserIdentity.ViewModels.UserManagement.Users
{
    public class CreateUserResponse
    {
        public static CreateUserResponse Succeed(string message)
        {
            return new CreateUserResponse
            {
                Success = true,
                Message = message
            };
        }
        public static CreateUserResponse Fail(string reason)
        {
            return new CreateUserResponse { Success = false, Message = reason };
        }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}
