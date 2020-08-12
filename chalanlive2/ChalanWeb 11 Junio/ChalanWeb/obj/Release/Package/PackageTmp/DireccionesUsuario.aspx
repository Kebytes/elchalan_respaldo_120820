<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DireccionesUsuario.aspx.cs" Inherits="ChalanWeb.DireccionesUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .btn-enviar {
                border: none;
                padding: 10px;
                font-size: 22px;
                color: #FFF;
                background: #2364D2;
                box-shadow: 2px 2px 10px 0 rgba(0,85,212,0.4);
                cursor: pointer;
                border: 2px solid #CED6E0;
                font-size: 18px;
                height: 50px;
                width: 25%;
                padding: 5px 12px;
                transition: .3s ease all;
                border-radius: 15px;
            }
    </style>

    <main class="main">
        <nav aria-label="breadcrumb" class="breadcrumb-nav">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Cuenta.aspx"><i class="icon-home"></i></a></li>
                    <li class="breadcrumb-item active" aria-current="page">Mis direcciones</li>
                </ol>
                <asp:Button ID="ButtonRegresar" runat="server" Text="Regresar" CssClass="btn-enviar" OnClick="ButtonRegresar_Click"/>
            </div>
            <!-- End .container -->
        </nav>

        <div class="container">

            <div class="row">
                <div class="col-lg-8">

                    <ul class="checkout-steps">
                        <li>
                            <h2 class="step-title">Dirección de envío</h2>

                            <div class="shipping-step-addresses">

                                <asp:Repeater runat="server" ID="repetidorDirecciones" OnItemCommand="repetidorDirecciones_ItemCommand">
                                    <ItemTemplate>

                                        <div class="shipping-address-box" style="border: 1px solid black;">
                                            <address>
                                                <%# Eval("Calle") %> <%# Eval("Numero_Ext") %>
                                                <br>
                                                <%# Eval("Colonia") %>
                                                <br>
                                                <%# Eval("Municipio") %> ,  <%# Eval("Estado") %>
                                                <br>
                                                <%# Eval("CP") %>
                                                <%--<input type="text" class="form-control" name="idDireccion" placeholder="Nombre" value="<%# Eval("Id") %>">--%>
                                            </address>

                                            <%--<div class="address-box-action clearfix">--%>

                                                <%--<a href="#" id="<%# Eval("Id") %>" class="btn btn-sm btn-outline-secondary float-right">Enviar aquí
                                                </a>--%>
                                                <asp:Button ID="ButtonDetalles" runat="server" CssClass="btn btn-sm btn-outline-info float-right" Text="Modificar" CommandName="Add" CommandArgument='<%# Eval("Id") %>' />

                                            <%--</div>--%>
                                            <!-- End .address-box-action -->
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!-- End .shipping-step-addresses -->
                            <%--<a href="NuevaDireccion.aspx" class="btn btn-sm btn-outline-secondary btn-new-address" data-toggle="modal" data-target="#addressModal">+ Agregar nueva dirección</a>--%>
                            <a href="NuevaDireccion.aspx" class="btn btn-sm btn-outline-secondary btn-new-address">+ Agregar nueva dirección</a>
                        </li>


                        <%--                        <li>
                            <div class="checkout-step-shipping">
                                <h2 class="step-title">Método de envío</h2>

                                <table class="table table-step-shipping">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input type="radio" name="shipping-method" value="local" checked="checked"></td>
                                            <td><strong>$29.00</strong></td>
                                            <td>Reparto Local</td>
                                            <td>Torreón</td>
                                        </tr>


                                    </tbody>
                                </table>
                            </div>
                            <!-- End .checkout-step-shipping -->
                        </li>--%>
                        <%--  <li>
                            <div class="checkout-step-shipping">
                                <h2 class="step-title">Método de pago</h2>

                                <table class="table table-step-shipping">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input type="radio" name="payment-method" value="tarjeta" checked="checked"></td>
                                            <td>Tarjeta de Crédito</td>
                                            <td>Al recibir el pedido</td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <input type="radio" name="payment-method" value="efectivo"></td>
                                            <td>Efectivo</td>
                                            <td>Al recibir el pedido</td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                            <!-- End .checkout-step-shipping -->
                        </li>--%>
                    </ul>
                </div>
                <!-- End .col-lg-8 -->

                <%--<div class="col-lg-4">
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
                        </div>
                        <!-- End #order-cart-section -->
                    </div>
                    <%--<asp:Button ID="ButtonFinalizar" CssClass="btn btn-primary float-md-left" runat="server" Text="Finalizar compra" OnClick="ButtonFinalizar_Click" />--%>
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
        <%--</div>--%>
        <!-- End .container -->

        <div class="mb-6"></div>
        <!-- margin -->
    </main>

</asp:Content>
