using Domain.Exceptions;
using System.Net;

namespace Api.Middleware
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                var errorResponse = CreateErrorResponse(context, ex, ex.Message, HttpStatusCode.NotFound);

                await context.Response.WriteAsJsonAsync(errorResponse);

            }
            catch (Exception ex)
            {
                var errorResponse = CreateErrorResponse(context, ex, "An unhandled exception occurred.", HttpStatusCode.InternalServerError);

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }

        private object CreateErrorResponse(HttpContext context, Exception ex, string errorMessage, HttpStatusCode statusCode)
        {
            _logger.LogError(ex, errorMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                status = context.Response.StatusCode,
                message = errorMessage,
            };
            return errorResponse;
        }
    }
}
}
