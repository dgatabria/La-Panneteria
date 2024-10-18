<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home-Webmaster.aspx.cs" Inherits="La_Panneteria.WebForm2" EnableEventValidation="false" %>
<% 
    if (Request.Cookies["SessionToken"] != null)
    {
        HttpCookie SessionCookie = Request.Cookies["SessionToken"];
        //Response.Write("Testeando cookie de sesion: " + SessionCookie.Value.ToString());
        if (SessionCookie.Value.ToString()  != Session.SessionID.ToString())
        {
            Response.Redirect("/Default");
        } else
        {
            if (Security.SessionManager.GetInstance != null)
            {
                switch (Security.SessionManager.GetInstance.Usuario.Perfil.Nombre)
                {
                    case "CLIENTE":
                        Response.Redirect("/Main");
                        break;
                    case "ADMIN":
                        Response.Redirect("/Home-Admin");
                        break;
                }
            } else
                    {
                Response.Redirect("/Default");
                    }
        }
    } else {
        Response.Redirect("/Default");
    }
    %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>La Panneteria</title>
    <link rel="icon" type="image/x-icon" href="../Home/images/favicon.ico">
    <link rel="stylesheet" href="/CSS/WebMasterMain.css">



</head>

<body>

    <script>
        const getCookieValue = (name) => (
            document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)')?.pop() || ''
        )

        // Get the modal
        var modal1 = document.getElementById("ModalBackup");
        var modal2 = document.getElementById("ModalLogs");
        var modal3 = document.getElementById("ModalXml");
        var modal4 = document.getElementById("ModalProductos");
        var modal5 = document.getElementById("ModalUsuarios");
        var modal6 = document.getElementById("ModalRoles");


        // Get the <span> element that closes the modal
        var span1 = document.getElementsByClassName("close")[0];
        var span2 = document.getElementsByClassName("close")[1];
        var span3 = document.getElementsByClassName("close")[2];
        var span4 = document.getElementsByClassName("close")[3];
        var span5 = document.getElementsByClassName("close")[4];
        var span6 = document.getElementsByClassName("close")[5];

        //var span = document.getElementsByClassName("close")[0];
        //var modal_contenido_login = '<span class="close" id="close_btn">&times;</span>';
        //modal_contenido_login += '<table class="tabla_usuarios"><tr class="fila_usuarios"><th class="header_usuarios">Nombre de Usuario</th><th class="header_usuarios">Nombre</th><th class="header_usuarios">Apellido</th><th class="header_usuarios">Rol</th><th class="header_usuarios">Estado</th></tr><tr class="fila_usuarios"><td>admin@lp.com</td><td>Damian</td><td>Gatabria</td><td>Administrador</td><td>Activo</td></tr><tr class="fila_usuarios"><td>webmaster@lp.com</td><td>Eduardo</td><td>Alvarez</td><td>WebMaster</td><td>Activo</td></tr><tr class="fila_usuarios"><td>cliente@lp.com</td><td>Gabriel</td><td>Bernardini</td><td>Cliente</td><td>Activo</td></tr></table>';
        //modal_contenido_login += '</td><td align="center"><br><br><br><button>Desbloquear</button><br><br><button>Cambiar Contraseña</button><br><br><button>Eliminar</button>';
        //modal_contenido_login += '<table class="tabla_usuarios"><tr class="fila_usuarios"><th colspan="4" align="center" class="header_usuarios"><b>Consola de Backup y Restore<b></th><tr><td colspan="4">&nbsp;</td></tr></tr><tr class="fila_usuarios"><th class="header_usuarios">Archivo de Backup:</th><th class="header_usuarios"><input type="text"></input></th><th class="header_usuarios"><button>Examinar</button></th><th class="header_usuarios"><button>Comenzar</button></th></tr><tr class="fila_usuarios"><th class="header_usuarios">Archivo para Restore:</th><th class="header_usuarios"><input type="text"></input></th><th class="header_usuarios"><button>Examinar</button></th><th class="header_usuarios"><button>Comenzar</button></th></tr></table>';
        function mostrar_modal_backup() {
            document.cookie = "webmaster_modal_visible=backup";
            document.getElementById("ModalBackup").style.display = "block";
        }
        function cerrar_modal_backup() {
            document.cookie = "webmaster_modal_visible=none";
            document.getElementById("ModalBackup").style.display = "none";
        }
        function mostrar_modal_logs() {
            document.cookie = "webmaster_modal_visible=logs";
            document.getElementById("ModalLogs").style.display = "block";
        }
        function cerrar_modal_logs() {
            document.cookie = "webmaster_modal_visible=none";
            document.getElementById("ModalLogs").style.display = "none";
        }
        function mostrar_modal_xml() {
            document.cookie = "webmaster_modal_visible=xml";
            document.getElementById("ModalXml").style.display = "block";
        }
        function cerrar_modal_xml() {
            document.cookie = "webmaster_modal_visible=none";
            document.getElementById("ModalXml").style.display = "none";
        }
        function mostrar_modal_productos() {
            document.cookie = "webmaster_modal_visible=productos";
            document.getElementById("ModalProductos").style.display = "block";
        }
        function cerrar_modal_productos() {
            document.cookie = "webmaster_modal_visible=none";
            document.getElementById("ModalProductos").style.display = "none";
        }
        function mostrar_modal_usuarios() {
            document.cookie = "webmaster_modal_visible=usuarios";
            document.getElementById("ModalUsuarios").style.display = "block";
        }
        function cerrar_modal_usuarios() {
            document.cookie = "webmaster_modal_visible=none";
            document.getElementById("ModalUsuarios").style.display = "none";
        }
        function mostrar_modal_roles() {
            document.cookie = "webmaster_modal_visible=roles";
            document.getElementById("ModalRoles").style.display = "block";
        }
        function cerrar_modal_roles() {
            document.cookie = "webmaster_modal_visible=none";
            document.getElementById("ModalRoles").style.display = "none";
        }

        window.onclick = function (event) {
            var modal1 = document.getElementById("ModalBackup");
            var modal2 = document.getElementById("ModalLogs");
            var modal3 = document.getElementById("ModalXml");
            var modal4 = document.getElementById("ModalProductos");
            var modal5 = document.getElementById("ModalUsuarios");
            var modal6 = document.getElementById("ModalRoles");

            if (event.target == modal1) {
                modal1.style.display = "none";
                document.cookie = "webmaster_modal_visible=none";
            }
            if (event.target == modal2) {
                modal2.style.display = "none";
                document.cookie = "webmaster_modal_visible=none";
            }
            if (event.target == modal3) {
                modal3.style.display = "none";
                document.cookie = "webmaster_modal_visible=none";
            }
            if (event.target == modal4) {
                modal4.style.display = "none";
                document.cookie = "webmaster_modal_visible=none";
            }
            if (event.target == modal5) {
                modal5.style.display = "none";
                document.cookie = "webmaster_modal_visible=none";
            }
            if (event.target == modal6) {
                modal6.style.display = "none";
                document.cookie = "webmaster_modal_visible=none";
            }
            
        }
        function CerrarSesion() {

            document.cookie = 'SessionToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC";'
            location.reload();

        }

        function ValidarProducto() {

	    var nombre = document.getElementById("txtNombreProducto").value;
	    var precio = parseFloat(document.getElementById("txtPrecioProducto").value);
	    var categoria = document.getElementById("ddlCategoria").value;

	    if (nombre === "") {
		    alert("Debe elegir un nombre valido");
		    return false;
	    }

	    if (isNaN(precio) || precio <= 0) {
		    alert("Debe elegir un precio valida");
		    return false;
	    }

	    if (categoria === "") {
		    alert("Debe elegir una categoria valida");
		    return false;
	    }
	    return true;
    }

    </script>

       
    <div class="grid">
        <header>
            
          <ul class="header">
            <li><a href="#inicio"><img src="/Images/Logo/logonegro.png" height="50" /></a></li>
          </ul>
        </header>
        <logout>
                <ul class="logout">
                 <li><a runat="server" onserverclick="CerrarSesion" name="logout" id="logout"><p>Cerrar Sesi&oacute;n</p></a></li>
                </ul>
        </logout>
        <titulo>
            <div style="width:100%;background-color:whitesmoke;align-content:center;text-align:center;font-family: 'Brush Script MT', cursive;font-size: 60px;">
                <p>Panel del WebMaster</p>
            </div>
        </titulo>
        <div class="menu" id="menu_container">
                        <div align="center" class="menuitem" onclick="mostrar_modal_backup()"><br /><img src="/Images/WebMaster/Backup.jpg" alt="Backup y Restore" width="95%">
                        <p id="texto_backup"><b>Backup y Restore</b></p></div>
                        <div align="center" class="menuitem" onclick="mostrar_modal_logs()"><br /><img src="/Images/WebMaster/logs.jpg" alt="Bitácora del sistema" width="95%">
                        <p id="texto_logs"><b>Bit&aacute;cora del sistema</b></p></div>
                        <div align="center" class="menuitem" onclick="mostrar_modal_xml()"><br /><img src="/Images/WebMaster/lista.png" alt="XML" width="95%">
                        <p id="texto_logs"><b>Lista de precios</b></p></div>
                        <div align="center" class="menuitem" onclick="mostrar_modal_productos()"><br /><img src="/Images/WebMaster/productos.jpg" alt="ABM Productos" width="95%">
                        <p id="texto_logs"><b>ABM de Productos</b></p></div>
                        <div align="center" class="menuitem" onclick="mostrar_modal_usuarios()"><br /><img src="/Images/WebMaster/ABMusuarios.jpg" alt="ABM Productos" width="95%">
                        <p id="texto_logs"><b>ABM de Usuarios</b></p></div>
                        <div align="center" class="menuitem" onclick="mostrar_modal_roles()"><br /><img src="/Images/WebMaster/ABMroles.jpg" alt="ABM Productos" width="95%">
                        <p id="texto_logs"><b>ABM de Roles</b></p></div>

        </div> 
