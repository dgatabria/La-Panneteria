 <%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="La_Panneteria._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server"> 
    <link rel="stylesheet" href="/CSS/login.css">
<style>
    body {background-color: #704141;
          background-image: url("/Images/ppal/panaderia-bg.jpg");
           background-repeat: no-repeat;
           background-size: cover;
    } 
        .button1 {
        background-color: white;
        color: black;
        
        border-radius: 4px;
        font-size: 24px;
}
    .button1:hover {
        background-color: #78736F; /* Green */
         color: white;
}
</style>

            


            
            <div style="width:100%; height:4em; text-align:centeR; "><h1>La Panneteria</h1></div>
            <div style="width:100%; height:48em; text-align:centeR; align-content:center;">


            <button type="button" class="button1" id="myBtn" AutoPostBack="false">Inicie Sesion</button>
            <button type="button" class="button1" id="myBtn2" AutoPostBack="false">Registrese</button>

            </div>




<!-- The Modal -->
<div id="myModal" class="modal">

  <!-- Modal content -->
  <div class="modal-content" style="justify-content:center" id="modal-content">
      
    <span class="close">&times;</span>
  <br /><br />





  </div>

</div>

        <script>
            // Get the modal
            var modal = document.getElementById("myModal");

            // Get the button that opens the modal
            var btn = document.getElementById("myBtn");
            var btn2 = document.getElementById("myBtn2");

            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];

            var modal_contenido_login = '<main><div class="body"><br><h1>Login</h1><ul class="login"><li class="login">Mail:</li><li class="login"><input type="email" name="email" id="email"></li></ul><ul class="login"><li class="login">Contraseña:</li><li class="login"><input type="password" name="password" id="password"></li></ul><br><input type="submit" value="Iniciar Sesion" runat="server" onServerClick="do_login"></div></main>';
            var modal_contenido_signup = '<main><div class="body"><br><h1>Registro</h1><ul class="login"><li class="login">Mail:</li><li class="login"><input type="email" name="email" id="email"></li></ul><ul class="login"><li class="login">Contraseña:</li><li class="login"><input type="password" name="password" id="password"></li></ul><br><input type="submit" value="Registrarse" runat="server" onServerClick="do_signup"></div></main>';



            // When the user clicks on the button, open the modal
            btn.onclick = function () {
                document.getElementById("modal-content").innerHTML = modal_contenido_login;
                modal.style.display = "block";
            }
            // When the user clicks on the button, open the modal
            btn2.onclick = function () {
                document.getElementById("modal-content").innerHTML = modal_contenido_signup;
                modal.style.display = "block";
            }

            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                modal.style.display = "none";
            }

            // When the user clicks anywhere outside of the modal, close it
            window.onclick = function (event) {
                if (event.target == modal) {
                    modal.style.display = "none";
                }
            }
            
            
        </script>


 </asp:Content> 
