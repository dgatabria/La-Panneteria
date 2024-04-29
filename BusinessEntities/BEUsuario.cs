using AbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    [Serializable]
    public class BEUsuario : Entidad
    {
        public BEUsuario()
        {

        }
        public string username { get; set; }
        public string Hashedpassword { get; set; }
        public string Password { get; set; }
        public bool Locked { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public BEPerfil Perfil { get; set; }
        public BEIdioma Idioma { get; set; }

        public override string ToString()
        {
            return username + " (" + Nombre + " " + Apellido + ")";
        }

    }
}
