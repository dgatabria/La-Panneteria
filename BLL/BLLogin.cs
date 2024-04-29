using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLogin
    {
        Acceso bd;
        public int ValidarUsuario(BEUsuario usuario)
        {
            bd = new Acceso();
            string Query = "doLogin";
            Hashtable ht = new Hashtable();
            ht.Add("@username", usuario.username);
            ht.Add("@hashedPassword", usuario.Hashedpassword);

            return bd.LeerSPRT(Query, ht);

        }
        public int verifyPass(BEUsuario usuario, string pw)
        {
            bd = new Acceso();
            string Query = "doLogin";
            Hashtable ht = new Hashtable();
            ht.Add("@username", usuario.username);
            ht.Add("@hashedPassword", pw);
            int i = bd.LeerSPRT(Query, ht);
            return i;


        }
        public int changePass(BEUsuario usuario, string pw)
        {
            bd = new Acceso();
            string Query = "ChangePW";
            Hashtable ht = new Hashtable();
            ht.Add("@Codigo", usuario.Codigo);
            ht.Add("@hashedPassword", pw);
            int i = bd.LeerSPRT(Query, ht);
            return i;


        }
    }
}
