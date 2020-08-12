<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallePedidoNuevo.aspx.cs" Inherits="ChalanWeb.DetallePedidoNuevo" Async="true" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        var map;
        var marker;

        function initMap() {
            //Ubicación de inicio
            var cdmx = { lat: <%= this.latitud %>, lng: <%= this.longitud %> };

            //Mapa
            var map = new google.maps.Map(document.getElementById('map'), { center: cdmx, zoom: 13 });

            //Marcador
            marker = new google.maps.Marker({ position: cdmx, map: map, draggable: true, animation: google.maps.Animation.DROP, icon: '<%= ResolveClientUrl("~/assets/images/place.png")%>' });

            //Geocoder
            var geocoder = new google.maps.Geocoder();

            //Dirección del marcador
            google.maps.event.addListener(marker, "dragend", function (event) {
                var lat, long, address, resultArray, citi;

                var geocoder = new google.maps.Geocoder();

                geocoder.geocode({ latLng: marker.getPosition() }, function (result, status) {
                });
            });
        }
    </script>

    <script type="text/javascript">
        function Confirmation() {
            return confirm("¿Estás seguro de querer cancelar este pedido?");
        }
    </script>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDydLi3gMd1ewUXvc1A2nRtgJwxDqOrA1Y&callback=initMap"
        async defer></script>

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
                    <li class="breadcrumb-item active" aria-current="page">Detalles de tu última orden.</li>
                </ol>
                ¡GRACIAS POR TU ORDEN!<br />
                <strong>Tu orden es la número </strong>#<asp:Literal ID="literalNumeroOrden" runat="server"></asp:Literal>
                
                <br />
                Recibirás una llamada para confirmar la disponibilidad del producto.<br />
                <strong>Estado de la orden: </strong> <asp:Literal ID="literalStatusOrden" runat="server"></asp:Literal>
                 
                <br />
                <strong>Fecha de creación: </strong><asp:Literal ID="literalFechaCreacion" runat="server"></asp:Literal>
                
                <br />
                <strong>Fecha de entrega: </strong><asp:Literal ID="literalFechaEntrega" runat="server"></asp:Literal>
                 
                <asp:Button ID="ButtonRegresar" runat="server" Text="Regresar" CssClass="btn-enviar" OnClick="ButtonRegresar_Click"/>
            </div>
            
        </nav>

        <div class="container">
            <div class="row">
                <div class="col-lg-8">
                    <div class="cart-table-container">
                        <table class="table table-cart">
                            <thead>
                                <tr>
                                    <th class="product-col">Producto</th>
                                    <th class="price-col">Precio</th>
                                    <th class="qty-col">Cantidad</th>
                                    <th>Subtotal</th>
                                </tr>
                            </thead>
                            <tbody>

                                <asp:Repeater ID="repetidorDetalles" runat="server">
                                    <ItemTemplate>

                                        <tr class="product-row">
                                            <td class="product-col">
                                                <figure class="product-image-container">
                                                    <a href="<%# string.Concat( "Product?id=", Eval("Id_Producto"))%>" class="product-image">
                                                        <%--<img src="<%# Eval("Imagen") %>" onerror="this.onerror=null; this.src='/assets/images/chalandefault.jpg'" alt="product">--%>
                                                        <img style="width: 178px !important; height: 178px !important; object-fit: contain;" onerror="this.onerror=null; this.src='/assets/images/chalandefault.jpg'" src="<%# Eval("Imagen") %> ">
                                                    </a>
                                                </figure>
                                                <h2 class="product-title">
                                                    <a><%# Eval("Nombre") %></a>
                                                </h2>
                                            </td>
                                            <td>$<%# Eval("Precio") %></td>
                                            <td>
                                                <input class="vertical-quantity form-control" type="text" value="<%# Eval("Cantidad") %>">
                                            </td>
                                            <td>$<%# Eval("Costo") %></td>
                                        </tr>
                                        <tr class="product-action-row">
                                            <br />
