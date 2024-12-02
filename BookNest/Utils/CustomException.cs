using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;

namespace BookNest.Utils
{
    public class CustomException : Exception
    {
        public HttpStatusCode ErrorCode { get; set; } = HttpStatusCode.InternalServerError;
        public CustomException(string message) : base(message) { }
        public CustomException(HttpStatusCode errorCode, string message) : base(message) {
            ErrorCode = errorCode;
        }
    }

    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string message) : base( HttpStatusCode.Forbidden,message) { }
    }

    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message) { }
    }

    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message) { }
    }

    public class ValidationException : CustomException
    {
        public ValidationException(string message) : base(HttpStatusCode.BadRequest, message) { }
    }
}
