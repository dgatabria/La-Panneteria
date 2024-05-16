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
    public class BLLReceta
    {
        Acceso bd;
        public bool GuardarReceta(string Receta, BEArticulo articulo)
        {
            Hashtable tmp = new Hashtable();

            string Query = "GuardarReceta";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Articulo", articulo.Codigo);
            ht.Add("@Receta", Receta);

            bd.LeerSPRT(Query, ht);
            return true;
        }
        public string LeerReceta(BEArticulo articulo)
        {
            DataTable tablas;
            string Query = "ListarReceta";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Articulo", articulo.Codigo);
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                return tablas.Rows[0]["RECETA"].ToString();

            }
            else
            {
                return "";
            }

        }
    }
}
