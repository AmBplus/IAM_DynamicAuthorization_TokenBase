using AccessManagement.Services;
using Base.Shared.ResultUtility;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccessManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public  class PermissionController : ApiBaseController
    {
        IMediator Mediator { get; set; }

        public PermissionController(IMediator mediator)
        {
            Mediator = mediator;
        }
        /// <summary>
        /// اضافه کردن پرمیشن 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add Permission")]
        
        public async Task<IActionResult> AddPermission(AddPermissionCommandRequest request)
        {
            return Ok(await Mediator.Send(request));
        }
        /// <summary>
        /// اضافه کردن پرمیشن به نقش
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Add Permission To Role")]
        [HttpPost]
        public async Task<IActionResult> AddPermissionToRole([FromBody]AddPermissionToRoleCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

    }
}
