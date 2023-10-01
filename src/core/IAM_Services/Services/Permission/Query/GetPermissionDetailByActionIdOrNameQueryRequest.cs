using AccessManagement.Data;
using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Permission.Query
{
    public record GetPermissionDetailByActionIdOrNameQueryRequest : IRequest<ResultOperation<GetPermissionDetailByActionIdOrNameQueryResponse>>
    {
        public string Name { get; set; }    
        public int Id { get; set; }
       
    }
    public record PermissionOperationName(int OperationId, string OperationName);
    public record GetPermissionDetailByActionIdOrNameQueryResponse 
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public IEnumerable<PermissionOperationName> PermissionOperations { get; set; }

    }
    public class GetPermissionDetailByActionIdOrNameQueryHandler : IRequestHandler<GetPermissionDetailByActionIdOrNameQueryRequest, ResultOperation<GetPermissionDetailByActionIdOrNameQueryResponse>>
    {
        IAccessManagementDbContext Context;

        public GetPermissionDetailByActionIdOrNameQueryHandler(IAccessManagementDbContext context)
        {
            Context = context;
        }

        public async Task<ResultOperation<GetPermissionDetailByActionIdOrNameQueryResponse>> Handle(GetPermissionDetailByActionIdOrNameQueryRequest request, CancellationToken cancellationToken)
        {

            var result = await Context.Permissions.
                Where(x => (request.Id > 0 && request.Id == x.Id) ||
                (!string.IsNullOrWhiteSpace(request.Name) && x.Name.Contains(request.Name)))
               
                .Select(x => new GetPermissionDetailByActionIdOrNameQueryResponse
                {
                    Name = x.Name,
                    Id = x.Id,
                    //PermissionOperations =
                    //x.PermissionOperation.
                    //Select(c => new PermissionOperationName(c.Id, c.Name))
                }).FirstOrDefaultAsync();
                
            if(result == null )
            {
                return ResultOperation<GetPermissionDetailByActionIdOrNameQueryResponse>.ToFailedResult("نتیجه ای یافت نشد");
            }
            return result.ToSuccessResult();    
        }
    }
}
