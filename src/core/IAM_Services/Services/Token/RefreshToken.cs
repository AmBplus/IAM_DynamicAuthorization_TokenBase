using AccessManagement.Data;
using AccessManagement.Entities;
using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace AccessManagement.Services;
    // Request model
    public class RefreshTokenRequest : IRequest<ResultOperation<AuthResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpireAt { get; set; }   
        public DateTime RefreshTokenExpireAt { get; set; }
    }

// Handler 
public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, ResultOperation<AuthResponse>>
{
    private readonly UserManager<UserEntity> userManager;
    private readonly ITokenService jwtService;
    private readonly IAccessManagementDbContext context;


    public RefreshTokenHandler(UserManager<UserEntity> userManager,
                               ITokenService tokenService,
                               IAccessManagementDbContext context)
    {
        this.userManager = userManager;
        this.jwtService = tokenService;
        this.context = context;
    }

    public async Task<AuthResponse> Handle(RefreshTokenRequest request,
                                           CancellationToken cancellationToken)
    {
        //// Validate access token
        //var principal = jwtService.GetPrincipal(request.AccessToken);

        //// Get user and validate refresh token
        //var user = await userManager.FindByNameAsync(principal?.Identity?.Name);
        //Context.UserTokens.Add(new UserTokenEntity
        //{
        //    RefreshTokenHash = request.RefreshToken
        //});
        //if (user.RefreshToken != request.RefreshToken ||
        //    user.RefreshTokenExpireTime < Base.Shared.Utility.Now)
        //{
        //    return null;
        //}

        //// Generate new tokens
        //var tokens = jwtService.GetAuthTokenResult(principal.Claims);

        //// Update user's refresh token
        //user.RefreshToken = tokens.RefreshToken;
        //user.RefreshTokenExpireTime = tokens.RefreshTokenExpires;

        //await userManager.UpdateAsync(user);

        //return tokens;
        throw new NotImplementedException();
    }

    Task<ResultOperation<AuthResponse>> IRequestHandler<RefreshTokenRequest, ResultOperation<AuthResponse>>.Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
