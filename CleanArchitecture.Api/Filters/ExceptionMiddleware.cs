using CleanArchitecture.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (DomainValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            //return StatusCode(ex.StatusCode, new { message = ex.Message });
            await context.Response.WriteAsJsonAsync(new { message = ex.Message, StatusCode = StatusCodes.Status400BadRequest });
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
            };
            await context.Response.WriteAsJsonAsync(JsonSerializer.Serialize(response));
        }
    }
}
