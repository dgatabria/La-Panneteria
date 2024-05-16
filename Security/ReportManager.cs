using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BLL;

namespace Security
{
    public class ReportManager
    {
        BLLReporte blr = new BLLReporte();

        public int VentasXMes(int mes)
        {
            return blr.VentasXMes(mes);
        }
        public Dictionary<string, int> VentasXProdAnual()
        {
            return blr.VentasXProdAnual();
        }

        public Dictionary<string, int> Elaboracion(DateTime fechai, DateTime fechaf)
        {
            return blr.Elaboracion(fechai, fechaf);
        }
        public Dictionary<string, int> ConsumoInsumos(DateTime fechai, DateTime fechaf)
        {
            return blr.ConsumoInsumos(fechai, fechaf);
        }
        public List<BERecomendacionInsumo> RecomendacionInsumos(DateTime fechai, DateTime fechaf)
        {
            return blr.RecomendacionInsumos(fechai, fechaf);
        }
        public List<BERecomendacionProducto> RecomendacionesElaboracion(DateTime fechai, DateTime fechaf)
        {
            return blr.RecomendacionesElaboracion(fechai, fechaf);

        }
        public List<BEVencimientoProductos> VencimientoProductos()
        {
            return blr.VencimientoProductos();

        }
    }
}
