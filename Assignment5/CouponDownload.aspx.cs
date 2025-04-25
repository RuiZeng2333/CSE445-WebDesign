using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Security.Cryptography;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Assignment5WebApp
{
    public partial class CouponDownload : Page
    {
        private const string COOKIE_NAME = "MyAuth";
        private const string HMAC_KEY = "I-Love-Coooooodiiiiiiing-for-real!";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Authentication and authorization (unchanged)
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
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(HMAC_KEY))) // Fixed line
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

            // Update member stats
            string tier = UpdateMemberStats(email);

            int couponCount = tier.Equals("Pro", StringComparison.OrdinalIgnoreCase) ? 2 : 1;
            // Generate PDF with existing logic
            GeneratePdf(hotelName, address, couponCount, email, tier);  // Always generate 1 coupon
        }

        private string UpdateMemberStats(string email)
        {
            string filePath = Server.MapPath("~/App_Data/Member.xml");
            var doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(filePath);

            XmlNode userNode = doc.SelectSingleNode($"/Members/Member[Email='{email}']");
            if (userNode == null) return "Standard";

            // Get or create download count
            XmlNode countNode = userNode.SelectSingleNode("DownloadCount");
            if (countNode == null)
            {
                countNode = doc.CreateElement("DownloadCount");
                userNode.AppendChild(countNode);
                countNode.InnerText = "0";
            }

            // Update count
            int downloads = int.Parse(countNode.InnerText) + 1;
            countNode.InnerText = downloads.ToString();

            // Update tier
            XmlNode tierNode = userNode.SelectSingleNode("Tier");
            if (tierNode == null)
            {
                tierNode = doc.CreateElement("Tier");
                userNode.AppendChild(tierNode);
                tierNode.InnerText = "Standard";
            }

            string tier = downloads > 5 ? "Pro" : "Standard";
            tierNode.InnerText = tier;

            doc.Save(filePath);
            return tier;
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
                doc.Add(new Paragraph($"Member Tier: {tier}"));
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

                    doc.Add(new Paragraph($"Coupon #{i + 1}:",
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
                Response.End();
            }
        }

        // Rest of the helper methods remain unchanged
        private void RedirectToLogin() => Response.Redirect("http://webstrar66.fulton.asu.edu/Page0/Login.aspx", true);

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