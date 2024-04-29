using AbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BEDomicilio : Entidad
    {
        public string Calle { get; set; }
        public string Altura { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string CP { get; set; }
        public string Localidad { get; set; }

        public override string ToString()
        {
            return Calle + " " + Altura + ", piso " + Piso + ", Depto " + Depto + ", " + Localidad;
        }
    }
}
