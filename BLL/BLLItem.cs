using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BusinessEntities;

namespace BLL
{
    public class BLLItem
    {
        Acceso bd;
        public int Guardar(BEUsuario Autor, BEPedido Pedido, BEItem Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun item especificado");
            string Query = "GuardarItem";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@ID_PEDIDO", Pedido.Codigo);
            ht.Add("@ID_ARTICULO", Objeto.Articulo.Codigo);
            ht.Add("@CANTIDAD", Objeto.Cantidad);
            ht.Add("@PRECIOUNITARIO", Objeto.PrecioUnitario);

            return bd.LeerSPRT(Query, ht);

        }


        public BindingList<BEItem> ListarTodoPorPedido(BEPedido Objeto)
        {


            BindingList<BEItem> ListaItems = new BindingList<BEItem>();

            BEItem tmpitem = new BEItem();
            BEArticulo tmparticulo = new BEArticulo();


            DataTable tablas;
            string Query = "ListarItemsDePedido";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);


            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {

                    tmpitem = new BEItem();
                    tmparticulo = new BEArticulo();

                    tmparticulo.Codigo = Convert.ToInt32(fila["ID_ARTICULO"]);
                    tmparticulo.Descripcion = fila["DESCRIPCION"].ToString();
                    tmparticulo.PrecioUnitario = Convert.ToDouble(fila["PrecioUnitario"]);
                    tmpitem.Cantidad = Convert.ToInt32(fila["CANTIDAD"]);
                    tmpitem.PrecioUnitario = tmparticulo.PrecioUnitario;
                    tmpitem.Articulo = tmparticulo;

                    ListaItems.Add(tmpitem);
                }
            }
            return ListaItems;

        }
    }
}
