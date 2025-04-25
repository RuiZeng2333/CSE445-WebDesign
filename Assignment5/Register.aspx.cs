using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Assignment5WebApp
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnReg_Click(object sender, EventArgs e) {
            string emailIn = textEmail.Text.Trim();
            string passIn = txtPw.Text;
            string confirmPw = txtConfirmPw.Text;
            if (string.IsNullOrEmpty(emailIn) || string.IsNullOrEmpty(passIn) || string.IsNullOrEmpty(confirmPw))
            {
                lblMessage.Text = "Email and Password (as well as the Confirm Password) cannot be empty.";
                return;
            }

            if (passIn != confirmPw) {
                lblMessage.Text = "Passwords do not match.";
                return;
            }

            if (!Captcha1.IsCaptchaValid()) {
                lblMessage.Text = "Incorrect CAPTCHA. Please try again.";
            }
            string filePath = Server.MapPath("~/App_Data/Member.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode root = doc.DocumentElement;
            //Let's compare and check to see if the email is already exists;
            foreach (XmlNode member in root.SelectNodes("Member"))
            {
                if (member["Email"]?.InnerText.Trim() == emailIn)
                {
                    lblMessage.Text = "User already exists.";
                    return;
                }
            }
            //If the user info does not exist, we need to create and add new members to the Member.xml;
            XmlElement newMember = doc.CreateElement("Member");
            XmlElement emailNode = doc.CreateElement("Email");
            emailNode.InnerText = emailIn;

            XmlElement pwNode = doc.CreateElement("Password");
            pwNode.InnerText = EncryptHelper.EncryptPassword(passIn);

            XmlElement roleNode = doc.CreateElement("Role");
            roleNode.InnerText = "Member";

            // Add DownloadCount and Tier
            XmlElement downloadCountNode = doc.CreateElement("DownloadCount");
            downloadCountNode.InnerText = "0"; // Initialize to 0

            XmlElement tierNode = doc.CreateElement("Tier");
            tierNode.InnerText = "Standard"; // Default tier

            newMember.AppendChild(emailNode);
            newMember.AppendChild(pwNode);
            newMember.AppendChild(roleNode);
            newMember.AppendChild(downloadCountNode);
            newMember.AppendChild(tierNode);

            root.AppendChild(newMember);
            doc.Save(filePath);

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Registration successful! Redirecting to login page in 3 seconds...";

            //3 seconds;
            Response.AddHeader("REFRESH", "3;URL=Login.aspx");
        }
    }
}