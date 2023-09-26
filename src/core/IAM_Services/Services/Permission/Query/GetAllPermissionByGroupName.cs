using AccessManagement.Data;
using Base.Shared.ResultUtility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Permission.Query
{
    public record GetAllPermissionByGroupQueryResponse
    {
        public int GroupId { get; set; }   
        public string GroupName { get; set; }   
        public IEnumerable<ActionPermissionSummeryDto>   Actions { get; set; }
    }
    public record ActionPermissionSummeryDto()
    {
        public int ActionId { get; set; } 
        public string ActionName { get; set; }  
        public IEnumerable<PermissionOperationName> permissionOperations { get; set; } 
    }
    public record PermissionOperationName(int OperationId, string OperationName);
    public class GetAllPermissionOfGroupQueryRequest : IRequest<ResultOperation<GetAllPermissionByGroupQueryResponse>>
    {
        public int? Id { get; set; }
        public string? GroupName { get; set; }   
    }
    public class GetAllPermissionByGroupQueryHandler : IRequestHandler<GetAllPermissionOfGroupQueryRequest, ResultOperation<GetAllPermissionByGroupQueryResponse>>
    {
        IAccessManagementDbContext Context { get; }

        public GetAllPermissionByGroupQueryHandler(IAccessManagementDbContext context)
        {
            Context = context;
        }

        public async Task<ResultOperation<GetAllPermissionByGroupQueryResponse>> Handle(GetAllPermissionOfGroupQueryRequest request, CancellationToken cancellationToken)
        {
           var result = Context.GroupPermissions.Where(x => (request.Id != null && request.Id == x.Id) ||
            (string.IsNullOrWhiteSpace(request.GroupName) && x.Name.Contains(request.GroupName)))
                .Select(x => new GetAllPermissionByGroupQueryResponse()
                {
                    GroupId = x.Id,
                    GroupName = x.Name,
                    Actions = x.ApplicationPermissions.Select(c =>
                    new ActionPermissionSummeryDto()
                    {
                        ActionId = c.Id,
                        ActionName = x.Name,
                        permissionOperations = c.PermissionOperation.Select(o =>

                        new PermissionOperationName(o.Id, o.Name))
                    }
                    )
                }).SingleOrDefault();
            if(result == null)
            {
              return   ResultOperation<GetAllPermissionByGroupQueryResponse>.ToFailedResult("برای این گروه چیزی پیدا نشد");
            }
            return result.ToSuccessResult();
        }
    }

}
