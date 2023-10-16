using AccessManagement.Data;
using AccessManagement.Entities;

using Base.Shared.Date;
using Base.Shared.ResultUtility;
using Base.Shared.Security;
using IAM_Services.Services.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace AccessManagement.Services;
    // Request model
    public class RefreshTokenRequest : IRequest<ResultOperation<RefreshTokenResponse>>
    {
    public string Token { get; set; }
    public string RefreshToken { get; set; }

}
public class RefreshTokenResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string TokenExpireTime { get; set; }
    public string RefreshTokenExpireTime { get; set; }
}
/// <summary>
/// گرفتن توکن جدید بر اساس توکن و رفرش توکن قدیمی
/// </summary>
public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, ResultOperation<RefreshTokenResponse>>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IAccessManagementDbContext _context;
   

    public RefreshTokenHandler(UserManager<UserEntity> userManager,
                               ITokenService tokenService,
                               IAccessManagementDbContext context
                               
        )
    {
        this._userManager = userManager;
        this._tokenService = tokenService;
        this._context = context;
     
    }

    public async Task<ResultOperation<RefreshTokenResponse>> Handle(RefreshTokenRequest request,
                                           CancellationToken cancellationToken)
    {
        if (request.Token == null ||
         request.RefreshToken == null  )
        {
            return ResultOperation<RefreshTokenResponse>.ToFailedResult(" Token Or Refresh Token Is Null");
        }
    
        // بررسی اینکه توکن معتبر هست یا خیر
        var principal = _tokenService.GetPrincipal(request.Token);
        if(principal == null)
        {
            return ResultOperation<RefreshTokenResponse>.ToFailedResult("توکن نا معتبر");
        }
        // گرفتن توکن کاربر با استفاده از توکن ارسالی
        var userToken = await _context.UserTokens
            .Where(x => x.TokenHash == HashHelper.GetSha256(request.Token))
            .FirstOrDefaultAsync();

        if(userToken == null)
        {
            return ResultOperation<RefreshTokenResponse>.ToFailedResult("UnValid Token");
        }
        // بررسی اینکه رفرش توکن ارسالی با رفرش توکن ذخیره شده برای توکن تطابق دارد یا خیر
        if(userToken.RefreshTokenHash != HashHelper.GetSha256(request.RefreshToken))
        {
            return ResultOperation<RefreshTokenResponse>.ToFailedResult("UnValid Token OR Refresh Token");
        }    
        // بررسی اینکه زمان رفرش توکن هنوز معتبر است یا خیر
        if(userToken.RefreshTokenExp < DateUtility.DateTimeNow)
        {
            return ResultOperation<RefreshTokenResponse>.ToFailedResult("Refresh Token Is Expired");
        }
        // گرفتن دوباره کاربر و تولید توکن جدید
        var user = await _userManager.FindByNameAsync(principal?.Identity?.Name);
        if (user == null)
        {
            return ResultOperation<RefreshTokenResponse>.ToFailedResult("bad request"); 
        }
        // دریافت نقش های کاربر
            var userRoles = await _userManager.GetRolesAsync(user);
        // دریافت کلیم های کاربر
        var userClaim = await _userManager.GetClaimsAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ),
                    new Claim(ClaimTypes.Email, user?.Email),

                };
        // ایجاد کلیم برای توکن
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            foreach (var item in userClaim)
            {
                authClaims.Add(item);
            }
            var tokenResult = _tokenService.GetAuthTokenResult(authClaims);



        // ذخیره توکن جدید برای کاربر 
            await _context.UserTokens.AddAsync(new UserTokenEntity
            {
                RefreshTokenHash = tokenResult.RefreshToken,
                RefreshTokenExp = tokenResult.RefreshTokenExpires,
                Id = Guid.NewGuid(),
                User = user,
                UserId = user.Id,
                TokenExp = tokenResult.AccessTokenExpires,
                TokenHash = HashHelper.GetSha256(tokenResult.AccessToken),
                Device = "Test"


            });
            await _context.SaveChangesAsync();
            
           return new RefreshTokenResponse
            {
                Token = tokenResult.AccessToken,
                TokenExpireTime = tokenResult.AccessTokenExpires.ToString(),
                RefreshTokenExpireTime = tokenResult.RefreshTokenExpires.ToString(),
                RefreshToken = tokenResult.RefreshToken
            }.ToSuccessResult();
       
    
    }

}
