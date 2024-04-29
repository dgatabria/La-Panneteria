using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLInsumo
    {
        Acceso bd;
        public List<BEInsumo> ListarTodos()
        {
            List<BEInsumo> lista = new List<BEInsumo>();


            BEInsumo tmpi;

            DataTable tablas;
            string Query = "ListarTodoInsumos";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);


            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpi = new BEInsumo();
                    tmpi.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpi.Nombre = fila["NOMBRE"].ToString();

                    lista.Add(tmpi);
                }
            }
            return lista;
        }

        public List<BEStockInsumos> ListarStockInsumos()
        {
            List<BEStockInsumos> lista = new List<BEStockInsumos>();


            BEStockInsumos tmpi;

            DataTable tablas;
            string Query = "ObtenerStockInsumos";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);


            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpi = new BEStockInsumos();
                    tmpi.Codigo = Convert.ToInt32(fila["Codigo"]);
                    tmpi.Nombre = fila["NOMBRE"].ToString();
                    tmpi.Cantidad = Convert.ToInt32(fila["Cantidad"]);

                    lista.Add(tmpi);
                }
            }
            return lista;
        }
        public List<BEStockInsumos> GenerarSugerenciaDeCompra()
        {
            List<BEStockInsumos> lista = new List<BEStockInsumos>();


            BEStockInsumos tmpi;

            DataTable tablas;
            string Query = "GenerarSugerenciaDeCompra";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);


            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpi = new BEStockInsumos();
                    tmpi.Codigo = Convert.ToInt32(fila["Codigo"]);
                    tmpi.Nombre = fila["NOMBRE"].ToString();
                    tmpi.Cantidad = Convert.ToInt32(fila["Cantidad"]);

                    lista.Add(tmpi);
                }
            }
            return lista;
        }
        public int Guardar(BEUsuario Autor, BEInsumo insumo)
        {
            if (insumo == null) throw new Exception("Ningun insumo especificado");
            string Query = "GuardarInsumo";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@ID", insumo.Codigo);
            ht.Add("@NOMBRE", insumo.Nombre);

            return bd.LeerSPRT(Query, ht);

        }
        public int Eliminar(BEUsuario Autor, BEInsumo insumo)
        {
            if (insumo == null) throw new Exception("Ningun insumo especificado");
            string Query = "EliminarInsumo";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@ID", insumo.Codigo);
            return bd.LeerSPRT(Query, ht);

        }

        public int RechazarPedido(BEUsuario Autor, BEPedidoInsumos bep)
        {
            if (bep == null) throw new Exception("Ningun insumo especificado");
            string Query = "RechazarPedidoInsumos";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@ID", bep.Codigo);
            return bd.LeerSPRT(Query, ht);

        }
        public int EnviarPedido(BEUsuario Autor, BEPedidoInsumos bep)
        {
            if (bep == null) throw new Exception("Ningun insumo especificado");
            string Query = "EnviarPedidoInsumos";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@ID", bep.Codigo);
            return bd.LeerSPRT(Query, ht);

        }
        public int RecibirPedido(BEUsuario Autor, BEPedidoInsumos bep)
        {
            if (bep == null) throw new Exception("Ningun insumo especificado");
            string Query = "RecibirPedidoInsumos";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@ID", bep.Codigo);
            return bd.LeerSPRT(Query, ht);

        }
        public int AltaPedido(BEUsuario Autor, List<BEStockInsumos> ings)
        {
            bd = new Acceso();
            if (ings == null) throw new Exception("Ningun insumo especificado");
            string Query = "AltaPedidoInsumos";
            Hashtable ht = new Hashtable();
            ht.Add("@Autor", Autor.Codigo);
            int j = bd.LeerSPRT(Query, ht);
            if (j == 0)
            {
                throw new Exception();
            }

            Query = "AgregarInsumoAPedido";
            foreach (BEStockInsumos ing in ings)
            {
                ht = new Hashtable();
                ht.Add("@Autor", Autor.Codigo);
                ht.Add("@PEDIDO", j);
                ht.Add("@INSUMO", ing.Codigo);
                ht.Add("@CANTIDAD", ing.Cantidad);
                if (bd.LeerSPRT(Query, ht) == 0)
                {
                    throw new Exception();
                }
            }
            return j;


        }
        public List<BEPedidoInsumos> ListarPedidos()
        {
            List<BEPedidoInsumos> tmpl = new List<BEPedidoInsumos>();
            //List<BEIngrediente> tmpli;
            BEStockInsumos tmping;

            BEPedidoInsumos tmppi;

            DataTable tablas;
            DataTable tablas2;
            string Query = "ListarPedidosInsumos";
            string Query2 = "ListarContenidoPedidoInsumos";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            Hashtable ht2;
            tablas = bd.LeerSP(Query, ht);


            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmppi = new BEPedidoInsumos();
                    tmppi.Codigo = Convert.ToInt32(fila["ID"]);
                    tmppi.Fecha = Convert.ToDateTime(fila["Fecha"]);
                    if (fila["Fecha_Entregado"] is DBNull)
                    {

                    }
                    else
                    {
                        tmppi.FechaEntregado = Convert.ToDateTime(fila["Fecha_Entregado"]);
                    }
                    tmppi.Estado = fila["Estado"].ToString();
                    tmppi.Insumos = new List<BEStockInsumos>();


                    //tmpli = new List<BEIngrediente>();
                    ht2 = new Hashtable();
                    ht2.Add("@Codigo", tmppi.Codigo);
                    tablas2 = bd.LeerSP(Query2, ht2);
                    foreach (DataRow fila2 in tablas2.Rows)
                    {
                        tmping = new BEStockInsumos();

                        tmping.Codigo = Convert.ToInt32(fila2["ID_INSUMO"]);
                        tmping.Cantidad = Convert.ToInt32(fila2["Cantidad"]);
                        tmping.Nombre = fila2["NOMBRE"].ToString();
                        tmppi.Insumos.Add(tmping);
                    }
                    tmpl.Add(tmppi);
                }
            }
            return tmpl;
        }

        public List<BEPedidoInsumos> ListarPedidosAlta()
        {
            List<BEPedidoInsumos> tmpl = new List<BEPedidoInsumos>();
            //List<BEIngrediente> tmpli;
            BEStockInsumos tmping;

            BEPedidoInsumos tmppi;

            DataTable tablas;
            DataTable tablas2;
            string Query = "ListarPedidosInsumosAlta";
            string Query2 = "ListarContenidoPedidoInsumos";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            Hashtable ht2;
            tablas = bd.LeerSP(Query, ht);


            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmppi = new BEPedidoInsumos();
                    tmppi.Codigo = Convert.ToInt32(fila["ID"]);
                    tmppi.Fecha = Convert.ToDateTime(fila["Fecha"]);
                    if (fila["Fecha_Entregado"] is DBNull)
                    {

                    }
                    else
                    {
                        tmppi.FechaEntregado = Convert.ToDateTime(fila["Fecha_Entregado"]);
                    }
                    tmppi.Estado = fila["Estado"].ToString();
                    tmppi.Insumos = new List<BEStockInsumos>();


                    //tmpli = new List<BEIngrediente>();
                    ht2 = new Hashtable();
                    ht2.Add("@Codigo", tmppi.Codigo);
                    tablas2 = bd.LeerSP(Query2, ht2);
                    foreach (DataRow fila2 in tablas2.Rows)
                    {
                        tmping = new BEStockInsumos();

                        tmping.Codigo = Convert.ToInt32(fila2["ID_INSUMO"]);
                        tmping.Cantidad = Convert.ToInt32(fila2["Cantidad"]);

                        tmping.Nombre = fila2["NOMBRE"].ToString();
                        tmppi.Insumos.Add(tmping);
                    }
                    tmpl.Add(tmppi);
                }
            }
            return tmpl;
        }

        public List<BEPedidoInsumos> ListarPedidosEnviados()
        {
            List<BEPedidoInsumos> tmpl = new List<BEPedidoInsumos>();
            //List<BEIngrediente> tmpli;
            BEStockInsumos tmping;

            BEPedidoInsumos tmppi;

            DataTable tablas;
            DataTable tablas2;
            string Query = "ListarPedidosInsumosEnviados";
            string Query2 = "ListarContenidoPedidoInsumos";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            Hashtable ht2;
            tablas = bd.LeerSP(Query, ht);


            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmppi = new BEPedidoInsumos();
                    tmppi.Codigo = Convert.ToInt32(fila["ID"]);
                    tmppi.Fecha = Convert.ToDateTime(fila["Fecha"]);
                    if (fila["Fecha_Entregado"] is DBNull)
                    {

                    }
                    else
                    {
                        tmppi.FechaEntregado = Convert.ToDateTime(fila["Fecha_Entregado"]);
                    }
                    tmppi.Estado = fila["Estado"].ToString();
                    tmppi.Insumos = new List<BEStockInsumos>();


                    //tmpli = new List<BEIngrediente>();
                    ht2 = new Hashtable();
                    ht2.Add("@Codigo", tmppi.Codigo);
                    tablas2 = bd.LeerSP(Query2, ht2);
                    foreach (DataRow fila2 in tablas2.Rows)
                    {
                        tmping = new BEStockInsumos();

                        tmping.Codigo = Convert.ToInt32(fila2["ID_INSUMO"]);
                        tmping.Cantidad = Convert.ToInt32(fila2["Cantidad"]);

                        tmping.Nombre = fila2["NOMBRE"].ToString();
                        tmppi.Insumos.Add(tmping);
                    }
                    tmpl.Add(tmppi);
                }
            }
            return tmpl;
        }
    }
}
