using AccessManagement.Data;
using Base.Shared.ResultUtility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var role = _dbContext.Roles.FirstOrDefault(r => r.Name == request.RoleName);
        var permission = _dbContext.Permissions.FirstOrDefault(p => p.Name == request.PermissionName);

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

