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
    public class BLLBitacora
    {
        Acceso bd;

        public void FijarVersionBitacoraCambios(BEUsuario Actor, BEEventoBitacoraCambios evento)
        {

            Hashtable tmp = new Hashtable();


            string Query = "FijarEventoBitacoraCambios" + evento.GetTipo();

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Actor", Actor.Codigo);
            ht.Add("@Producto", evento.IdArticulo);
            ht.Add("@Version", evento.Version);

            bd.LeerSPRT(Query, ht);

        }
        public List<BEEventoBitacoraCambios> ListarCambiosInsumos(BEUsuario Actor, DateTime fechai, DateTime fechaf)
        {
            List<BEEventoBitacoraCambios> tmplista = new List<BEEventoBitacoraCambios>();

            DataTable tablas;
            string Query = "ListarBitacoraDeCambiosInsumos";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Actor", Actor.Codigo);
            ht.Add("@FECHAI", fechai);
            ht.Add("@FECHAF", fechaf);

            tablas = bd.LeerSP(Query, ht);

            BEEventoBitacoraCambios tmpentrada; ;

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {

                    tmpentrada = new BEEventoBitacoraCambios("Insumos");
                    tmpentrada.Fecha = Convert.ToDateTime(fila["Fecha"]);
                    tmpentrada.Username = fila["USERNAME"].ToString();
                    tmpentrada.IdArticulo = Convert.ToInt32(fila["ARTICULO"]);
                    tmpentrada.Descripcion = fila["NOMBRE"].ToString();
                    tmpentrada.Cantidad = Convert.ToInt32(fila["CANTIDAD"]);
                    tmpentrada.Version = Convert.ToInt32(fila["VERSION"]);

                    tmplista.Add(tmpentrada);
                }
            }

            return tmplista;
        }
        public List<BEEventoBitacoraCambios> ListarCambiosProductos(BEUsuario Actor, DateTime fechai, DateTime fechaf)
        {
            List<BEEventoBitacoraCambios> tmplista = new List<BEEventoBitacoraCambios>();

            DataTable tablas;
            string Query = "ListarBitacoraDeCambiosProductos";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Actor", Actor.Codigo);
            ht.Add("@FECHAI", fechai);
            ht.Add("@FECHAF", fechaf);

            tablas = bd.LeerSP(Query, ht);

            BEEventoBitacoraCambios tmpentrada; ;

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {

                    tmpentrada = new BEEventoBitacoraCambios("Productos");
                    tmpentrada.Fecha = Convert.ToDateTime(fila["Fecha"]);
                    tmpentrada.Username = fila["USERNAME"].ToString();
                    tmpentrada.IdArticulo = Convert.ToInt32(fila["ARTICULO"]);
                    tmpentrada.Descripcion = fila["NOMBRE"].ToString();
                    tmpentrada.Cantidad = Convert.ToInt32(fila["CANTIDAD"]);
                    tmpentrada.Version = Convert.ToInt32(fila["VERSION"]);

                    tmplista.Add(tmpentrada);
                }
            }

            return tmplista;
        }
        public List<BEEventoBitacora> ListarEventos(string actor, DateTime fechai, DateTime fechaf, string criticidad)
        {
            List<BEEventoBitacora> tmplista = new List<BEEventoBitacora>();

            DataTable tablas;
            string Query = "ListarEventosBitacora";

            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Usuario", actor);
            ht.Add("@FECHAI", fechai);
            ht.Add("@FECHAF", fechaf);
            ht.Add("@CRITICIDAD", criticidad);
            tablas = bd.LeerSP(Query, ht);

            BEEventoBitacora tmpentrada; ;

            if (tablas.Rows.Count > 0)
            {
                foreach (DataRow fila in tablas.Rows)
                {

                    tmpentrada = new BEEventoBitacora();
                    tmpentrada.Codigo = Convert.ToInt32(fila["ID"]);
                    tmpentrada.Fecha = Convert.ToDateTime(fila["HORA"]);
                    tmpentrada.Actor = fila["ACTOR"].ToString();
                    tmpentrada.Mensaje = fila["MENSAJE"].ToString();
                    tmpentrada.Modulo = fila["MODULO"].ToString();
                    tmpentrada.Criticidad = fila["CRITICIDAD"].ToString();

                    tmplista.Add(tmpentrada);
                }
            }

            return tmplista;
        }
    }
}
