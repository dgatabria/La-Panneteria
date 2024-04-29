using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public class LanguageManager
    {
        BLLIdioma bli = new BLLIdioma();
        public List<BEIdioma> Idiomas;
        public LanguageManager()
        {
            Idiomas = bli.ListarIdiomas();
        }

        public List<BEIdioma> ListarIdiomas()
        {
            Idiomas = bli.ListarIdiomas();
            return Idiomas;
        }

        public Hashtable ListarPalabras(BEIdioma idioma)
        {
            return bli.ListarPalabras(idioma);
        }

        public void CambiarPalabras(BEIdioma idioma, string tag, string texto)
        {
            bli.CambiarPalabra(idioma, tag, texto);
        }
        public void CrearIdioma(string Nombre)
        {
            bli.CrearIdioma(Nombre);
        }
        public void EliminarIdioma(BEIdioma idioma)
        {
            bli.EliminarIdioma(idioma);
        }
    }
}
