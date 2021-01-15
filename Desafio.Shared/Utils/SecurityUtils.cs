using System;
using System.Security.Cryptography;

namespace Desafio.Shared.Utils
{
    public class SecurityUtils
    {
        public static string HashSHA1(string plainText)
        {
            return GetSHA1HashData(plainText);
        }

        private static string GetSHA1HashData(string data)
        {
            var SHA1 = new SHA1CryptoServiceProvider();
            var byteV = System.Text.Encoding.UTF8.GetBytes(data);
            var byteH = SHA1.ComputeHash(byteV);

            SHA1.Clear();

            return Convert.ToBase64String(byteH);
        }
    }
}