<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaEnvio.aspx.cs" Inherits="ChalanWeb.PruebaEnvio" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script type="text/javascript" src="https://resources.openpay.mx/lib/openpay-js/1.2.38/openpay.v1.min.js"></script>
    <script type="text/javascript" src="https://resources.openpay.mx/lib/openpay-data-js/1.2.38/openpay-data.v1.min.js"></script>


    <script type="text/javascript">
        function SetHiddenField() {
            <%--document.getElementById('<%=HiddenField1.ClientID%>').value = vv;--%>
            //alert(document.getElementById('idDireccionRepeater').value);
            //alert(document.getElementById('idDireccionRepeater').value);

            //var x = this.getElementById('idDireccionRepeater').value;
            //alert(x);

            //var elemArray = this.getElementsByName('DireccionAEnviar');
            //var parensillo = $(this).parent('div');
            //alert(elemArray.text);
            //alert("pedo");
        }
    </script>

    <script type="text/javascript">
        function MyFunction() {
            alert("Hola");
            OpenPay.setSandboxMode(true);
            var deviceDataId = OpenPay.deviceData.setup("formId", "Aber");
            console.log(deviceDataId);
            alert(deviceDataId);
            var radio = document.getElementById("DireccionAEnviar");
            radio.value = deviceDataId;
            alert(radio.value);
        }
    </script>

    <script type='text/javascript'>
        $(document).on("click", ".btn btn-sm btn-outline-secondary float-right", function () {

            var parensillo = $(this).parent('div');
            var x = parensillo.getElementsByTagName('a');
            alert(x.length);
            //alert("HOLA");

            return false;
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            if (window.location.href.indexOf("CompraError") > -1) {
                $.notify("No se llevó a cabo la compra.", "error");
            }
        });
    </script>

    <script type="text/javascript">
        function TarjetaClick() {
            var x = document.getElementById('tarjeta');
            var y = document.getElementById('UserCards');
            //alert("Efectivo");
            y.style.display = 'block';
            //alert("Tarjeta");
            //alert(x.length);
            x.checked = true;
        }
    </script>

    <script type="text/javascript">
        function EfectivoClick() {
            var x = document.getElementById('efectivo');
            var y = document.getElementById('UserCards');
            //alert("Efectivo");
            y.style.display = 'none';
            x.checked = true;
        }
    </script>

