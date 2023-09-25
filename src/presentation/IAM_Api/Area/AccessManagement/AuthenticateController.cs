using AccessManagement.Services;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ApiBaseController
{

    private readonly IMediator _mediator;
    public AuthenticateController(

        IMediator mediator)
    {
   
        _mediator = mediator;
    }

    [HttpPost]
   
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest model)
    {
      var result =  await _mediator.Send(model);
        return Ok(result);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommandRequest model)
    {
      var result =  await    _mediator.Send(model);
        return MapToApiResult(result);
    }


}
