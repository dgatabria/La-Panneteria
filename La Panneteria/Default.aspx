<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="La_Panneteria._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
    <div class="body">
        <br>

        <h1>Login</h1>
        <ul class="login">
            <li class="login"> 
                Mail:        
            </li>
            <li class="login">
                <input type="email" name="email" id="email">
            </li>
        </ul>
        
        <ul class="login">
            <li class="login"> 
                Contraseña: 
            </li>
            <li class="login">
                <input type="password" name="password" id="password">
            </li>
        </ul>
        
        <br>
    
        <input type="submit" value="Iniciar Sesion" runat="server" onServerClick="do_login">
    </div>


    </main>

</asp:Content>
