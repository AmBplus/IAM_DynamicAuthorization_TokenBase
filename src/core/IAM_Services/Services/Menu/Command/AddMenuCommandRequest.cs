using AccessManagement.Data;
using Base.Shared.ResultUtility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.Menu.Command
{
    public record AddMenuCommandRequest(string Name , Uri Url) : IRequest<ResultOperation>
    {
       
      
        public string? Description { get; set; }
        public string? Title { get; set; }

        
    }
    public class AddMenuCommandHandler : IRequestHandler<AddMenuCommandRequest, ResultOperation>
    {
        IAccessManagementDbContext Context { get; }

        public AddMenuCommandHandler(IAccessManagementDbContext context)
        {
            Context = context;
        }

        public async Task<ResultOperation> Handle(AddMenuCommandRequest request, CancellationToken cancellationToken)
        {
            Context.MenuEntities.Add(new Entities.MenuEntity

            { Name = request.Name,Description = request.Description,Title = request.Title,Url = request.Url.OriginalString });
            await Context.SaveChangesAsync(); 
            return ResultOperation.ToSuccessResult();
        }
    }
}
