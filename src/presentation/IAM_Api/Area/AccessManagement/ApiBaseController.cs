using Base.Shared.ResultUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
        [NonAction]
        protected  IActionResult MapToApiResult( ResultOperation<int> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else if(result.Data == StatusCodes.Status400BadRequest)
                        {
                return BadRequest(result.Message);  
            }
            else
                return StatusCode(result.Data, result);
        }
    }
}
