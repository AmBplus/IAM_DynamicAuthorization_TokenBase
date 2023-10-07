using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Helper
{
    public  static class SecurityHelper
    {

        
        public static string Getsha256Hash(string value)
        {
            RandomNumberGenerator random = RandomNumberGenerator.Create();
            var algorithm = new SHA256CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(value);
            var byteHash = algorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}
