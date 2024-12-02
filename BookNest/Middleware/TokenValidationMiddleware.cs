

using BookNest.Utils;

namespace BookNest.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenValidationMiddleware> _logger;

        public TokenValidationMiddleware(RequestDelegate next,
            ILogger<TokenValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {

            var excludedPaths = new[] { "/api/auth/login", "/api/auth/signup", "/api/auth/refreshtoken", "/auth/register", "/swagger", "/api/users/" };

            // Check if the current path is excluded
            var path = context.Request.Path.Value;
            if (excludedPaths.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context); // Skip middleware and proceed to the next
                return;
            }
            // Extract the Authorization header
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Split(" ").Last();

                using (var scope = serviceProvider.CreateScope())
                {
                    var tokenService = scope.ServiceProvider.GetService<TokenService>();
                    // Validate and extract the token
                    var principal = tokenService.ValidateAccessToken(token);
                    if (principal != null)
                    {
                        context.User = principal;
                    }
                    else
                    {
                        _logger.LogWarning("Invalid token.");
                        throw new UnauthorizedException("Invalid token.");
                    }
                }
            }
            else
            {
                _logger.LogWarning("Authorization header missing or malformed.");
                throw new UnauthorizedException("Invalid token.");
            }

            await _next(context);
        }
    }

}
