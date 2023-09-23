
using AccessManagement.Contract;
using AccessManagement.Data;
using AccessManagement.Entities;
using AccessManagement.Helper;
using Base.Shared.ResultUtility;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace AccessManagement.Services
{
    
    public class UserTokenRepository : IUserTokenRepository
    {

        private readonly IAccessManagementDbContext context;
        private readonly JwtSettings jwtSettings;

        public UserTokenRepository(IAccessManagementDbContext context,JwtSettings jwtSettings)
        {
            this.context = context;
            this.jwtSettings = jwtSettings;
        }

    
   
    
 
        public void SaveToken(SaveTokenRequest request)
        {
            context.UserTokens.Add(request.UserToken);
            context.SaveChanges();
        }

        public ApplicationUserToken FindRefreshToken(FindRefreshTokenRequest request)
        {
            SecurityHelper securityHelper = new SecurityHelper();

            string RefreshTokenHash = securityHelper.Getsha256Hash(request.RefreshToken);
            var usertoken = context.UserTokens
                .SingleOrDefault(p => p.RefreshTokenHash == RefreshTokenHash);
            return usertoken;

        }

        public ResultOperation<ResponseCreateToken> CreateToken(ApplicationUser user)
        {
            SecurityHelper securityHelper = new SecurityHelper();


            var claims = new List<Claim>
                {
                    new Claim ("UserId", user.Id.ToString()),
                    new Claim ("Name",  user?.UserName??""),
                };
            string key = jwtSettings.Secret;
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenexp = DateTime.Now.AddMinutes(jwtSettings.TokenValidityInMinutes);
            var token = new JwtSecurityToken(
                issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                expires: tokenexp,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid();

            context.UserTokens.Add(new ApplicationUserToken()
            {
                MobileModel = "Iphone pro max",
                TokenExp = tokenexp,
                TokenHash = securityHelper.Getsha256Hash(jwtToken),
                User = user,
                RefreshTokenHash = securityHelper.Getsha256Hash(refreshToken.ToString()),
                RefreshTokenExp = DateTime.Now.AddDays(30)
            });
            return new ResponseCreateToken().ToSuccessResult();
        }

        public void DeleteToken(DeleteTokenRequest request)
        {
            var token = FindRefreshToken(new FindRefreshTokenRequest(request.RefreshToken));
            if (token != null)
            {
                context.UserTokens.Remove(token);
                context.SaveChanges();
            }
        }

        public bool CheckExistToken(CheckExitTokenRequest request)
        {
            SecurityHelper securityHelper = new SecurityHelper();
            string tokenHash = securityHelper.Getsha256Hash(request.Token);
            var userToken = context.UserTokens.Where(p => p.TokenHash == tokenHash).FirstOrDefault();
            return userToken == null ? false : true;
        }
    }
}
