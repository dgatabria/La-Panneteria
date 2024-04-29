using AbstractionLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BEPedido : Entidad
    {
        public BECliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEntregado { get; set; }
        public string Estado { get; set; }
        public BEDomicilio Domicilio { get; set; }
        public BindingList<BEItem> Items;

        public List<BEPago> Pagos;

        public BEPedido()
        {
            Items = new BindingList<BEItem>();
            Pagos = new List<BEPago>();
        }

    }
}
