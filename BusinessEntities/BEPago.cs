using AbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BEPago : Entidad
    {
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public int NroTicket { get; set; }

    }
}
