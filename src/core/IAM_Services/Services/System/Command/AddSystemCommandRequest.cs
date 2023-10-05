using AccessManagement.Data;
using Base.Shared.ResultUtility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Services.System.Command
{
    public record AddSystemCommandRequest(string Name) : IRequest<ResultOperation>
    {
        
    }
    public class AddSystemCommandHandler : IRequestHandler<AddSystemCommandRequest, ResultOperation>
    {
        IAccessManagementDbContext Context { get;  }

        public AddSystemCommandHandler(IAccessManagementDbContext context)
        {
            Context = context;
        }

        public async Task<ResultOperation> Handle(AddSystemCommandRequest request, CancellationToken cancellationToken)
        {
            var flag =  Context.SystemEntities.Any(x=>x.Name == request.Name);
            if (flag)
            {
                return ResultOperation.ToSuccessResult("سیستم از قبل وجود دارد");
            }
            await Context.SystemEntities.AddAsync(new Entities.SystemEntity
            {
                Name = request.Name,
            });
            await Context.SaveChangesAsync();
            return ResultOperation.ToSuccessResult();
        }
    }
}
