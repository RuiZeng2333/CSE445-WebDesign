using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Assignment5.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If they hit Login.aspx but already have a valid auth cookie,
            // send them straight into the member area by default:
            if (User.Identity.IsAuthenticated)
                Response.Redirect("~/MemberPage.aspx", true);
        }

        protected void LogIn(object sender, EventArgs e)
        {
            string user = Email.Text.Trim();
            string pass = Password.Text;

            // 1) Member?
            if (XmlUserManager.ValidateUser("~/App_Data/Member.xml", user, pass))
            {
                Session["Username"] = user;
                Session["Role"] = "member";
                Response.Redirect("~/MemberPage.aspx", true);
                return;
            }

            // 2) Staff?
            if (XmlUserManager.ValidateUser("~/App_Data/Staff.xml", user, pass))
            {
                Session["Username"] = user;
                Session["Role"] = "staff";
                Response.Redirect("~/StaffPage.aspx", true);
                return;
            }

            // 3) No match
            FailureText.Text = "Invalid username or password.";
            ErrorMessage.Visible = true;
        }
    }
}
