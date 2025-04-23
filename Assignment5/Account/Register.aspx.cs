using System;
using System.Web.UI;
using System.Web.Security;

namespace Assignment5.Account
{
    public partial class Register : Page
    {
        protected void RegisterUser(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string username = Email.Text; // Email as username
                string password = Password.Text;

                try
                {
                    XmlUserManager.AddMember(username, password);
                    FormsAuthentication.SetAuthCookie(username, false);
                    Response.Redirect("~/MemberPage.aspx");
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = "Registration failed: " + ex.Message;
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}