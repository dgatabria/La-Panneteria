<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MantenimientoDV.aspx.cs" Inherits="La_Panneteria.MantenimientoDV" EnableEventValidation="false" %>
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
            //Response.Write("<script>alert(\"" + Security.SessionManager.GetInstance.Usuario.Perfil.Nombre + "\")</script>");
            if (Security.SessionManager.GetInstance.Usuario.Perfil.Nombre != "WEBMASTER")
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
    <link rel="stylesheet" href="/CSS/MantenimientoDV.css">

   

</head>

<body style="background-color: #f58f8f">




    <script>

        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the <span> element that closes the modal

        var span = document.getElementsByClassName("close")[0];

        function mostrar_modal_dv() {
      
            document.getElementById("modal-content").innerHTML = document.getElementById("oculto_recupera_dv").innerHTML;
            document.getElementById("myModal").style.display = "block";
        }
        function mostrar_modal_restore() {

            document.getElementById("modal-content").innerHTML = document.getElementById("oculto_restore").innerHTML;
            document.getElementById("myModal").style.display = "block";
        }

        // When the user clicks anywhere outside of the modal, close it
        span.onclick = function () {
            document.getElementById("myModal").style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
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
            <div style="width:100%;background-color:whitesmoke;align-content:center;text-align:center;font-size: 60px;">
                <p>¡La base de datos está dañada!</p>
            </div>
   
        </titulo>
        <mensaje>
    <div style="width:100%;background-color:whitesmoke;align-content:center;text-align:center;">
    <%   
        // no llego a la bll desde aca. PAsar por SecurityLayer.
        Security.DatabaseManager dbm = new Security.DatabaseManager();
        System.Data.DataTable dt;
        dt = dbm.CalculaDV();
        //DB.
        int rc = Convert.ToInt32(dt.Rows[0]["ERROR_CODE"]);
        int failed_h = Convert.ToInt32(dt.Rows[0]["FAILED_H"]);
        int failed_v = Convert.ToInt32(dt.Rows[0]["FAILED_V"]);
        string table_name = Convert.ToString(dt.Rows[0]["TABLE_NAME"]);
        string message = Convert.ToString(dt.Rows[0]["MESSAGE"]);

        Response.Write(message);
        %>
       </div>  
        </mensaje>
    <!-- The Modal -->
    <form runat="server"> 
        <div id="myModal" class="modal">
             <!-- Modal content -->
             <div class="modal-content" style="justify-content:center" id="modal-content">
                   <br /><br />
             </div>
        </div>
    </form>
    <!-- ventana de recuperacion de dv -->
<div id="oculto_recupera_dv" style="visibility:hidden">
    <span class="close" id="close_btn">&times;</span>
   <table class="tabla_usuarios">
        <tr class="fila_usuarios">
              <th colspan="4" align="center" class="header_usuarios"><b>&nbsp;&nbsp;¿Recalcular el Dígito verificador?&nbsp;&nbsp;</b></th>
        </tr> 
        <tr>
              <td colspan="4">&nbsp;</td>
        </tr>
        <tr class="fila_usuarios">
              <td colspan="4" align="center"><button onclick='document.getElementById("myModal").style.display="none"'>Cancelar</button>&nbsp;&nbsp;
                                            <button type="button" runat='server' onServerClick='recalcularDV'>Iniciar</button>
              </td >
        </tr >
   </table > 
</div>

    <!-- ventana de restore -->
<div id="oculto_restore" style="visibility:hidden">
    <span class="close" id="close_btn2">&times;</span>
   <table class="tabla_usuarios">
        <tr class="fila_usuarios">
              <th colspan="4" align="center" class="header_usuarios"><b>&nbsp;&nbsp;Iniciar Restore&nbsp;&nbsp;</b></th>
        </tr> 
        <tr>
              <td colspan="4"><input id="uploadFile" runat="server" name="uploadFile" type="file" accept=".bak" /></td>
        </tr>
        <tr>
              <td colspan="4"><asp:label ID="lblUploadResult" runat="server"></asp:label></td>
        </tr>
        <tr class="fila_usuarios">
              <td colspan="4" align="center"><button onclick='document.getElementById("myModal").style.display="none"'>Cancelar</button>&nbsp;&nbsp;
                                            <button type="button" runat='server' onServerClick='iniciarRestore'>Iniciar</button>
              </td >
        </tr >
   </table > 
</div>
        <div class="menu" id="menu_container">
                        <div align="center" class="menuitem" onclick="mostrar_modal_restore()"><br /><img src="/Images/WebMaster/Backup.jpg" alt="Backup y Restore" width="95%">
                        <p id="texto_backup"><b>Restaurar la Base de Datos</b></p></div>
                        <div align="center" class="menuitem" onclick="mostrar_modal_dv()"><br /><img src="/Images/WebMaster/DV.jpg" alt="Digito Verificador" width="95%">
                        <p id="texto_reparar"><b>Recalcular los DD.VV.</b></p></div>
        </div> 
        
       
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

