using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment5WebApp
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                GenerateCaptcha();
            }
        }
        private void GenerateCaptcha() {
            string code = new Random().Next(1000, 9999).ToString();
            labelCaptcha.Text = code;
            Session["captcha"] = code;
        }
        public bool IsCaptchaValid() {
            return txtCaptchaIn.Text == Session["captcha"]?.ToString();
        }
    }
}