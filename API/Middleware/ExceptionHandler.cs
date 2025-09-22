using System;

namespace API.Middleware;

public class ExceptionHandler
{
    private readonly ILogger<ExceptionHandler> logger;
    private readonly RequestDelegate next;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next)
    {
        this.logger = logger;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var errorId = Guid.NewGuid();
            logger.LogError(ex.Message, "An unhandled exception occurred. Error ID: {ErrorId}", errorId);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            
            var error = new
            {
                Id = errorId,
                ErrorMessage = "Something went wrong. Please try again later."
            };
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
