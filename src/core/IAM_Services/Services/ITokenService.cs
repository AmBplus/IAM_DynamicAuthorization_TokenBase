
using AccessManagement.Services;
using Base.Shared;
using Base.Shared.ResultUtility;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace AccessManagement.Services;

    public class JwtService : IJwtService
    {
  
        private JwtSettings JwtSettings { get; set; }

    public JwtService(JwtSettings jwtSettings)
    {
        JwtSettings = jwtSettings;
    }


     public (string,DateTime) GenerateToken(IEnumerable<Claim> claims)
        {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var expire = Base.Shared.Utility.Now.AddMinutes(JwtSettings.TokenValidityInMinutes);
        var tokeOptions = new JwtSecurityToken(
            issuer: JwtSettings.ValidIssuer,
            audience: JwtSettings.ValidAudience,
            claims: claims,
            expires: expire,
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        
        return (tokenString, expire);
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null) return null;

                var symmetricKey = Convert.FromBase64String(JwtSettings.Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;

            }
            catch (Exception)
            {
                return null;
            }
        }

    }

    public interface IJwtService
    {
    (string, DateTime) GenerateToken(IEnumerable<Claim> claims);
        ClaimsPrincipal GetPrincipal(string token);

    }

    public interface ITokenService {
         AuthResponse GetAuthTokenResult(IEnumerable<Claim> claims);
    ClaimsPrincipal GetPrincipal(string token);
    }

public class TokenService : ITokenService
{
    IJwtService jwtService;
    JwtSettings JwtSettings;
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public TokenService(IJwtService jwtService, JwtSettings jwtSettings = null)
    {
        this.jwtService = jwtService;
        JwtSettings = jwtSettings;
    }

    public AuthResponse GetAuthTokenResult(IEnumerable<Claim> claims)
    {
        var token  = jwtService.GenerateToken(claims);
        var refreshToken = GenerateRefreshToken();
        return new AuthResponse()
        {
            RefreshToken = refreshToken,
            AccessToken = token.Item1,
            AccessTokenExpires = token.Item2,
            RefreshTokenExpires = Base.Shared.Utility.Now.AddDays(JwtSettings.RefreshTokenValidityInDays)
        };
    }
    public ClaimsPrincipal GetPrincipal(string token)
    {
       return jwtService.GetPrincipal(token);
    }
}

 