using AccessManagement.Services;
using AccessManagement.Services.Permission.Query;
using Base.Shared.ResultUtility;
using AccessManagement.Services.Permission;
using AccessManagement.Services.Permission.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
        /// اضافه کردن گروه مجوز ها
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddGroupPermission")]
        public async Task<IActionResult> AddGroupPermission(string name,CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name)) return new  BadRequestObjectResult(ResultOperation.ToFailedResult("مقدار اسم خالی یا نال است"));
            return Ok( await Mediator.Send(new AddGroupPermissionCommandRequest(name)));
        }



        /// <summary>
        /// اضافه کردن پرمیشن 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPermission")]
        
        public async Task<IActionResult> AddPermission([FromBody]AddPermissionCommandRequest request)
        {
            return Ok(await Mediator.Send(request));
        }
        /// <summary>
        /// اضافه کردن مجوز  به نقش
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AddPermissionToRole")]
        [HttpPost]
        [Authorize("")]
       
        public async Task<IActionResult> AddPermissionToRole([FromBody]AddPermissionToRoleCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
        /// <summary>
        /// دریافت گروه های مجوز ها
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetPermissionGroups")]
        [HttpGet]
        public async Task<IActionResult> GetPermissionGroups()
        {
            var result = await Mediator.Send(new GetAllPermissionGroupQueryRequest());
            return Ok(result);
        }

        /// <summary>
        /// گرفتن مجوز ها بر اساس گروه
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllPermissionByGroupName")]
        public async Task<IActionResult> GetAllPermissionByGroupName(int? id = null, string name = null)
        {
            GetAllPermissionOfGroupQueryRequest request = new GetAllPermissionOfGroupQueryRequest() { Id = id,GroupName = name};    
            var result = await Mediator.Send(request);
            return Ok(result);
        }


        [Route("GetPermissionDetailByActionIdOrName")]
        [HttpPost]
        public async Task<IActionResult> GetPermissionDetailByActionIdOrName([FromBody] GetPermissionDetailByActionIdOrNameQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }



    }
}
