using Microsoft.AspNetCore.Http;

namespace Base.AspCore.Infrastructure;

public static class CookiesManger
{
    public static void AddCookie(HttpContext context, string token, string value)
    {
        context.Response.Cookies.Append(token, value, GetCookieOptions(context));
    }

    public static bool CookieIsExist(HttpContext context, string token)
    {
        return context.Request.Cookies.ContainsKey(token);
    }

    public static string GetCookie(HttpContext context, string token)
    {
        string cookieValue;
        if (!context.Request.Cookies.TryGetValue(token, out cookieValue)) return null;
        return cookieValue;
    }

    public static void RemoveCookie(HttpContext context, string token)
    {
        if (context.Request.Cookies.ContainsKey(token)) context.Response.Cookies.Delete(token);
    }


    public static Guid GetDeviceIdFromCookie(HttpContext context)
    {
        var browserId = GetCookie(context, "DeviceId");
        if (browserId == null)
        {
            var value = Guid.NewGuid().ToString();
            AddCookie(context, "DeviceId", value);
            browserId = value;
        }

        Guid guidBowser;
        Guid.TryParse(browserId, out guidBowser);
        return guidBowser;
    }

    private static CookieOptions GetCookieOptions(HttpContext context)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
            Secure = context.Request.IsHttps,
            Expires = DateTime.Now.AddDays(100)
        };
    }
}
