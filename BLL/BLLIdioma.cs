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
    public class BLLIdioma
    {
        Acceso bd;


        public void EliminarIdioma(BEIdioma idioma)
        {
            Hashtable tmp = new Hashtable();

            string Query = "BorrarIdioma";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", idioma.Codigo);

            bd.LeerSPRT(Query, ht);
        }

        public void CrearIdioma(string Nombre)
        {
            Hashtable tmp = new Hashtable();

            string Query = "CrearIdioma";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Nombre", Nombre);

            bd.LeerSPRT(Query, ht);
        }
        public void CambiarPalabra(BEIdioma idioma, string tag, string texto)
        {
            Hashtable tmp = new Hashtable();

            string Query = "CambiarPalabra";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@CodigoIdioma", idioma.Codigo);
            ht.Add("@tag", tag);
            ht.Add("@texto", texto);
            bd.LeerSPRT(Query, ht);
        }
        public Hashtable ListarPalabras(BEIdioma idioma)
        {
            Hashtable tmp = new Hashtable();
            DataTable tablas;
            string Query = "ListarPalabras";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", idioma.Codigo);
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmp.Add(fila["TAG"].ToString(), fila["TEXTO"].ToString());
                }

            }
            return tmp;
        }
        public BEIdioma ListarObjeto(BEIdioma idioma)
        {
            BEIdioma tmp = new BEIdioma();
            DataTable tablas;
            string Query = "ListarIdioma";
            bd = new Acceso();
            Hashtable ht = new Hashtable();

            if ( idioma.Codigo == 0)
            {
                Query = "ListarIdiomaPorNombre";
                ht.Add("@Nombre", idioma.Nombre);
                tablas = bd.LeerSP(Query, ht);
                if (tablas.Rows.Count > 0)
                {
                    foreach (DataRow fila in tablas.Rows)
                    {
                        tmp.Codigo = Convert.ToInt32(fila["ID"]);
                        tmp.Nombre = fila["Nombre"].ToString();
                        tmp.Palabras = ListarPalabras(tmp);
                    }

                }
                return tmp;
            } else
            {
                ht.Add("@Codigo", idioma.Codigo);
                tablas = bd.LeerSP(Query, ht);
                if (tablas.Rows.Count > 0)
                {
                    foreach (DataRow fila in tablas.Rows)
                    {
                        tmp.Codigo = Convert.ToInt32(fila["ID"]);
                        tmp.Nombre = fila["Nombre"].ToString();
                        tmp.Palabras = ListarPalabras(tmp);
                    }

                }
                return tmp;
            }


        }

        public List<BEIdioma> ListarIdiomas()
        {
            List<BEIdioma> tmplista = new List<BEIdioma>();
            BEIdioma tmp;
            DataTable tablas;
            string Query = "ListarIdiomas";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmp = new BEIdioma();
                    tmp.Codigo = Convert.ToInt32(fila["ID"]);
                    tmp.Nombre = fila["Nombre"].ToString();
                    tmp.Palabras = ListarPalabras(tmp);
                    tmplista.Add(tmp);
                }

            }
            return tmplista;
        }
    }
}
