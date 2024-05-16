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
    public class BLLIngrediente
    {
        Acceso bd;
        public List<BEIngrediente> ListarIngredientes(BEArticulo articulo)
        {


            List<BEIngrediente> lista = new List<BEIngrediente>();
            BEIngrediente tmpi;
            BEInsumo ins;
            DataTable tablas;
            string Query = "ListarIngredientesPorArticulo";
            Hashtable ht = new Hashtable();
            ht.Add("@Articulo", articulo.Codigo);
            bd = new Acceso();
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpi = new BEIngrediente();
                    ins = new BEInsumo();
                    ins.Codigo = Convert.ToInt32(fila["Codigo"]);
                    ins.Nombre = fila["Nombre"].ToString();
                    tmpi.insumo = ins;
                    tmpi.Cantidad = Convert.ToInt32(fila["Cantidad"]);


                    lista.Add(tmpi);
                }
            }

            return lista;
        }



        public int AgregarIngredienteAArticulo(BEUsuario autor, BEIngrediente ingrediente, BEArticulo articulo)
        {
            string Query = "AgregarIngredienteAArticulo";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            //ht.Add("@CodigoIngrediente", ingrediente.Codigo);
            ht.Add("@CodigoInsumo", ingrediente.insumo.Codigo);
            ht.Add("@CodigoArticulo", articulo.Codigo);
            ht.Add("@Autor", autor.Codigo);
            ht.Add("@Cantidad", ingrediente.Cantidad);
            return bd.LeerSPRT(Query, ht);

        }
        public int QuitarIngredienteAArticulo(BEUsuario autor, BEIngrediente ingrediente, BEArticulo articulo)
        {
            string Query = "QuitarIngredienteAArticulo";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            //ht.Add("@CodigoIngrediente", ingrediente.Codigo);
            ht.Add("@CodigoInsumo", ingrediente.insumo.Codigo);
            ht.Add("@CodigoArticulo", articulo.Codigo);
            ht.Add("@Autor", autor.Codigo);
            ht.Add("@Cantidad", ingrediente.Cantidad);
            return bd.LeerSPRT(Query, ht);

        }
    }
}
