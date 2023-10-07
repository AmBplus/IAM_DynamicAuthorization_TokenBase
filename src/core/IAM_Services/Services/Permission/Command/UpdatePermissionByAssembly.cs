using AccessManagement.Data;
using AccessManagement.Entities;
using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AccessManagement.Services.Permission.Command
{
    public record UpdatePermissionByAssemblyCommandRequest : IRequest<ResultOperation>
    {
        public bool IsEnable { get; set; } = false;
    }
    public class UpdatePermissionByAssemblyCommandHandler : IRequestHandler<UpdatePermissionByAssemblyCommandRequest, ResultOperation>
    {
        IPermissionHelper permissionHelper;
        IAccessManagementDbContext context;

        public UpdatePermissionByAssemblyCommandHandler(IPermissionHelper permissionHelper, IAccessManagementDbContext context)
        {
            this.permissionHelper = permissionHelper;
            this.context = context;
        }

        public async Task<ResultOperation> Handle(UpdatePermissionByAssemblyCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.IsEnable == false) return ResultOperation.ToSuccessResult("بروزرسانی خودرکار مجوز ها فعال نیست");
            var result =  await permissionHelper.GetInfo();
            if(result == null) { throw new Exception("UnValid Exception"); }
    
            // Group by namespace
            var actionsByNamespace = result.GroupBy(x => x.NameSpace);
            
            foreach (var systemGroup in actionsByNamespace)
            {
                var system = await CheckExitOrAddSystem(systemGroup.Key);
                // Group by controller name
                var controllers = systemGroup.GroupBy(x => x.ControllerName);

                foreach (var controllerGroup in controllers)
                {
                    // Add system if needed
                    var group = await CheckExitOrAddGroup(controllerGroup.Key);

                    // Loop through actions
                    foreach (var action in controllerGroup)
                    {
                           await CheckExitPermissionOrCreate(system,group, action.ActionName);
                    }
                }
            }
            return ResultOperation.ToSuccessResult();   
        }

        private async Task<SystemEntity> CheckExitOrAddSystem(string systemName)
        {
            var system = await context.SystemEntities
                .Where(x => x.Name.Contains(systemName))
                .FirstOrDefaultAsync();
            if (system != null) return system;
           
           
            await context.SystemEntities.AddAsync(new Entities.SystemEntity
            {
            Name = systemName
            });
            await context.SaveChangesAsync();
            system = await context.SystemEntities.
                Where(x => x.Name.Contains(systemName))
                .FirstOrDefaultAsync();
            if(system == null) throw new Exception("خطای سیستمی ، گروه سیستم نباید خالی یا نال باشد");
            return system;
        }

        private async Task CheckExitPermissionOrCreate(SystemEntity system, GroupPermissionEntity groupPermission,string actionName)
        {
            var permissionName = permissionHelper.GetPermissionName(system.Name,groupPermission.Name,actionName);
            if (context.Permissions.Any(x => x.Name.Contains(permissionName)))  return;
            await context.Permissions.AddAsync(new Entities.PermissionEntity
            {
                ActionName = actionName,
                Name = permissionName,
                GroupPermission = groupPermission,
                System = system 
            });
            await context.SaveChangesAsync();
        }

        private async Task<GroupPermissionEntity> CheckExitOrAddGroup(string groupName)
        {
            var group = await context.GroupPermissions
                .Where(x => x.Name.Contains(groupName))
                .FirstOrDefaultAsync();
            if (group != null) return group;

            
                await context.GroupPermissions.AddAsync
               
                (new Entities.GroupPermissionEntity
                {
                    Name = groupName,
                });
                await context.SaveChangesAsync();

            group= await context.GroupPermissions

                .Where(x => x.Name.Contains(groupName))
                .FirstOrDefaultAsync();
            if(group == null) throw new Exception("خطای سیستمی ، گروه محوز ها نمی تواند خالی یا نال باشد");
            return group;
        }
    }
    public interface IPermissionHelper
    {
        public Task<List<GetControllerInfoForPermission>> GetInfo();
        public string GetPermissionName(string system, string groupName, string actionName);
    }
    public record GetControllerInfoForPermission
    {
        public string ControllerName { get; set; }
        public string NameSpace { get; set; }
        public string ControllerActions { get; set; }
        public string ActionRequest { get; set; }
        public string ActionRoute { get; set; }
        public string ControllerRoute { get; set; }
        public string ActionName { get; set; }
    }
}
