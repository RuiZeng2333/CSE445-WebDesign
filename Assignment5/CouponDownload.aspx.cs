using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Security.Cryptography;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Assignment5WebApp
{
    public partial class CouponDownload : Page
    {
        private const string COOKIE_NAME = "MyAuth";
        private const string HMAC_KEY = "I-Love-Coooooodiiiiiiing-for-real!";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Authentication and authorization
            HttpCookie cookie = Request.Cookies[COOKIE_NAME];
            if (cookie == null) RedirectToLogin();

            string token;
            try { token = Encoding.UTF8.GetString(Convert.FromBase64String(cookie.Value)); }
            catch { RedirectToLogin(); return; }

            var parts = token.Split(new[] { ':' }, 2);
            if (parts.Length != 2) RedirectToLogin();

            string payload = parts[0];
            string signature = parts[1];

            string expected;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(HMAC_KEY)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                expected = Convert.ToBase64String(hash);
            }

            if (!AreEqual(signature, expected)) RedirectToLogin();

            var payloadParts = payload.Split('|');
            if (payloadParts.Length != 2) RedirectToLogin();

            string email = payloadParts[0];
            string role = payloadParts[1];

            if (!role.Equals("Member", StringComparison.OrdinalIgnoreCase))
                RedirectToLogin();

            // Get request parameters
            string hotelName = Request.QueryString["hotelName"];
            string address = Request.QueryString["address"];
            if (string.IsNullOrWhiteSpace(hotelName) || string.IsNullOrWhiteSpace(address))
            {
                Response.Write("Invalid request");
                return;
            }

            // Call the MembershipService to update download count
            string serviceUrl = "~/MembershipService.aspx";

            // We'll use the current cookie for authentication with the service
            string tierData = CallMembershipService(serviceUrl);

            // Parse the response
            var serializer = new JavaScriptSerializer();
            dynamic result = serializer.Deserialize<dynamic>(tierData);

            string tier = result["tier"];
            int couponCount = result["coupons"];

            // Generate PDF with coupon count
            GeneratePdf(hotelName, address, couponCount, email, tier);
        }

        // Method to call the membership service
        private string CallMembershipService(string serviceUrl)
        {
            try
            {
                // Resolve the relative path to an absolute URL
                string resolvedPath = ResolveUrl("~/MembershipService.aspx");
                Uri baseUri = new Uri(Request.Url.GetLeftPart(UriPartial.Authority));
                Uri fullUri = new Uri(baseUri, resolvedPath);

                // Add the action parameter
                string fullUrl = fullUri.ToString() + "?action=update";

                // Create request with the current cookie
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullUrl);
                request.CookieContainer = new CookieContainer();

                // Add authentication cookie to the request
                HttpCookie authCookie = Request.Cookies[COOKIE_NAME];
                if (authCookie != null)
                {
                    Cookie cookie = new Cookie(COOKIE_NAME, authCookie.Value)
                    {
                        Domain = baseUri.Host, // Use the current domain (e.g., localhost)
                        Path = "/"
                    };
                    request.CookieContainer.Add(cookie);
                }

                // Get response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string rawResponse = reader.ReadToEnd();
                    System.Diagnostics.Debug.WriteLine("Service Response: " + rawResponse); // Log the response
                    return rawResponse;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string errorResponse = reader.ReadToEnd();
                        throw new Exception("Service error: " + errorResponse);
                    }
                }
                throw new Exception("Error calling membership service: " + ex.Message);
            }
        }

        // Pdf generation logic
        private void GeneratePdf(string hotelName, string address, int count, string member, string tier)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var doc = new Document(PageSize.A4, 36, 36, 36, 36);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // Header with tier information
                doc.Add(new Paragraph("Hotel Coupon", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)));
                doc.Add(new Paragraph(string.Format("Member Tier: {0}", tier)));
                doc.Add(new Paragraph("Hotel: " + hotelName));
                doc.Add(new Paragraph("Address: " + address));
                doc.Add(new Paragraph("Member: " + member));
                doc.Add(new Paragraph(" "));

                // Generate multiple coupons based on count
                for (int i = 0; i < count; i++)
                {
                    var rnd = new Random();
                    string firstPart = string.Concat(
                        Enumerable.Range(0, 6)
                                .Select(_ => (char)('A' + rnd.Next(26)))
                                .ToArray()
                    );

                    string middlePart = string.Concat(
                        Enumerable.Range(0, 6)
                                .Select(_ => (char)('A' + rnd.Next(26)))
                                .ToArray()
                    );

                    string lastPart = string.Concat(
                        Enumerable.Range(0, 6)
                                .Select(_ => (char)('A' + rnd.Next(26)))
                                .ToArray()
                    );

                    string couponCode = String.Format("{0}-{1}-{2}", firstPart, middlePart, lastPart);

                    doc.Add(new Paragraph(string.Format("Coupon #{0}:", i + 1),
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    doc.Add(new Paragraph(couponCode,
                        FontFactory.GetFont(FontFactory.COURIER, 14)));
                    doc.Add(new Paragraph(" "));
                }

                doc.Close();

                byte[] pdfBytes = ms.ToArray();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=HotelCoupon.pdf");
                Response.BinaryWrite(pdfBytes);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        // Helper methods
        private void RedirectToLogin()
        {
            Response.Redirect("~/Login.aspx", true);
        }

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