</head>
<body onload="MyFunction()">
    <form id="formId" runat="server">
        <asp:HiddenField ID="Aber" runat="server" Visible="false"/>
        <input type="radio" id="DireccionAEnviar" name="DireccionAEnviar" class="form-control"/>
        <div>
            
            <main class="main">
                <nav aria-label="breadcrumb" class="breadcrumb-nav">
                    <div class="container">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Default.aspx"><i class="icon-home"></i></a></li>
                            <li class="breadcrumb-item active" aria-current="page">Checkout</li>
                        </ol>
                    </div>
                    <!-- End .container -->
                </nav>

                <div class="container">
                    <ul class="checkout-progress-bar">
                        <li class="active">
                            <span>Envío</span>
                        </li>
                        <li>
                            <span>Confirmación de Pedido</span>
                        </li>
                    </ul>

                    <div class="row">
                        <div class="col-lg-8">

                            <ul class="checkout-steps">
                                <li>
                                    <h2 class="step-title">Dirección de envío</h2>

                                    <div class="shipping-step-addresses">

                                        <asp:Repeater runat="server" ID="repetidorDirecciones" EnableViewState="false">
                                            <ItemTemplate>

                                                <div class="shipping-address-box" id="Buscar">
                                                    <address>
                                                        <%# Eval("Calle") %> <%# Eval("Numero_Ext") %>
                                                        <br>
                                                        <%# Eval("Colonia") %>
                                                        <br>
                                                        <%# Eval("Municipio") %>, <%# Eval("Estado") %>
                                                        <br>
                                                        <%# Eval("CP") %>
                                                        <%--<input type="text" class="form-control" name="idDireccion" placeholder="Nombre" value="<%# Eval("Id") %>">--%>
                                                    </address>
                                                    <input type="radio" id="DireccionAEnviar" name="DireccionAEnviar" value="<%# Eval("Id") %>" class="form-control" hidden="hidden"></td>
                                            <div class="address-box-action clearfix">

                                                <a href="#" id="<%# Eval("Id") %>" class="btn btn-sm btn-outline-secondary float-right">Enviar aquí
                                                </a>

                                            </div>
                                                    <!-- End .address-box-action -->
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <!-- End .shipping-step-addresses -->
                                    <a href="#" class="btn btn-sm btn-outline-secondary btn-new-address" data-toggle="modal" data-target="#addressModal">+ Agregar nueva dirección</a>
                                </li>


                                <li>
                                    <div class="checkout-step-shipping">
                                        <h2 class="step-title">Método de envío</h2>

                                        <table class="table table-step-shipping">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <input type="radio" name="shipping-method" value="local" checked="checked"/></td>
                                                    <td><strong>$29.00</strong></td>
                                                    <td>Reparto Local</td>
                                                    <td>Torreón</td>
                                                </tr>


                                            </tbody>
                                        </table>
                                    </div>
                                    <!-- End .checkout-step-shipping -->
                                </li>

                                <li>
                                    <div class="checkout-step-shipping">
                                        <h2 class="step-title">Método de pago</h2>

                                        <table class="table table-step-shipping">
                                            <tbody>
                                                <tr onclick="TarjetaClick(this)">
                                                    <td>
                                                        <input type="radio" name="payment-method" value="tarjeta" checked="checked" id="tarjeta"></td>
                                                    <td>Tarjeta de Crédito</td>
                                                    <td>Al recibir el pedido</td>
                                                </tr>

                                                <tr onclick="EfectivoClick(this)">
                                                    <td>
                                                        <input type="radio" name="payment-method" value="efectivo" id="efectivo"></td>
                                                    <td>Efectivo</td>
                                                    <td>Al recibir el pedido</td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>

                                    <div id="UserCards">
                                        <h2 class="step-title">Selecciona una tarjeta:</h2>

                                        <div class="shipping-step-addresses">

                                            <asp:Repeater runat="server" ID="repetidorTarjetas" EnableViewState="false">
                                                <ItemTemplate>

                                                    <div class="shipping-address-box" id="Cards">
                                                        <address>
                                                            <%# Eval("type") %>
                                                            <br>
                                                            <%# Eval("brand") %>
                                                            <br>
                                                            <%# Eval("card_number") %>
                                                            <br>
                                                            <%# Eval("holder_name") %>
                                                        </address>
                                                        <input type="radio" id="TarjetaSeleccionada" name="TarjetaSeleccionada" value="<%# Eval("Id") %>" class="form-control" hidden="hidden"></td>
                                            <div class="address-box-action clearfix">

                                                <a href="#" id="<%# Eval("Id") %>" name="SelectedCard" class="btn btn-sm btn-outline float-right">Seleccionar</a>

                                            </div>
                                                        <!-- End .address-box-action -->
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>

                                        <a href="Tarjeta.aspx" class="btn btn-sm btn-outline-secondary btn-new-address">+ Agregar nueva tarjeta</a>

                                    </div>
                                    <%--<div class="container">

                                    <div class="row">
                                        <div class="col-lg-8">
                                            <ul class="checkout-steps">
                                                <li>
                                                    <h2 class="step-title"></h2>

                                                    <div class="shipping-step-addresses">

                                                        <asp:Repeater runat="server" ID="repetidorTarjetas">
                                                            <ItemTemplate>

                                                                <div class="shipping-address-box">
                                                                    <address>
                                                                        <%# Eval("type") %>
                                                                        <br>
                                                                        <%# Eval("brand") %>
                                                                        <br>
                                                                        <%# Eval("card_number") %>
                                                                        <br>
                                                                        <%# Eval("holder_name") %>

                                                                        <input type="radio" id="TarjetaSeleccionada" name="TarjetaSeleccionada" value="<%# Eval("Id") %>" class="form-control" hidden="hidden">

                                                                        <a href="#" id="<%# Eval("Id") %>" class="btn btn-sm btn-outline float-right">Seleccionar</a>

                                                                    </address>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                    <a href="Tarjeta.aspx" class="btn btn-sm btn-outline-secondary btn-new-address">+ Agregar nueva tarjeta</a>
                                                </li>

                                            </ul>
                                        </div>

                                    </div>
                                    
                                </div>--%>
                                    <%--<div class="container">
                                    <div class="row">
                                        <div class="col-lg-8">
                                            <ul class="checkout-steps">
                                                <li>
                                                    <div class="shipping-step-addresses">

                                                        <asp:Repeater runat="server" ID="repetidorTarjetas">
                                                            <ItemTemplate>

                                                                <div class="shipping-address-box" style="border: 1px solid black;">
                                                                    <address>
                                                                        <%# Eval("type") %>

                                                                        <br>
                                                                        <%# Eval("brand") %>
                                                                        <br>
                                                                        <%# Eval("card_number") %>
                                                                        <br>
                                                                        <%# Eval("holder_name") %>
                                                                    </address>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>

                                    </div>
                                    <!-- End .col-lg-4 -->
                                </div>--%>
                                    <!-- End .checkout-step-shipping -->
                                </li>
                            </ul>
                        </div>
                        <!-- End .col-lg-8 -->

                        <div class="col-lg-4">
                            <div class="order-summary">
                                <h3>Resumen de carrito</h3>

                                <h4>
                                    <a data-toggle="collapse" href="#order-cart-section" class="collapsed" role="button" aria-expanded="false" aria-controls="order-cart-section">
                                        <asp:Literal ID="literalCantidad" runat="server"></asp:Literal>
                                        productos </a>
                                </h4>

                                <div class="show" id="order-cart-section">
                                    <table class="table table-mini-cart">
                                        <tbody>
                                            <asp:Repeater runat="server" ID="repetidorCarro">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="product-col">
                                                            <figure class="product-image-container">
                                                                <a href="<%# string.Concat( "Product?id=", Eval("Id"))%>" class="product-image">
                                                                    <img src="<%# Eval("Imagen") %>" alt="product">
                                                                </a>
                                                            </figure>
                                                            <div>
                                                                <h2 class="product-title">
                                                                    <a href="<%# string.Concat( "Product?id=", Eval("Id"))%>"><%# Eval("Nombre") %></a>
                                                                </h2>

                                                                <span class="product-qty">Cantidad: <%# Eval("Cantidad") %></span>
                                                            </div>
                                                        </td>

                                                        <td class="price-col">$<%# Eval("Precio") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </tbody>

                                    </table>
                                    <br />
                                    <%--Total de tu compra: <a>$<%= this.total %></a>--%>
                                </div>
                                <!-- End #order-cart-section -->
                            </div>
                            <asp:Button ID="ButtonFinalizar" CssClass="btn btn-primary float-md-left" runat="server" Text="Finalizar compra" OnClick="ButtonFinalizar_Click"/>
                            <!-- End .order-summary -->
                        </div>
                        <!-- End .col-lg-4 -->
                    </div>
                    <!-- End .row -->

                    <div class="row">
                        <div class="col-lg-8">
                            <div class="checkout-steps-action">
                                <%--<a href="#" class="btn btn-primary float-right">Finalizar Compra</a>--%>
                                <%--<asp:Button ID="ButtonFinalizar" CssClass="btn btn-primary float-center" runat="server" Text="Finalizar compra"/>--%>
                            </div>
                            <!-- End .checkout-steps-action -->
                        </div>
                        <!-- End .col-lg-8 -->
                    </div>
                    <!-- End .row -->
                </div>
                <!-- End .container -->

                <div class="mb-6"></div>
                <!-- margin -->
            </main>
        </div>
    </form>
</body>
</html>


