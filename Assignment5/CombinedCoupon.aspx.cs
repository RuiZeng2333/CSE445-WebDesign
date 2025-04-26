using System;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using Assignment5;


namespace Assignment5WebApp
{
    public partial class TryIt : Page
    {
        /*        protected void Page_Load(object sender, EventArgs e)
                {
                    // Only members get in
                    if ((Session["Role"] as string) != "member")
                        Response.Redirect("~/Account/Login.aspx", true);


                }*/
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // send them to your login page
            HttpCookie cookie = Request.Cookies["MyAuth"];

            if (cookie != null)
            {
                try
                {
                    // Decode and verify the cookie
                    string token = System.Text.Encoding.UTF8.GetString(
                        Convert.FromBase64String(cookie.Value));

                    string[] parts = token.Split(new[] { ':' }, 2);
                    if (parts.Length == 2)
                    {
                        string payload = parts[0];
                        string[] payloadParts = payload.Split('|');

                        if (payloadParts.Length == 2)
                        {
                            string role = payloadParts[1];

                            // Redirect based on role
                            if (role.Equals("Staff", StringComparison.OrdinalIgnoreCase))
                            {
                                Response.Redirect("~/StaffPage.aspx", false);
                                Context.ApplicationInstance.CompleteRequest();
                                return;
                            }
                            else if (role.Equals("Member", StringComparison.OrdinalIgnoreCase))
                            {
                                Response.Redirect("~/MemberPage.aspx", false);
                                Context.ApplicationInstance.CompleteRequest();
                                return;
                            }
                        }
                    }
                }
                catch
                {
                    Response.Redirect("~/Login.aspx", true); // go to login if error occur
                }
            }
            Response.Redirect("~/Login.aspx", true);
        }

        protected void btnGetHotels_Click(object sender, EventArgs e)
        {
            //  Call  WCF service 
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
            {
                MaxReceivedMessageSize = 10000000
            };
            var endpoint = new EndpointAddress("http://webstrar66.fulton.asu.edu/Page0/Service1.svc");
            var factory = new ChannelFactory<IService1>(binding, endpoint);
            var client = factory.CreateChannel();

            // hotel list
            var hotels = new[]
            {
                new { Name="Desert Oasis",  Address="123 Sun Blvd, Phoenix, AZ" },
                new { Name="Canyon Retreat", Address="456 Cliff Rd, Sedona, AZ" },
                new { Name="Scottsdale Springs Resort",   Address="789 Camelback Rd, Scottsdale, AZ" },
                new { Name="Sedona Red Rocks Resort",     Address="101 Bell Rock View, Sedona, AZ" },
                new { Name="Grand Canyon Lodge",          Address="1000 Grand Canyon Plaza, Grand Canyon, AZ" },
                new { Name="Tucson Sun Palace",           Address="550 E Broadway Blvd, Tucson, AZ" },
                new { Name="Phoenix Palm Suites",         Address="202 E McDowell Rd, Phoenix, AZ" },
                new { Name="Flagstaff Mountain Lodge",    Address="98 N Turquoise Dr, Flagstaff, AZ" },
                new { Name="Yuma Riverside Hotel",        Address="350 Colorado River Rd, Yuma, AZ" },
                new { Name="Mesa Verde Inn",              Address="1230 S Main St, Mesa, AZ" },
                new { Name="Tempe Town Plaza Hotel",      Address="400 W University Dr, Tempe, AZ" },
                new { Name="Lake Havasu Beachside Inn",   Address="2200 London Bridge Rd, Lake Havasu City, AZ" }
            };

            ((IClientChannel)client).Close();
            factory.Close();

            // 2) Build UL of links back to this same page, passing hotelName+address to CouponDownload.aspx
            var sb = new System.Text.StringBuilder();
            sb.Append("<ul class='hotel-list'>");
            foreach (var h in hotels)
            {
                var n = HttpUtility.UrlEncode(h.Name);
                var a = HttpUtility.UrlEncode(h.Address);

                sb.Append("<li>");
                sb.Append("<span>" + h.Name + " \u2014 " + h.Address + "</span>");
                sb.Append("<a class='coupon-btn' href='CouponDownload.aspx?hotelName="
                    + n + "&address=" + a + "&count=5'>");
                sb.Append("Download Coupons</a>");
                sb.Append("</li>");
            }
            sb.Append("</ul>");

            litResult.Text = sb.ToString();
        }


    }
}
