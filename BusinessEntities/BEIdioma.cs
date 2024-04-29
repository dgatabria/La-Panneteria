using AbstractionLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BEIdioma : Entidad
    {
        public string Nombre { get; set; }
        public Hashtable Palabras { get; set; }

        public BEIdioma()
        {
            Palabras = new Hashtable();
        }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
