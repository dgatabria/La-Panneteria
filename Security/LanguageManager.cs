using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BLL;

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

        public BEIdioma ObtenerIdioma(int Codigo)
        {

            BEIdioma Idioma = new BEIdioma();
            Idioma.Codigo = Codigo;
            return bli.ListarObjeto(Idioma);
        }
        public BEIdioma ObtenerIdioma(string Nombre)
        {

            BEIdioma Idioma = new BEIdioma();
            Idioma.Nombre = Nombre;
            return bli.ListarObjeto(Idioma);
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
