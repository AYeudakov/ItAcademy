using System.Net;
using System.Text.Json;
using ItAcademy.Models.BaseModels;

namespace ItAcademy.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
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
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var message = exception switch
        {
            NullReferenceException => "Object was null",
            _ => "Internal Server Error from the custom middleware."
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(new Result(HttpStatusCode.InternalServerError, message)));
    }
}