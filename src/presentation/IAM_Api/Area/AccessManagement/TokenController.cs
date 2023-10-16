
using AccessManagement.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccessManagement.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IMediator mediator;

    public TokenController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [AllowAnonymous()]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody]RefreshTokenRequest request)
    {
        return Ok(await mediator.Send(request));
    }
    // revoke Refresh Token
    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke([FromBody] RevokeTokenRequest request)
    {
        await mediator.Send(request);
        return NoContent();
    }
}
