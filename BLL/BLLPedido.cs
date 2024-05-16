using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPedido
    {
        Acceso bd;

        public void Despachar(BEUsuario Autor, BEPedido Objeto)
        {
            string Query = "DespacharPedido";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@Codigo", Objeto.Codigo);


            bd.LeerSPRT(Query, ht);

        }
        public void VerificarExistencia(BEPedido Objeto)
        {
            string Query = "VerificarExistencia";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Codigo", Objeto.Codigo);

            if (bd.LeerSPRT(Query, ht) > 0)
            {
                throw new Exception("msgerror_ventas_nostock");
            }

        }
        public int Alta(BEUsuario Autor, BEPedido Objeto)
        {
            string Query = "GuardarPedido";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@Cliente", Objeto.Cliente.Codigo);

            // Ver que carajo hago con domicilio y cn items.
            BLLDomicilio bld = new BLLDomicilio();
            int i = bld.Guardar(Autor, Objeto.Domicilio);
            ht.Add("@DOMICILIO", i);

            i = bd.LeerSPRT(Query, ht);
            Objeto.Codigo = i;
            BLLItem bli = new BLLItem();
            foreach (BEItem item in Objeto.Items)
            {
                bli.Guardar(Autor, Objeto, item);
            }
            return i;

        }
        public int AplicarPago(BEUsuario Autor, BEPago Pago, BEPedido Objeto)
        {
            string Query = "AplicarPago";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@Pedido", Objeto.Codigo);
            ht.Add("@Monto", Pago.Monto);
            ht.Add("@NroTicket", Pago.NroTicket);

            return bd.LeerSPRT(Query, ht);

        }
        public int AplicarPagoYCerrar(BEUsuario Autor, BEPago Pago, BEPedido Objeto)
        {
            string Query = "AplicarPagoYCerrar";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@Pedido", Objeto.Codigo);
            ht.Add("@Monto", Pago.Monto);
            ht.Add("@NroTicket", Pago.NroTicket);

            return bd.LeerSPRT(Query, ht);

        }
        public List<BEPedido> ListarAbiertos()
        {
            List<BEPedido> ListaPedidos = new List<BEPedido>();
            BEPedido tmppedido;
            BEDomicilio tmpdomicilio = new BEDomicilio();
            BECliente tmpcliente;
            BEDomicilio tmpdimicilio = new BEDomicilio();
            List<BEItem> tmpitems = new List<BEItem>();
            BLLCliente blc = new BLLCliente();
            BLLItem bli = new BLLItem();
            BLLDomicilio bld = new BLLDomicilio();
            BLLPago blp = new BLLPago();

            DataTable tablas;
            string Query = "ListarPedidosAbiertos";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmppedido = new BEPedido();
                    tmpdimicilio = new BEDomicilio();
                    tmppedido.Codigo = Convert.ToInt32(fila["ID"]);
                    tmppedido.Fecha = Convert.ToDateTime(fila["Fecha"]);
                    try
                    {
                        tmppedido.FechaEntregado = Convert.ToDateTime(fila["FechaEntregado"]);
                    }
                    catch { }
                    tmppedido.Estado = fila["Estado"].ToString();
                    tmpcliente = new BECliente();
                    tmpcliente.Codigo = Convert.ToInt32(fila["Cliente"]);
                    tmppedido.Cliente = blc.ListarObjeto(tmpcliente);
                    tmpdimicilio.Codigo = Convert.ToInt32(fila["Domicilio"]);
                    tmppedido.Domicilio = bld.ListarObjeto(tmpdimicilio);
                    tmppedido.Items = bli.ListarTodoPorPedido(tmppedido);
                    tmppedido.Pagos = blp.ListarPagosPorPedido(tmppedido);

                    ListaPedidos.Add(tmppedido);
                }
            }
            return ListaPedidos;
        }

        public List<BEPedido> ListarDespachados()
        {
            List<BEPedido> ListaPedidos = new List<BEPedido>();
            BEPedido tmppedido;
            BEDomicilio tmpdomicilio = new BEDomicilio();
            BECliente tmpcliente;
            BEDomicilio tmpdimicilio = new BEDomicilio();
            List<BEItem> tmpitems = new List<BEItem>();
            BLLCliente blc = new BLLCliente();
            BLLItem bli = new BLLItem();
            BLLDomicilio bld = new BLLDomicilio();
            BLLPago blp = new BLLPago();

            DataTable tablas;
            string Query = "ListarPedidosDespachados";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmppedido = new BEPedido();
                    tmpdimicilio = new BEDomicilio();
                    tmppedido.Codigo = Convert.ToInt32(fila["ID"]);
                    tmppedido.Fecha = Convert.ToDateTime(fila["Fecha"]);
                    try
                    {
                        tmppedido.FechaEntregado = Convert.ToDateTime(fila["FechaEntregado"]);
                    }
                    catch { }
                    tmppedido.Estado = fila["Estado"].ToString();
                    tmpcliente = new BECliente();
                    tmpcliente.Codigo = Convert.ToInt32(fila["Cliente"]);
                    tmppedido.Cliente = blc.ListarObjeto(tmpcliente);
                    tmpdimicilio.Codigo = Convert.ToInt32(fila["Domicilio"]);
                    tmppedido.Domicilio = bld.ListarObjeto(tmpdimicilio);
                    tmppedido.Items = bli.ListarTodoPorPedido(tmppedido);
                    tmppedido.Pagos = blp.ListarPagosPorPedido(tmppedido);

                    ListaPedidos.Add(tmppedido);
                }
            }
            return ListaPedidos;
        }
        public BEPedido ListarDespachado(BEPedido pedido)
        {

            BEPedido tmppedido = new BEPedido();
            BEDomicilio tmpdomicilio = new BEDomicilio();
            BECliente tmpcliente;
            List<BEItem> tmpitems = new List<BEItem>();
            BLLCliente blc = new BLLCliente();
            BLLItem bli = new BLLItem();
            BLLDomicilio bld = new BLLDomicilio();
            BLLPago blp = new BLLPago();

            DataTable tablas;
            string Query = "ListarPedidoDespachado";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", pedido.Codigo);
            tablas = bd.LeerSP(Query, ht);

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmppedido = new BEPedido();
                    tmpdomicilio = new BEDomicilio();
                    tmppedido.Codigo = Convert.ToInt32(fila["ID"]);
                    tmppedido.Fecha = Convert.ToDateTime(fila["Fecha"]);
                    try
                    {
                        tmppedido.FechaEntregado = Convert.ToDateTime(fila["FechaEntregado"]);
                    }
                    catch { }
                    tmppedido.Estado = fila["Estado"].ToString();
                    tmpcliente = new BECliente();
                    tmpcliente.Codigo = Convert.ToInt32(fila["Cliente"]);
                    tmppedido.Cliente = blc.ListarObjeto(tmpcliente);
                    tmpdomicilio.Codigo = Convert.ToInt32(fila["Domicilio"]);
                    tmppedido.Domicilio = bld.ListarObjeto(tmpdomicilio);
                    tmppedido.Items = bli.ListarTodoPorPedido(tmppedido);
                    tmppedido.Pagos = blp.ListarPagosPorPedido(tmppedido);


                }
            }
            return tmppedido;
        }
    }
}
