using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace La_Panneteria
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void CerrarSesion(object sender, EventArgs args)
        {
            HttpCookie cookie2 = new HttpCookie("SessionToken");
            cookie2.Expires = DateTime.Now;
            Response.Cookies.Add(cookie2);

            SessionManager.Logout();
            Response.Redirect("/Default");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            FooterWebService WebService = new FooterWebService();
            Footer footer = WebService.GetFooter();

            Label1.Text = footer.Direccion;
            Label2.Text = footer.Telefono;
            Label3.Text = footer.Mail;
            Label4.Text = footer.Instagram;
        }
    }
}