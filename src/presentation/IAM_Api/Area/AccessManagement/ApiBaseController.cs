using Base.Shared.ResultUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Controllers
{
   
    [ApiController]
    
    public abstract class ApiBaseController : ControllerBase
    {
        [NonAction]
        protected  IActionResult MapToApiResult( ResultOperation<int> result)
        {
            if (result.isSuccess)
            {
                return Ok(result.message);
            }
            else if(result.Data == StatusCodes.Status400BadRequest)
                        {
                return BadRequest(result.message);  
            }
            else
                return StatusCode(result.Data, result);
        }
    }
}
