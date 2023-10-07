using AccessManagement.Data;
using AccessManagement.Entities;
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
    public record AddPermissionCommandRequest
        (string SystemName,string GroupName, string Action) :
        IRequest<ResultOperation>;

    public class AddPermissionCommandHandler
        : IRequestHandler<AddPermissionCommandRequest, ResultOperation>
    {
        IAccessManagementDbContext Context { get; }

        public AddPermissionCommandHandler(IAccessManagementDbContext context)
        {
            Context = context;
        }

        public async Task<ResultOperation> Handle(AddPermissionCommandRequest request, CancellationToken cancellationToken)
        {

            var groupPermission = await Context.GroupPermissions.SingleOrDefaultAsync(x => x.Name == request.GroupName);
            if (groupPermission == null) { throw new Exception("گروه پیدا نشد"); }
            var system = await Context.SystemEntities.SingleOrDefaultAsync(x => x.Name == request.SystemName);
            if (system == null) { throw new Exception("گروه پیدا نشد"); }
            var permissionName = $"{system.Name}:{groupPermission.Name}:{request.Action}";
            if(Context.Permissions.Any(x => x.Name == permissionName)) return ResultOperation.ToFailedResult("Already Exists") ;
            Context.Permissions.Add(new Entities.PermissionEntity
            {
                ActionName = request.Action,
                System = system,
               
                GroupPermission = groupPermission,
                Name = permissionName 


            });
            await Context.SaveChangesAsync();
            return ResultOperation.ToSuccessResult();
        }
    }
}
