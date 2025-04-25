using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using SecurityDLL;

namespace Assignment5
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAuth = Session["Username"] != null;
            pnlLoggedIn.Visible = isAuth;


            if (isAuth)
                lblUsername.Text = (string)Session["Username"];
        }

        protected void btnTryIt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInput.Text))
                lblHashResult.Text = CryptoHelper.HashPassword(txtInput.Text);
        }

        protected void btnMember_Click(object s, EventArgs e)
        {
            if (Session["Role"] as string == "member")
                Response.Redirect("~/MemberPage.aspx", true);
            else
                Response.Redirect("~/Account/Login.aspx", true);
        }

        protected void btnStaff_Click(object s, EventArgs e)
        {
            if (Session["Role"] as string == "staff")
                Response.Redirect("~/StaffPage.aspx", true);
            else
                Response.Redirect("~/Account/Login.aspx", true);
        }

        protected void btnLogout_Click(object s, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Default.aspx", true);
        }

        protected void btnWebDownloadTryIt_Click(object sender, EventArgs e)
        {
            // Navigate to your TryIt page
            Response.Redirect("~/WebDownloadTryIt.aspx", true);
        }

        protected void btnCreateCookie_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCookieEmail.Text))
            {
                AuthHelper.CreateAuthCookie(
                    txtCookieEmail.Text.Trim(),
                    ddlCookieRole.SelectedValue
                );
                lblCookieResult.Text = "Cookie created successfully!";
            }
        }

        protected void btnValidateCookie_Click(object sender, EventArgs e)
        {
            if (AuthHelper.ValidateAuthCookie(out string email, out string role))
            {
                lblCookieResult.Text = $"Valid cookie!<br>Email: {email}<br>Role: {role}";
            }
            else
            {
                lblCookieResult.Text = "Invalid or missing authentication cookie";
            }
        }
    }
}
