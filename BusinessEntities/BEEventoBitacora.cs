using AbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BEEventoBitacora : Entidad
    {
        public string Actor { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }
        public string Modulo { get; set; }
        public string Criticidad { get; set; }
    }
}
