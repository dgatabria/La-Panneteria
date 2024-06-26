using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessEntities;
using BLL;
using AbstractionLayer;
namespace Security
{
    public class Traductor
    {
        private BLLIdioma bli = new BLLIdioma();
        public List<BEIdioma> Idiomas { get; set; }
        private BEIdioma __idioma { get; set; }

        public BEIdioma IdiomaSeleccionado { get { return __idioma; } }
        private List<ITraducible> Suscriptores { get; set; }

        public void SeleccionarIdioma(BEIdioma idioma)
        {
            __idioma = idioma;
            Notificar();
        }
        public Traductor()
        {
            Suscriptores = new List<ITraducible>();
            Idiomas = bli.ListarIdiomas();
        }
        public void Suscribir(ITraducible form)
        {
            Suscriptores.Add(form);
        }
        public void Notificar()
        {
            foreach (ITraducible form in Suscriptores)
            {
                form.Actualizar();
            }
        }



    }
    public class SessionManager
    {

        private static SessionManager _session;
        public BEUsuario Usuario { get; set; }
        public DateTime FechaInicio { get; set; }

        public Traductor Traductor { get; set; }

        private string SessionToken { get; set; }

        public string GetSessionToken() { return SessionToken; }
        public static SessionManager GetInstance
        {
            get
            {
                if (_session == null) throw new Exception("Sesion no iniciada");
                return _session;
            }

        }

        public static bool VerificarToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            BLLogin checkToken = new BLLogin();
            BEUsuario tmpusuario;
            if (_session == null)
            {
                tmpusuario = checkToken.VerificarToken(token);
                if (tmpusuario != null)
                {
                    _session = new SessionManager();
                    _session.Usuario = tmpusuario;
                    _session.FechaInicio = DateTime.Now;
                    _session.Traductor = new Traductor();
                    _session.Traductor.SeleccionarIdioma(tmpusuario.Idioma);
                    _session.SessionToken = token;

                    return true;

                } else
                {
                    return false;
                }
            }
            return checkToken.VerificarToken(Security.SessionManager.GetInstance.Usuario, token);
        }
        public static bool TengoPermiso(string NombrePermiso)
        {
            if (_session == null) throw new Exception("Sesion no iniciada");

            foreach (BEPermiso perm in _session.Usuario.Perfil.Permisos)
            {
                if (perm.Nombre == NombrePermiso)
                    return true;
            }
            throw new Exception(GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_sessionmgr_permisodenegado"].ToString());

        }
        public static void Login(BEUsuario usuario,string sessionid)
        {
            //           if (_session == null)
            //            {
            // Regex de usuario
                Regex re = new Regex("^[a-zA-Z@\\.]+$");
                Crypto sec = new Crypto();

                if (!re.IsMatch(usuario.username))
                {
                    throw new Exception("msgerror_sessionmgr_invaliduser");
                }
                re = new Regex("['\";]");
                if (re.IsMatch(usuario.Password))
                {
                    throw new Exception("msgerror_sessionmgr_invalidpass");
                }

                BEUsuario tmpusuario = new BEUsuario();
                tmpusuario.username = usuario.username;
                tmpusuario.Hashedpassword = sec.GenerarMD5(usuario.Password);

                BLLogin checkLogin = new BLLogin();
                try
                {
                    string i = checkLogin.ValidarUsuario(tmpusuario,sessionid);

                    if (i != "")
                    {
                        BLLUsuario bllu = new BLLUsuario();
                        tmpusuario = bllu.ListarObjeto(tmpusuario);
                        _session = new SessionManager();
                        _session.Usuario = tmpusuario;
                        _session.FechaInicio = DateTime.Now;
                        _session.Traductor = new Traductor();
                        _session.Traductor.SeleccionarIdioma(tmpusuario.Idioma);
                        _session.SessionToken = i;

                        return;
                    }
                    throw new Exception("msgerror_sessionmgr_systemerror");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            //            }
            //            else
            //            {
            //                throw new Exception(GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_sessionmgr_sesionyainiciada"].ToString());
            //            }

        }
        public static void WebMasterLogin(BEUsuario usuario)
        {

            Regex re = new Regex("^[a-zA-Z@\\.]+$");
            Crypto sec = new Crypto();

            if (!re.IsMatch(usuario.username))
            {
                throw new Exception("msgerror_sessionmgr_invaliduser");
            }
            re = new Regex("['\";]");
            if (re.IsMatch(usuario.Password))
            {
                throw new Exception("msgerror_sessionmgr_invalidpass");
            }

            BEUsuario tmpusuario = new BEUsuario();
            tmpusuario.username = usuario.username;
            tmpusuario.Hashedpassword = sec.GenerarMD5(usuario.Password);

            BLLogin checkLogin = new BLLogin();
            try
            {
                string i = checkLogin.ValidarWebMaster(tmpusuario);

                if (i != "")
                {
                    BLLUsuario bllu = new BLLUsuario();
                    tmpusuario = bllu.ListarObjeto(tmpusuario);
                    _session = new SessionManager();
                    _session.Usuario = tmpusuario;
                    _session.FechaInicio = DateTime.Now;
                    _session.Traductor = new Traductor();
                    _session.Traductor.SeleccionarIdioma(tmpusuario.Idioma);
                    _session.SessionToken = i;

                    return;
                }
                throw new Exception("msgerror_sessionmgr_systemerror");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //            }
            //            else
            //            {
            //                throw new Exception(GetInstance.Traductor.IdiomaSeleccionado.Palabras["msgerror_sessionmgr_sesionyainiciada"].ToString());
            //            }

        }
        public static void Logout()
        {
            if (_session != null)
            {
                BLLogin checkLogin = new BLLogin();
                checkLogin.DestruirToken(_session.Usuario, _session.SessionToken);
                _session = null;
            }
            else
            {
                throw new Exception("Sesion no iniciada");
            }

        }
        private SessionManager()
        {

        }
    }
}
