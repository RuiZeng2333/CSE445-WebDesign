using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment5WebApp
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //clear the login session;
            Session.Clear();
            //Redirect back to the login page;
            Response.Redirect("Login.aspx");
        }
    }
}