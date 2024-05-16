using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BLL;


namespace Security
{
    public class LogManager
    {
        BLLBitacora bllb = new BLLBitacora();
        public List<BEEventoBitacora> ListarEventos(string actor, DateTime fechai, DateTime fechaf, string criticidad)
        {
            return bllb.ListarEventos(actor, fechai, fechaf, criticidad);
        }

        public List<BEEventoBitacoraCambios> ListarCambiosProductos(BEUsuario Actor, DateTime fechai, DateTime fechaf)
        {
            return bllb.ListarCambiosProductos(Actor, fechai, fechaf);
        }
        public List<BEEventoBitacoraCambios> ListarCambiosInsumos(BEUsuario Actor, DateTime fechai, DateTime fechaf)
        {
            return bllb.ListarCambiosInsumos(Actor, fechai, fechaf);
        }

        public void FijarVersionBitacoraCambios(BEEventoBitacoraCambios evento)
        {
            bllb.FijarVersionBitacoraCambios(SessionManager.GetInstance.Usuario, evento);
        }
    }
}
