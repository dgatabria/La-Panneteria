using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BusinessEntities;
using System.Data;

namespace Security
{
    public class DatabaseManager
    {
        BLLBD bd = new BLLBD();
        public void Backup(string path)
        {
            bd.Backup(SessionManager.GetInstance.Usuario, path);
        }
        public void Restore(string path)
        {
            bd.Restore(SessionManager.GetInstance.Usuario, path);
        }
        public void RecalculaDV()
        {
            bd.RecalculaDV();
        }
        public DataTable CalculaDV()
        {
            return bd.CalculaDV();
        }
    }
}
