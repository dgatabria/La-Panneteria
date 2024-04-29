using AbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BEInsumo : Entidad
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
    public class BEStockInsumos : Entidad
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }

    public class BERecomendacionInsumo
    {
        public BERecomendacionInsumo(string insumo, int cantidad)
        {
            this.Insumo = insumo;
            this.Cantidad = cantidad;
        }

        public string Insumo { get; set; }
        public int Cantidad { get; set; }
    }
}
