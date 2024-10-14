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
    public class BLLPermiso
    {
        Acceso bd;

        public BEPermiso ListarPermiso(BEPermiso Objeto)
        {
            DataTable tablas;
            bd = new Acceso();
            string Query = "ListarPermiso";
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);
            BEPermiso tmpperm = null;
            BEPerfil tmpperf = new BEPerfil(Objeto.Codigo, Objeto.Nombre);

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperm = new BEPermiso(Convert.ToInt32(fila["ID"]), fila["NOMBRE"].ToString());

                }
            }
            return tmpperm;
        }
        public List<BEPermiso> ListarTodoExcepto(IList<RBAC> permisos)
        {
            List<BEPermiso> ListaPerms = new List<BEPermiso>();
            DataTable tablas;
            string Query = "ListarNombresPermsExcepto";
            if (permisos == null)
            {
                throw new Exception("Debe expecificar la lista de permisos");
            }
            string s = "";
            int i = 0;
            foreach (BEPermiso perm in permisos)
            {
                s = s + perm.Nombre;
                i++;
                if (i < permisos.Count)
                {
                    s = s + ",";
                }
            }


            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Permisos", s);
            tablas = bd.LeerSP(Query, ht);

            //List<BEPermiso> tmpperms;
            BEPermiso tmpperm;

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperm = new BEPermiso(Convert.ToInt32(fila["ID"]), fila["NOMBRE"].ToString());
                    ListaPerms.Add(tmpperm);
                }



            }


            return ListaPerms;
        }


    }
}
