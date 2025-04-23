using System;
using System.Web;

namespace Assignment5
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Initialize application settings
            Application["TotalUsers"] = 0;
            Application["AppStartTime"] = DateTime.Now;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Redirect logic here
            if (Context.Request.Url.Host.Contains("localhost") ||
                Context.Request.Url.Host.Contains("127.0.0.1"))
            {
                Context.Response.RedirectPermanent("http://webstrar66.fulton.asu.edu/page4/Default.aspx");
            }
        }
    }
}