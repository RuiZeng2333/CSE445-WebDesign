using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;

namespace Assignment5
{
    public static class XmlUserManager
    {
        private static string MemberPath = HostingEnvironment.MapPath("~/App_Data/Member.xml");
        private static string StaffPath = HostingEnvironment.MapPath("~/App_Data/Staff.xml");

        public static void AddMember(string username, string password)
        {
            AddUserToXml(MemberPath, username, password);
        }

        public static void AddStaff(string username, string password)
        {
            AddUserToXml(StaffPath, username, password);
        }

        private static void AddUserToXml(string filePath, string username, string password)
        {
            XDocument doc;
            try { doc = XDocument.Load(filePath); }
            catch { doc = new XDocument(new XElement("Users")); }

            doc.Root.Add(new XElement("User",
                new XElement("Username", username),
                new XElement("Password", password)
            ));
            doc.Save(filePath);
        }

        public static bool ValidateUser(string virtualPath, string username, string password)
        {
            var filePath = HttpContext.Current.Server.MapPath(virtualPath);
            if (!System.IO.File.Exists(filePath)) return false;

            var doc = XDocument.Load(filePath);
            return doc.Root
                      .Elements("User")
                      .Any(u =>
                         (string)u.Element("Username") == username &&
                         (string)u.Element("Password") == password
                       );
        }

        
    }
}