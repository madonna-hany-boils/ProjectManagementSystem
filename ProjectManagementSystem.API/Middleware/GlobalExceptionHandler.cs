using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ProjectManagementSystem.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Log the detailed exception into the server logs
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            // Structure a clean response using ProblemDetails or your Result Pattern format
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                IsSuccess = false,
                Data = (object?)null,
                ErrorMessage = "An internal server error occurred. Please try again later.",
                Detailed = exception.Message // You can remove this line in production for security reasons
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken);

            // Return true to indicate that this exception has been handled successfully
            return true;
        }

        ValueTask<bool> IExceptionHandler.TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
