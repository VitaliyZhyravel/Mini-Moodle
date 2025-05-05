

namespace Mini_Moodle.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
           await _next(httpContext);
        }
        catch (Exception ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            if (ex.InnerException != null)
            {
                logger.LogError("{ExceptionType}{Message}",ex.GetType().ToString(), ex.InnerException.Message);
                await httpContext.Response.WriteAsJsonAsync($"{ex.Message} {ex.InnerException.Message}");
            }
            else 
            {
                logger.LogError("{ExceptionType}{Message}", ex.GetType().ToString(), ex.Message);
                await httpContext.Response.WriteAsJsonAsync("An unexpected error occurred");
            }
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseExeptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
