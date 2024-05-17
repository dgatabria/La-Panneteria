using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    public class BLLogin
    {
        Acceso bd;
        public string ValidarUsuario(BEUsuario usuario)
        {
            bd = new Acceso();
            string Query = "doLogin";
            //Hashtable ht = new Hashtable();
            //ht.Add("@username", usuario.username);
            //ht.Add("@hashedPassword", usuario.Hashedpassword);

            //return bd.LeerSPRT(Query, ht);
            List<SqlParameter> spc = new List<SqlParameter>();
            spc.Add(new SqlParameter("@username", usuario.username));
            spc[0].Direction = System.Data.ParameterDirection.Input;
            spc.Add(new SqlParameter("@hashedPassword", usuario.Hashedpassword));
            spc[1].Direction = System.Data.ParameterDirection.Input;
            spc.Add(new SqlParameter("@RetValue", SqlDbType.Int));
            spc[2].Direction = System.Data.ParameterDirection.Output;
            spc.Add(new SqlParameter("@SESSIONTOKEN", SqlDbType.NVarChar));
            spc[3].Direction = System.Data.ParameterDirection.Output;
            spc[3].Size = 100;
            SqlParameterCollection ipc = bd.LeerSPRTO(Query, spc);

            int i = Convert.ToInt32(ipc["@RetValue"].Value);
            if (i == 3)
            {
                throw new Exception("msgerror_sessionmgr_errordv");
            }
            if (i == 4)
            {
                throw new Exception("admin_start_recovery");
            }
            if (i == 0)
            {
                throw new Exception("msgerror_sessionmgr_loginincorrecto");
            }
            if (i == 2)
            {
                throw new Exception("msgerror_sessionmgr_usuariobloqueado");
            }
            if ( i == 1)
            {
                return ipc["@SESSIONTOKEN"].Value.ToString();
            }
            return "";

        }
        public bool VerificarToken(BEUsuario usuario, string token)
        {
            bd = new Acceso();
            string Query = "VerificarToken";
            Hashtable ht = new Hashtable();
            ht.Add("@username", usuario.username);
            ht.Add("@Token", token);
            int i = bd.LeerSPRT(Query, ht);
            if ( i == 1 )
            {
                return true;
            }
            return false;
        }
        public BEUsuario VerificarToken(string token)
        {
            bd = new Acceso();
            DataTable tablas;
            string Query = "ObtenerUsuarioDelToken";
            Hashtable ht = new Hashtable();
            ht.Add("@Token", token);
            tablas = bd.LeerSP(Query, ht);
            BEUsuario tmpusuario = null;
            if (tablas.Rows.Count > 0)
            { 
                BEPerfil tmprol;
                BLLPerfil blpf = new BLLPerfil();
                BLLIdioma bli = new BLLIdioma();
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
        public int verifyPass(BEUsuario usuario, string pw)
        {
            bd = new Acceso();
            string Query = "doLogin";
            Hashtable ht = new Hashtable();
            ht.Add("@username", usuario.username);
            ht.Add("@hashedPassword", pw);
            int i = bd.LeerSPRT(Query, ht);
            return i;
        }
        public int changePass(BEUsuario usuario, string pw)
        {
            bd = new Acceso();
            string Query = "ChangePW";
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", usuario.Codigo);
            ht.Add("@hashedPassword", pw);
            int i = bd.LeerSPRT(Query, ht);
            return i;


        }
    }
}
