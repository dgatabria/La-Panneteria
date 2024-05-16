using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DAL;

namespace BLL
{
    public class BLLDomicilio
    {
        Acceso bd;
        public int Guardar(BEUsuario Autor, BEDomicilio Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun domicilio especificado");
            string Query = "GuardarDomicilio";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@Calle", Objeto.Calle);
            ht.Add("@Altura", Objeto.Altura);
            ht.Add("@Piso", Objeto.Piso);
            ht.Add("@Depto", Objeto.Depto);
            ht.Add("@CP", Objeto.CP);
            ht.Add("@Localidad", Objeto.Localidad);
            return bd.LeerSPRT(Query, ht);

        }
        public BEDomicilio ListarObjeto(BEDomicilio Objeto)
        {
            BEDomicilio tmpdomicilio = new BEDomicilio();

            DataTable tablas;
            string Query = "ListarDomicilio";
            Hashtable ht = new Hashtable();
            bd = new Acceso();

            ht.Add("@Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {

                    tmpdomicilio = new BEDomicilio();
                    tmpdomicilio.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpdomicilio.Calle = fila["CALLE"].ToString();
                    tmpdomicilio.Altura = fila["ALTURA"].ToString();
                    tmpdomicilio.Piso = fila["PISO"].ToString();
                    tmpdomicilio.Depto = fila["DEPTO"].ToString();
                    tmpdomicilio.CP = fila["CP"].ToString();
                    tmpdomicilio.Localidad = fila["Localidad"].ToString();


                }
            }
            return tmpdomicilio;
        }
    }
}
