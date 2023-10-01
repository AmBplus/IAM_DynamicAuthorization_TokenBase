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
    public class GetAllMenuQueryRequest : IRequest<ResultOperation<List<GetAllMenuQueryResponse>>>
    {
    }
    public class GetAllMenuQueryResponse
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Description { get; set; } 
        public string Title { get; set; }   
    }

    public class GetAllMenuQueryHandler : IRequestHandler<GetAllMenuQueryRequest, ResultOperation<List<GetAllMenuQueryResponse>>>
    {
        IAccessManagementDbContext Context;

        public async Task<ResultOperation<List<GetAllMenuQueryResponse>>> Handle(GetAllMenuQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await Context.MenuEntities.Select(x=>
            new GetAllMenuQueryResponse
            {
                Id = x.Id,
                Name = x.Name,  
                Description = x.Description,    
                Title = x.Title,
                
            }).ToListAsync();
            return result.ToSuccessResult();    
        }
    }
}
