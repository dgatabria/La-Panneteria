using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCliente
    {
        Acceso bd;
        public bool Baja(BECliente Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun cliente especificado");

            string Query = "BorrarCliente";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            bd.LeerSPRT(Query, ht);
            return true;
        }
        public bool Baja(BEUsuario Autor, BECliente Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun cliente especificado");

            string Query = "BorrarCliente";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@Autor", Autor.Codigo);
            int ret = bd.LeerSPRT(Query, ht);
            if (ret != 0)
            {
                throw new Exception("No se pudo eliminar el cliente. Compruebe que no tenga pedidos abiertos.");
            }
            return true;
        }

        public bool Guardar(BECliente Objeto)
        {
            throw new NotImplementedException();
        }
        public bool Guardar(BEUsuario Autor, BECliente Objeto)
        {
            if (Objeto == null) throw new Exception("Ningun cliente especificado");
            string Query = "GuardarCliente";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", Objeto.Codigo);
            ht.Add("@Autor", Autor.Codigo);
            ht.Add("@DNI", Objeto.DNI);
            ht.Add("@NOMBRE", Objeto.Nombre);
            ht.Add("@APELLIDO", Objeto.Apellido);
            ht.Add("@TELEFONO", Objeto.Telefono);
            ht.Add("@EMAIL", Objeto.EMail);
            ht.Add("@DOMICILIO", Objeto.Domicilio.Codigo);
            ht.Add("@CALLE", Objeto.Domicilio.Calle);
            ht.Add("@ALTURA", Objeto.Domicilio.Altura);
            ht.Add("@PISO", Objeto.Domicilio.Piso);
            ht.Add("@DEPTO", Objeto.Domicilio.Depto);
            ht.Add("@CP", Objeto.Domicilio.CP);
            ht.Add("@LOCALIDAD", Objeto.Domicilio.Localidad);

            bd.LeerSPRT(Query, ht);
            return true;
        }

        public BECliente ListarObjeto(BECliente Objeto)
        {
            BECliente tmpcliente = new BECliente();
            BEDomicilio tmpdomicilio;
            DataTable tablas;
            string Query = "ListarCliente";
            Hashtable ht = new Hashtable();
            bd = new Acceso();

            ht.Add("@Codigo", Objeto.Codigo);
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpcliente = new BECliente();
                    tmpdomicilio = new BEDomicilio();
                    tmpdomicilio.Codigo = Convert.ToInt32(fila["COD_DOMI"]);
                    tmpdomicilio.Calle = fila["CALLE"].ToString();
                    tmpdomicilio.Altura = fila["ALTURA"].ToString();
                    tmpdomicilio.Piso = fila["PISO"].ToString();
                    tmpdomicilio.Depto = fila["DEPTO"].ToString();
                    tmpdomicilio.CP = fila["CP"].ToString();
                    tmpdomicilio.Localidad = fila["LOCALIDAD"].ToString();

                    tmpcliente.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpcliente.DNI = fila["DNI"].ToString();
                    tmpcliente.Nombre = fila["NOMBRE"].ToString();
                    tmpcliente.Apellido = fila["APELLIDO"].ToString();
                    tmpcliente.Telefono = fila["TELEFONO"].ToString();
                    tmpcliente.EMail = fila["EMAIL"].ToString();

                    tmpcliente.Domicilio = tmpdomicilio;


                }
            }
            return tmpcliente;
        }

        public BECliente ListarObjetoPorDNI(BECliente Objeto)
        {
            BECliente tmpcliente = new BECliente();
            BEDomicilio tmpdomicilio;
            DataTable tablas;
            string Query = "ListarClientePorDNI";
            Hashtable ht = new Hashtable();
            bd = new Acceso();

            ht.Add("@DNI", Objeto.DNI);
            tablas = bd.LeerSP(Query, ht);
            if (tablas.Rows.Count == 1)
            {
                foreach (DataRow fila in tablas.Rows)
                {
                    tmpcliente = new BECliente();
                    tmpdomicilio = new BEDomicilio();
                    tmpdomicilio.Codigo = Convert.ToInt32(fila["COD_DOMI"]);
                    tmpdomicilio.Calle = fila["CALLE"].ToString();
                    tmpdomicilio.Altura = fila["ALTURA"].ToString();
                    tmpdomicilio.Piso = fila["PISO"].ToString();
                    tmpdomicilio.Depto = fila["DEPTO"].ToString();
                    tmpdomicilio.CP = fila["CP"].ToString();
                    tmpdomicilio.Localidad = fila["LOCALIDAD"].ToString();

                    tmpcliente.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpcliente.DNI = fila["DNI"].ToString();
                    tmpcliente.Nombre = fila["NOMBRE"].ToString();
                    tmpcliente.Apellido = fila["APELLIDO"].ToString();
                    tmpcliente.Telefono = fila["TELEFONO"].ToString();
                    tmpcliente.EMail = fila["EMAIL"].ToString();

                    tmpcliente.Domicilio = tmpdomicilio;


                }
            }
            else
            {
                throw new Exception("El DNI ingresado no existe en la base de datos!");
            }
            return tmpcliente;

        }

        public List<BECliente> ListarTodo()
        {


            List<BECliente> ListaClientes = new List<BECliente>();
            DataTable tablas;
            string Query = "ListarTodoClientes";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            tablas = bd.LeerSP(Query, ht);

            BEDomicilio tmpdomicilio;
            BECliente tmpcliente;

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {

                    tmpcliente = new BECliente();
                    tmpdomicilio = new BEDomicilio();
                    tmpdomicilio.Codigo = Convert.ToInt32(fila["COD_DOMI"]);
                    tmpdomicilio.Calle = fila["CALLE"].ToString();
                    tmpdomicilio.Altura = fila["ALTURA"].ToString();
                    tmpdomicilio.Piso = fila["PISO"].ToString();
                    tmpdomicilio.Depto = fila["DEPTO"].ToString();
                    tmpdomicilio.CP = fila["CP"].ToString();
                    tmpdomicilio.Localidad = fila["LOCALIDAD"].ToString();

                    tmpcliente.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpcliente.DNI = fila["DNI"].ToString();
                    tmpcliente.Nombre = fila["NOMBRE"].ToString();
                    tmpcliente.Apellido = fila["APELLIDO"].ToString();
                    tmpcliente.Telefono = fila["TELEFONO"].ToString();
                    tmpcliente.EMail = fila["EMAIL"].ToString();

                    tmpcliente.Domicilio = tmpdomicilio;



                    ListaClientes.Add(tmpcliente);
                }
            }
            return ListaClientes;

        }
    }
}
