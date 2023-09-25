using AccessManagement.Data;
using AccessManagement.Entities;
using Base.Shared.ResultUtility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services
{
    public record AddPermissionCommandRequest
        (string GroupName, string Action,
        List<PermissionOperationEnum> PermissionOperations) : 
        IRequest<ResultOperation>;

    public class AddPermissionCommandHandler 
        : IRequestHandler<AddPermissionCommandRequest, ResultOperation>
    {
        IAccessManagementDbContext Context { get;  }

        public AddPermissionCommandHandler(IAccessManagementDbContext context)
        {
            Context = context;
        }

        public async Task<ResultOperation> Handle(AddPermissionCommandRequest request, CancellationToken cancellationToken)
        {
            List<PermissionOperationType> permissionOperationTypes = new List<PermissionOperationType>(); 
           var groupName = Context.GroupPermissions.SingleOrDefault(x => x.Name == request.GroupName);
            if (groupName == null) { throw new Exception("گروه پیدا نشد"); }
            foreach(var operation in request.PermissionOperations) {
                var res = Context.PermissionOperationTypes.
                       SingleOrDefault(x => x.Name == request.PermissionOperations.ToString());
             if (res == null) throw new Exception("عملیات درج شده وجود ندارد");
             permissionOperationTypes.Add(res);
                    }
            Context.ApplicationPermissions.Add(new Entities.ApplicationPermission
            {
                ActionName = request.Action,
                GroupName = request.GroupName,
                GroupPermission= groupName,
                Name = $"{request.GroupName}:{request.Action}",
                PermissionOperation = permissionOperationTypes ,
                Id = Guid.NewGuid(),
            });
            await Context.SaveChangesAsync();
            return ResultOperation.ToSuccessResult();
        }
    }
}
