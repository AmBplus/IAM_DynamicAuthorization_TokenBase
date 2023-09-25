
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

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        return Ok(await mediator.Send(request));
    }
    // In Token controller
    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke(RevokeTokenRequest request)
    {
        await mediator.Send(request);
        return NoContent();
    }
}
