
using AccessManagement.Data;
using AccessManagement.Entities;

using Base.Shared;
using Base.Shared.ResultUtility;
using Base.Shared.Security;
using IAM_Services.Services.User;
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
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IAccessManagementDbContext context;
    private readonly ITokenService _tokenService;


    public LoginUserCommandHandler(UserManager<UserEntity> userManager
        , RoleManager<RoleEntity> roleManager
        , ITokenService tokenService,
IAccessManagementDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        this.context = context;
    }



    public async Task<ResultOperation<LoginResponse>> Handle(LoginUserCommandRequest model, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(model.Username.Trim().ToString());
        if (user != null && await _userManager.CheckPasswordAsync(user, model?.Password?.Trim()?.ToString())) ;
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaim = await _userManager.GetClaimsAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ),
                    new Claim(ClaimTypes.Email, user?.Email),

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




            await context.UserTokens.AddAsync(new UserTokenEntity
            {
                RefreshTokenHash = HashHelper.GetSha256(tokenResult.RefreshToken),
                RefreshTokenExp = tokenResult.RefreshTokenExpires,
                Id = Guid.NewGuid(),
                User = user,
                UserId = user.Id,
                TokenExp = tokenResult.AccessTokenExpires,
                TokenHash = HashHelper.GetSha256(tokenResult.AccessToken),
                Device = "Test"


            });
            await context.SaveChangesAsync();
            var loginResponse =
            new LoginResponse
            {
                Token = tokenResult.AccessToken,
                TokenExpireTime = tokenResult.AccessTokenExpires.ToString(),
                RefreshTokenExpireTime = tokenResult.RefreshTokenExpires.ToString(),
                RefreshToken = tokenResult.RefreshToken
            };
            return loginResponse.ToSuccessResult();
        }
        //  return Unauthorized();
        return ResultOperation<LoginResponse>.ToFailedResult("نام کاربری یا رمز عبور تطابق نداشت یا اشتباه است");

    }
}

