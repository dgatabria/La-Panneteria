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
    <div class="grid">
        <header>
          <ul class="header">
            <li><a href="#inicio">Inicio</a></li>
            <li><a href="#panes">Panes</a></li>
            <li><a href="#dulces">Dulces</a></li>
            <li><a href="#salados">Salados</a></li>
          </ul>
        </header>
        
        <div class="menu">
          <div>
              <img src="https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp" alt="Pan" width="95%">
              <p>Pan Casero (x kg)</p>
              <h5>$2200</h5>
              <ul class="addItem">
                <li class="addItem">
                  <button>-</button>
                </li>
                <li class="quantity">
                  <div>0</div>
                </li>
                <li class="addItem">
                  <button>+</button>
                </li>
              </ul>
          </div>

          <div>
            <img src="https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp" alt="Pan" width="95%">
            <p>Pan Casero (x kg)</p>
            <h5>$2200</h5>
            <ul class="addItem">
              <li class="addItem">
                <button>-</button>
              </li>
              <li class="quantity">
                <div>0</div>
              </li>
              <li class="addItem">
                <button>+</button>
              </li>
            </ul>
          </div>
          <div>
            <img src="https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp" alt="Pan" width="95%">
            <p>Pan Casero (x kg)</p>
            <h5>$2200</h5>
            <ul class="addItem">
              <li class="addItem">
                <button>-</button>
              </li>
              <li class="quantity">
                <div>0</div>
              </li>
              <li class="addItem">
                <button>+</button>
              </li>
            </ul>
          </div>   
          <div>
            <img src="https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp" alt="Pan" width="95%">
            <p>Pan Casero (x kg)</p>
            <h5>$2200</h5>
            <ul class="addItem">
              <li class="addItem">
                <button>-</button>
              </li>
              <li class="quantity">
                <div>0</div>
              </li>
              <li class="addItem">
                <button>+</button>
              </li>
            </ul>
          </div>
          <div>
            <img src="https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp" alt="Pan" width="95%">
            <p>Pan Casero (x kg)</p>
            <h5>$2200</h5>
            <ul class="addItem">
              <li class="addItem">
                <button>-</button>
              </li>
              <li class="quantity">
                <div>0</div>
              </li>
              <li class="addItem">
                <button>+</button>
              </li>
            </ul>
          </div>
          <div>
            <img src="https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp" alt="Pan" width="95%">
            <p>Pan Casero (x kg)</p>
            <h5>$2200</h5>
            <ul class="addItem">
              <li class="addItem">
                <button>-</button>
              </li>
              <li class="quantity">
                <div>0</div>
              </li>
              <li class="addItem">
                <button>+</button>
              </li>
            </ul>
          </div>          
          <div>
            <img src="https://assets.elgourmet.com/wp-content/uploads/2023/03/pan-f_hspYqgfrL7zVJKc6X13BFWkPMdnITx-1024x683.png.webp" alt="Pan" width="95%">
            <p>Pan Casero (x kg)</p>
            <h5>$2200</h5>
            <ul class="addItem">
              <li class="addItem">
                <button>-</button>
              </li>
              <li class="quantity">
                <div>0</div>
              </li>
              <li class="addItem">
                <button>+</button>
              </li>
            </ul>
          </div>
        </div>

        <div class="cart">
          <h2>Su pedido</h2>
          <h4>Pan Casero $2200 x3</h4>
          <h4>Pan Casero $2200 x3</h4>
          <h4>Pan Casero $2200 x3</h4>
          <h4>Pan Casero $2200 x3</h4>
          <br>
          <h3>Total $26400</h3>

        </div>
        
        <footer>
            <p><b>Direccion:</b> Alberdi 534, CABA, Buenos Aires</p>
            <p><b>Teléfono:</b> 4635-3753</p>
            <p><b>Mail:</b> consultas@panneteria.com</p>
            <p><b>Instragram:</b> @Panneteria</p>          
        </footer>
      </div>
</body>
</html>
