using AccessManagement.Entities;
using Base.Shared.ResultUtility;
using AccessManagement.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Permission.Command
{
    public record AddGroupPermissionCommandRequest(string Name) :  IRequest<ResultOperation>
    {
    }
    public class AddGroupPermissionCommandHandler : IRequestHandler<AddGroupPermissionCommandRequest, ResultOperation>
    {
        IAccessManagementDbContext Context { get;  }

        public AddGroupPermissionCommandHandler(IAccessManagementDbContext context)
        {
            Context = context;
        }

        public async Task<ResultOperation> Handle(AddGroupPermissionCommandRequest request, CancellationToken cancellationToken)
        {
            await Context.GroupPermissions.AddAsync(new GroupPermissionEntity() { Name =  request.Name});
            await Context.SaveChangesAsync();
            return ResultOperation.ToSuccessResult("اضافه کردن گروه با موفقیت انجام شد");
            
        }
    }
}
