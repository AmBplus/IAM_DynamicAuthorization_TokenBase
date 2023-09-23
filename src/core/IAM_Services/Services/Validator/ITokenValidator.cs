using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AccessManagement.Contract;
namespace AccessManagement.Services
{
  
    public class TokenValidate : ITokenValidator
    {
        private UserRepository userRepository;
        private UserTokenRepository userTokenRepository;
        public TokenValidate(UserRepository userRepository, UserTokenRepository userTokenRepository)
        {
            this.userRepository = userRepository;
            this.userTokenRepository = userTokenRepository;
        }
        public async Task Execute(TokenValidatedContext context)
        {
            var claimsidentity = context.Principal.Identity as ClaimsIdentity;
            if(claimsidentity?.Claims == null  || !claimsidentity.Claims.Any()) 
            {
                context.Fail("claims not found....");
                return;
            }

            var userId = claimsidentity.FindFirst("UserId").Value;
            if(!Guid.TryParse(userId, out Guid userGuid))
            {
                context.Fail("claims not found....");
                return;
            }

            var user=  userRepository.GetUser(new GetUserRequest(userGuid));

            if(user.IsActive == false)
            {
                context.Fail("User not Active");
                return;
            }
           
   
             if(!(context.SecurityToken is JwtSecurityToken Token)
                || !userTokenRepository.CheckExistToken(new CheckExitTokenRequest(Token.RawData)))
            {
                context.Fail("توکد در دیتابیس وجود ندارد");
                return;
            }


        }
    }

}
