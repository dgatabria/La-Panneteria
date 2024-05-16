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
    public class BLLArticulo
    {
        Acceso bd;
        public bool Guardar(BEUsuario Autor, BEArticulo Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun articulo especificado");
            string Query = "GuardarArticulo";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@Nombre", Objeto.Descripcion);
            ht.Add("@Precio", Objeto.PrecioUnitario);
            bd.LeerSPRT(Query, ht);
            return true;
        }

        public int AgregarStock(BEUsuario Autor, BEStockArticulo Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun articulo especificado");
            string Query = "AgregarStockArticulo";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@Cantidad", Objeto.Cantidad);
            return bd.LeerSPRT(Query, ht);

        }

        public BEArticulo ListarObjeto(BEArticulo Objeto)
        {
            BEArticulo tmpart = new BEArticulo();

            DataTable tablas;
            string Query = "ListarArticulo";
            Hashtable ht = new Hashtable();
            bd = new Acceso();

            ht.Add("@Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpart.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpart.Descripcion = fila["Descripcion"].ToString();
                    tmpart.PrecioUnitario = Convert.ToDouble(fila["Precio"].ToString());
                }
            }
            return tmpart;
        }
        public List<BEArticulo> ListarTodos()
        {
            BEArticulo tmpart;
            List<BEArticulo> ba = new List<BEArticulo>();

            DataTable tablas;
            string Query = "ListarTodoArticulos";
            Hashtable ht = new Hashtable();
            bd = new Acceso();
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpart = new BEArticulo();
                    tmpart.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpart.Descripcion = fila["Descripcion"].ToString();
                    tmpart.PrecioUnitario = Convert.ToDouble(fila["Precio"].ToString());

                    ba.Add(tmpart);
                }
            }
            return ba;
        }

        public List<BEStockArticulo> ListarStock()
        {
            BEStockArticulo tmpart;
            List<BEStockArticulo> ba = new List<BEStockArticulo>();

            DataTable tablas;
            string Query = "ObtenerStockActual";
            Hashtable ht = new Hashtable();
            bd = new Acceso();
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpart = new BEStockArticulo();
                    tmpart.Codigo = Convert.ToInt32(fila["Codigo"]);
                    tmpart.Descripcion = fila["Descripcion"].ToString();
                    tmpart.Cantidad = Convert.ToInt32(fila["Cantidad"]);

                    ba.Add(tmpart);
                }
            }
            return ba;
        }

        public List<BEStockArticulo> ListarArticulosPendientes()
        {
            BEStockArticulo tmpart;
            List<BEStockArticulo> ba = new List<BEStockArticulo>();

            DataTable tablas;
            string Query = "ListarArticulosPendientes";
            Hashtable ht = new Hashtable();
            bd = new Acceso();
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpart = new BEStockArticulo();
                    tmpart.Codigo = Convert.ToInt32(fila["Codigo"]);
                    tmpart.Descripcion = fila["Descripcion"].ToString();
                    tmpart.Cantidad = Convert.ToInt32(fila["Cantidad"]);

                    ba.Add(tmpart);
                }
            }
            return ba;
        }
        public bool Eliminar(BEUsuario autor, BEArticulo art)
        {
            string Query = "BorrarArticulo";
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", art.Codigo);
            ht.Add("@Autor", autor.Codigo);
            bd = new Acceso();
            bd.LeerSPRT(Query, ht);
            return true;
        }
    }
}
