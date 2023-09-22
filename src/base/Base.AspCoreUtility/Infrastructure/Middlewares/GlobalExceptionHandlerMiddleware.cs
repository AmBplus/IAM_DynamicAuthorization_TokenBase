using Microsoft.AspNetCore.Http;

namespace Base.AspCore.Infrastructure.Middlewares;

public class GlobalExceptionHandlerMiddleware : object
{
    public GlobalExceptionHandlerMiddleware
        (RequestDelegate next)
    {
        Next = next;
    }

    private RequestDelegate Next { get; }

    public async Task
        InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await Next(httpContext);
        }
        catch //(System.Exception ex)
        {
            // Log Error (ex)!

            httpContext.Response.Redirect
                ("/Errors/Error500", false);
        }
    }
}
