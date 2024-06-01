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
    <!-- The Modal -->
<div id="myModal" class="modal">

  <!-- Modal content -->
  <div class="modal-content" style="justify-content:center" id="modal-content">
      
    <span class="close" id="close_btn">&times;</span>
  <br /><br />
  </div>

</div>
    <script>

        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the <span> element that closes the modal
        //var span = document.getElementsByClassName("close")[0];
        var span = document.getElementsByClassName("close")[0];
        var modal_contenido_login = '<span class="close" id="close_btn">&times;</span>';
        //modal_contenido_login += '<table class="tabla_usuarios"><tr class="fila_usuarios"><th class="header_usuarios">Nombre de Usuario</th><th class="header_usuarios">Nombre</th><th class="header_usuarios">Apellido</th><th class="header_usuarios">Rol</th><th class="header_usuarios">Estado</th></tr><tr class="fila_usuarios"><td>admin@lp.com</td><td>Damian</td><td>Gatabria</td><td>Administrador</td><td>Activo</td></tr><tr class="fila_usuarios"><td>webmaster@lp.com</td><td>Eduardo</td><td>Alvarez</td><td>WebMaster</td><td>Activo</td></tr><tr class="fila_usuarios"><td>cliente@lp.com</td><td>Gabriel</td><td>Bernardini</td><td>Cliente</td><td>Activo</td></tr></table>';
        //modal_contenido_login += '</td><td align="center"><br><br><br><button>Desbloquear</button><br><br><button>Cambiar Contraseña</button><br><br><button>Eliminar</button>';
        modal_contenido_login += '<table class="tabla_usuarios"><tr class="fila_usuarios"><th colspan="4" align="center" class="header_usuarios"><b>Consola de Backup y Restore<b></th><tr><td colspan="4">&nbsp;</td></tr></tr><tr class="fila_usuarios"><th class="header_usuarios">Archivo de Backup:</th><th class="header_usuarios"><input type="text"></input></th><th class="header_usuarios"><button>Examinar</button></th><th class="header_usuarios"><button>Comenzar</button></th></tr><tr class="fila_usuarios"><th class="header_usuarios">Archivo para Restore:</th><th class="header_usuarios"><input type="text"></input></th><th class="header_usuarios"><button>Examinar</button></th><th class="header_usuarios"><button>Comenzar</button></th></tr></table>';
        function mostrar_modal() {
      
            document.getElementById("modal-content").innerHTML = modal_contenido_login;
            document.getElementById("myModal").style.display = "block";
        }


        // When the user clicks anywhere outside of the modal, close it
        span.onclick = function () {
            modal.style.display = "none";
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
            <div style="width:100%;background-color:whitesmoke;align-content:center;text-align:center;font-family: 'Brush Script MT', cursive;font-size: 60px;">
                <p>Panel del WebMaster</p>
            </div>
        </titulo>
        <div class="menu" id="menu_container">
                        <div align="center" class="menuitem" onclick="mostrar_modal()"><br /><img src="/Images/WebMaster/Backup.jpg" alt="Backup y Restore" width="95%">
                        <p id="texto_backup"><b>Backup y Restore</b></p></div>
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


