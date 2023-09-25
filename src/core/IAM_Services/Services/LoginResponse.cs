using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Services
{
  
    public class LoginResponse
    {
        public string Token { get; set; }
        public string TokenExpireTime { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpireTime { get; set; }
    }
}
    