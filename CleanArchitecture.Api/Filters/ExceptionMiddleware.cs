using CleanArchitecture.Domain.Exceptions;
using System.Net;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Logando o erro para análise futura
        _logger.LogError(exception, "Ocorreu uma exceção inesperada.");

        // Definir o status code com base no tipo de exceção
        context.Response.StatusCode = exception switch
        {
            DomainValidationException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        // Resposta personalizada
        var response = new
        {
            message = exception.Message,
            exceptionType = exception.GetType().Name,
            details = exception.StackTrace // Evite enviar isso em produção para não expor detalhes sensíveis
        };

        // Retorna a resposta JSON
        return context.Response.WriteAsJsonAsync(response);
    }
}
