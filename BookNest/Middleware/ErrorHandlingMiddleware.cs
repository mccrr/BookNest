using BookNest.Utils;
using System.Security.Cryptography.X509Certificates;

namespace BookNest.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try
            {
                await _next(context);
            }
            catch (Exception ex) {
                Handle(context,ex);
            }
        }

        private static IBaseResponse Handle(HttpContext context, Exception ex) {
            return BaseResponse<object>
                .ErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
