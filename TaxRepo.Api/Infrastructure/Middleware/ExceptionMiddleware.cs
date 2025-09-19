using System.Net;
using System.Text.Json;
using TaxRepo.Application.Exceptions;
using TaxRepo.Infrastructure.Exceptions;

namespace TaxRepo.Api.Infrastructure.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var (status, message) = ex switch
                {
                    EntityNotFoundException => (HttpStatusCode.NotFound, ex.Message),
                    InvalidDateRangeException => (HttpStatusCode.BadRequest, ex.Message),
                    ArgumentException or FormatException => (HttpStatusCode.BadRequest, ex.Message),
                    UnauthorizedAccessException => (HttpStatusCode.Forbidden, "Forbidden."),
                    _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
                };

                context.Response.StatusCode = (int)status;
                context.Response.ContentType = "application/json";

                var payload = new
                {
                    error = message,
                    status = (int)status,
                    traceId = context.TraceIdentifier
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(payload, JsonOptions));
            }
        }
    }
}
