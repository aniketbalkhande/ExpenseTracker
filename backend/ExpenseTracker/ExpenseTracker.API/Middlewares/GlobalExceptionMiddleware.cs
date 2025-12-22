using System.Net;
using System.Text.Json;

namespace ExpenseTracker.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = GetStatusCode(ex);

            var response = new
            {
                context.Response.StatusCode,
                ex.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception ex)
        {
            switch (ex)
            {
                case UnauthorizedAccessException:
                    return (int)HttpStatusCode.Unauthorized;

                case KeyNotFoundException:
                    return (int)HttpStatusCode.InternalServerError;

                default:
                    return (int)StatusCodes.Status500InternalServerError;
            }
        }
    }
}
