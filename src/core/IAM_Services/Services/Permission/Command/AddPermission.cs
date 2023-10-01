using AccessManagement.Data;
using AccessManagement.Entities;
using Base.Shared.ResultUtility;
using MediatR;
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

            var groupName = Context.GroupPermissions.SingleOrDefault(x => x.Name == request.GroupName);
            if (groupName == null) { throw new Exception("گروه پیدا نشد"); }
            var systemName = Context.SystemEntities.SingleOrDefault(x => x.Name == request.SystemName);
            if (systemName == null) { throw new Exception("گروه پیدا نشد"); }
            Context.Permissions.Add(new Entities.PermissionEntity
            {
                ActionName = request.Action,
                
                GroupPermission = groupName,
                Name = $"{request.GroupName}:{request.Action}",


            });
            await Context.SaveChangesAsync();
            return ResultOperation.ToSuccessResult();
        }
    }
}
