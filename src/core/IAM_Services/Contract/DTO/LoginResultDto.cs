using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Contract
{
    public class LoginResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public LoginDataDto Data { get; set; }
    }

    public class LoginDataDto
    {
        public string Token { get; set; }
        
        public DateTime TokenExp { get; set; }
        
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExp { get; set; } 
    }
}
