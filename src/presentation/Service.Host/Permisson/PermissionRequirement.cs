using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AccessManagement.Authorizations;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission) =>
        Permission = permission;

    public string Permission { get; }
}

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        // TODO: Write logic to verify permission
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
public class PermissionAttribute : AuthorizeAttribute
{
    const string POLICY_PREFIX = "Permission";

    public PermissionAttribute(string requiredPermission) =>
        RequiredPermission = requiredPermission;

    public string? RequiredPermission
    {
        get
        {
            return Policy?.Substring(POLICY_PREFIX.Length);
        }
        set
        {
            Policy = $"{POLICY_PREFIX}{value}";
        }
    }
}
public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    const string POLICY_PREFIX = "Permission";

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return Task.FromResult(
            new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser().Build());
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return Task.FromResult<AuthorizationPolicy?>(null);
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
        {
            var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            policy.AddRequirements(new PermissionRequirement(policyName.Substring(POLICY_PREFIX.Length)));
            return Task.FromResult((AuthorizationPolicy?)policy.Build());
        }
        else
        {
            return Task.FromResult<AuthorizationPolicy?>(null);
        }
    }
}