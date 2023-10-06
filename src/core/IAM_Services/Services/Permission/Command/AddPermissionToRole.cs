using Base.Shared.ResultUtility;
using AccessManagement.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccessManagement.Services;

public class AddPermissionToRoleCommandRequest : IRequest<ResultOperation>
{
    public string RoleName { get; set; }
    public string PermissionName { get; set; }
}

public class AddPermissionToRoleHandler : IRequestHandler<AddPermissionToRoleCommandRequest, ResultOperation>
{
    private readonly IAccessManagementDbContext _dbContext;

    public AddPermissionToRoleHandler(IAccessManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultOperation> Handle(AddPermissionToRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles.Include(x=>x.Permissions).SingleAsync(r => r.Name == request.RoleName);
        var permission = await _dbContext.Permissions.SingleAsync(p => p.Name.Contains(request.PermissionName));

        if (role == null || permission == null)
            throw new Exception("Role or Permission not found");

        role.Permissions.Add(permission);

        await _dbContext.SaveChangesAsync();

        return ResultOperation.ToSuccessResult();
    }
}

public class PermissionService
{
    private readonly IMediator _mediator;

    public PermissionService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task AddPermissionToRole(string roleName, string permissionName)
    {
        await _mediator.Send(new AddPermissionToRoleCommandRequest
        {
            RoleName = roleName,
            PermissionName = permissionName
        });
    }
}

