using AccessManagement.Services.Permission.Command;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Permission
{
    public class PermissionHelper : IPermissionHelper
    {
        public PermissionHelper(IActionDescriptorCollectionProvider actionDescriptorCollection)
        {
            ActionDescriptorCollection = actionDescriptorCollection;
        }

        public IActionDescriptorCollectionProvider ActionDescriptorCollection { get; }

        public async Task<List<GetControllerInfoForPermission>> GetInfo()
        {
            var endpoints = ActionDescriptorCollection.ActionDescriptors.Items;
            return endpoints
         .OfType<ControllerActionDescriptor>()
         .Where(c => c.MethodInfo.GetCustomAttributes(typeof(NonActionAttribute), true).Length == 0)
         .Select(c => new GetControllerInfoForPermission
         {

             ControllerName = c.ControllerName,
             NameSpace = c?.ControllerTypeInfo?.Namespace,
             ControllerActions = c?.MethodInfo?.Name,
             ActionRequest = string.Join(", ", c.ActionConstraints.Select(ac => ac.GetType().Name.Replace("Attribute", ""))),
             ActionRoute = c.AttributeRouteInfo?.Template,
             ControllerRoute = c.RouteValues["controller"],
             ActionName = c.ActionName
         }) 
         .ToList();

        }
        public string GetPermissionName(string system,string groupName ,string  actionName) 
        {
            return $"{system}:{groupName}:{actionName}";
        }
    }
}
