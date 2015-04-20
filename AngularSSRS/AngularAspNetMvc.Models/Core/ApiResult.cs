
namespace AngularAspNetMvc.Models.Core
{
    public class ApiResult<T>
    {
        public ApiResult(T result, bool success = true, string errorMessage = "")
        {
            Result = result;
            Success = success;
            ErrorMessage = errorMessage;
        }

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
