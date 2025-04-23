using System;
using System.Security.Cryptography;
using System.Text;

namespace SecurityDLL
{
    public static class CryptoHelper
    {
        public static string HashPassword(string plainText)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}