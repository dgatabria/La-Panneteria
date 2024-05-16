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
    public class ClientManager
    {
        BLLCliente blc = new BLLCliente();
        public BECliente Listar(BECliente cliente)
        {

            return blc.ListarObjeto(cliente);



        }
        public BECliente ListarPorDNI(BECliente cliente)
        {

            return blc.ListarObjetoPorDNI(cliente);


        }
        public List<BECliente> ListarTodos()
        {

            return blc.ListarTodo();


        }
        public bool Guardar(BECliente Cliente)
        {

            return blc.Guardar(Cliente);


        }
        public bool Guardar(BEUsuario Autor, BECliente Cliente)
        {

            if (Cliente == null) { throw new Exception(SessionManager.GetInstance.Usuario.Idioma.Palabras["msgerror_securitylayer_noclientselected"].ToString()); }
            Regex re = new Regex("^[a-zA-Z]+$");
            if (!re.IsMatch(Cliente.Nombre))
            {
                throw new Exception(SessionManager.GetInstance.Usuario.Idioma.Palabras["msgerror_securitylayer_invalidchars_name"].ToString());
            }
            if (!re.IsMatch(Cliente.Apellido))
            {
                throw new Exception(SessionManager.GetInstance.Usuario.Idioma.Palabras["msgerror_securitylayer_invalidchars_surname"].ToString());
            }
            if (!re.IsMatch(Cliente.Domicilio.Calle))
            {
                throw new Exception(SessionManager.GetInstance.Usuario.Idioma.Palabras["msgerror_securitylayer_invalidchars_clientcalle"].ToString());
            }
            if (!re.IsMatch(Cliente.Domicilio.Localidad))
            {
                throw new Exception(SessionManager.GetInstance.Usuario.Idioma.Palabras["msgerror_securitylayer_invalidchars_clientcity"].ToString());
            }
            return blc.Guardar(Autor, Cliente);



        }
        public bool Eliminar(BECliente Cliente)
        {

            return blc.Baja(Cliente);

        }
        public bool Eliminar(BEUsuario Autor, BECliente Cliente)
        {

            return blc.Baja(Autor, Cliente);

        }
    }
}
