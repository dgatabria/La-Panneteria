<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home-Admin.aspx.cs" Inherits="La_Panneteria.Home_Admin" EnableEventValidation="false"  %>
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
                    case "WEBMASTER":
                        Response.Redirect("/Home-WebMaster");
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
    <link rel="stylesheet" href="/CSS/AdminMain.css">
</head>

<body>
<form runat="server"> 

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- The Modal -->
<div id="myModal" class="modal">

  <!-- Modal content -->
  <div class="modal-content" style="justify-content:center" id="modal-content">
      
    <span class="close" id="close_btn">&times;</span>
  <br /><br />
  </div>

</div>
    </form>
    <script>
        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the <span> element that closes the modal
        //var span = document.getElementsByClassName("close")[0];
        var span = document.getElementsByClassName("close")[0];
        var modal_contenido_usuarios = '<span class="close" id="close_btn">&times;</span><table><tr class="fila_usuarios"><th colspan="6" align="center" class="header_usuarios"><b>Consola de Administraci&oacute;n de Usuarios<b></th></tr><tr><tr><td><br></td></tr><td>';
        modal_contenido_usuarios += '<table class="tabla_usuarios"><tr class="fila_usuarios"><th class="header_usuarios">Nombre de Usuario</th><th class="header_usuarios">Nombre</th><th class="header_usuarios">Apellido</th><th class="header_usuarios">Rol</th><th class="header_usuarios">Bloqueado</th><th class="header_usuarios">Acciones</th></tr><tr class="fila_usuarios"><td>admin@lp.com</td><td>Damian</td><td>Gatabria</td><td>Administrador</td><td align="center"><input type="checkbox" /></td><td align="center"><img src="/Images/Admin/delete.svg" /><img src="/Images/Admin/pass.svg" /></td>  </tr><tr class="fila_usuarios"><td>webmaster@lp.com</td><td>Eduardo</td><td>Alvarez</td><td>WebMaster</td><td align="center"><input type="checkbox" /></td><td align="center"><img src="/Images/Admin/delete.svg" /><img src="/Images/Admin/pass.svg" /></td>   </tr><tr class="fila_usuarios"><td>cliente@lp.com</td><td>Gabriel</td><td>Bernardini</td><td>Cliente</td><td align="center"><input type="checkbox" /></td><td align="center"><img src="/Images/Admin/delete.svg" /><img src="/Images/Admin/pass.svg" /></td>  </tr><tr><td><input type="text"></input></td><td><input type="text"></input></td><td><input type="text"></input></td><td><select><option selected>Cliente</option><option>Administrador</option><option>WebMaster</option></select></td><td colspan="2" align="center"><button>Crear Usuario</button></tr></table>';
        modal_contenido_usuarios += '</td></tr></table>'

        var modal_contenido_productos = '<span class="close" id="close_btn">&times;</span><table><tr class="fila_usuarios"><th colspan="6" align="center" class="header_usuarios"><b>Consola de Administraci&oacute;n de Productos<b></th></tr><tr><tr><td><br></td></tr><td>';
        modal_contenido_productos += '<table class="tabla_usuarios"><tr class="fila_usuarios"><th class="header_usuarios">Descripcion</th><th class="header_usuarios">Precio Unitario</th><th class="header_usuarios">Imagen</th><th class="header_usuarios">Categoria</th><th class="header_usuarios">Acciones</th></tr><tr class="fila_usuarios"><td>Miga Triple J/Q</td><td>500</td><td align="center"><button>...</button></td><td>Salados</td><td align="center"><img src="/Images/Admin/delete.svg" /></td>  </tr><tr class="fila_usuarios"><td>Porcion Torta Selva Negra</td><td>2000</td><td align="center"><button>...</button></td><td>Dulces</td><td align="center"><img src="/Images/Admin/delete.svg" /></td>   </tr><tr class="fila_usuarios"><td>Flautita</td><td>150</td><td align="center"><button>...</button></td><td>Panes</td><td align="center"><img src="/Images/Admin/delete.svg" /></td>  </tr><tr><td><input type="text"></input></td><td><input type="text"></input></td><td><input type="text"></input></td><td><select><option selected>Salados</option><option>Dulces</option><option>Panes</option></select></td><td align="center"><button>Crear</button></tr></table>';
        modal_contenido_productos += '</td></tr></table>'

        var modal_contenido_insumos = '<span class="close" id="close_btn">&times;</span><table><tr class="fila_usuarios"><th colspan="6" align="center" class="header_usuarios"><b>Consola de Administraci&oacute;n de Insumos<b></th></tr><tr><tr><td><br></td></tr><td>';
        modal_contenido_insumos += '<table class="tabla_usuarios"><tr class="fila_usuarios"><th class="header_usuarios">Descripcion</th><th class="header_usuarios">Acciones</th></tr><tr class="fila_usuarios"><td>Harina</td><td align="center"><img src="/Images/Admin/delete.svg" /></td>  </tr><tr class="fila_usuarios"><td>Huevo</td><td align="center"><img src="/Images/Admin/delete.svg" /></td>   </tr><tr class="fila_usuarios"><td>Azucar</td><td align="center"><img src="/Images/Admin/delete.svg" /></td>  </tr><tr><td><input type="text"></input></td><td align="center"><button>Crear</button></tr></table>';
        modal_contenido_insumos += '</td></tr></table>'

        function mostrar_modal_usuarios() {

            document.getElementById("modal-content").innerHTML = modal_contenido_usuarios;
            document.getElementById("myModal").style.display = "block";
        }
        function mostrar_modal_productos() {

            document.getElementById("modal-content").innerHTML = modal_contenido_productos;
            document.getElementById("myModal").style.display = "block";
        }
        function mostrar_modal_insumos() {

            document.getElementById("modal-content").innerHTML = modal_contenido_insumos;
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
                  <li><a runat="server" onserverclick="CerrarSesion" name="logout" id="logout"><p>Cerrar Sesi&oacute;n</p></a></li>
                </ul>
        </logout>
        <titulo>
            <div style="width:100%;background-color:whitesmoke;align-content:center;text-align:center;font-family: 'Brush Script MT', cursive;font-size: 60px;">
                <p>Panel del Administrador</p>
            </div>
        </titulo>
        <div class="menu" id="menu_container">
                        <div align="center" class="menuitem" onclick="mostrar_modal_insumos()"><br /><img src="/Images/Admin/ABMInsumos.jpg" alt="Insumos" width="95%">
                        <p id="texto_insumos"><b>Inventario de Insumos</b></p></div>
                        <div align="center" class="menuitem" onclick="mostrar_modal_productos()"><br /><img src="/Images/Admin/ABMProductos.jpg" alt="Productos" width="95%">
                        <p id="texto_backup"><b>Inventario de productos</b></p></div>
                        <div align="center" class="menuitem" onclick="mostrar_modal_usuarios()"><br /><img src="/Images/Admin/ABMUsuarios.jpg" alt="Backup" width="95%">
                        <p id="texto_usuarios"><b>Adm. de Usuarios</b></p></div>
            
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

