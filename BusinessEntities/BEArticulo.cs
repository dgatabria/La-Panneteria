using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractionLayer;

namespace BusinessEntities
{
    public class BEArticulo : Entidad
    {

        public string Descripcion { get; set; }
        public double PrecioUnitario { get; set; }

        public string RecetaENC { get; set; }

        public string URL { get; set; }
        public string Categoria { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }
    }

    public class BEStockArticulo : Entidad
    {

        public string Descripcion { get; set; }
        public int Cantidad { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
    public class BERecomendacionProducto
    {
        public BERecomendacionProducto(string producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }

        public string Producto { get; set; }
        public int Cantidad { get; set; }
    }

    public class BEVencimientoProductos
    {
        public BEVencimientoProductos(string producto, int cantidad, DateTime fecha)
        {
            Producto = producto;
            Cantidad = cantidad;
            Fecha = fecha;
        }

        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
