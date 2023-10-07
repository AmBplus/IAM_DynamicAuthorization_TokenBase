using AccessManagement.SeedData;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using AccessManagement.Controllers;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Routing;
using IAM_Persistence.Migrations;

namespace AccessManagement.SeedData
{
    public class SeedControllerPermission : ISeedControllerPermission
    {
       

        

        public SeedControllerData GetInfo()
        {
            var groupPendingName = typeof(PermissionController).Name;
            if(groupPendingName.EndsWith("Controller")) groupPendingName = groupPendingName.Substring(0, groupPendingName.Length - 10);
            var groupPermissionName = groupPendingName;
            return new SeedControllerData
            {
            GroupPermission = groupPermissionName,
            nameSpace = typeof(PermissionController)?.Namespace,
            Actions = typeof(PermissionController)
            .GetMembers(BindingFlags.Public | BindingFlags.Instance)
            .Where(c => c.GetCustomAttributes(typeof(NonActionAttribute), true).Length == 0)
            .Where(m=>m.MemberType == MemberTypes.Method)
            .Where(m=> m.GetCustomAttributes<HttpMethodAttribute>().Any())
            .Select(x => x.Name).ToList()

            };

        }
    }
}
