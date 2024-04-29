using AbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BEIngrediente : Entidad
    {
        public BEInsumo insumo { get; set; }
        public int Cantidad { get; set; }


    }
}
