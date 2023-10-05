using AccessManagement.Data;
using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Permission.Command;

public class HasRolePermissionCommandRequest : IRequest<ResultOperation>
{
    public string PermissionName { get; set; }
    public string RoleName { get; set; }    

}
public class HasRolePermissionCommandHandler : IRequestHandler<HasRolePermissionCommandRequest, ResultOperation>
{
    IAccessManagementDbContext Context;
public HasRolePermissionCommandHandler(IAccessManagementDbContext context)
    {
        Context = context;
    }

    public async Task<ResultOperation> Handle(HasRolePermissionCommandRequest request, CancellationToken cancellationToken)
    {

        var role = await Context.Roles
            .Where(x => x.Name == request.RoleName)
            .Include(x => x.Permissions).SingleOrDefaultAsync();
        if(role == null)
        {
            throw new Exception("UnValid Request");
        }
        var flag = role.Permissions.Any(x=>x.Name == request.PermissionName);
        if (flag)
        {
            return ResultOperation.ToFailedResult("نقش به این رول دسترسی ندارد");
        }

        return ResultOperation.ToSuccessResult();
        
    }
}



