using AccessManagement.Services;
using AccessManagement.Services.Permission.Query;
using Base.Shared.ResultUtility;
using IAM_Services.Services.Permission;
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
        [Route("AddPermission")]
        
        public async Task<IActionResult> AddPermission([FromBody]AddPermissionCommandRequest request)
        {
            return Ok(await Mediator.Send(request));
        }
        /// <summary>
        /// اضافه کردن پرمیشن به نقش
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AddPermissionToRole")]
        [HttpPost]
        public async Task<IActionResult> AddPermissionToRole([FromBody]AddPermissionToRoleCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
        /// <summary>
        /// دریافت گروه های پرمیشن ها
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetPermissionGroups")]
        [HttpPost]
        public async Task<IActionResult> GetPermissionGroups([FromBody] GetAllPermissionGroupQueryRequest request)
        {
            var result = await Mediator.Send(request);
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
    }
}
