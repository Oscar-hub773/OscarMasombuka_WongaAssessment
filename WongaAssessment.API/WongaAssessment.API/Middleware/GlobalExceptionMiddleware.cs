using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using WongaAssessment.API.Exceptions;
using WongaAssessment.API.Models.DTOs.Response;

namespace WongaAssessment.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;         
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";

                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

                var response = new ErrorResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = httpContext.Response.StatusCode,
                    Timestamp = DateTime.UtcNow
                };

                var result = JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}
