using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class ProductManager
    {
        BLLArticulo bla = new BLLArticulo();
        public List<BEArticulo> ListarArticulos()
        {
            return bla.ListarTodos();
        }
        public List<BEStockArticulo> ListarStock()
        {
            return bla.ListarStock();
        }

        public List<BEStockArticulo> ListarArticulosPendientes()
        {
            return bla.ListarArticulosPendientes();
        }
        public void AgregarStock(BEStockArticulo ba)
        {
            if (bla.AgregarStock(SessionManager.GetInstance.Usuario, ba) != 0)
            {
                throw new Exception("msgerror_elaboracion_nostockinsumos");
            }
        }
    }
}
