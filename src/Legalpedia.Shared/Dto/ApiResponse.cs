using System.Collections.Generic;

namespace Legalpedia.Shared.Dto
{
    public class ApiResponse<T>
    {
        public string message { get; set; }
        public string exceptionMessage { get; set; }
        public bool success { get; set; }
        public T result { get; set; }

        public Error error { get; set; }

        public ApiResponse(bool success, string message = "", T data = default(T))
        {
            this.success = success;
            this.message = message;
            result = data;
        }

        public ApiResponse() { }
    }

    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
        public string details { get; set; }
        public List<ValidationError> validationErrors { get; set; }
    }

    public class ValidationError
    {
        public string message { get; set; }
        public List<string> members { get; set; }
    }
}
