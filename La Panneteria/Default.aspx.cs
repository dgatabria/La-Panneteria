using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using Security;
using AbstractionLayer;
using System.Text.RegularExpressions;

namespace La_Panneteria
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_ServerClick()
        {

        }
        protected void do_login(object sender, EventArgs e)
        {
            BEUsuario usuario = new BEUsuario();
            usuario.username = Request.Form["email"];
            usuario.Password = Request.Form["password"];
            try
            {
                SessionManager.Login(usuario);
            }
            catch (Exception ex)
            {
                //
                
                if (ex.Message == "admin_start_recovery")
                {
                    // El DV está roto y el usuario es admin. Hacer la parte de recovery
                    //Response.Write("Hay que hacer el recovery del dv");
                    //Server.Transfer("/MantenimientoDV",true);
                    try
                    {
                        SessionManager.WebMasterLogin(usuario);
                        HttpCookie cookie2 = new HttpCookie("SessionToken");
                        cookie2.Secure = true;
                        cookie2.Value = SessionManager.GetInstance.GetSessionToken();
                        Response.Cookies.Add(cookie2);
                    } catch (Exception ex2) {
                        Response.Write("<script>alert(\"La base está dañada y no funciona la cuenta de webmaster. Excepcion:" + ex2.Message +" Se recomienda restaurar la base.\")</script>");
                        return;
                    }
                    
                    Response.Redirect("/MantenimientoDV");
                    return;
                }
                if (ex.Message == "msgerror_sessionmgr_errordv")
                {
                    // el DV está mal, pero el usuario no es admin.
                    Response.Write("<script>alert(\"El sistema no está disponible en este momento. Inténtelo más tarde.\")</script>");
                    return;
                }
                Response.Write("Excepcion: " + ex.Message);
                return;
            }
            HttpCookie cookie = new HttpCookie("SessionToken");
            cookie.Secure = true;
            cookie.Value = SessionManager.GetInstance.GetSessionToken();
            Response.Cookies.Add(cookie);
            switch (SessionManager.GetInstance.Usuario.Perfil.Nombre)
            {
                case "CLIENTE":
                    Response.Redirect("/Main");
                    break;
                case "WEBMASTER":
                    Response.Redirect("/Home-WebMaster");
                    break;
                case "ADMIN":
                    Response.Redirect("/Home-Admin");
                    break;

            }
            
            //Response.Write("Se hizo el login OK");

        }
        protected void do_signup(object sender, EventArgs e)
        {
            BEUsuario usuario = new BEUsuario();
            usuario.username = Request.Form["email"];
            usuario.Password = Request.Form["password"];
            try
            {
                SessionManager.Login(usuario);
            }
            catch (Exception ex)
            {
                Response.Write("Excepcion: " + ex.Message);
                return;
 
            }
            HttpCookie cookie = new HttpCookie("SessionToken");
            cookie.Secure = true;
            cookie.Value = SessionManager.GetInstance.GetSessionToken();
            Response.Cookies.Add(cookie);
            Response.Redirect("/Main");
            //Response.Write("Se hizo el login OK");

        }

    }
}