<form runat="server"> 
    <!-- The Modal -->
     
<div id="ModalLogs" class="modal">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
  <!-- Modal content -->
  <div class="modal-content" style="justify-content:center" id="modal-content-logs">
    <span class="close" id="close_btn3" onclick="cerrar_modal_logs()">&times;</span>

   <table class="tabla_usuarios">
        <tr class="fila_usuarios">
              <th colspan="8" align="center" class="header_usuarios"><b>&nbsp;&nbsp;Bit&aacute;cora del sistema&nbsp;&nbsp;</b></th>
        </tr> 
        <tr>
              <td>&nbsp;Desde Fecha:&nbsp;</td>
              <td><asp:TextBox type="DateTimePicker" TextMode="Date" runat="server" name="fechai" id="fechai" OnTextChanged="TraerLogs"  AutoPostBack="True" /></td>
              <td colspan="4"> &nbsp; </td>
              <td>&nbsp;Hasta Fecha:&nbsp;</td>
              <td><asp:TextBox TextMode="Date" runat="server" name="fechaf" id="fechaf" OnTextChanged="TraerLogs"  AutoPostBack="True" /></td>
        </tr>
        <tr>
              <td>&nbsp;Criticidad:&nbsp;</td>
              <td><asp:CheckBox runat="server" name="chk_Urgente" id="Crit_Urgente" OnCheckedChanged="TraerLogs" AutoPostBack="True" /><label for="Crit_Urgente">Urgente</label></td>
              <td><asp:CheckBox runat="server" name="chk_Error" id="Crit_Error" OnCheckedChanged="TraerLogs" AutoPostBack="True" /><label for="Crit_Error">Error</label></td>
              <td><asp:CheckBox runat="server" name="chk_Advertencia" id="Crit_Advertencia" OnCheckedChanged="TraerLogs" AutoPostBack="True" /><label for="Crit_Advertencia">Advertencia</label></td>
              <td><asp:CheckBox runat="server" name="chk_Inf" id="Crit_inf" OnCheckedChanged="TraerLogs" AutoPostBack="True" /><label for="Crit_inf">Informativo</label></td>
              <td>&nbsp;<input type="hidden" runat="server" id="contador_pagina" name="contador_pagina" value="0" /></td>
              <td>&nbsp;Actor:&nbsp;</td>
              <td><asp:TextBox runat="server" size="15" maxlength="50" OnTextChanged="TraerLogs" name="logs_actor"  id="logs_actor" AutoPostBack="True" /> </td>
        </tr>
        <tr>
            <td colspan="8" id="contenedor_tabla_eventos" name="contenedor_tabla_eventos" runat="server">
                &nbsp;
            </td>
        </tr>
        <tr class="fila_usuarios">
              <td colspan="8" align="center">
                    <asp:Button runat='server' name="BotonAntes" id="BotonAntes" onclick="TraerLogsAntes" Text="<" ></asp:Button>&nbsp;&nbsp;
                    <asp:Button runat='server' name="BotonDespues" id="BotonDespues" onclick="TraerLogsDespues" Text=">" ></asp:Button>
              </td >
        </tr >
   </table >       

  </div>

        </ContentTemplate>
        </asp:UpdatePanel>     
