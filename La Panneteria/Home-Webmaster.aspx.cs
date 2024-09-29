using BusinessEntities;
using Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

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

            Response.Clear();
            Response.ContentType = "application/octet-stream";

            Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
            Response.TransmitFile(fullpath);
            Response.End();
        }

        protected void TraerLogsAntes(object sender, EventArgs args)
        {
            int pi;
            try
            {
                pi = Convert.ToInt32(contador_pagina.Value);
                if (pi == 0)
                {
                    BotonAntes.Enabled = false;
                    return;
                }
                pi--;
                contador_pagina.Value = pi.ToString();
                TraerLogs(sender, args);
                BotonDespues.Enabled = true;
                return;
            }
            catch
            {
                pi = 0;
                contador_pagina.Value = pi.ToString();
                TraerLogs(sender, args);
                BotonAntes.Enabled = false;
                return;
            }
        }

        protected void TraerLogsDespues(object sender, EventArgs args)
        {
            int pi;
            try
            {
                pi = Convert.ToInt32(contador_pagina.Value);
                pi++;
                contador_pagina.Value = pi.ToString();
                TraerLogs(sender, args);
                return;
            }
            catch
            {
                pi = 1;
                contador_pagina.Value = pi.ToString();
                TraerLogs(sender, args);
                return;
            }
        }

        protected void TraerLogs(object sender, EventArgs args)
        {
            int pi;
            try
            {
                pi = Convert.ToInt32(contador_pagina.Value);
            }
            catch
            {
                pi = 1;
            }

            string crits = "";
            int e = 0;
            if (Crit_Urgente.Checked)
            {
                crits = "1,";
                e = 1;
            }
            if (Crit_Error.Checked)
            {
                if (e > 0)
                {
                    crits += ",";
                }
                crits += "2";
                e = 1;
            }
            if (Crit_Advertencia.Checked)
            {
                if (e > 0)
                {
                    crits += ",";
                }
                crits += "3";
                e = 1;
            }
            if (Crit_inf.Checked)
            {
                if (e > 0)
                {
                    crits += ",";
                }
                crits += "4";
            }
            LogManager lm = new LogManager();
            DateTime fechaInicio;
            if (!DateTime.TryParse(fechai.Text, out fechaInicio))
            {
                fechaInicio = DateTime.Today.AddDays(-1);
            }
            DateTime fechaFin;
            if (!DateTime.TryParse(fechaf.Text, out fechaFin))
            {
                fechaFin = DateTime.Today;
            }

            int j = 0;
            string tabla = "<table class='tabla_logs'><tr><th>Hora</th><th>Criticidad</th><th>Modulo</th><th>Actor</th><th>Mensaje</th></tr>";
            foreach (BEEventoBitacora evento in lm.ListarEventos(logs_actor.Text, fechaInicio, fechaFin, crits, pi))
            {
                tabla += "<tr><td>" + evento.Fecha.ToString() + "</td><td>" + evento.Criticidad + "</td><td>" + evento.Modulo + "</td><td>" + evento.Actor + "</td><td>" + evento.Mensaje + "</td></tr>";
                j++;
            }
            tabla += "</table>";

            contenedor_tabla_eventos.InnerHtml = tabla;
            if (pi == 0)
            {
                BotonAntes.Enabled = false;
            }
            else
            {
                BotonAntes.Enabled = true;
            }
            if (j < 10)
            {
                BotonDespues.Enabled = false;
            }
            else
            {
                BotonDespues.Enabled = true;
            }

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

        }
        protected void CerrarSesion(object sender, EventArgs args)
        {
            HttpCookie cookie2 = new HttpCookie("SessionToken");
            cookie2.Expires = DateTime.Now;
            Response.Cookies.Add(cookie2);

            SessionManager.Logout();
            Response.Redirect("/Default");
        }
        protected void actualizarListaPrecios(object sender, EventArgs args)
        {
            string strFileName;
            string strFilePath;
            string strFolder;
            strFolder = @"C:\tmp\";
            // Retrieve the name of the file that is posted.
            strFileName = uploadFileXml.PostedFile.FileName;
            strFileName = Path.GetFileName(strFileName);
            if (uploadFileXml.Value != "")
            {
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }

                strFilePath = strFolder + strFileName;

                try
                {
                    uploadFileXml.PostedFile.SaveAs(strFilePath);
                    var consulta =
                    from articulo in XElement.Load(strFilePath).Elements("Producto")
                    select new BEArticulo
                    {
                        Codigo = Convert.ToInt32(Convert.ToString(articulo.Element("id").Value).Trim()),
                        Descripcion = Convert.ToString(articulo.Element("nombre").Value).Trim(),
                        PrecioUnitario = Convert.ToDouble((articulo.Element("precio").Value.Trim()))
                    };

                    List<BEArticulo> Articulos = consulta.ToList<BEArticulo>();

                    ProductManager Manager = new ProductManager();

                    Manager.ActualizarPrecioArticulos(Articulos);

                    Response.Write("<script> alert(\"La lista de precios fue actualizada\")  </script>");

                }
                catch (Exception ex)
                {
                }


            }
        }

        protected void descargarListaPrecios(object sender, EventArgs args)
        {
            string strFolder = @"C:\tmp\";
            string fileName = "listaPrecios-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xml";
            string fullpath = strFolder + fileName;

            ProductManager Manager = new ProductManager();

            List<BEArticulo> Articulos = Manager.ListarArticulos();

            if (!Directory.Exists(strFolder))
            {
                Directory.CreateDirectory(strFolder);
            }
            
            try
            {
                new XDocument(new XElement("Productos", "")).Save(fullpath);

                XDocument xmlDoc = XDocument.Load(fullpath);

                foreach (BEArticulo Articulo in Articulos)
                {
                    xmlDoc.Element("Productos").Add(new XElement("Producto",
                                                            new XElement("id", Articulo.Codigo.ToString()),
                                                            new XElement("nombre", Articulo.Descripcion),
                                                            new XElement("precio", Articulo.PrecioUnitario.ToString())));
                }

                xmlDoc.Save(fullpath);

                Response.Clear();
                Response.ContentType = "application/octet-stream";

                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.TransmitFile(fullpath);
                Response.End();
            }
            catch (Exception e)
            {

            }
        }

        protected void GuardarProducto(object sender, EventArgs args)
        {
            if (fileImagenProducto.HasFile)
            {
                try
                {
                    string nombre = txtNombreProducto.Value;
                    double precio = Convert.ToDouble(txtPrecioProducto.Value);
                    int categoria = Convert.ToInt16(ddlCategoria.Value);

                    List<String> Categorias = new List<String>();
                    Categorias.Add("Panes");
                    Categorias.Add("Dulces");
                    Categorias.Add("Salados");

                    string nombreCategoria = Categorias[categoria - 1];

                    string folderPath = Server.MapPath("~/Images/" + nombreCategoria + "/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string filePath = folderPath + Path.GetFileName(fileImagenProducto.FileName);
                    fileImagenProducto.SaveAs(filePath);

                    
                    string strFileName = "Images/" + nombreCategoria + "/" + fileImagenProducto.FileName;

                    BEArticulo Articulo = new BEArticulo();
                    Articulo.Descripcion = nombre;
                    Articulo.PrecioUnitario = precio;
                    Articulo.URL = strFileName;
                    Articulo.Categoria = categoria.ToString();

                    OrderManager orderManager = new OrderManager();
                    orderManager.GuardarArticulo(Articulo);
                }
                catch (Exception ex)
                {
                    Response.Write("<script> alert(\"Fallo al guardar el articulo\")  </script>");
                }
            }
            else
            {
                Response.Write("<script> alert(\"Debe seleccionar una imagen\")  </script>");

            }
        }
    }
}