using AbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BECliente : Entidad
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public BEDomicilio Domicilio { get; set; }
        public string Telefono { get; set; }
        public string EMail { get; set; }

        public override string ToString()
        {
            return Nombre + " " + Apellido + "(" + DNI + ")";
        }
    }
}
