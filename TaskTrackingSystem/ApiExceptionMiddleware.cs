using System.Net;
using System.Text.Json;
using TaskTrackingSystem.Application.Common.Exceptions;

namespace TaskTrackingSystem
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
         
        public ApiExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (ForbiddenAccessException ex)
            {
                await WriteErrorResponse(context, HttpStatusCode.Forbidden, "Access Denied", ex.Message);
            }
            catch (Exception ex)
            {
                await WriteErrorResponse(context, HttpStatusCode.InternalServerError, "Server Error", ex.Message);
            }
        }

        private static async Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, string error, string message)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var result = new
            {
                error,
                message,
                status = (int)statusCode,
            };

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await context.Response.WriteAsync(json);
        }
    }
}