</div>

<div id="ModalBackup" class="modal">
    
   
        <ContentTemplate>
  <!-- Modal content -->
  <div class="modal-content" style="justify-content:center" id="modal-content-backup">
        <span class="close" id="close_btn4" onclick="cerrar_modal_backup()">&times;</span>
   <table class="tabla_usuarios">
        <tr class="fila_usuarios">
              <th colspan="6" align="center" class="header_usuarios"><b>&nbsp;&nbsp;Consola de Backup y Restore&nbsp;&nbsp;</b></th>
        </tr> 
        <tr>
              <td>&nbsp;Restore:&nbsp;</td>
              <td colspan="4"><input id="uploadFile" runat="server" name="uploadFile" type="file" accept=".bak" /></td>
              <td><asp:Button runat='server' onClick='iniciarRestore' name="BotonRestore" id="BotonRestore" Text="Iniciar Restore" AutoPostBack="True"></asp:Button></td>
        </tr>
        <tr>
              <td>&nbsp;Backup:&nbsp;</td>
              <td colspan="5"><asp:Button runat='server' onClick='iniciarBackup' name="BotonBackup" id="BotonBackup" Text="Descargar Backup" AutoPostBack="True"></asp:Button></td>
        </tr>
        <tr class="fila_usuarios">
              <td colspan="6" align="center"><button name="BotonCerrar" id="BotonCerrar" onclick='document.getElementById("myModal").style.display="none"'>Cerrar</button>&nbsp;&nbsp;
              </td >
        </tr >
   </table > 
  </div>
        </ContentTemplate>
      
