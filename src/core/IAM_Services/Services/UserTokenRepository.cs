
using AccessManagement.Data;
using AccessManagement.Entities;
using AccessManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AccessManagement.Services
{
    public interface IUserTokenRepository
    {

    }
    public class UserTokenRepository
    {

        private readonly IAccessManagementDbContext context;
        public UserTokenRepository(IAccessManagementDbContext context)
        {
            this.context = context;
        }

        public void SaveToken(ApplicationUserToken userToken)
        {
            context.UserTokens.Add(userToken);
            context.SaveChanges();
        }

        public ApplicationUserToken FindRefreshToken(string RefreshToken)
        {
            SecurityHelper securityHelper = new SecurityHelper();

            string RefreshTokenHash = securityHelper.Getsha256Hash(RefreshToken);
            var usertoken = context.UserTokens
                .SingleOrDefault(p => p.RefreshTokenHash == RefreshTokenHash);
            return usertoken;


        }

        public void DeleteToken(string RefreshToken)
        {
            var token = FindRefreshToken(RefreshToken);
            if(token != null)
            {
                context.UserTokens.Remove(token);
                context.SaveChanges();
            }
        }

        public bool CheckExistToken(string Token)
        {
            SecurityHelper securityHelper = new SecurityHelper();
            string tokenHash = securityHelper.Getsha256Hash(Token);
            var userToken = context.UserTokens.Where(p => p.TokenHash == tokenHash).FirstOrDefault();
            return userToken == null ? false : true;
        }

    }
}
