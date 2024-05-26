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
    <link rel="stylesheet" href="/CSS/AdminMain.css">
</head>

<body>
    <script>
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
                        <div align="center" class="menuitem"><br /><img src="/Images/WebMaster/Backup.jpg" alt="Backup y Restore" width="95%">
                        <p id="texto_insumos"><b>Backup y Restore</b></p></div>
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
        ListarTodo();
        ActualizarPedido();

    </script>
</html>


