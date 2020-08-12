<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="ChalanWeb.Cuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            background-color: #fefefe;
            margin: auto;
            padding: 0;
            border: 1px solid #888;
            width: 80%;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
            -webkit-animation-name: animatetop;
            -webkit-animation-duration: 0.4s;
            animation-name: animatetop;
            animation-duration: 0.4s
        }

        /* Add Animation */
        @-webkit-keyframes animatetop {
            from {
                top: -300px;
                opacity: 0
            }

            to {
                top: 0;
                opacity: 1
            }
        }

        @keyframes animatetop {
            from {
                top: -300px;
                opacity: 0
            }

            to {
                top: 0;
                opacity: 1
            }
        }

        /* The Close Button */
        .close {
            color: white;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }

        .modal-header {
            padding: 2px 16px;
            background-color: #5cb85c;
            color: white;
        }

        .modal-body {
            padding: 2px 16px;
        }

        .modal-footer {
            padding: 2px 16px;
            background-color: #5cb85c;
            color: white;
        }
    </style>

    <script type="text/javascript">
                var modal = document.getElementById("myModal");

                // Get the button that opens the modal
                var btn = document.getElementById("myBtn");

                // Get the <span> element that closes the modal
                var span = document.getElementsByClassName("close")[0];

                // When the user clicks the button, open the modal 
                btn.onclick = function () {
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

    <main class="main">
        <nav aria-label="breadcrumb" class="breadcrumb-nav">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Default"><i class="icon-home"></i></a></li>
                    <li class="breadcrumb-item active" aria-current="page">Cuenta</li>
                </ol>
            </div>
            <!-- End .container -->
        </nav>

        <div class="container">
            <div class="row">
                <div class="col-lg-9 order-lg-last dashboard-content">
                    <h2>Información de tu cuenta</h2>

                    <asp:Panel ID="Panel1" runat="server" DefaultButton="ButtonActualizar">
                        <form action="Cuenta.aspx" method="post">
                            <div class="row">
                                <div class="col-sm-11">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group required-field">
                                                <label for="acc-name">Nombre</label>
                                                <input type="text" class="form-control" id="textNombre" name="textNombre" required value="<%= this.Nombre %>">
                                            </div>
                                            <!-- End .form-group -->
                                        </div>
                                        <!-- End .col-md-4 -->

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="acc-mname">Apellido Paterno</label>
                                                <input type="text" class="form-control" id="textApellidoPat" name="textApellidoPat" value="<%= this.ApellidoP %>">
                                            </div>
                                            <!-- End .form-group -->
                                        </div>
                                        <!-- End .col-md-4 -->

                                        <div class="col-md-4">
                                            <div class="form-group required-field">
                                                <label for="acc-lastname">Apellido Materno</label>
                                                <input type="text" class="form-control" id="textApellidoMat" name="textApellidoMat" required value="<%= this.ApellidoM %>">
                                            </div>
                                            <!-- End .form-group -->
                                        </div>
                                        <!-- End .col-md-4 -->
                                    </div>
                                    <!-- End .row -->
                                </div>
                                <!-- End .col-sm-11 -->
                            </div>
                            <!-- End .row -->

                            <%-- <p>Fecha de nacimiento</p>
                        <input type="date" class="form-control" id="Fecha" required runat="server">--%>

                            <p>Teléfono</p>
                            <input type="text" class="form-control" id="Telefono" name="Telefono" placeholder="Teléfono" required value="<%= this.Telefono %>" />

                            <p>Tarjeta Chalán</p>
                            <input type="text" class="form-control" placeholder="Tarjeta Chalán" id="Tarjeta" name="Tarjeta" value="<%= this.TarjetaChal %>">

                            <div class="form-group required-field">
                                <label for="acc-email">Email</label>
                                <input type="email" class="form-control" id="textemail" name="textemail" required value="<%= this.Email %>">
                            </div>
                            <!-- End .form-group -->

                            <div class="form-group required-field">
                                <label for="acc-password">Contraseña</label>
                                <input type="password" class="form-control" id="textcontrasena" name="textcontrasena" value="<%= this.Pass %>">
                            </div>
                            <!-- End .form-group -->

                            <div class="mb-2"></div>
                            <!-- margin -->

                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="change-pass-checkbox" value="1">
                                <label class="custom-control-label" for="change-pass-checkbox">Change Password</label>
                            </div>
                            <!-- End .custom-checkbox -->

                            <div id="Cuenta-chage-pass">
                                <h3 class="mb-2">Cambiar contraseña</h3>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group required-field">
                                            <label for="acc-pass2">Cotnraseña nueva</label>
                                            <input type="password" class="form-control" id="acc-pass2" name="acc-pass2">
                                        </div>
                                        <!-- End .form-group -->
                                    </div>
                                    <!-- End .col-md-6 -->

                                    <div class="col-md-6">
                                        <div class="form-group required-field">
                                            <label for="acc-pass3">Confirmar contraseña</label>
                                            <input type="password" class="form-control" id="acc-pass3" name="acc-pass3">
                                        </div>
                                        <!-- End .form-group -->
                                    </div>
                                    <!-- End .col-md-6 -->
                                </div>
                                <!-- End .row -->
                            </div>
                            <!-- End #Cuenta-chage-pass -->

                            <div class="required text-right">* Requerido</div>
                            <div class="form-footer">

                                <div class="form-footer-right">
                                    <%--<button type="submit" class="btn btn-primary">Actualizar Información</button>--%>
                                    <asp:Button ID="ButtonActualizar" CssClass="btn btn-primary" runat="server" Text="Actualizar información" OnClick="ButtonActualizar_Click" />
                                </div>
                            </div>
                            <!-- End .form-footer -->
                        </form>
                    </asp:Panel>
                </div>
                <!-- End .col-lg-9 -->

                <aside class="sidebar col-lg-3">
                    <div class="widget widget-dashboard">
                        <h3 class="widget-title">Mi Cuenta</h3>

                        <ul class="list">

                            <li><a href="#">Información de tu cuenta</a></li>
                            <li><a href="DireccionesUsuario.aspx">Direcciones</a></li>
                            <li><a href="NuevaDireccion.aspx">Nueva Dirección</a></li>
                            <li><a href="DetallePedidoNuevo.aspx">Pedido pendiente</a></li>
                            <li><a href="MisOrdenes.aspx">Mis órdenes</a></li>
                            <li><a href="MisTarjetas.aspx">Mis tarjetas</a></li>
                            <li><a href="AddCard.aspx">Agregar tarjeta</a></li>
                            <%--<li><a href="PruebaPopUp.aspx">PopUp</a></li>--%>

                        </ul>
                    </div>
                    <!-- End .widget -->
                </aside>
                <!-- End .col-lg-3 -->
            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        
        <!-- margin -->
    </main>
    <!-- End .main -->
</asp:Content>
