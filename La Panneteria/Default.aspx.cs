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
                Response.Write("Excepcion: " + ex.Message);
                return;
                if (ex.Message == "admin_start_recovery")
                {
                    // hacer la parte de recovery
                    Response.Write("Hay que hacer el recovery del dv");
                }
                /*
                Regex re = new Regex("^msgerror_sessionmgr");

                if (re.IsMatch(ex.Message))
                {
                    
                    MessageBox.Show((comboBox1.SelectedItem as BEIdioma).Palabras[ex.Message].ToString());
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }*/
            }
            Response.Write("Se hizo el login OK");

        }

    }
}