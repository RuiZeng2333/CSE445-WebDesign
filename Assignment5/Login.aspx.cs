using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Security.Cryptography;
using Assignment5WebApp;
using System.Text;

namespace Assignment5WebApp
{
    public partial class Login : System.Web.UI.Page
    {
        private const string COOKIE_NAME = "MyAuth";
        private const string HMAC_KEY = "I-Love-Coooooodiiiiiiing-for-real!";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // 1) Captcha check
            if (!Captcha1.IsCaptchaValid())
            {
                labelMessage.Text = "Incorrect CAPTCHA. Please try again.";
                return;
            }

            string emailIn = textEmail.Text.Trim();
            string passIn = txtPw.Text;
            string encryptedIn = EncryptHelper.EncryptPassword(passIn);

            // 2) Load Member.xml
            string filePath = Server.MapPath("~/App_Data/Member.xml");
            var doc = new XmlDocument();
            doc.Load(filePath);
            var members = doc.SelectNodes("/Members/Member");
            bool memFound = false;
            string role = "";

            foreach (XmlNode member in members)
            {
                string emailStored = member["Email"]?.InnerText.Trim();
                string passStored = member["Password"]?.InnerText.Trim();
                string roleStored = member["Role"]?.InnerText.Trim() ?? "Member";

                if (emailIn == emailStored && encryptedIn == passStored)
                {
                    memFound = true;
                    role = roleStored;

                    // 3) Build and sign cookie
                    string payload = emailIn + "|" + roleStored;
                    string signature;
                    using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(HMAC_KEY)))
                    {
                        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                        signature = Convert.ToBase64String(hash);
                    }

                    string token = Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(payload + ":" + signature)
                    );

                    var cookie = new HttpCookie(COOKIE_NAME, token)
                    {
                        HttpOnly = true,
                        Path = "/",
                        Expires = DateTime.UtcNow.AddHours(2)
                    };

                    Response.Cookies.Add(cookie);
                    break;
                }
            }

            // 4) Redirect on success
            if (memFound)
            {
                try
                {
                    if (role.Equals("Staff", StringComparison.OrdinalIgnoreCase))
                    {
                        Response.Redirect("~/StaffPage.aspx", false);
                    }
                    else
                    {
                        // This is the important line to ensure redirection to MemberPage.aspx
                        Response.Redirect("~/MemberPage.aspx", false);
                    }
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {
                    // Log the exception or display it for debugging
                    labelMessage.Text = "Redirect error: " + ex.Message;
                }
            }
            else
            {
                labelMessage.Text = "Invalid email or password.";
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }
    }
}
