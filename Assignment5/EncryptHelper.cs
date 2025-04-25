using System;
//This is required for SHA256 hashing;
using System.Security.Cryptography; 
using System.Text;

namespace Assignment5WebApp
{
    public class EncryptHelper
    {
        //The whole point of this helper function is that to help us successfully encrypt passwords;
        public static string EncryptPassword(string password)
        {
           using (SHA256 sha = SHA256.Create())
           {
                //Convert the password string into a byte array using UTF8 encoding; as required;
              byte[] bytes = Encoding.UTF8.GetBytes(password);
                //Compute the hash of the byte array using SHA256;
              byte[] hash = sha.ComputeHash(bytes);
                //Convert the hashed byte array into a base64-encoded string and return it; 
              return Convert.ToBase64String(hash);
           }
        }
    }
}
