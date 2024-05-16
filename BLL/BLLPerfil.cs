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
    public class BLLPerfil
    {
        Acceso bd;
        public bool Baja(BEUsuario autor, BEPerfil Objeto)
        {
            string Query = "BorrarPerfil";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("Autor", autor.Codigo);
            ht.Add("Codigo", Objeto.Codigo);
            return (bd.LeerSPRT(Query, ht) == 0);


        }

        public bool Guardar(BEUsuario autor, BEPerfil Objeto)
        {
            if (Objeto == null) throw new Exception("Error guardando el perfil.");

            string Query = "GuardarPerfil";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            string s = "";
            int ii = 0;
            foreach (BEPermiso p in Objeto.Permisos)
            {
                s = s + p.Codigo.ToString();
                ii++;
                if (ii < Objeto.Permisos.Count)
                {
                    s = s + ",";
                }
            }
            ht.Add("@Permisos", s);
            ht.Add("@Codigo", Objeto.Codigo.ToString());
            ht.Add("@Nombre", Objeto.Nombre);
            ht.Add("@Autor", autor.Codigo);


            int i = bd.LeerSPRT(Query, ht);
            if (i == 1)
            {
                throw new Exception("Ya existe un perfil con ese nombre");
            }




            return true;


        }

        public BEPerfil ListarObjeto(BEPerfil Objeto)
        {
            DataTable tablas;
            bd = new Acceso();
            string Query = "ListarPermisosPorRol";
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);
            BEPermiso tmpperm;
            BEPerfil tmpperf = new BEPerfil(Objeto.Codigo, Objeto.Nombre);

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperm = new BEPermiso(Convert.ToInt32(fila["ID_PERMISO"]), fila["NOMBRE_PERMISO"].ToString());
                    tmpperf.AgregarHijo(tmpperm);
                }
            }
            return tmpperf;

        }

        public List<BEPerfil> ListarTodo()
        {
            List<BEPerfil> ListaRoles = new List<BEPerfil>();
            DataTable tablas;
            string Query = "ListarNombresRoles";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);

            BEPerfil tmprol;
            BEPermiso tmpperm;

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmprol = new BEPerfil(Convert.ToInt32(fila["ID_PERFIL"]), fila["NOMBRE"].ToString());
                    ListaRoles.Add(tmprol);
                }

                foreach (BEPerfil perf in ListaRoles)
                {

                    Query = "ListarPermisosPorRol";
                    ht = new Hashtable();
                    ht.Add("@Codigo", perf.Codigo);
                    tablas = bd.LeerSP(Query, ht);
                    if (tablas.Rows.Count > 0)
                    {
                        foreach (DataRow fila in tablas.Rows)
                        {
                            tmpperm = new BEPermiso(Convert.ToInt32(fila["ID_PERMISO"]), fila["NOMBRE_PERMISO"].ToString());
                            perf.AgregarHijo(tmpperm);
                        }
                    }
                }

            }


            return ListaRoles;
        }
    }
}
