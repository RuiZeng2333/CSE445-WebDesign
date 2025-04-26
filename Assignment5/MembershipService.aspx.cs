using System;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Security.Cryptography;

namespace Assignment5WebApp
{
    public partial class MembershipService : System.Web.UI.Page
    {
        private const string COOKIE_NAME = "MyAuth";
        private const string HMAC_KEY = "I-Love-Coooooodiiiiiiing-for-real!";
        private const string XML_PATH = "~/App_Data/Member.xml";
        private const int PRO_TIER_THRESHOLD = 5;

        protected void Page_Load(object sender, EventArgs e)
        {

            Response.ClearHeaders();
            Response.ClearContent();

            // Read action parameter
            string action = Request.QueryString["action"];

            // Validate request via authentication cookie
            string email = ValidateRequest();
            if (email == null)
            {
                SendErrorResponse("Authentication failed");
                return;
            }

            try
            {
                // Process based on action parameter
                string actionLower = (action ?? "").ToLower(); // Handle null and convert to lowercase
                switch (actionLower)
                {
                    case "update":
                        string tier = UpdateMemberDownloadCount(email);
                        SendJsonResponse(string.Format("{{\"tier\":\"{0}\",\"coupons\":{1}}}", tier, GetCouponAllowance(tier)));
                        break;

                    case "get":
                        tier = GetMemberTier(email);
                        SendJsonResponse(string.Format("{{\"tier\":\"{0}\",\"coupons\":{1}}}", tier, GetCouponAllowance(tier)));
                        break;

                    default:
                        SendErrorResponse("Invalid action specified");
                        break;
                }
            }
            catch (Exception ex)
            {
                SendErrorResponse("Error: " + ex.Message);
            }
        }

        // Validates the request by checking the authentication cookie
        private string ValidateRequest()
        {
            HttpCookie cookie = Request.Cookies[COOKIE_NAME];
            if (cookie == null) 
                {
                SendErrorResponse("Missing authentication cookie");
                return null;
                }

            string token;
            try
            {
                token = Encoding.UTF8.GetString(Convert.FromBase64String(cookie.Value));
            }
            catch
            {
                return null;
            }

            string[] parts = token.Split(new[] { ':' }, 2);
            if (parts.Length != 2) return null;

            string payload = parts[0];
            string signature = parts[1];

            // Verify HMAC signature
            string expected;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(HMAC_KEY)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                expected = Convert.ToBase64String(hash);
            }

            if (!AreEqual(signature, expected)) return null;

            // Extract email and role
            string[] payloadParts = payload.Split('|');
            if (payloadParts.Length != 2) return null;

            string email = payloadParts[0];
            string role = payloadParts[1];

            // Only members are allowed
            if (!role.Equals("Member", StringComparison.OrdinalIgnoreCase))
                return null;

            return email;
        }

        // Updates the member's download count and checks for tier upgrade
        private string UpdateMemberDownloadCount(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email", "Email cannot be null or empty");

            string filePath = Server.MapPath(XML_PATH);
            var doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(filePath);

            XmlNode userNode = doc.SelectSingleNode(string.Format("/Members/Member[Email='{0}']", email));
            if (userNode == null)
                return "Standard"; // Return default tier if member not found

            // Get or create download count node
            XmlNode countNode = userNode.SelectSingleNode("DownloadCount");
            if (countNode == null)
            {
                countNode = doc.CreateElement("DownloadCount");
                userNode.AppendChild(countNode);
                countNode.InnerText = "0";
            }

            // Increment download count
            int downloads = int.Parse(countNode.InnerText) + 1;
            countNode.InnerText = downloads.ToString();

            // Update tier based on download count
            XmlNode tierNode = userNode.SelectSingleNode("Tier");
            if (tierNode == null)
            {
                tierNode = doc.CreateElement("Tier");
                userNode.AppendChild(tierNode);
                tierNode.InnerText = "Standard";
            }

            // Determine tier based on download count
            string tier = downloads > PRO_TIER_THRESHOLD ? "Pro" : "Standard";
            tierNode.InnerText = tier;

            // Save changes to XML file
            doc.Save(filePath);
            return tier;
        }

        // Gets the member's current tier without updating download count
        private string GetMemberTier(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email", "Email cannot be null or empty");

            string filePath = Server.MapPath(XML_PATH);
            var doc = new XmlDocument();
            doc.XmlResolver = null;
            doc.Load(filePath);

            XmlNode userNode = doc.SelectSingleNode(string.Format("/Members/Member[Email='{0}']", email));
            if (userNode == null)
                return "Standard";

            XmlNode tierNode = userNode.SelectSingleNode("Tier");
            return tierNode != null ? tierNode.InnerText : "Standard";
        }

        // Gets the number of coupon downloads a member is entitled to based on their tier
        private int GetCouponAllowance(string tier)
        {
            return tier.Equals("Pro", StringComparison.OrdinalIgnoreCase) ? 2 : 1;
        }

        // Helper methods for responses
        private void SendJsonResponse(string json)
        {
            Response.Clear();
            Response.ContentType = "application/json";
            Response.Write(json);
            
        }

        private void SendErrorResponse(string errorMessage)
        {
            Response.Clear();
            Response.ContentType = "application/json";
            Response.Write(string.Format("{{\"error\":\"{0}\"}}", errorMessage));
            Response.StatusCode = 400;
            
        }

        // Constant-time string comparison to prevent timing attacks
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