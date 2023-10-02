using AccessManagement.Data;
using Base.Shared.ResultUtility;
using MediatR;
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

            var ActionsByGroup = result.GroupBy(x => x.ControllerName);

            foreach (var item in ActionsByGroup)
            {
                bool flag = false;
                
                foreach (var action in item) 
                {
                    var groupName = $"{action.ControllerNameSpace}:{action.ControllerName}";
                    if (!flag ) {

                        
                        await CheckExitOrAddGroup($"{groupName}");
                        flag = true;
                    }
                    await CheckExitPermissionOrCreate(groupName, action.ActionName);  

                }

               
            }
            return ResultOperation.ToSuccessResult();   
        }

        private async Task CheckExitPermissionOrCreate(string groupName, string actionName)
        {
           var group = await context.GroupPermissions.Where(x => x.Name.Contains(groupName)).FirstOrDefaultAsync();
            if (group == null) throw new Exception("UnValid Operation");
            var permissionName = $"{groupName}:{actionName}";
            if (context.Permissions.Any(x => x.Name.Contains(permissionName)))  return;
            await context.Permissions.AddAsync(new Entities.PermissionEntity
            {
                ActionName = actionName,
                Name = permissionName,
                GroupPermission = group,
            });
            await context.SaveChangesAsync();
        }

        private async Task CheckExitOrAddGroup(string groupName)
        {
            if(!await context.GroupPermissions.AnyAsync(x=>x.Name.Contains(groupName)))
            {
                await context.GroupPermissions.AddAsync(new Entities.GroupPermissionEntity
                {
                    Name = groupName,
                });
                await context.SaveChangesAsync();
            }
            
        }
    }
    public interface IPermissionHelper
    {
        public Task<List<GetControllerInfoForPermission>> GetInfo();
    }
    public record GetControllerInfoForPermission
    {
        public string ControllerName { get; set; }
        public string ControllerNameSpace { get; set; }
        public string ControllerActions { get; set; }
        public string ActionRequest { get; set; }
        public string ActionRoute { get; set; }
        public string ControllerRoute { get; set; }
        public string ActionName { get; set; }
    }
}
