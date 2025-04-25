using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Assignment5WebApp
{
    public class encrypt
    {
        //Let's encrypt the input using AES and return base64 encoded result;
        //My teammate works on the decryption part, I need to make sure we use the same AES method for encryption and decryption;
        public static string Encrypt(string plainText, string passPhrase) {
            if (plainText == null) throw new ArgumentNullException(nameof(plainText));
            if (passPhrase == null) throw new ArgumentNullException(nameof(passPhrase));

            byte[] key = GenerateKeyFromPassphrase(passPhrase);
            using (var aes = Aes.Create()) {
                aes.KeySize = 256;
                aes.Key = key;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.GenerateIV();

                using (var ms = new MemoryStream()) {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private static byte[] GenerateKeyFromPassphrase(string passPhrase) {
            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(passPhrase));
        }
    }
}