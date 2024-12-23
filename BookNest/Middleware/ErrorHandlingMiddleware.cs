using BookNest.Utils;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

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
                await Handle(context,ex);
            }
        }

        private static async Task Handle(HttpContext context, Exception ex) {
            string message;
            HttpStatusCode statusCode;
            if (ex is CustomException customException)
            {
                statusCode = customException.ErrorCode;
                message = customException.Message;
            }
            else
            {
                message = "An unexpected error occured.";
                statusCode = HttpStatusCode.InternalServerError;
            }
            var response =  BaseResponse<object>
                .ErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            var responseText = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(responseText);
        }
    }
}
