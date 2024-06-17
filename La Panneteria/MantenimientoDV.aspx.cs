using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using Security;
using System.IO;
using static System.Net.WebRequestMethods;

namespace La_Panneteria
{
    public partial class MantenimientoDV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, "");
        }

        protected void recalcularDV(object sender, EventArgs args)
        {
            DatabaseManager dbm = new DatabaseManager();
            dbm.RecalculaDV();
            Response.Redirect("/Default");
        }
        protected void iniciarRestore(object sender, EventArgs args)
        {
            string strFileName;
            string strFilePath;
            string strFolder;
            strFolder = @"C:\tmp\";
            // Retrieve the name of the file that is posted.
            strFileName = uploadFile.PostedFile.FileName;
            strFileName = Path.GetFileName(strFileName);
            if (uploadFile.Value != "")
            {
                // Create the folder if it does not exist.
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }
                // Save the uploaded file to the server.
                strFilePath = strFolder + strFileName;
                try
                {
                    uploadFile.PostedFile.SaveAs(strFilePath);
                    DatabaseManager dbm = new DatabaseManager();
                    dbm.Restore(strFilePath);
                    lblUploadResult.Text = strFileName + " Se restauró exitosamente.";                    
                }
                catch (Exception ex)
                {
                    lblUploadResult.Text = "Hubo un error: " + ex.Message;
                }


            }




            
            //DatabaseManager dbm = new DatabaseManager();
            //dbm.RecalculaDV();
            //Response.Redirect("/Default");
        }
    }
}