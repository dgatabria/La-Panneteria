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

        function actualizarCarrito() {

            let articulos = carrito.keys();
            document.getElementById("carrito_de_compras").innerHTML = '<h2>Su pedido</h2>';
            let total = 0;
            while (true) {
                let item = articulos.next();
                if (item.done) break;
                if (carrito.get(item.value).Cantidad == 0) { continue; }
                total += carrito.get(item.value).Precio * carrito.get(item.value).Cantidad;
                document.getElementById("carrito_de_compras").innerHTML += '<h4>' + carrito.get(item.value).Descripcion + ' x ' + carrito.get(item.value).Cantidad + '</h4>'
            }
            document.getElementById("carrito_de_compras").innerHTML += '<h3>Total: ' + total + '</h3>';

        }

        function AgregarAlCarrito(articulo, precio) {
            if (carrito.get(articulo) === undefined) {
                let item = {
                    Descripcion: [""],
                    Cantidad: [0],
                    Precio: [0]
                }
                carrito.set(articulo, item);
            }

            item = carrito.get(articulo);

            item.Descripcion = document.getElementById("descripcion_item_" + articulo).innerHTML;

            item.Cantidad++;
            item.Precio = precio;
            carrito.set(articulo, item);
            actualizarCarrito();
            document.getElementById("contador_art_" + articulo).innerHTML = carrito.get(articulo).Cantidad;
        }
        function SacarDelCarrito(articulo, precio) {
            if (carrito.get(articulo) === undefined) {
                let item = {
                    Descripcion: [""],
                    Cantidad: [0],
                    Precio: [0]
                }
                carrito.set(articulo, item);
            }
            item = carrito.get(articulo);
            item.Descripcion = document.getElementById("descripcion_item_" + articulo).innerHTML;
            if (item.Cantidad > 0) {
                item.Cantidad--;
            }
            item.Precio = precio;
            carrito.set(articulo, item);
            actualizarCarrito();
            document.getElementById("contador_art_" + articulo).innerHTML = carrito.get(articulo).Cantidad;
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
            for (let i = 0; i < carrito.length; i++) {
                alert(carrito[i]);
                document.getElementById("contador_art_" + i).innerHTML = carrito[i];
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
            Response.Write("<p><b>"+ articulo.Descripcion + ":</b>&nbsp;");
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
            Response.Write("<p><b>"+ articulo.Descripcion + ":</b>&nbsp;");
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
            Response.Write("<p><b>"+ articulo.Descripcion + ":</b>&nbsp;");
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
          <h2>Su pedido</h2>
          <h4>Pan Casero $2200 x3</h4>
          <h4>Pan Casero $2200 x3</h4>
          <h4>Pan Casero $2200 x3</h4>
          <h4>Pan Casero $2200 x3</h4>
          <br>
          <h3>Total $26400</h3>

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
    <script>ListarTodo();</script>
</html>
