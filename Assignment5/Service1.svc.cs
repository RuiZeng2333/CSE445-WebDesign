using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;      
using SecurityDLL;

namespace Assignment5
{
    public class Service1 : IService1
    {
        public string WebDownload(string url)
        {
            try
            {
                using (var wc = new WebClient())
                {
                    wc.Headers["User-Agent"] = "Mozilla/5.0";
                    return wc.DownloadString(url);
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
