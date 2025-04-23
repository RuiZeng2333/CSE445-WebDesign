using System;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Web.UI;  // for IService1

namespace Assignment5
{
    public partial class WebForm1 : Page
    {
        protected void btnShowContent_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            txtContent.Text = "";

            string url = txtURL.Text.Trim();
            if (string.IsNullOrEmpty(url))
            {
                lblError.Text = "Please enter a valid URL.";
                return;
            }

            // 1) Configure WCF binding & endpoint
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
            {
                MaxReceivedMessageSize = 10_000_000
            };
            var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var address = new EndpointAddress("http://webstrar66.fulton.asu.edu/Page4/Service1.svc");
            var factory = new ChannelFactory<IService1>(binding, address);
            var client = factory.CreateChannel();

            try
            {
                // 2) Call the service
                string content = client.WebDownload(url);

                // 3) Clean up
                ((IClientChannel)client).Close();
                factory.Close();

                // 4) Display result or error
                if (content.StartsWith("Error:"))
                    lblError.Text = content;
                else
                    txtContent.Text = content;
            }
            catch (Exception ex)
            {
                ((IClientChannel)client).Abort();
                factory.Abort();
                lblError.Text = "Error: " + ex.Message;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string url = txtURL.Text.Trim();
            if (string.IsNullOrEmpty(url))
            {
                lblError.Text = "Please enter a valid URL.";
                return;
            }

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
            {
                MaxReceivedMessageSize = 10_000_000
            };
            var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var address = new EndpointAddress("http://webstrar66.fulton.asu.edu/Page4/Service1.svc");
            var factory = new ChannelFactory<IService1>(binding, address);
            var client = factory.CreateChannel();

            try
            {
                // Remove duplicate declaration of 'content'
                string content = client.WebDownload(url); // Single declaration here

                if (content.StartsWith("Error:"))
                {
                    lblError.Text = content;
                }
                else
                {
                    // Force download
                    Response.Clear();
                    Response.ContentType = "text/plain";
                    Response.AddHeader("Content-Disposition", "attachment; filename=WebDownload.txt");
                    Response.Write(content);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ((IClientChannel)client).Abort();
                factory.Abort();
                lblError.Text = "Error: " + ex.Message;
            }
            finally
            {
                // Ensure client is closed even if an error occurs
                ((IClientChannel)client).Close();
                factory.Close();
            }
        }
    }
}