</div>


<div id="ModalXml" class="modal">
    
   
        <ContentTemplate>
  <!-- Modal content -->
  <div class="modal-content" style="justify-content:center" id="modal-content-xml">
        <span class="close" id="close_btn6" onclick="cerrar_modal_xml()">&times;</span>
   <table class="tabla_usuarios">
        <tr class="fila_usuarios">
              <th colspan="6" align="center" class="header_usuarios"><b>&nbsp;&nbsp;Administración de Precios&nbsp;&nbsp;</b></th>
        </tr> 
        <tr>
              <td>&nbsp;Importar&nbsp;</td>
              <td colspan="4"><input id="uploadFileXml" runat="server" name="uploadFileXml" type="file" accept=".xml" /></td>
              <td><asp:Button runat='server' onClick='actualizarListaPrecios' name="BotonRestore" id="Button1" Text="Actualizar Precios" AutoPostBack="True"></asp:Button></td>
        </tr>
        <tr>
              <td>&nbsp;Exportar&nbsp;</td>
              <td colspan="5"><asp:Button runat='server' onClick='descargarListaPrecios' name="BotonBackup" id="Button2" Text="Exportar lista de precios" AutoPostBack="True"></asp:Button></td>
        </tr>
        <tr class="fila_usuarios">
              <td colspan="6" align="center"><button name="BotonCerrar" id="BotonCerrar" onclick='document.getElementById("myModal").style.display="none"'>Cerrar</button>&nbsp;&nbsp;
              </td >
        </tr >
   </table > 
  </div>
        </ContentTemplate>
      
</div>

<div id="ModalProductos" class="modal">
    <ContentTemplate>
<div class="modal-content" style="justify-content:center" id="modal-content-productos">
    <span class="close" id="close_btn2" onclick="cerrar_modal_productos()">&times;</span>
