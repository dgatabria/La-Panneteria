using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace La_Panneteria
{
    public partial class Home_Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CerrarSesion(object sender, EventArgs args)
        {
            HttpCookie cookie2 = new HttpCookie("SessionToken");
            cookie2.Expires = DateTime.Now;
            Response.Cookies.Add(cookie2);

            SessionManager.Logout();
            Response.Redirect("/Default");
        }
    }
}