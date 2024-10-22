using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL;
using BusinessEntities;

namespace Security
{
    public class UserManager
    {
        Crypto sec = new Crypto();
        public List<BEUsuario> ListarUsuarios()
        {

            BLLUsuario blu = new BLLUsuario();
            return blu.ListarTodo();
        }
        public List<BEPerfil> ListarPerfiles()
        {

            BLLPerfil blr = new BLLPerfil();
            return blr.ListarTodo();
        }

        public BEUsuario ListarUsuarioPorNombre(BEUsuario blus)
        {
            BLLUsuario blu = new BLLUsuario();
            return blu.ListarObjeto(blus);
        }
        public BEUsuario ListarUsuarioPorID(BEUsuario blus)
        {
            BLLUsuario blu = new BLLUsuario();
            return blu.ListarPorID(blus);
        }
        public bool Guardar(BEUsuario usuario)
        {
            Regex re = new Regex("^[a-zA-Z@\\.]+$");
            if (!re.IsMatch(usuario.username))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_username"].ToString());
            }
            if (!re.IsMatch(usuario.Nombre))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_name"].ToString());
            }
            if (!re.IsMatch(usuario.username))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_surname"].ToString());
            }
            if ((usuario.Password == null) || (usuario.Password == ""))
            {
                return GuardarSinPW(usuario);
            }

            re = new Regex("['\";]");
            if (re.IsMatch(usuario.Password))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_password"].ToString());
            }
            usuario.Hashedpassword = sec.GenerarMD5(usuario.Password);
            BLLUsuario blu = new BLLUsuario();
            return blu.Guardar(SessionManager.GetInstance.Usuario, usuario);

        }
        public bool GuardarSinPW(BEUsuario usuario)
        {
            Regex re = new Regex("^[a-zA-Z]+$");
            if (!re.IsMatch(usuario.username))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_username"].ToString());
            }
            if (!re.IsMatch(usuario.Nombre))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_name"].ToString());
            }
            if (!re.IsMatch(usuario.username))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_surname"].ToString());
            }


            BLLUsuario blu = new BLLUsuario();
            return blu.GuardarSinPW(SessionManager.GetInstance.Usuario, usuario);

        }
        public bool Borrar(BEUsuario usuario)
        {
            BLLUsuario blu = new BLLUsuario();
            return blu.Baja(SessionManager.GetInstance.Usuario, usuario);

        }
        public bool Unlock(BEUsuario usuario)
        {
            BLLUsuario blu = new BLLUsuario();
            return blu.Unlock(usuario);
        }
        public bool Lock(BEUsuario usuario)
        {
            BLLUsuario blu = new BLLUsuario();
            return blu.Lock(usuario);
        }
        public int VerifyPW(BEUsuario usuario, string pw)
        {
            Crypto sec = new Crypto();
            Regex re = new Regex("^[a-zA-Z]+$");
            if (!re.IsMatch(usuario.username))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_username"].ToString());
            }
            re = new Regex("['\";]");
            if (re.IsMatch(pw))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_password"].ToString());
            }
            BLLogin blll = new BLLogin();
            int i = blll.verifyPass(usuario, sec.GenerarMD5(pw));
            return i;

        }
        public int ChangePW(BEUsuario usuario, string pw)
        {
            Crypto sec = new Crypto();
            Regex re = new Regex("^[a-zA-Z]+$");
            if (!re.IsMatch(usuario.username))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_username"].ToString());
            }
            re = new Regex("['\";]");
            if (re.IsMatch(pw))
            {
                throw new Exception(SessionManager.GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_securitylayer_invalidchars_password"].ToString());
            }
            BLLogin blll = new BLLogin();
            int i = blll.changePass(usuario, sec.GenerarMD5(pw));
            return i;

        }
    }
}
