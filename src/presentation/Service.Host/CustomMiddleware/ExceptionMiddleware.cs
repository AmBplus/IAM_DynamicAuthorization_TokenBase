namespace AccessManagement.Middleware;
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
            // Log error
            
            Console.WriteLine(ex.ToString());
            // Return 500 Internal Server Error
            await _next(httpContext);
        }
    }
}