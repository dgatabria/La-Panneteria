using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BLL;

namespace Security
{
    public class ResourceManager
    {
        BLLInsumo bli = new BLLInsumo();
        public bool GuardarInsumo(BEInsumo ins)
        {
            bli.Guardar(SessionManager.GetInstance.Usuario, ins);
            return true;
        }

        public List<BEStockInsumos> ListarStockInsumos()
        {
            return bli.ListarStockInsumos();
        }
        public List<BEStockInsumos> GenerarSugerenciaDeCompra()
        {
            return bli.GenerarSugerenciaDeCompra();
        }
        public bool EliminarInsumo(BEInsumo ins)
        {
            bli.Eliminar(SessionManager.GetInstance.Usuario, ins);
            return true;
        }

        public List<BEInsumo> ListarTodoInsumos()
        {
            return bli.ListarTodos();
        }

        // AltaPedido(BEUsuario Autor, List<BEIngrediente> ings)

        public int AltaPedido(List<BEStockInsumos> ings)
        {
            return bli.AltaPedido(SessionManager.GetInstance.Usuario, ings);
        }
        public List<BEPedidoInsumos> ListarPedidos()
        {
            return bli.ListarPedidos();
        }
        public List<BEPedidoInsumos> ListarPedidosAlta()
        {
            return bli.ListarPedidosAlta();
        }
        public List<BEPedidoInsumos> ListarPedidosEnviados()
        {
            return bli.ListarPedidosEnviados();
        }
        public void RechazarPedido(BEPedidoInsumos bep)
        {
            bli.RechazarPedido(SessionManager.GetInstance.Usuario, bep);
        }
        public void EnviarPedido(BEPedidoInsumos bep)
        {
            bli.EnviarPedido(SessionManager.GetInstance.Usuario, bep);
        }
        public void RecibirPedido(BEPedidoInsumos bep)
        {
            bli.RecibirPedido(SessionManager.GetInstance.Usuario, bep);
        }
    }
}
