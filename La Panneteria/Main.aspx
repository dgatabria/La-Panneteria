<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="La_Panneteria.WebForm1"%>
<% 
    if (Request.Cookies["SessionToken"] != null)
    {
        HttpCookie SessionCookie = Request.Cookies["SessionToken"];
        //Response.Write("Testeando cookie de sesion: " + SessionCookie.Value.ToString());
        if (! Security.SessionManager.VerificarToken(SessionCookie.Value.ToString()))
        {
            Response.Redirect("/Default");
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
    <link rel="stylesheet" href="../main.css">
</head>

<body>
    <script>
        
        let carrito = new Map();
        let cookie_as_string = '';

        // Adapatado de http://www.quirksmode.org/js/cookies.html#script
        function getCookie(name) {

            var nameEQ = name + "=";
            var ca = document.cookie.split(';');

            for (var i = 0; i < ca.length; i++) {

                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    return decodeURIComponent(c.substring(nameEQ.length, c.length));
                }

            }

            return null;

        }

        function ObtenerItemsDelPedido(item) {

            oldcookie = getCookie('carrito');
            //alert(tmpcookie);
            if (oldcookie === null) {
                return null;
            }
            let ca = oldcookie.split('|');
            let j = 0;
            for (let i = 0; i < ca.length; i = i + 4) {
                //alert('id: ' + ca[i] + '; cantidad: ' + ca[i + 1]  + '; Precio: ' + ca[i +2 ] + '; Descripcion: ' + ca[i + 3 ])
                index = ca[i];
                if (index == item) {
                    let res = { 
                        Cantidad: ca[i + 1],
                        Precio: ca[i + 2],
                        Descripcion: atob(ca[i + 3])
                    }
                return res;

                }
            }
        }
        function AgregarAlCarrito(item, precio) {
            
            oldcookie = getCookie('carrito');
            newcookie = ''
            //alert(tmpcookie);
            if (oldcookie == null) {
                desc = btoa(document.getElementById("descripcion_item_" + item).innerHTML);
                document.cookie = 'carrito=' + item + '|1|' + precio + '|' + desc;
                ActualizarPedido();
                return;
            }
            let ca = oldcookie.split('|');
            let j = 0;
            for (let i = 0; i < ca.length; i = i + 4) {
                //alert('id: ' + ca[i] + '; cantidad: ' + ca[i + 1]  + '; Precio: ' + ca[i +2 ] + '; Descripcion: ' + ca[i + 3 ])
                index = ca[i];
                //alert("comparando " + index + " con " + ca[i]);
                if (parseInt(index) == item) {
                   // alert("eran iguales");
                    j = 1;
                    cant = parseInt(ca[i + 1]) + 1;
                    //alert("vieja cant: " + parseint(ca[i + 1]) + '. nueva cant: ' + cant);
                    if (i > 0) {
                        newcookie += '|';
                    }
                    newcookie += ca[i] + '|' + cant + '|' + ca[i + 2] + '|' + ca[i + 3];

                } else {
                    if (i > 0) {
                        newcookie += '|';
                    }
                    newcookie += ca[i] + '|' + ca[i + 1] + '|' + ca[i + 2] + '|' + ca[i + 3];
                }
            }
            if (j == 0) {
                if (newcookie.length > 0) {
                    newcookie += '|';
                }
                newcookie += item + '|1|' + precio + '|' + btoa(document.getElementById("descripcion_item_" + item).innerHTML);

            }
            document.cookie = 'carrito=' + newcookie;
            ActualizarPedido();
        }

        function SacarDelCarrito(item, precio) {

            oldcookie = getCookie('carrito');
            newcookie = ''
            //alert(tmpcookie);
            if (oldcookie === null) {
                return 0;
            }
            let ca = oldcookie.split('|');

            for (let i = 0; i < ca.length; i = i + 4) {
                //alert('id: ' + ca[i] + '; cantidad: ' + ca[i + 1]  + '; Precio: ' + ca[i +2 ] + '; Descripcion: ' + ca[i + 3 ])
                index = ca[i];
                if (index == item) {
                    j = 1;
                    if (ca[i + 1] > 0) {
                        cant = ca[i + 1] - 1;
                        if (cant == 0) {
                            document.getElementById("contador_art_" + item).innerHTML = '0';
                            continue;
                        }
                    }

                    if (newcookie.length > 0) {
                        newcookie += '|';
                    }
                    newcookie += ca[i] + '|' + cant + '|' + ca[i + 2] + '|' + ca[i + 3];

                } else {
                    if (newcookie.length > 0) {
                        newcookie += '|';
                    }
                    newcookie += ca[i] + '|' + ca[i + 1] + '|' + ca[i + 2] + '|' + ca[i + 3];
                }
            }
            if (newcookie == '') {
                document.cookie = 'carrito=; expires=Thu, 01 Jan 1970 00:00:00 UTC";'
            } else {
                document.cookie = 'carrito=' + newcookie;
            }
            
            ActualizarPedido();
        }
        function ActualizarPedido() {

            oldcookie = getCookie('carrito');
            newcookie = ''
            document.getElementById("carrito_de_compras").innerHTML = '<h2>Su pedido</h2>';
            //alert(tmpcookie);
            if ((oldcookie == null) || (oldcookie.length == 0)) {
                
                document.getElementById("carrito_de_compras").innerHTML += '<br><h3>Total: $0</h3>';
                return 0;
            }

            let ca = oldcookie.split('|');
            total = 0;
            for (let i = 0; i < ca.length; i = i + 4) {
                if (ca[i + 1] == null) { continue; }
                //alert('id: ' + ca[i] + '; cantidad: ' + ca[i + 1]  + '; Precio: ' + ca[i +2 ] + '; Descripcion: ' + ca[i + 3 ])
                index = ca[i];
                document.getElementById("contador_art_" + ca[i]).innerHTML = ca[i + 1];
                document.getElementById("carrito_de_compras").innerHTML += '<h4>' + atob(ca[i + 3]) + ' x ' + ca[i + 1] + '</h4>';
                total += ca[i + 1] * ca[i + 2];
            }

            document.getElementById("carrito_de_compras").innerHTML += '<br><h3>Total: $' + total + '</h3>';
        }

        <% 
        BLL.BLLArticulo bla;
        List<BusinessEntities.BEArticulo> arts;
        %>
        function ListarTodo() {

            document.getElementById('menu_container').innerHTML = '<%
        bla = new BLL.BLLArticulo();
        arts = bla.ListarTodos();
        foreach (BusinessEntities.BEArticulo articulo in arts)
        {
            Response.Write("<div align=\"center\"><img src=\"" + articulo.URL + "\" alt=\"" + articulo.Descripcion + "\" width=\"95%\">");
            //Response.Write("<div><img src=\"https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp\" alt=\"" + articulo.Descripcion + "\" width=\"95%\">");
            Response.Write("<p id=\"descripcion_item_" + articulo.Codigo + "\"><b>"+ articulo.Descripcion + ":</b>&nbsp;");
            Response.Write("$"+ articulo.PrecioUnitario + "</p>");
            Response.Write("<ul class=\"addItem\">");
            Response.Write("<li class=\"addItem\">");
            Response.Write("<button onclick=\"SacarDelCarrito(" + articulo.Codigo.ToString() + "," + articulo.PrecioUnitario.ToString() + ")\">-</button></li>");
            Response.Write("<li class=\"quantity\">");
            Response.Write("<div id=\"contador_art_" + articulo.Codigo.ToString() + "\">");  
            Response.Write("0</div></li><li class=\"addItem\">");
            Response.Write("<button onclick=\"AgregarAlCarrito(" + articulo.Codigo.ToString() + "," + articulo.PrecioUnitario.ToString() + ")\">+</button></li></ul></div>");
        }

                %>';
            let articulos = carrito.keys();
            while (true) {
                let item = articulos.next();
                if (item.done) break;
                if (carrito.get(item.value).Cantidad == 0) { continue; }    
                if (document.getElementById("contador_art_" + item.value) === null) { continue; }
                document.getElementById("contador_art_" + item.value).innerHTML = carrito.get(item.value).Cantidad;
            }
           


        }
        function ListarPanes() {

            document.getElementById('menu_container').innerHTML = '<%
            bla = new BLL.BLLArticulo();
            arts = bla.ListarTodos();
           foreach (BusinessEntities.BEArticulo articulo in arts)
           {
            if (articulo.Categoria == "Panes")
                {
            Response.Write("<div align=\"center\"><img src=\"" + articulo.URL + "\" alt=\"" + articulo.Descripcion + "\" width=\"95%\">");
            //Response.Write("<div><img src=\"https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp\" alt=\"" + articulo.Descripcion + "\" width=\"95%\">");
            Response.Write("<p id=\"descripcion_item_" + articulo.Codigo + "\"><b>"+ articulo.Descripcion + ":</b>&nbsp;");
            Response.Write("$"+ articulo.PrecioUnitario + "</p>");
            Response.Write("<ul class=\"addItem\">");
            Response.Write("<li class=\"addItem\">");
            Response.Write("<button onclick=\"SacarDelCarrito(" + articulo.Codigo.ToString() + "," + articulo.PrecioUnitario.ToString() + ")\">-</button></li>");
            Response.Write("<li class=\"quantity\">");
            Response.Write("<div id=\"contador_art_" + articulo.Codigo.ToString() + "\">");  
            Response.Write("0</div></li><li class=\"addItem\">");
            Response.Write("<button onclick=\"AgregarAlCarrito(" + articulo.Codigo.ToString() + "," + articulo.PrecioUnitario.ToString() + ")\">+</button></li></ul></div>");
                }

           }%>';
            let articulos = carrito.keys();
            while (true) {
                let item = articulos.next();
                if (item.done) break;
                if (carrito.get(item.value).Cantidad == 0) { continue; }
                if (document.getElementById("contador_art_" + item.value) === null) { continue; }
                document.getElementById("contador_art_" + item.value).innerHTML = carrito.get(item.value).Cantidad;
            }
        }
        function ListarDulces() {

            document.getElementById('menu_container').innerHTML = '<%
            bla = new BLL.BLLArticulo();
            arts = bla.ListarTodos();
           foreach (BusinessEntities.BEArticulo articulo in arts)
           {
            if (articulo.Categoria == "Dulces")
                {
            Response.Write("<div align=\"center\"><img src=\"" + articulo.URL + "\" alt=\"" + articulo.Descripcion + "\" width=\"95%\">");
            //Response.Write("<div><img src=\"https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp\" alt=\"" + articulo.Descripcion + "\" width=\"95%\">");
            Response.Write("<p id=\"descripcion_item_" + articulo.Codigo + "\"><b>"+ articulo.Descripcion + ":</b>&nbsp;");
            Response.Write("$"+ articulo.PrecioUnitario + "</p>");
            Response.Write("<ul class=\"addItem\">");
            Response.Write("<li class=\"addItem\">");
            Response.Write("<button onclick=\"SacarDelCarrito(" + articulo.Codigo.ToString() + "," + articulo.PrecioUnitario.ToString() + ")\">-</button></li>");
            Response.Write("<li class=\"quantity\">");
            Response.Write("<div id=\"contador_art_" + articulo.Codigo.ToString() + "\">");  
            Response.Write("0</div></li><li class=\"addItem\">");
            Response.Write("<button onclick=\"AgregarAlCarrito(" + articulo.Codigo.ToString() + "," + articulo.PrecioUnitario.ToString() + ")\">+</button></li></ul></div>");
                }

           }%>';
            let articulos = carrito.keys();
            while (true) {
                let item = articulos.next();
                if (item.done) break;
                if (carrito.get(item.value).Cantidad == 0) { continue; }
                if (document.getElementById("contador_art_" + item.value) === null) { continue; }
                document.getElementById("contador_art_" + item.value).innerHTML = carrito.get(item.value).Cantidad;
            }
        }
        function ListarSalados() {

            document.getElementById('menu_container').innerHTML = '<%
            bla = new BLL.BLLArticulo();
            arts = bla.ListarTodos();
           foreach (BusinessEntities.BEArticulo articulo in arts)
           {
            if (articulo.Categoria == "Salados")
                {
            Response.Write("<div align=\"center\"><img src=\"" + articulo.URL + "\" alt=\"" + articulo.Descripcion + "\" width=\"95%\">");
            //Response.Write("<div><img src=\"https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp\" alt=\"" + articulo.Descripcion + "\" width=\"95%\">");
            Response.Write("<p id=\"descripcion_item_" + articulo.Codigo + "\"><b>"+ articulo.Descripcion + ":</b>&nbsp;");
            Response.Write("$"+ articulo.PrecioUnitario + "</p>");
            Response.Write("<ul class=\"addItem\">");
            Response.Write("<li class=\"addItem\">");
            Response.Write("<button onclick=\"SacarDelCarrito(" + articulo.Codigo.ToString() + "," + articulo.PrecioUnitario.ToString() + ")\">-</button></li>");
            Response.Write("<li class=\"quantity\">");
            Response.Write("<div id=\"contador_art_" + articulo.Codigo.ToString() + "\">");  
            Response.Write("0</div></li><li class=\"addItem\">");
            Response.Write("<button onclick=\"AgregarAlCarrito(" + articulo.Codigo.ToString() + "," + articulo.PrecioUnitario.ToString() + ")\">+</button></li></ul></div>");
                }

           }%>';
            let articulos = carrito.keys();
            while (true) {
                let item = articulos.next();
                if (item.done) break;
                if (carrito.get(item.value).Cantidad == 0) { continue; }
                if (document.getElementById("contador_art_" + item.value) === null) { continue; }
                document.getElementById("contador_art_" + item.value).innerHTML = carrito.get(item.value).Cantidad;
            }

           }     

    </script>
    <div class="grid">
        <header>
          <ul class="header">
            <li><a href="#inicio" onclick="javascript:ListarTodo()">Inicio</a></li>
            <li><a href="#panes" onclick="ListarPanes()">Panes</a></li>
            <li><a href="#dulces" onclick="ListarDulces()">Dulces</a></li>
            <li><a href="#salados" onclick="ListarSalados()">Salados</a></li>
          </ul>
        </header>
        <titulo>
            <div style="width:100%;background-color:whitesmoke;align-content:center;text-align:center;font-family: 'Brush Script MT', cursive;font-size: 60px;">
                <p>Nuestros Productos</p>
            </div>
        </titulo>
        <div class="menu" id="menu_container">

 
        </div> 

        <div class="cart" id="carrito_de_compras">
          <h2>Haga su pedido!</h2>


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
