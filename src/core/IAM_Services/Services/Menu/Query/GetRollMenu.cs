using AccessManagement.Data;
using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Menu.Query
{
    public record GetRollMenuQueryRequest(Guid Id) : IRequest<ResultOperation<List<GetRollMenuQueryResponse>>>
    {
        
             
    }
    public record GetRollMenuQueryResponse
    {

        public int MenuId { get; set; } 
        public string MenuName { get; set; }    
        public string MenuUrl { get; set; }

        public string MenuTitle { get; set; }
        public Guid RoleId { get; set; }    
        public string RoleName { get; set; }
    }

    public class GetRollMenuQueryHandler : IRequestHandler<GetRollMenuQueryRequest, ResultOperation<List<GetRollMenuQueryResponse>>>
    {
        IAccessManagementDbContext Context;

        public GetRollMenuQueryHandler(IAccessManagementDbContext context)
        {
            Context = context;
        }

        public async Task<ResultOperation<List<GetRollMenuQueryResponse>>> Handle(GetRollMenuQueryRequest request, CancellationToken cancellationToken)
        {
           var result = await Context.RoleMenuEntities
                .Where(x => x.RoleId == request.Id)
                .Select(x=> new GetRollMenuQueryResponse
                {
                    RoleId = x.RoleId,
                    MenuId = x.MenuEntityId,
                    MenuName = x.MenuEntity.Name,
                    MenuTitle = x.MenuEntity.Title,
                    MenuUrl = x.MenuEntity.Url,
                })
                .ToListAsync() ;
            if (result == null) return ResultOperation<List<GetRollMenuQueryResponse>>.ToFailedResult();
            return result.ToSuccessResult() ;   
        }
    }
}
