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

namespace IAM_Api.Area.AccessManagement
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
        
        public async Task<IActionResult> GetPaginateUser(GetPaginateUsersQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);  
        }

        /// <summary>
        /// گرفتن نقش ها به صورت صفحه به صفحه
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPaginateRole")]

        public async Task<IActionResult> GetPaginateRoles(GetPaginateRolesQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserById")]

        public async Task<IActionResult> GetUserById(GetUserByIdQueryRequest request)
        {
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

        public async Task<IActionResult> GetUserByEmail(GetUserByEmailQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// گرفتن کاربر با نام کاربری
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserByUserName")]

        public async Task<IActionResult> GetUserByUserName(GetUserByUsernameQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
    }

}
