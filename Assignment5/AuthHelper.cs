using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Assignment5  // Match your project's namespace
{
    public static class AuthHelper
    {
        private const string COOKIE_NAME = "MyAuth";
        private const string HMAC_KEY = "I-Love-Coooooodiiiiiiing-for-real!";

        public static void CreateAuthCookie(string email, string role)
        {
            string payload = $"{email}|{role}";
            string signature;

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(HMAC_KEY)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                signature = Convert.ToBase64String(hash);
            }

            string token = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{payload}:{signature}")
            );

            var cookie = new HttpCookie(COOKIE_NAME, token)
            {
                HttpOnly = true,
                Path = "/",
                Expires = DateTime.UtcNow.AddHours(2)
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static bool ValidateAuthCookie(out string email, out string role)
        {
            email = role = null;
            var cookie = HttpContext.Current.Request.Cookies[COOKIE_NAME];

            if (cookie == null) return false;

            try
            {
                string token = Encoding.UTF8.GetString(
                    Convert.FromBase64String(cookie.Value)
                );

                var parts = token.Split(new[] { ':' }, 2);
                if (parts.Length != 2) return false;

                string payload = parts[0];
                string signature = parts[1];

                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(HMAC_KEY)))
                {
                    byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                    string expected = Convert.ToBase64String(hash);
                    if (!AreEqual(signature, expected)) return false;
                }

                var payloadParts = payload.Split('|');
                if (payloadParts.Length != 2) return false;

                email = payloadParts[0];
                role = payloadParts[1];
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool AreEqual(string a, string b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}