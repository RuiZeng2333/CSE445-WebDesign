using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Security.Cryptography;

namespace Assignment5WebApp
{
    public partial class MemberPage : System.Web.UI.Page
    {
        // must match the values in Login.aspx.cs
        private const string COOKIE_NAME = "MyAuth";
        private const string HMAC_KEY = "I-Love-Coooooodiiiiiiing-for-real!";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1) Read the cookie
                HttpCookie cookie = Request.Cookies[COOKIE_NAME];
                if (cookie == null)
                {
                    RedirectToLogin();
                    return;
                }

                // 2) Decode and split payload:signature
                string token;
                try
                {
                    token = Encoding.UTF8.GetString(
                        Convert.FromBase64String(cookie.Value));
                }
                catch
                {
                    RedirectToLogin();
                    return;
                }

                string[] parts = token.Split(new[] { ':' }, 2);
                if (parts.Length != 2)
                {
                    RedirectToLogin();
                    return;
                }

                string payload = parts[0];  // "email|role"
                string signature = parts[1];

                // 3) Verify HMAC
                string expected;
                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(HMAC_KEY)))
                {
                    byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                    expected = Convert.ToBase64String(hash);
                }
                if (!AreEqual(signature, expected))
                {
                    RedirectToLogin();
                    return;
                }

                // 4) Extract email and role
                string[] pr = payload.Split('|');
                if (pr.Length != 2)
                {
                    RedirectToLogin();
                    return;
                }
                string email = pr[0];
                string role = pr[1];

                // 5) Only members allowed
                if (!role.Equals("Member", StringComparison.OrdinalIgnoreCase))
                {
                    RedirectToLogin();
                    return;
                }

                // 6) Show welcome message
                lblWelcome.Text = "Hello, " + email + ". Welcome home!";

                // 7) Load tier/discount from Member.xml
                string filePath = Server.MapPath("~/App_Data/Member.xml");
                var doc = new XmlDocument();
                doc.Load(filePath);
                var members = doc.SelectNodes("/Members/Member");
                foreach (XmlNode m in members)
                {
                    string storedEmail = m["Email"]?.InnerText.Trim();
                    if (storedEmail == email)
                    {
                        lblEmail.Text = storedEmail;
                        lblTier.Text = m["Tier"]?.InnerText ?? "Standard";
                        lblDiscount.Text = m["Discount"]?.InnerText ?? "15%";
                        break;
                    }
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear the auth cookie
            var cookie = new HttpCookie(COOKIE_NAME, "")
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                Path = "/"
            };
            Response.Cookies.Add(cookie);

            // Redirect to login or default page
            Response.Redirect("~/CombinedCoupon.aspx", true);
        }

        private void RedirectToLogin()
        {
            Response.Redirect("~/Login.aspx", true);
        }

        // Constant-time comparison to mitigate timing attacks
        private bool AreEqual(string a, string b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
