using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    [Serializable]
    public class BEEventoBitacoraCambios
    {
        public BEEventoBitacoraCambios() { }
        private string tipo { get; set; }
        public BEEventoBitacoraCambios(string tipo)
        {
            this.tipo = tipo;
        }
        public string GetTipo()
        {
            return tipo;
        }
        public string Username { get; set; }

        public int IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int Version { get; set; }
    }
}
