using Microsoft.AspNetCore.Mvc;

namespace IAM_Services.Services.User
{

    public class LoginResponse
    {
        public string Token { get; set; }
        public string TokenExpireTime { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpireTime { get; set; }
    }
}
