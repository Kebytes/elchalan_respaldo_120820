<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ChalanWeb.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="main">
        <nav aria-label="breadcrumb" class="breadcrumb-nav">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Default.aspx"><i class="icon-home"></i></a></li>
                    <li class="breadcrumb-item active" aria-current="page">Login</li>
                </ol>
            </div>
            <!-- End .container -->
        </nav>

        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <div class="heading">
                        <h2 class="title">Iniciar Sesión</h2>
                        <p>Ingresa tu email y contraseña para iniciar sesión.</p>
                    </div>
                    <!-- End .heading -->
                    <asp:Panel ID= "Panel1"  runat = "server" DefaultButton="Button1">
                        <form action="Login.aspx" method="post">
                                  <p>Email</p>
                        <input type="email" class="form-control" name="email" id="email"  placeholder="Email" required>
                                   <p>Contraseña</p>
                        <input type="password" class="form-control" name="password" id="password" placeholder="Contraseña" required>

                        <div class="form-footer">
                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Iniciar Sesión" OnClick="Button1_Click" ValidationGroup="one" />

                            <a href="#" class="forget-pass">¿Olvidaste tu contraseña?</a>
                        </div>
                        <!-- End .form-footer -->
                    </form>
                    </asp:Panel>
                    
                </div>
                <!-- End .col-md-6 -->

                <div class="col-md-6">
                    <div class="heading">
                        <h2 class="title">Regístrate patrón</h2>
                        <p>Crea tu cuenta para comenzar a comprar en El Chalán.</p>
                    </div>
                    <!-- End .heading -->
                    <asp:Panel ID= "Panel2"  runat = "server" DefaultButton="ButtonRegistro">
                    <form action="Login.aspx" method="post">
                    <p>Nombre</p>
                    <input type="text" class="form-control" name="nombre" placeholder="Nombre" required>
                    <p>Apellido Paterno</p>
                    <input type="text" class="form-control" name="ApellidoP" placeholder="Apellido Paterno" required>
                    <p>Apellido Materno</p>
                    <input type="text" class="form-control" name="ApellidoM" placeholder="Apellido Materno" required>
                    <p>Teléfono</p>
                    <input type="text" class="form-control" name="Telefono" placeholder="Teléfono" required>
                    <p>Fecha de nacimiento</p>
                    <input type="date" class="form-control" name="Fecha" required>
                    <p>Tarjeta Chalán</p>
                    <input type="text" class="form-control" placeholder="Tarjeta Chalán" name="Tarjeta">

                    <h2 class="title mb-2">Información para iniciar sesión</h2>
                    <p>Email</p>
                    <input type="email" class="form-control" placeholder="Email" name="emailR" required>
                    <p>Contraseña</p>
                    <input type="password" class="form-control" placeholder="Contraseña" name="passwordR" required>
                    <p>Confirmar contraseña</p>
                    <input type="password" class="form-control" placeholder="Confirmar Contraseña" name="PasswordC" required>

                    <div class="form-footer">
                        <asp:Button ID="ButtonRegistro" CssClass="btn btn-primary" runat="server" Text="Crear tu cuenta" OnClick="ButtonRegistro_Click"/>
                    </div>
                    <!-- End .form-footer -->
                       </form>
                    </asp:Panel>
                    
                </div>
                <!-- End .col-md-6 -->
            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->


        <div class="mb-5"></div>
        <!-- margin -->
    </main>
    <!-- End .main -->

    
    <script type="text/javascript">
        function check() {
            alert("evento load detectado!");
            document.getElementById("email").textContent = "Bravo@gmail.com"; 
            document.getElementById("password").textContent = "Johnny Bravo"; 
            //return false;
        }
    </script>

        <script>
      function load() {
          //alert("evento load detectado!");
          //document.getElementById("email").value = "Bravo@gmail.com";
          //document.getElementById("password").value = "***";
      }
      window.onload = load;
    </script>

</asp:Content>