<table class="tabla_usuarios">
    <tr class="fila_usuarios">
        <th colspan="6" align="center" class="header_usuarios">
            <b>&nbsp;&nbsp;Administración de productos&nbsp;&nbsp;</b>
        </th>
    </tr>

    <tr>
        <td>&nbsp;Nombre del Producto&nbsp;</td>
        <td colspan="5">
            <input id="txtNombreProducto" runat="server" type="text" placeholder="Ingrese el nombre del producto" style="width:100%;" />
        </td>
    </tr>

    <tr>
        <td>&nbsp;Precio&nbsp;</td>
        <td colspan="5">
            <input id="txtPrecioProducto" runat="server" type="text" placeholder="Ingrese el precio" style="width:100%;" />
        </td>
    </tr>

    <tr>
        <td>&nbsp;Imagen&nbsp;</td>
        <td colspan="4">
            <asp:FileUpload ID="fileImagenProducto" runat="server" /> 
        </td>
            </tr>

    <tr>
        <td>&nbsp;Categoría&nbsp;</td>
        <td colspan="5">
            <select id="ddlCategoria" runat="server" style="width:100%;">
                <option value="">Seleccione una categoría</option>
                <option value="1">Panes</option>
                <option value="2">Dulces</option>
                <option value="3">Salados</option>
            </select>
        </td>
    </tr>

    <tr>
        <td colspan="6" align="center">
        <asp:Button ID="btnGuardarProducto" runat="server" Text="Guardar Producto" OnClientClick="return ValidarProducto()" OnClick="GuardarProducto" />
        </td>
    </tr>
</table>

</div>
    </ContentTemplate>
      
</div>



    <div id="ModalUsuarios" class="modal" >
    <ContentTemplate>
<div class="modal-content" style="justify-content:center" id="modal-content-usuarios">
    <span class="close" id="close_btn5" onclick="cerrar_modal_usuarios()">&times;</span>

    <asp:Table ID="Table1" runat="server" CssClass="tabla_usuarios" BorderWidth="1"></asp:Table>
    <br />
</div>
    </ContentTemplate>
      
</div>
        <div id="ModalRoles" class="modal" >
    <ContentTemplate>
<div class="modal-content" style="justify-content:center" id="modal-content-roles">
    <span class="close" id="close_btn7" onclick="cerrar_modal_roles()">&times;</span>
    <table class="tabla_usuarios">
        <tr align="center">
            <td colspan="2" ><b>Administracion de roles</b></td>
        </tr>
        <tr align="center">
            <td colspan="2"><br />Seleccione un rol</td>
        </tr>
        <tr align="center">
            <td colspan="2">
                    <asp:ListBox  ID="ListBoxRoles" runat="server" OnSelectedIndexChanged="ListBoxRoles_SelectedIndexChanged"></asp:ListBox>
            </td>
        </tr>
        <tr align="center">
            <td>Roles</td>
            <td>Permisos</td>
        </tr>
        <tr align="center">
            <td>
                <asp:CheckBoxList ID="CheckBoxListRoles" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RewriteRole"></asp:CheckBoxList>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxListPermisos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RewriteRole"></asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td colspan="2"> <asp:Label ID="LabelErroresRBAC" runat="server" Text="Label"></asp:Label> </td>
        </tr>
        
    </table>
    
    <br />
</div>
    </ContentTemplate>
      
</div>
<asp:HiddenField ID="hfModalVisible" runat="server" Value="none" />


</form>  

        <footer>
            <br />
            <p><b>Direccion:</b> Alberdi 534, CABA, Buenos Aires</p>
            <p><b>Teléfono:</b> 4635-3753</p>
            <p><b>Mail:</b> consultas@panneteria.com</p>
            <p><b>Instragram:</b> @Panneteria</p>          
        </footer>
      </div>
</body>
    <script>
        let x = getCookieValue("webmaster_modal_visible");
        if (x === "usuarios") {
            mostrar_modal_usuarios();
        }
        if (x === "backup") {
            mostrar_modal_backup();
        }
        if (x === "xml") {
            mostrar_modal_xml();
        }
        if (x === "productos") {
            mostrar_modal_productos();
        }
        if (x === "roles") {
            mostrar_modal_roles();
        }
    </script>
</html>


