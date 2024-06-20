<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home-Webmaster.aspx.cs" Inherits="La_Panneteria.WebForm2" %>
<% 
    if (Request.Cookies["SessionToken"] != null)
    {
        HttpCookie SessionCookie = Request.Cookies["SessionToken"];
        //Response.Write("Testeando cookie de sesion: " + SessionCookie.Value.ToString());
        if (! Security.SessionManager.VerificarToken(SessionCookie.Value.ToString()))
        {
            Response.Redirect("/Default");
        } else
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

        // Get the modal
        var modal1 = document.getElementById("ModalBackup");
        var modal2 = document.getElementById("ModalLogs");

        // Get the <span> element that closes the modal
        var span1 = document.getElementsByClassName("close")[0];
        var span2 = document.getElementsByClassName("close")[1];
        //var span = document.getElementsByClassName("close")[0];
        //var modal_contenido_login = '<span class="close" id="close_btn">&times;</span>';
        //modal_contenido_login += '<table class="tabla_usuarios"><tr class="fila_usuarios"><th class="header_usuarios">Nombre de Usuario</th><th class="header_usuarios">Nombre</th><th class="header_usuarios">Apellido</th><th class="header_usuarios">Rol</th><th class="header_usuarios">Estado</th></tr><tr class="fila_usuarios"><td>admin@lp.com</td><td>Damian</td><td>Gatabria</td><td>Administrador</td><td>Activo</td></tr><tr class="fila_usuarios"><td>webmaster@lp.com</td><td>Eduardo</td><td>Alvarez</td><td>WebMaster</td><td>Activo</td></tr><tr class="fila_usuarios"><td>cliente@lp.com</td><td>Gabriel</td><td>Bernardini</td><td>Cliente</td><td>Activo</td></tr></table>';
        //modal_contenido_login += '</td><td align="center"><br><br><br><button>Desbloquear</button><br><br><button>Cambiar Contraseña</button><br><br><button>Eliminar</button>';
        //modal_contenido_login += '<table class="tabla_usuarios"><tr class="fila_usuarios"><th colspan="4" align="center" class="header_usuarios"><b>Consola de Backup y Restore<b></th><tr><td colspan="4">&nbsp;</td></tr></tr><tr class="fila_usuarios"><th class="header_usuarios">Archivo de Backup:</th><th class="header_usuarios"><input type="text"></input></th><th class="header_usuarios"><button>Examinar</button></th><th class="header_usuarios"><button>Comenzar</button></th></tr><tr class="fila_usuarios"><th class="header_usuarios">Archivo para Restore:</th><th class="header_usuarios"><input type="text"></input></th><th class="header_usuarios"><button>Examinar</button></th><th class="header_usuarios"><button>Comenzar</button></th></tr></table>';
        function mostrar_modal_backup() {
      
            document.getElementById("ModalBackup").style.display = "block";
        }
        
        function mostrar_modal_logs() {

            document.getElementById("ModalLogs").style.display = "block";
           
        }
        // When the user clicks anywhere outside of the modal, close it
        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            var modal1 = document.getElementById("ModalBackup");
            var modal2 = document.getElementById("ModalLogs");
            if (event.target == modal1) {
                modal1.style.display = "none";
            }
            if (event.target == modal2) {
                modal2.style.display = "none";
            }
            
        }
        function CerrarSesion() {

            document.cookie = 'SessionToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC";'
            location.reload();

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
                  <li><a href="#logout" onclick="CerrarSesion()"><p>Cerrar Sesi&oacute;n</p></a></li>
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
        </div> 
<form runat="server"> 
    <!-- The Modal -->
     
<div id="ModalLogs" class="modal">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
  <!-- Modal content -->
  <div class="modal-content" style="justify-content:center" id="modal-content-logs">
    <span class="close" id="close_btn3" onclick="document.getElementById('ModalLogs').style.display='none'">&times;</span>

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
        <span class="close" id="close_btn2" onclick="document.getElementById('ModalBackup').style.display='none'">&times;</span>
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
              <td colspan="4" align="center"><button name="BotonCerrar" id="BotonCerrar" onclick='document.getElementById("myModal").style.display="none"'>Cerrar</button>&nbsp;&nbsp;
              </td >
        </tr >
   </table > 
  </div>
        </ContentTemplate>
      
</div>


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


    </script>
</html>


