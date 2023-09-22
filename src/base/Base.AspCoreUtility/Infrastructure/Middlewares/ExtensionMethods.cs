using Microsoft.AspNetCore.Builder;
using Base.AspCore.Infrastructure.Middlewares;

namespace Base.AspCore.Infrastructure.Middlewares;

public static class ExtensionMethods
{
    public static IApplicationBuilder
        UseCultureCookie(this IApplicationBuilder app)

    {
        // UseMiddleware -> //using Microsoft.AspNetCore.Builder;
        return app.UseMiddleware<CultureCookieHandlerMiddleware>();
    }

    public static IApplicationBuilder
        UseGlobalException(this IApplicationBuilder app)
    {
        // UseMiddleware -> //using Microsoft.AspNetCore.Builder;
        return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }

    public static IApplicationBuilder
        UseActivationKeys(this IApplicationBuilder app)
    {
        // UseMiddleware -> //using Microsoft.AspNetCore.Builder;
        return app.UseMiddleware<ActivationKeysHandlerMiddleware>();
    }
}
