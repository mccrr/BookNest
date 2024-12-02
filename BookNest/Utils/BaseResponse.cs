using System.Net;

namespace BookNest.Utils
{
    public class BaseResponse<T> : IBaseResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode Status { get; set; }
        public T? Body { get; set; }

        object IBaseResponse.Body => Body;

        public BaseResponse() { }

        public BaseResponse(bool success,HttpStatusCode status, string message, T body)
        {
            Success = success;
            Message = message;
            Body = body;
            Status = status;
        }

        // Factory method for a success response
        public static BaseResponse<T> SuccessResponse(T body)
        {
            return new BaseResponse<T>(true,HttpStatusCode.OK, null, body);
        }

        // Factory method for an error response
        public static BaseResponse<T> ErrorResponse(HttpStatusCode status, string message)
        {
            return new BaseResponse<T>(false,HttpStatusCode.BadRequest, message, default(T)); // Body will be null or default(T) for errors
        }
    }
}
