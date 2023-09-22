using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Base.AspCore.Infrastructure.Settings;

namespace Base.AspCore.Infrastructure.Middlewares;

public class ActivationKeysHandlerMiddleware : object
{
    public ActivationKeysHandlerMiddleware
        (RequestDelegate next)
    {
        Next = next;
    }

    private RequestDelegate Next { get; }

    public async Task
        InvokeAsync(HttpContext httpContext,
            ApplicationSettings applicationSettings)
    {
        if (applicationSettings == null ||
            applicationSettings.ActivationKeys == null ||
            applicationSettings.ActivationKeys.Length == 0)
        {
            // WriteAsync() -> using Microsoft.AspNetCore.Http;
            await httpContext.Response.WriteAsync("No Activation Key");

            return;
        }

        var domain =
            httpContext.Request.Host.Value;

        //domain = "dtat.ir";

        var validActivationKey =
            GetValidActivationKeyByDomain(domain);

        if (string.IsNullOrWhiteSpace(validActivationKey))
        {
            // WriteAsync() -> using Microsoft.AspNetCore.Http;
            await httpContext.Response.WriteAsync("No Activation Key");

            return;
        }

        var contains =
            applicationSettings.ActivationKeys
                .Where(current => current.ToLower() == validActivationKey.ToLower())
                .Any();

        if (contains == false)
        {
            // WriteAsync() -> using Microsoft.AspNetCore.Http;
            await httpContext.Response.WriteAsync("No Activation Key");

            return;
        }

        await Next(httpContext);
    }

    #region Static Member(s)

    private static string GetSha256(string value)
    {
        using var mySHA256 = SHA256.Create();

        var stringBuilder =
            new StringBuilder();

        try
        {
            var valueBytes =
                Encoding.UTF8.GetBytes(value);

            // Compute the hash of the fileStream.
            var hashBytes =
                mySHA256.ComputeHash(valueBytes);

            foreach (var theByte in hashBytes)
                stringBuilder.Append
                    (theByte.ToString("x2"));

            return stringBuilder.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }

    private static string GetValidActivationKeyByDomain(string domain)
    {
        var result =
            GetSha256(domain);

        return result;
    }

    #endregion /Static Member(s)
}
