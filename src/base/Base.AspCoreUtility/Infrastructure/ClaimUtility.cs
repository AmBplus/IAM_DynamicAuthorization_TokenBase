using System.Security.Claims;

namespace Base.AspCore.Infrastructure;

public static class ClaimUtility
{
    public static long? GetUserId(this ClaimsPrincipal User)
    {
        try
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var userId = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                return userId;
            }

            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
