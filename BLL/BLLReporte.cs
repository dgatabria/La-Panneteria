using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BusinessEntities;

namespace BLL
{
    public class BLLReporte
    {
        Acceso bd;
        public int VentasXMes(int mes)
        {
            int tmp = 0;
            DataTable tablas;
            string Query = "ReporteVentasPorMes";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Mes", mes.ToString());
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmp = Convert.ToInt32(fila["Ventas"]);
                }

            }
            return tmp;
        }
        public Dictionary<string, int> VentasXProdAnual()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            DataTable tablas;
            string Query = "ReporteVentasxProdAnual";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    dic.Add(fila["Descripcion"].ToString(), Convert.ToInt32(fila["CANTIDAD"]));
                }

            }
            return dic;
        }

        public Dictionary<string, int> Elaboracion(DateTime fechai, DateTime fechaf)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            DataTable tablas;
            string Query = "ReporteElaboracion";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Inicio", fechai.ToString());
            ht.Add("@Fin", fechaf.ToString());
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    dic.Add(fila["Descripcion"].ToString(), Convert.ToInt32(fila["Cantidad"]));
                }

            }
            return dic;
        }
        public Dictionary<string, int> ConsumoInsumos(DateTime fechai, DateTime fechaf)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            DataTable tablas;
            string Query = "ReporteConsumoInsumos";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Inicio", fechai.ToString());
            ht.Add("@Fin", fechaf.ToString());
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    dic.Add(fila["Descripcion"].ToString(), Convert.ToInt32(fila["Cantidad"]));
                }

            }
            return dic;
        }
        public List<BERecomendacionProducto> RecomendacionesElaboracion(DateTime fechai, DateTime fechaf)
        {
            List<BERecomendacionProducto> lr = new List<BERecomendacionProducto>();
            BERecomendacionProducto rec;
            DataTable tablas;
            string Query = "ReporteElaboracion";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Inicio", fechai.ToString());
            ht.Add("@Fin", fechaf.ToString());
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    rec = new BERecomendacionProducto(fila["Descripcion"].ToString(), Convert.ToInt32(fila["Cantidad"]));
                    lr.Add(rec);
                }

            }
            return lr;
        }

        public List<BERecomendacionInsumo> RecomendacionInsumos(DateTime fechai, DateTime fechaf)
        {
            List<BERecomendacionInsumo> lr = new List<BERecomendacionInsumo>();
            BERecomendacionInsumo rec;
            DataTable tablas;
            string Query = "ReporteConsumoInsumos";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Inicio", fechai.ToString());
            ht.Add("@Fin", fechaf.ToString());
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    rec = new BERecomendacionInsumo(fila["Descripcion"].ToString(), Convert.ToInt32(fila["Cantidad"]));
                    lr.Add(rec);
                }

            }
            return lr;
        }
        public List<BEVencimientoProductos> VencimientoProductos()
        {
            List<BEVencimientoProductos> lr = new List<BEVencimientoProductos>();
            BEVencimientoProductos rec;
            DataTable tablas;
            string Query = "ObtenerStockActualConFecha";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    rec = new BEVencimientoProductos(fila["Descripcion"].ToString(), Convert.ToInt32(fila["Cantidad"]), Convert.ToDateTime(fila["Fecha"]).AddDays(15));
                    lr.Add(rec);
                }

            }
            return lr;
        }
    }
}
