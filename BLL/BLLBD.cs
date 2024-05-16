using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BusinessEntities;


namespace BLL
{
    public class BLLBD
    {
        Acceso bd;
        public void Backup(BEUsuario autor, string path)
        {
            Hashtable tmp = new Hashtable();

            string Query = "DBBackup";
            bd = new Acceso();
            Hashtable ht = new Hashtable();
            ht.Add("@Autor", autor.Codigo);
            ht.Add("@Path", path);

            bd.LeerSPRT(Query, ht);
        }
        public void Restore(BEUsuario autor, string path)
        {
            Hashtable tmp = new Hashtable();

            string Query = " ALTER DATABASE [PASTALINDA] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            bd = new Acceso();
            bd.EscribirConsulta(Query);
            Query = "USE master RESTORE DATABASE [PASTALINDA] FROM DISK = '" + path + "' WITH REPLACE;";
            bd.EscribirConsulta(Query);
            Query = "ALTER DATABASE [PASTALINDA] SET MULTI_USER;";
            bd.EscribirConsulta(Query);

        }

        public void RecalculaDV()
        {
            Hashtable tmp = new Hashtable();

            string Query = "RecalculaDV";
            bd = new Acceso();
            Hashtable ht = new Hashtable();


            bd.LeerSPRT(Query, ht);
        }
    }
}
