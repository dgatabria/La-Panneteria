using BusinessEntities;
using Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace La_Panneteria
{
    public partial class WebForm2 : System.Web.UI.Page
    {



        protected void BtnEliminarUsuario(object sender, EventArgs e)
        {
            
            if  (sender is Button)
            {
                string s = ((Button)sender).ID;
                Regex re = new Regex("btn_eliminar_usuario_");

                int i = Convert.ToInt32(re.Replace(s, ""));
                UserManager userManager = new UserManager();
                BEUsuario bEUsuario = new BEUsuario();
                bEUsuario.Codigo = i;
                userManager.Borrar(bEUsuario);
                CargarTablaUsuarios();
            }
        }

        protected void PwChange(object sender, EventArgs e)
        {

            if (sender is TextBox)
            {
                string s = ((TextBox)sender).ID;
                Regex re = new Regex("txt_password_usuario_");

                int i = Convert.ToInt32(re.Replace(s, ""));
                UserManager userManager = new UserManager();
                BEUsuario bEUsuario = new BEUsuario();
                bEUsuario.Codigo = i;
                bEUsuario = userManager.ListarUsuarioPorID(bEUsuario);
                s = ((TextBox)sender).Text;
                userManager.ChangePW(bEUsuario, s);
                CargarTablaUsuarios();
            }
        }
        protected void LockUnlock(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                string s = ((CheckBox)sender).ID;
                CheckBox cb = (CheckBox)sender;
                Regex re = new Regex("cb_locked_usuario_");

                int i = Convert.ToInt32(re.Replace(s, ""));
                UserManager userManager = new UserManager();
                BEUsuario bEUsuario = new BEUsuario();
                bEUsuario.Codigo = i;
                bEUsuario = userManager.ListarUsuarioPorID(bEUsuario);
                if (cb.Checked)
                {
                    userManager.Lock(bEUsuario);
                } else
                {
                    userManager.Unlock(bEUsuario);
                }
                CargarTablaUsuarios();
            }
        }
        protected void BtnCrearUsuario(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                BEUsuario usuario = new BEUsuario();
                usuario.username = ((TextBox)FindControl("txt_nuevo_usuario_username")).Text;
                usuario.Nombre = ((TextBox)FindControl("txt_nuevo_usuario_nombre")).Text;
                usuario.Apellido = ((TextBox)FindControl("txt_nuevo_usuario_apellido")).Text;
                usuario.Password = ((TextBox)FindControl("txt_nuevo_usuario_password")).Text;
                usuario.Idioma = SessionManager.GetInstance.Usuario.Idioma;


                RBACManager rb = new RBACManager();
                usuario.Perfil =  rb.ListarRol(Convert.ToInt32(((DropDownList)FindControl("ddl_nuevo_usuario_perfil")).SelectedValue));

                UserManager userManager= new UserManager();
                userManager.Guardar(usuario);

                CargarTablaUsuarios();
            }
        }

        protected void ReasignaRol(object sender, EventArgs e)
        {
            if (sender is DropDownList)
            {
                string s = ((DropDownList)sender).ID;
                DropDownList ddl = (DropDownList)sender;
                Regex re = new Regex("ddl_perfil_usuario_");

                int i = Convert.ToInt32(re.Replace(s, ""));
                UserManager userManager = new UserManager();
                RBACManager rb = new RBACManager();
                BEUsuario bEUsuario = new BEUsuario();
                bEUsuario.Codigo = i;
                bEUsuario = userManager.ListarUsuarioPorID(bEUsuario);
                BEPerfil bEPerfil = rb.ListarRol(Convert.ToInt32(ddl.SelectedValue));

                bEUsuario.Perfil = bEPerfil;
                userManager.Guardar(bEUsuario);
                CargarTablaUsuarios();
            }
        }
        void CargarTablaUsuarios()
        {
            Security.UserManager userManager = new Security.UserManager();
            RBACManager RBACManager = new RBACManager();
            TableRow row = new TableRow();
            TableCell tableCell = new TableCell();
            DropDownList ddl;
            Button btn;
            TextBox textBox;
            CheckBox cb;
            Table1.Rows.Clear();
            row.BorderWidth = 1;
            row.BackColor = System.Drawing.Color.Beige;
            tableCell.Text = "Usuario";
            row.Cells.Add(tableCell);
            //tableCell.BorderWidth = 1;
            tableCell = new TableCell();

            tableCell = new TableCell();
            tableCell.Text = "Contraseña";
            //tableCell.BorderWidth = 1;
            row.Cells.Add(tableCell);


            tableCell = new TableCell();
            tableCell.Text = "Bloqueado";
            //tableCell.BorderWidth = 1;
            row.Cells.Add(tableCell);


            tableCell = new TableCell();
            tableCell.Text = "Nombre";
            //tableCell.BorderWidth = 1;
            row.Cells.Add(tableCell);


            tableCell = new TableCell();
            tableCell.Text = "Apellido";
            //tableCell.BorderWidth = 1;
            row.Cells.Add(tableCell);

            tableCell = new TableCell();
            tableCell.Text = "Rol";
            //tableCell.BorderWidth = 1;
            row.Cells.Add(tableCell);
            Table1.Rows.Add(row);

            tableCell = new TableCell();
            tableCell.Text = "Acciones";
            //tableCell.BorderWidth = 1;
            row.Cells.Add(tableCell);
            Table1.Rows.Add(row);
            foreach (BEUsuario usuario in userManager.ListarUsuarios())
            {
                row = new TableRow();

                tableCell = new TableCell();
                tableCell.Text = usuario.username;
                row.Cells.Add(tableCell);

                tableCell = new TableCell();

                textBox = new TextBox();
                textBox.ID = "txt_password_usuario_" + usuario.Codigo.ToString();
                textBox.TextMode = TextBoxMode.Password;
                textBox.TextChanged += PwChange;
                tableCell.Controls.Add(textBox);

                
                row.Cells.Add(tableCell);

                cb = new CheckBox();
                cb.Checked = usuario.Locked;
                cb.AutoPostBack = true;
                cb.ID = "cb_locked_usuario_" + usuario.Codigo.ToString();
                
                cb.CheckedChanged += LockUnlock;
                tableCell = new TableCell();
                tableCell.Controls.Add(cb);
                
                row.Cells.Add(tableCell);

                tableCell = new TableCell();
                tableCell.Text = usuario.Nombre;
                row.Cells.Add(tableCell);
                tableCell = new TableCell();
                tableCell.Text = usuario.Apellido;
                row.Cells.Add(tableCell);
                tableCell = new TableCell();

                ddl = new DropDownList();
                ddl.ID = "ddl_perfil_usuario_" + usuario.Codigo.ToString();

                
                foreach (BEPerfil perfil in RBACManager.ListarRoles())
                {
                    ddl.Items.Add(new ListItem(perfil.Nombre, perfil.Codigo.ToString()));
                    if (perfil.Codigo == usuario.Perfil.Codigo)
                    {
                        ddl.SelectedValue = perfil.Codigo.ToString();  
                    }

                }
                ddl.AutoPostBack = true;
                ddl.SelectedIndexChanged += ReasignaRol;
                tableCell.Controls.Add(ddl);
                //tableCell.Text = usuario.Perfil.Nombre.ToString();
                row.Cells.Add(tableCell);
                tableCell = new TableCell();


                btn = new Button();
                btn.Text = "Eliminar";
                btn.ID = "btn_eliminar_usuario_" + usuario.Codigo.ToString();
                btn.Click += BtnEliminarUsuario;
                tableCell.Controls.Add(btn);
                row.Cells.Add(tableCell);

                Table1.Rows.Add(row);
               
            }
            // Creacion de usuarios

            row = new TableRow();

            tableCell = new TableCell();
            textBox = new TextBox();
            textBox.ID = "txt_nuevo_usuario_username";
            tableCell.Controls.Add(textBox);
            row.Cells.Add(tableCell);


            tableCell = new TableCell();
            textBox = new TextBox();
            textBox.TextMode = TextBoxMode.Password;
            textBox.ID = "txt_nuevo_usuario_password";
            tableCell.Controls.Add(textBox);
            row.Cells.Add(tableCell);


            tableCell = new TableCell();
            row.Cells.Add(tableCell);

            tableCell = new TableCell();
            textBox = new TextBox();
            textBox.ID = "txt_nuevo_usuario_nombre";
            tableCell.Controls.Add(textBox);
            row.Cells.Add(tableCell);

            tableCell = new TableCell();
            textBox = new TextBox();
            textBox.ID = "txt_nuevo_usuario_apellido";
            tableCell.Controls.Add(textBox);
            row.Cells.Add(tableCell);

            tableCell = new TableCell();
            ddl = new DropDownList();
            ddl.ID = "ddl_nuevo_usuario_perfil";


            foreach (BEPerfil perfil in RBACManager.ListarRoles())
            {
                ddl.Items.Add(new ListItem(perfil.Nombre, perfil.Codigo.ToString()));
            }
            tableCell.Controls.Add(ddl);
            //tableCell.Text = usuario.Perfil.Nombre.ToString();

            row.Cells.Add(tableCell);
            tableCell = new TableCell();
            btn = new Button();
            btn.Text = "Crear";
            btn.ID = "btn_crear_usuario";
            btn.Click += BtnCrearUsuario;
            tableCell.Controls.Add(btn);
            row.Cells.Add(tableCell);

            Table1.Rows.Add(row);

        }

        protected void CargarTablaRBAC()
        {
            RBACManager rbm = new RBACManager();
            ListBoxRoles.DataSource = null;
            ListBoxRoles.DataSource = rbm.ListarRoles();
            ListBoxRoles.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablaUsuarios();
                CargarTablaRBAC();
                ListBoxRoles.AutoPostBack = true;
                CheckBoxListPermisos.AutoPostBack = true;
                CheckBoxListRoles.AutoPostBack = true;
            }
            

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

        protected void RewriteRole(Object sender, EventArgs e)
        {
            RBACManager rbm = new RBACManager();
            BEPerfil perfseleccionado;
            if (ListBoxRoles.SelectedIndex != -1)
            {
                perfseleccionado = rbm.ListarRol(ListBoxRoles.SelectedValue);
            }

            else { return; }

            BEPerfil perfreescrito = new BEPerfil(perfseleccionado.Codigo,perfseleccionado.Nombre);

            foreach (ListItem item in CheckBoxListRoles.Items)
            {
                if (item.Selected)
                {
                    perfreescrito.AgregarHijo(new BEPerfil(Convert.ToInt32(item.Value), item.Text));
                }
            }
            foreach (ListItem item in CheckBoxListPermisos.Items)
            {
                if (item.Selected)
                {
                    perfreescrito.AgregarHijo(new BEPermiso(Convert.ToInt32(item.Value), item.Text));
                }
            }

            //try
            //{
                rbm.GuardarPerfil(SessionManager.GetInstance.Usuario, perfreescrito);
           // } catch (Exception ex)
            //{
              //  LabelErroresRBAC.Text = ex.Message;
           // }
        }
        protected void ListBoxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            RBACManager rbm = new RBACManager();
            BEPerfil perfseleccionado;
            if (ListBoxRoles.SelectedIndex != -1)
            {
                perfseleccionado = rbm.ListarRol(ListBoxRoles.SelectedValue);
            } else {  return; }
            
            if (perfseleccionado == null){ return; }

            perfseleccionado = rbm.ListarRolFull(perfseleccionado);
            int i = 0;
            CheckBoxListRoles.Items.Clear();

            
            foreach (BEPerfil perfil in rbm.ListarRoles())
            {
                
                if (perfil.Codigo != perfseleccionado.Codigo) {
                    CheckBoxListRoles.Items.Add(new ListItem(perfil.Nombre, perfil.Codigo.ToString(), true));
                } else
                {
                    CheckBoxListRoles.Items.Add(new ListItem(perfil.Nombre, perfil.Codigo.ToString(), false));
                }
                CheckBoxListRoles.Items[i].Selected = rbm.RolContieneRol(perfseleccionado, perfil);
                i++;

            }



            CheckBoxListPermisos.Items.Clear(); 
            CheckBoxListPermisos.DataSource = null;
            bool check;
            List<RBAC> pp = perfseleccionado.ObtenerHijos();
            i = 0;
            foreach (BEPermiso bp in rbm.ListarPermisos())
            {
                check = false;
                
                foreach (RBAC perm in pp)
                {
                    if (perm is BEPermiso)
                    {
                        if (perm.Codigo == bp.Codigo) { check = true; }
                    }
                }
                CheckBoxListPermisos.Items.Add(new ListItem(bp.Nombre, bp.Codigo.ToString(),true));
                CheckBoxListPermisos.Items[i].Selected = check;
                i++;
            }

        }
    }
}