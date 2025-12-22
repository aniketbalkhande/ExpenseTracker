using System.Diagnostics;

namespace ExpenseTracker.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Log incoming request
                _logger.LogInformation(
                    "Incoming Request: {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path
                );

                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                // Log outgoing response
                _logger.LogInformation(
                    "Outgoing Response: {StatusCode} | {Method} {Path} | Duration: {ElapsedMilliseconds}ms",
                    context.Response.StatusCode,
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds
                );
            }
        }
    }
}
