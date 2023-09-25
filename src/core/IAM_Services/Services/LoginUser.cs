
using AccessManagement.Entities;
using Base.Shared;
using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace AccessManagement.Services;
    public record LoginUserCommandRequest : IRequest<ResultOperation<LoginResponse>>
    {
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, ResultOperation<LoginResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    private readonly ITokenService _tokenService;
    

    public LoginUserCommandHandler(UserManager<ApplicationUser> userManager
        , RoleManager<ApplicationRole> roleManager
        , ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
   
    }



    public async Task<ResultOperation<LoginResponse>> Handle(LoginUserCommandRequest model, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaim = await _userManager.GetClaimsAsync(user);    

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            foreach (var item in userClaim)
            {
                authClaims.Add(item);
            }
            var tokenResult = _tokenService.GetAuthTokenResult(authClaims);
            
            
            user.RefreshToken = tokenResult.RefreshToken;
            var refreshTokenExpireTime = tokenResult.RefreshTokenExpires;
            user.RefreshTokenExpireTime = refreshTokenExpireTime;
            await _userManager.UpdateAsync(user);
            var loginResponse =
            new LoginResponse
            {
                Token = tokenResult.AccessToken,
                TokenExpireTime = tokenResult.AccessTokenExpires.ToString(),
                RefreshTokenExpireTime = refreshTokenExpireTime.ToString() ,
                RefreshToken = tokenResult.RefreshToken
            };
            return loginResponse.ToSuccessResult();
        }
        //  return Unauthorized();
        return ResultOperation<LoginResponse>.ToFailedResult();

    }
}

