using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;      
using SecurityDLL;
using iTextSharp.text;
using iTextSharp.text.pdf;

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

        public byte[] GenerateCouponPdf(string hotelName, string address, int couponCount)
        {
            using (var ms = new MemoryStream())
            {
                var doc = new Document(PageSize.A4, 36, 36, 36, 36);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // Hotel info
                doc.Add(new Paragraph(hotelName) { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph(address) { SpacingAfter = 20f });

                // Random codes
                var rnd = new Random();
                for (int i = 0; i < couponCount; i++)
                {
                    string code = GenerateCode(rnd);
                    doc.Add(new Paragraph(code));
                }

                doc.Close();
                return ms.ToArray();
            }
        }

        private string GenerateCode(Random rnd)
        {
            string Part() =>
                new string(Enumerable.Range(0, 6)
                    .Select(_ => (char)('A' + rnd.Next(26)))
                    .ToArray());

            return $"{Part()}-{Part()}-{Part()}";
        }
    }
}