<%--                                            <div style="display:inline">
                                                <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-sm btn-outline-danger" Text="Cancelar pedido" CommandName="Del" CommandArgument='<%# Eval("Id_Pedido") %>'/>
                                                
                                            </div>--%>
                                            <td colspan="4" class="clearfix">
                                                <div class="float-left">
                                                </div>
                                                <!-- End .float-left -->

                                                <%--<div class="float-right">
                                                  <a href="#" title="Remove product" class="btn-remove"><span class="sr-only">Eliminar</span></a>
                                            </div>--%><!-- End .float-right -->
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                <asp:Button ID="Buttonprueba" runat="server" CssClass="btn btn-sm btn-outline-danger" Text="Cancelar Pedido" OnClick="Buttonprueba_Click" OnClientClick="return Confirmation();"/>
                                                <%--<asp:Button ID="ButtonUpdate" runat="server" CssClass="btn btn-sm btn-outline-warning" Text="Modificar" CommandName="Upd" CommandArgument='<%# Eval("Id") %>' />--%>
                                            

                                <%--                                <tfoot>
                                    <tr>
                                        <td colspan="4" class="clearfix">
                                            <div class="float-left">
                                                <a href="category.html" class="btn btn-outline-secondary">Continue Shopping</a>
                                            </div><!-- End .float-left -->

                                            <div class="float-right">
                                                <a href="#" class="btn btn-outline-secondary btn-clear-cart">Clear Shopping Cart</a>
                                                <a href="#" class="btn btn-outline-secondary btn-update-cart">Update Shopping Cart</a>
                                            </div><!-- End .float-right -->
                                        </td>
                                    </tr>
                                </tfoot>--%>
                        </table>
                        <div class="portlet-body" style="width: 400px; height: 50px;">
                            <div id="map" class="gmaps"></div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
                    <!-- End .cart-table-container -->

                    <%--<div class="cart-discount">
                            <h4>Apply Discount Code</h4>
                            <form action="#">
                                <div class="input-group">
                                    <input type="text" class="form-control form-control-sm" placeholder="Enter discount code"  required>
                                    <div class="input-group-append">
                                        <button class="btn btn-sm btn-primary" type="submit">Apply Discount</button>
                                    </div>
                                </div><!-- End .input-group -->
                            </form>
                        </div>--%><!-- End .cart-discount -->
                </div>
                <!-- End .col-lg-8 -->

                <div class="col-lg-4">
                    <div class="cart-summary">
                        <h3>Resumen</h3>

                        <%--<h4>
                                <a data-toggle="collapse" href="#total-estimate-section" class="collapsed" role="button" aria-expanded="false" aria-controls="total-estimate-section">Estimate Shipping and Tax</a>
                            </h4>--%>

                        <%--<div class="collapse" id="total-estimate-section">
                                <form action="#">
                                    <%--<div class="form-group form-group-sm">
                                        <label>Country</label>
                                        <div class="select-custom">
                                            <select class="form-control form-control-sm">
                                                <option value="USA">United States</option>
                                                <option value="Turkey">Turkey</option>
                                                <option value="China">China</option>
                                                <option value="Germany">Germany</option>
                                            </select>
                                        </div><!-- End .select-custom -->
                                    </div><!-- End .form-group -->--%>

                        <%--                                    <div class="form-group form-group-sm">
                                        <label>State/Province</label>
                                        <div class="select-custom">
                                            <select class="form-control form-control-sm">
                                                <option value="CA">California</option>
                                                <option value="TX">Texas</option>
                                            </select>
                                        </div><!-- End .select-custom -->
                                    </div><!-- End .form-group -->--%>

                        <%--<div class="form-group form-group-sm">
                                        <label>Zip/Postal Code</label>
                                        <input type="text" class="form-control form-control-sm">
                                    </div><!-- End .form-group -->

                                    <div class="form-group form-group-custom-control">
                                        <label>Flat Way</label>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="flat-rate">
                                            <label class="custom-control-label" for="flat-rate">Fixed $5.00</label>
                                        </div><!-- End .custom-checkbox -->
                                    </div><!-- End .form-group -->--%>

                        <%--                                    <div class="form-group form-group-custom-control">
                                        <label>Best Rate</label>
                                        <div class="custom-control custom-checkbox">
                                            <input type="checkbox" class="custom-control-input" id="best-rate">
                                            <label class="custom-control-label" for="best-rate">Table Rate $15.00</label>
                                        </div><!-- End .custom-checkbox -->
                                    </div><!-- End .form-group -->--%>
                        <%--</form>
                            </div><!-- End #total-estimate-section -->--%>

                        <table class="table table-totals">
                            <tbody>
                                <tr>
                                    <td>Subtotal</td>
                                    <td>$<asp:Literal ID="LiteralSubTotal" runat="server" Text="0"></asp:Literal></td>
                                </tr>

                                <tr>
                                    <td>Impuestos</td>
                                    <td>$0.00</td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>Precio Total</td>
                                    <td>$<asp:Literal ID="LiteralTotal" runat="server" Text="0"></asp:Literal></td>
                                </tr>
                            </tfoot>
                        </table>

                        <%--                        <div class="checkout-methods">
                            <a href="Envio" class="btn btn-block btn-sm btn-primary">Finalizar compra</a>

                        </div>--%>
                        <!-- End .checkout-methods -->
                    </div>
                    <!-- End .cart-summary -->
                </div>
                <!-- End .col-lg-4 -->
            </div>
            <!-- End .row -->
        </div>
        <!-- End .container -->

        <div class="mb-6"></div>
        <!-- margin -->
    </main>
    <!-- End .main -->
</asp:Content>
