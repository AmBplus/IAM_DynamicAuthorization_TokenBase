using AccessManagement.Data;
using AccessManagement.Entities;
using Base.Shared.ResultUtility;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AccessManagement.Services.Permission.Command;

public class HasRolePermissionCommandRequest : IRequest<ResultOperation>
{
    public string PermissionName { get; set; }
    public string RoleName { get; set; }    

}
public class HasRolePermissionCommandHandler : IRequestHandler<HasRolePermissionCommandRequest, ResultOperation>
{
    IDapperAccessManagementDbContext Context;
public HasRolePermissionCommandHandler(IDapperAccessManagementDbContext context)
    {
        Context = context;
    }

    public async Task<ResultOperation> Handle(HasRolePermissionCommandRequest request, CancellationToken cancellationToken)
    {

     using(var connection = Context.CreateConnection()) {

            var role = await connection.QuerySingleOrDefaultAsync<RoleEntity>(
            "SELECT * FROM AspNetRoles WHERE Name = @RoleName",
              new { RoleName = request.RoleName });
            var permission = await connection.QuerySingleOrDefaultAsync<PermissionEntity>(
            "SELECT * FROM Permissions WHERE Name = @PermissionName",
              new { PermissionName = request.PermissionName });
            if (role == null)
            {
                throw new Exception("UnValid Request");
            }
            if(permission == null)
            {
                throw new Exception("UnValid Request");
            }
            var flag = await connection.QuerySingleOrDefaultAsync<bool>(
                "SELECT 1 FROM PermissionEntityRoleEntity WHERE RolesId = @RoleId AND PermissionsId = @PermissionId",
                new { RoleId = role.Id, PermissionId = permission.Id});
            connection.Dispose();
            if (!flag)
            {
                return ResultOperation.ToFailedResult("نقش به این رول دسترسی ندارد");
            }
            return ResultOperation.ToSuccessResult();

        }

    }
}



