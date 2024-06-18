using Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace La_Panneteria
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void iniciarBackup(object sender, EventArgs args)
        {
            string strFolder = @"C:\tmp\";
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "-lapanneteria.bak";
            string fullpath = strFolder + fileName;
            DatabaseManager dbm = new DatabaseManager();
            dbm.Backup(fullpath);

            
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition",
               string.Format("attachment; filename={0}", fileName));
            Response.TransmitFile(fullpath);
            Response.End();
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
                    //lblUploadResult.Text = strFileName + " Se restauró exitosamente.";
                }
                catch (Exception ex)
                {
                    //lblUploadResult.Text = "Hubo un error: " + ex.Message;
                }


            }





            //DatabaseManager dbm = new DatabaseManager();
            //dbm.RecalculaDV();
            //Response.Redirect("/Default");
        }
    }
}