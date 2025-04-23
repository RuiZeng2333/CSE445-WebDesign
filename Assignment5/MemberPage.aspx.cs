using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment5
{
    public partial class MemberPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["Role"] as string) != "member")
                Response.Redirect("~/Account/Login.aspx", true);
        }
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", true);
        }
    }
}