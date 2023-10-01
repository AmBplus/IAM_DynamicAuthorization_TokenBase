//using AccessManagement.Data;
//using Base.Shared.ResultUtility;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AccessManagement.Services.Menu.Command
//{
//    public record AddMenuRoleCommandRequest(Guid RoleId, int MenuId) : IRequest<ResultOperation>
//    {
//     public string? Description { get; set; }   
//     public string? Name { get; set; }
//    }
//    public class AddMenuRoleCommandHandler : IRequestHandler<AddMenuRoleCommandRequest, ResultOperation>
//    {
//        IAccessManagementDbContext Context { get; }

//        public AddMenuRoleCommandHandler(IAccessManagementDbContext context)
//        {
//            Context = context;
//        }

//        public async Task<ResultOperation> Handle(AddMenuRoleCommandRequest request, CancellationToken cancellationToken)
//        {
//           var role = await Context.Roles.SingleOrDefaultAsync(x=>x.Id == request.RoleId);
//           if(role == null)
//            {
//                throw new Exception("نقش یافت نشد");
//            }
//            var menu = await Context.MenuEntities.SingleOrDefaultAsync(x => x.Id == request.MenuId);
//            if (menu == null)
//            {
//                throw new Exception("منو یافت نشد");
//            }
//           await Context.RoleMenuEntities.AddAsync(new AccessManagement.Entities.RoleMenuEntity
//            {
//                RoleId = role.Id,
//                Role = role,
//                MenuEntityId = menu.Id,
//                MenuEntity = menu,
//                Description = menu.Description,
//                Name = menu.Name,
//            });
//            await Context.SaveChangesAsync();   

//        }
//    }
//}
