using AccessManagement.Services;
using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccessManagement.Controllers
{
   
    
    public  class PermissionController : ApiBaseController
    {
        IMediator Mediator { get; set; }

        public PermissionController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddPermissionToRole([FromBody]AddPermissionToRoleCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

    }
}
