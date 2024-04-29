using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    internal class OrderManager
    {
        BLLArticulo blla = new BLLArticulo();
        BLLPedido blp = new BLLPedido();


        public void GuardarArticulo(BEArticulo articulo)
        {
            blla.Guardar(SessionManager.GetInstance.Usuario, articulo);
        }
        public void BorrarArticulo(BEArticulo articulo)
        {
            blla.Eliminar(SessionManager.GetInstance.Usuario, articulo);
        }
        public int GuardarPedido(BEPedido Pedido)
        {
            return blp.Alta(SessionManager.GetInstance.Usuario, Pedido);

        }
        public List<BEPedido> ListarPedidosAbiertos()
        {
            return blp.ListarAbiertos();

        }

        public void DespacharPedido(BEPedido pedido)
        {
            blp.Despachar(SessionManager.GetInstance.Usuario, pedido);

        }
        public void VerificarExistencia(BEPedido pedido)
        {
            blp.VerificarExistencia(pedido);

        }

        public int AplicarPago(BEPedido pedido, BEPago pago)
        {
            return blp.AplicarPago(SessionManager.GetInstance.Usuario, pago, pedido);
        }
        public int AplicarPagoYCerrar(BEPedido pedido, BEPago pago)
        {
            return blp.AplicarPagoYCerrar(SessionManager.GetInstance.Usuario, pago, pedido);
        }

        public BEPedido ListarPedidoDespachado(BEPedido pedido)
        {
            return blp.ListarDespachado(pedido);
        }
    }
}
