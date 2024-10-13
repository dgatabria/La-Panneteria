using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BusinessEntities;
using System.Text.RegularExpressions;

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

            VerificarReferenciasCirculares(Objeto);

            string Query = "GuardarPerfil";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            string s = "";
            string r = "";

            foreach (RBAC p in Objeto.Permisos)
            {
                if ( p is BEPermiso )
                {
                    s = s + p.Codigo.ToString() + ",";
                }
                if (p is BEPerfil)
                {
                    r = r + p.Codigo.ToString() + ",";
                }
                Regex regex = new Regex(",$");
                s = regex.Replace(s, "");
                r = regex.Replace(s, "");
            }
            ht.Add("@Permisos", s);
            ht.Add("@Roles", r);
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
        /*
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
        */
        public BEPermiso ListarPermiso2(RBAC Objeto)
        {
            // un permiso no puede tener hijos
            DataTable tablas;
            bd = new Acceso();
            string Query = "ListarPermiso";
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);
            BEPermiso tmpperm = null;
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperm = new BEPermiso(Convert.ToInt32(fila["ID_PERMISO"]), fila["NOMBRE_PERMISO"].ToString());
                }
            }
            return tmpperm;

        }
        public BEPerfil ListarRol2(RBAC Objeto)
        {
            // un rol puede tener hijos
            DataTable tablas;
            bd = new Acceso();
            string Query = "ListarRol";
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);
            BEPerfil tmpperf = null;
            BEPerfil tmpperfh = null;
            BEPermiso tmpperm = null;
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperf = new BEPerfil(Convert.ToInt32(fila["ID"]), fila["NOMBRE"].ToString());
                }
                // Buscar roles hijos
                Query = "ListarRolesPorRol";
                ht = new Hashtable();
                ht.Add("@Codigo", Objeto.Codigo);
                tablas = bd.LeerSP(Query, ht);
                
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperfh = new BEPerfil(Convert.ToInt32(fila["ID_PERFIL"]), fila["NOMBRE_PERFIL"].ToString());
                    tmpperfh = ListarRol2(tmpperf);
                    tmpperm.AgregarHijo(tmpperfh);
                }
                // Buscar permisos hijos
                Query = "ListarPermisosPorRol";
                ht = new Hashtable();
                ht.Add("@Codigo", Objeto.Codigo);
                tablas = bd.LeerSP(Query, ht);
                tmpperm = null;
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperm = new BEPermiso(Convert.ToInt32(fila["ID_PERMISO"]), fila["NOMBRE_PERMISO"].ToString());
                    tmpperm.AgregarHijo(tmpperf);
                }
            }
            return tmpperf;
        }

        public List<BEPermiso> ConjugarRol(BEPerfil Objeto)
        {
            // un rol puede tener hijos
            DataTable tablas;
            bd = new Acceso();
            string Query = "ListarRol";
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);
            List<BEPermiso> tmplista = new List<BEPermiso>();
            BEPerfil tmpperf = null;
            BEPerfil tmpperfh = null;
            BEPermiso tmpperm = null;
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperf = new BEPerfil(Convert.ToInt32(fila["ID"]), fila["NOMBRE"].ToString());
                }
                // Buscar roles hijos
                Query = "ListarRolesPorRol";
                ht = new Hashtable();
                ht.Add("@Codigo", Objeto.Codigo);
                tablas = bd.LeerSP(Query, ht);

                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperfh = new BEPerfil(Convert.ToInt32(fila["ID_PERFIL"]), fila["NOMBRE_PERFIL"].ToString());
                    foreach (BEPermiso perm  in ConjugarRol(tmpperfh))
                    {
                        tmplista.Add(perm);
                    }
                }
                // Buscar permisos hijos
                Query = "ListarPermisosPorRol";
                ht = new Hashtable();
                ht.Add("@Codigo", Objeto.Codigo);
                tablas = bd.LeerSP(Query, ht);
                tmpperm = null;
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpperm = new BEPermiso(Convert.ToInt32(fila["ID_PERMISO"]), fila["NOMBRE_PERMISO"].ToString());
                    tmplista.Add(tmpperm);
                }
            }

            // Remover duplicados
            List<BEPermiso> tmplista2 = new List<BEPermiso>();
            bool i = false;
            foreach (BEPermiso perm in tmplista)
            {
                i = false;
                foreach (BEPermiso tmpper in tmplista2)
                {
                    if (perm.Codigo == tmpper.Codigo)
                    {
                        i = true;
                    }
                }
                if (!i)
                { 
                    tmplista2.Add(perm);
                }
            }
            return tmplista2;
        }

        void CuentaHijos(Hashtable ht,int j, int i, int m)
        {
            i++;
            if (i > m) { throw new Exception("Referencia circular detectada"); }
            if (ht.Count > 0)
            {
                foreach (DictionaryEntry entrada in ht)
                {
                    // Armo la lista de hijos
                    if (Convert.ToInt32(entrada.Key) == j)
                    {
                        CuentaHijos(ht, Convert.ToInt32(entrada.Value), i, m);
                    }
                    
                }
            }
        }
        public void VerificarReferenciasCirculares(BEPerfil Objeto)
        {
            Hashtable mapa = new Hashtable();
            DataTable tablas;
            bd = new Acceso();
            string Query = "ListarMapaDeRoles";
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);
            int i,j;
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    mapa.Add(Convert.ToInt32(fila["ID_PERFIL_PADRE"]), Convert.ToInt32(fila["ID_PERFIL_HIJO"]));
                }
            }
            // emular la generacion del ID para el insert en la tabla
            if (Objeto.Codigo == 0)
            {
                i = 0;
                foreach (DictionaryEntry entrada in mapa)
                {
                    if (Convert.ToInt32(entrada.Key) > i) {  i = Convert.ToInt32(entrada.Key); }
                }
                i++;
            } else { i = Objeto.Codigo; }
            // emular el insert de los roles hijos del rol a crear
            foreach (RBAC r in Objeto.ObtenerHijos())
            {
                if (r is BEPerfil)
                {
                    mapa.Add(i, r.Codigo);
                }
            }
            // Llamar a CuentaHijos para cada entrada en la tabla. 
            // Si un rol tiene más hijos que entradas hay en la tabla, necesariamente hay 
            // una referencia circular.
            i = 0;
            j = mapa.Count;
            foreach (DictionaryEntry entrada in mapa)
            {
                i = 0;
                j = Convert.ToInt32(entrada.Key);
                // Armo la lista de hijos
                CuentaHijos(ht, Convert.ToInt32(entrada.Value), i, j);
            }

        }
        public RBAC ListarObjeto2(RBAC Objeto)
        {
            if (Objeto is BEPermiso)
            {
                return ListarPermiso2(Objeto);
            } else { 
                return ListarRol2(Objeto);
            }
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
                foreach (BEPerfil perf in ListaRoles)
                {

                    Query = "ListarRolesPorRol";
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
        public List<BEPerfil> ListarTodo2()
        {
            List<BEPerfil> ListaRoles = new List<BEPerfil>();
            DataTable tablas;
            string Query = "ListarNombresRoles";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);

            BEPerfil tmprol;

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmprol = new BEPerfil(Convert.ToInt32(fila["ID_PERFIL"]), fila["NOMBRE"].ToString());
                    tmprol = ListarRol2(tmprol);
                    ListaRoles.Add(ListarRol2(tmprol));
                }
            }

            return ListaRoles;
        }
    }
}
