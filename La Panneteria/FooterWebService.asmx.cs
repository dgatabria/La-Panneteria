using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace La_Panneteria
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class FooterWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public Footer GetFooter()
        {
            return new Footer("Alberdi 534, CABA, Buenos Aires", "4635-3753", "consultas@panneteria.com", "@Panneteria");
        }
    }

    public class Footer
    {
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public string Instagram { get; set; }

        public Footer(string Dir, string Tel, string Mail, string Instagram)
        {
            this.Direccion = Dir;
            this.Telefono = Tel;
            this.Mail = Mail;
            this.Instagram = Instagram;
        }
    }
}
