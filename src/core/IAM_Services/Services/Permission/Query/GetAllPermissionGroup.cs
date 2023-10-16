using Base.Shared.ResultUtility;
using AccessManagement.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Permission
{
    public record GetAllPermissionGroupQueryRequest : IRequest<ResultOperation<List<GetAllPermissionGroupQueryResponse>>> { }
    public record GetAllPermissionGroupQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetAllPermissionGroupHandler : IRequestHandler<GetAllPermissionGroupQueryRequest, ResultOperation<List<GetAllPermissionGroupQueryResponse>>>
    {
        IAccessManagementDbContext Context { get; }

        public GetAllPermissionGroupHandler(IAccessManagementDbContext context)
        {
            Context = context;
        }

        public async Task<ResultOperation<List<GetAllPermissionGroupQueryResponse>>> Handle(GetAllPermissionGroupQueryRequest request, CancellationToken cancellationToken)
        {
            var groupPermission = Context.GroupPermissions.Select
                (x => new GetAllPermissionGroupQueryResponse()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            if (groupPermission == null)
            {
                return ResultOperation<List<GetAllPermissionGroupQueryResponse>>.
                    ToFailedResult("گروه پرمیشن پیدا نشد");
            }
            return groupPermission.ToSuccessResult("عملیات با موفقیت انجام شد");
        }
    }
}
