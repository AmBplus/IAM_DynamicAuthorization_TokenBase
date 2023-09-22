using System.Globalization;
using Microsoft.AspNetCore.Http;
using Base.AspCore.Infrastructure.Settings;

namespace Base.AspCore.Infrastructure.Middlewares;

public class CultureCookieHandlerMiddleware : object
{
    public CultureCookieHandlerMiddleware
    (RequestDelegate next,
        ApplicationSettings applicationSettings)
    {
        Next = next;

        ApplicationSettings = applicationSettings;
    }

    private RequestDelegate Next { get; }

    private ApplicationSettings ApplicationSettings { get; }

    public async Task InvokeAsync
        (HttpContext httpContext)
    {
        // **************************************************
        var defaultCultureName =
            ApplicationSettings.CultureSettings.DefaultCultureName;

        var supportedCultureNames =
            ApplicationSettings.CultureSettings.SupportedCultureNames;
        // **************************************************

        // **************************************************
        var currentCultureName =
            GetCultureNameByCookie
            (httpContext,
                supportedCultureNames);

        if (currentCultureName == null) currentCultureName = defaultCultureName;

        SetCulture(currentCultureName);
        // **************************************************

        await Next(httpContext);
    }

    #region Static Member(s)

    public static readonly string CookieName = "Culture.Cookie";

    public static string? GetCultureNameByCookie
    (HttpContext httpContext,
        IList<string>? supportedCultureNames)
    {
        if (supportedCultureNames == null ||
            supportedCultureNames.Count == 0)
            return null;

        var cultureName =
            httpContext.Request.Cookies[CookieName];

        if (string.IsNullOrWhiteSpace(cultureName)) return null;

        if (supportedCultureNames == null ||
            supportedCultureNames.Contains(cultureName) == false)
            return null;

        return cultureName;
    }

    public static void SetCulture(string? cultureName)
    {
        if (string.IsNullOrWhiteSpace(cultureName) == false)
        {
            var cultureInfo =
                new CultureInfo(cultureName);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }

    public static void CreateCookie
        (HttpContext httpContext, string cultureName)
    {
        // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.cookieoptions?view=aspnetcore-6.0

        var cookieOptions =
            new CookieOptions
            {
                Path = "/", // Default: [null]

                // Domain: Gets or sets the domain to associate the cookie with.
                // نباید تغییر دهیم، خودش به طور
                // خودکار بر اساس دامنه سایت تنظیم می‌شود

                Secure = false, // Default: [false]

                // Secure: Gets or sets a value that indicates whether to transmit
                // the cookie using Secure Sockets Layer (SSL)--that is, over HTTPS only.

                HttpOnly = false, // Default: [false]

                // HttpOnly: Gets or sets a value that indicates
                // whether a cookie is accessible by client-side script.
                // [true] if a cookie must not be accessible by client-side script; otherwise, [false].

                IsEssential = false, // Default: [false]

                // Indicates if this cookie is essential for the application
                // to function correctly. If true then consent policy checks may be bypassed.

                MaxAge = null, // Default: [null]

                // MaxAge: Gets or sets the max-age for the cookie.

                Expires =
                    DateTimeOffset.UtcNow.AddYears(1), // Default: [null]

                // Expires: Gets or sets the expiration date and time for the cookie.

                SameSite =
                    SameSiteMode.Unspecified // Default: [Unspecified]

                // SameSite: Gets or sets the value for the SameSite attribute of the cookie.

                // The SameSiteMode representing the enforcement mode of the cookie:

                // Lax			1	Indicates the client should send the cookie with "same-site"
                //					requests, and with "cross-site" top-level navigations.
                // None			0	Indicates the client should disable same-site restrictions.
                // Strict		2	Indicates the client should only send the cookie with
                //					"same-site" requests.
                // Unspecified	-1	No SameSite field will be set, the client should
                //					follow its default cookie policy.
            };

        httpContext.Response.Cookies.Delete(CookieName);

        if (string.IsNullOrWhiteSpace(cultureName) == false)
            httpContext.Response.Cookies.Append
                (CookieName, cultureName, cookieOptions);
    }

    #endregion /Static Member(s)
}
