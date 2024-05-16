using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Security.Crypto;
using BusinessEntities;
using BLL;

namespace Security
{
    public class RecipeeManager
    {
        BLLReceta blr = new BLLReceta();
        BLLIngrediente bli = new BLLIngrediente();
        string clave = "Encr1pt4_Con_3st@!";
        public bool GuardarReceta(string Receta, BEArticulo articulo)
        {
            string enc = StringCipher.Encrypt(Receta, clave);
            blr.GuardarReceta(enc, articulo);
            return true;
        }
        public string LeerReceta(BEArticulo articulo)
        {
            string des = blr.LeerReceta(articulo);
            if ((des != null) && (des != ""))
            {
                des = StringCipher.Decrypt(des, clave);
            }

            return des;
        }
        public List<BEIngrediente> ListarIngredientes(BEArticulo articulo)
        {
            return bli.ListarIngredientes(articulo);
        }

        public bool GuardarIngrediente(BEIngrediente ingrediente, BEArticulo articulo)
        {
            return true;
        }
        public int AgregarIngredienteAArticulo(BEIngrediente ingrediente, BEArticulo articulo)
        {
            return bli.AgregarIngredienteAArticulo(SessionManager.GetInstance.Usuario, ingrediente, articulo);
        }

        public int QuitarIngredienteAArticulo(BEIngrediente ingrediente, BEArticulo articulo)
        {
            return bli.QuitarIngredienteAArticulo(SessionManager.GetInstance.Usuario, ingrediente, articulo);
        }
    }
}
