using AccessManagement.Controllers;
using AccessManagement.Services.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiBaseController
    {
        IMediator Mediator { get; }

        public UserController(IMediator mediator)
        {
            Mediator = mediator;
        }
        /// <summary>
        /// گرفتن کاربران به صورت صفحه به صفحه
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPaginateUser")]
        
        public async Task<IActionResult> GetPaginateUser(int page, int pageSize)
        {
            var requestPaginateUser = new GetPaginateUsersQueryRequest(page,pageSize){ };
            var result = await Mediator.Send(requestPaginateUser);
            return Ok(result);  
        }

        /// <summary>
        /// گرفتن نقش ها به صورت صفحه به صفحه
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPaginateRole")]

        public async Task<IActionResult> GetPaginateRoles(int page, int pageSize)
        {
            var request = new GetPaginateRolesQueryRequest(page, pageSize); 
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserById")]

        public async Task<IActionResult> GetUserById([FromHeader] GetUserByIdQueryRequest request)
        {
            // localizer.get("GetUser")
            var result = await Mediator.Send(request);
            return Ok(result);
        }


        /// <summary>
        /// گرفتن کاربر با ایمیل
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserByEmail")]

        public async Task<IActionResult> GetUserByEmail([FromHeader] GetUserByEmailQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// گرفتن کاربر با نام کاربری
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserByUserName")]

        public async Task<IActionResult> GetUserByUserName([FromHeader] GetUserByUsernameQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
    }

}
