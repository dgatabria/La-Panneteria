using AbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BEPedidoInsumos : Entidad
    {
        public DateTime Fecha { get; set; }
        public DateTime FechaEntregado { get; set; }
        public List<BEStockInsumos> Insumos { get; set; }
        public string Estado { get; set; }
    }
}
