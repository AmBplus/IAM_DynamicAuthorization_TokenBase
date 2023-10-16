using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Base.Shared.Security
{
    public class HashHelper
    {
        public static string GetSha256(string text)
        {
            var inputBytes =
                System.Text.Encoding.UTF8.GetBytes(s: text);

            var sha =
                System.Security.Cryptography.SHA256.Create();

            var outputBytes =
                sha.ComputeHash(buffer: inputBytes);

            sha.Dispose();
            //sha = null;

            var result =
                System.Convert.ToBase64String(inArray: outputBytes);

            return result;
        }
    }
}

