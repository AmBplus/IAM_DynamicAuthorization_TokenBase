using AccessManagement.Services.Permission.Command;
using AccessManagement.Services.Permission.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Security;

[System.AttributeUsage
	(System.AttributeTargets.Class | System.AttributeTargets.Method)]
public class CustomAuthorizeAttribute : System.Attribute,
	Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
{
	private string NameSpaceName { get; set; }
	private string ActionName { get; set; }
	private string ControllerName { get; set; }
    

	public CustomAuthorizeAttribute()
	{ }

	

	public async void OnAuthorization(Microsoft.AspNetCore
		.Mvc.Filters.AuthorizationFilterContext context)
	{
		var services =
			context.HttpContext.RequestServices;

		// **************************************************
	
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        if (descriptor != null)
        {
            NameSpaceName = descriptor.ControllerTypeInfo?.Namespace;
            ActionName = descriptor.ActionName;
			ControllerName = descriptor.ControllerName;

        }
		if (string.IsNullOrWhiteSpace(ActionName)
			|| string.IsNullOrWhiteSpace(ControllerName)
			|| string.IsNullOrWhiteSpace(NameSpaceName))
			throw new Exception("UnValidRequest");
        // Check if the action has an anonymous attribute
        if (context.ActionDescriptor.HasAnonymousAttribute())
        {
            return;
        }
        // **************************************************
        var authenticatedUserService = services
        .GetService<AuthenticatedUserService>();
        // Get the action descriptor from the context 
        var mediator = services.GetService<IMediator>();
        // **************************************************
        if (authenticatedUserService == null)
		{
			context.Result = new Microsoft
				.AspNetCore.Mvc.BadRequestResult();

			return;
		}
		// **************************************************

		// **************************************************
		var httpContextService = services.GetService
			<Features.Common.HttpContextService>();

		if (httpContextService == null)
		{
			context.Result = new Microsoft
				.AspNetCore.Mvc.BadRequestResult();

			return;
		}
		// **************************************************

		// **************************************************
		if (authenticatedUserService.IsAuthenticated == false)
		{
			context.Result = new Microsoft
				.AspNetCore.Mvc.ChallengeResult
				(authenticationScheme: Constants.Scheme.Default);

			return;
		}
        #region // check for latter
        // **************************************************

        // **************************************************
        // *** Check Remote IP ******************************
        // **************************************************
        //var remoteIp =
        //	httpContextService.GetRemoteIpAddress();

        //if (string.IsNullOrWhiteSpace(value: remoteIp))
        //{
        //	context.Result = new Microsoft
        //		.AspNetCore.Mvc.BadRequestResult();

        //	return;
        //}
        //// **************************************************

        // **************************************************
        //var userIP =
        //	authenticatedUserService.UserIP;

        //if (string.IsNullOrWhiteSpace(value: userIP))
        //{
        //	context.Result = new Microsoft
        //		.AspNetCore.Mvc.BadRequestResult();

        //	return;
        //}
        // **************************************************

        // **************************************************
        //if (userIP != remoteIp)
        //{
        //	context.Result = new Microsoft
        //		.AspNetCore.Mvc.ChallengeResult
        //		(authenticationScheme: Constants.Scheme.Default);

        //	return;
        //}

        #endregion

         var permissionResult = await mediator.Send( new GetPermissionDetailByActionIdOrNameQueryRequest() {
            Name = $"{NameSpaceName}:{ControllerName}:{ActionName}"
        });
        if(permissionResult.IsSuccess == false)
        {
            throw new Exception("خطای سیستمی");
        }
        var userRole = authenticatedUserService.Role;

        if(string.IsNullOrWhiteSpace(userRole))
        {
            throw new Exception("نقش نمی تواند خالی یا نال باشد");
        }



        var roleHasPermissionResult = await mediator.Send(new HasRolePermissionCommandRequest()
        {
            PermissionName = permissionResult.Data.Name,
            RoleName = userRole
        });

        if(roleHasPermissionResult.IsSuccess == false)
        {
           context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
            return;
        }

        return;
    }

}
// Extension method to check for the attribute
public static class ActionDescriptorExtensions
{

	public static bool HasAnonymousAttribute(this ActionDescriptor action)
	{
		return action.EndpointMetadata.Any(m =>
		  m is AllowAnonymousAttribute);
	}
}
