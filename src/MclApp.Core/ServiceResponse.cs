namespace MclApp.Core
{
    public class ServiceResponse<T>
    {
        public T Object;
        public bool Success { get; set; }
        public bool HasData { get; set; }
        public string Error { get; set; }
        public string Messages { get; set; }
        public ServiceResponse<T> SuccessResponse(T entity)
        {
            return new ServiceResponse<T>()
            {
                Object = entity,
                HasData = true,
                Success = true,
            };
        }
        public ServiceResponse<T> SuccessResponseMessage(T entity, string message)
        {
            return new ServiceResponse<T>()
            {
                Object = entity,
                HasData = true,
                Success = true,
                Messages = message
            };
        }
        public ServiceResponse<T> SuccessWithNoResponse()
        {
            return new ServiceResponse<T>()
            {
                HasData = false,
                Success = true,
            };
        }

        public ServiceResponse<T> FailedResponse(string errorMessage)
        {
            return new ServiceResponse<T>()
            {
                HasData = false,
                Success = false,
                Error = errorMessage
            };
        }


    }
}