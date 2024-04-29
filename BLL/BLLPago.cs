using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPago
    {
        Acceso bd;
        public List<BEPago> ListarPagosPorPedido(BEPedido Objeto)
        {
            List<BEPago> lista = new List<BEPago>();

            BEPago tmppago = new BEPago();

            DataTable tablas;
            string Query = "ListarPagosPorPedido";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);


            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {

                    tmppago = new BEPago();
                    tmppago.Codigo = Convert.ToInt32(fila["ID"]);
                    tmppago.NroTicket = Convert.ToInt32(fila["NroTicket"]);
                    tmppago.Monto = Convert.ToDouble(fila["Monto"]);
                    tmppago.Fecha = Convert.ToDateTime(fila["Fecha"]);

                    lista.Add(tmppago);
                }
            }
            return lista;
        }
    }
}
