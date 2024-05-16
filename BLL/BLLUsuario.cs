using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractionLayer;
using DAL;
using BusinessEntities;


namespace BLL
{
    public class BLLUsuario : IGestor<BEUsuario>
    {
        Acceso bd;

        public bool Unlock(BEUsuario Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun usuario especificado");

            string Query = "DesbloquearUsuario";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            bd.LeerSPRT(Query, ht);
            return true;
        }
        public bool Unlock(BEUsuario autor, BEUsuario Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun usuario especificado");

            string Query = "DesbloquearUsuario";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@Autor", autor.Codigo);
            bd.LeerSPRT(Query, ht);
            return true;
        }
        public bool Baja(BEUsuario Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun usuario especificado");

            string Query = "BorrarUsuario";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            bd.LeerSPRT(Query, ht);
            return true;
        }
        public bool Baja(BEUsuario autor, BEUsuario Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun usuario especificado");

            string Query = "BorrarUsuario";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@Autor", autor.Codigo);
            bd.LeerSPRT(Query, ht);
            return true;
        }

        public bool Guardar(BEUsuario Objeto)
        {
            if (Objeto == null) throw new Exception("Error guardando el usuario.");

            string Query = "GuardarUsuario";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@USERNAME", Objeto.username);
            ht.Add("@HASHED_PASSWORD", Objeto.Hashedpassword);
            ht.Add("@NOMBRE_PERFIL", Objeto.Perfil.Nombre);
            ht.Add("@NOMBRE", Objeto.Nombre);
            ht.Add("@PERFIL_ID", Objeto.Perfil.Codigo);
            ht.Add("@APELLIDO", Objeto.Apellido);

            int i = bd.LeerSPRT(Query, ht);
            if (i == 1)
            {
                throw new Exception("Ya existe un usuario con ese nombre");
            }


            return true;
        }
        public bool Guardar(BEUsuario autor, BEUsuario Objeto)
        {
            if (Objeto == null) throw new Exception("Error guardando el usuario.");

            string Query = "GuardarUsuario";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Autor", autor.Codigo);
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@USERNAME", Objeto.username);
            ht.Add("@HASHED_PASSWORD", Objeto.Hashedpassword);
            ht.Add("@PERFIL_ID", Objeto.Perfil.Codigo);
            ht.Add("@NOMBRE", Objeto.Nombre);
            ht.Add("@APELLIDO", Objeto.Apellido);
            ht.Add("@IDIOMA", Objeto.Idioma.Codigo);

            int i = bd.LeerSPRT(Query, ht);
            if (i == 1)
            {
                throw new Exception("Ya existe un usuario con ese nombre");
            }


            return true;
        }
        public bool GuardarSinPW(BEUsuario autor, BEUsuario Objeto)
        {
            if (Objeto == null) throw new Exception("Error guardando el usuario.");
            if (Objeto.Codigo == 0) throw new Exception("No se puede crear usuarios nuevos sin password");
            string Query = "GuardarUsuarioSinPW";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Autor", autor.Codigo);
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@USERNAME", Objeto.username);
            ht.Add("@PERFIL_ID", Objeto.Perfil.Codigo);
            ht.Add("@NOMBRE", Objeto.Nombre);
            ht.Add("@APELLIDO", Objeto.Apellido);
            ht.Add("@IDIOMA", Objeto.Idioma.Codigo);

            int i = bd.LeerSPRT(Query, ht);
            if (i == 1)
            {
                throw new Exception("Ya existe un usuario con ese nombre");
            }


            return true;
        }
        public BEUsuario ListarObjeto(BEUsuario Objeto)
        {
            BEUsuario tmpusuario = new BEUsuario();
            BEPerfil tmprol;
            DataTable tablas;
            BLLIdioma bli = new BLLIdioma();
            string Query = "ListarUsuarioPorNombre";
            Hashtable ht = new Hashtable();
            bd = new Acceso();
            BLLPerfil blpf = new BLLPerfil();

            ht.Add("@USERNAME", Objeto.username);
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpusuario = new BEUsuario();
                    tmprol = new BEPerfil(Convert.ToInt32(fila["ID_PERFIL"]), fila["PERFIL_NOMBRE"].ToString());
                    tmpusuario.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpusuario.username = fila["USERNAME"].ToString();
                    tmpusuario.Nombre = fila["NAME"].ToString();
                    tmpusuario.Apellido = fila["SURNAME"].ToString();
                    tmpusuario.Perfil = blpf.ListarObjeto(tmprol);
                    BEIdioma bld = new BEIdioma();
                    bld.Codigo = Convert.ToInt32(fila["IDIOMA"]);
                    tmpusuario.Idioma = bli.ListarObjeto(bld);
                    if (Convert.ToInt32(fila["LOCKED"]) > 2) tmpusuario.Locked = true; else tmpusuario.Locked = false;
                }
            }
            return tmpusuario;

        }

        public List<BEUsuario> ListarTodo()
        {
            List<BEUsuario> ListaUsuarios = new List<BEUsuario>();
            DataTable tablas;
            string Query = "ListarTodoUsuarios";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);
            BEUsuario tmpusuario;
            BEPerfil tmprol;
            BLLPerfil blpf = new BLLPerfil();

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpusuario = new BEUsuario();
                    tmprol = new BEPerfil(Convert.ToInt32(fila["ID_PERFIL"]), fila["PERFIL_NOMBRE"].ToString());
                    tmpusuario.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpusuario.username = fila["USERNAME"].ToString();
                    tmpusuario.Nombre = fila["NAME"].ToString();
                    tmpusuario.Apellido = fila["SURNAME"].ToString();
                    tmpusuario.Perfil = blpf.ListarObjeto(tmprol);
                    if (Convert.ToInt32(fila["LOCKED"]) > 2) tmpusuario.Locked = true; else tmpusuario.Locked = false;

                    ListaUsuarios.Add(tmpusuario);
                }
            }
            return ListaUsuarios;
        }
    }
}
