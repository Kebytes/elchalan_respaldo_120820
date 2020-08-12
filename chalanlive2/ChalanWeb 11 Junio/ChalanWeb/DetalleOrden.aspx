<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleOrden.aspx.cs" Inherits="ChalanWeb.DetalleOrden" %>

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

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDydLi3gMd1ewUXvc1A2nRtgJwxDqOrA1Y&callback=initMap"
        async defer></script>



    <main class="main">
        <nav aria-label="breadcrumb" class="breadcrumb-nav">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Default.aspx"><i class="icon-home"></i></a></li>
                    <li class="breadcrumb-item active" aria-current="page">Detalles de tu orden.</li>
                </ol>
                <strong>Detalle de la orden: </strong>#<asp:Literal ID="literalNumeroOrden" runat="server"></asp:Literal>:
                
                <br />
                <strong>Estado de la orden: </strong> <asp:Literal ID="literalStatusOrden" runat="server"></asp:Literal>
                <br />
                <strong>Fecha de creación: </strong><asp:Literal ID="literalFechaCreacion" runat="server"></asp:Literal>
                <br />
                <strong>Fecha de entrega: </strong><asp:Literal ID="literalFechaEntrega" runat="server"></asp:Literal>
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
                                                    <a href="<%# string.Concat( "Product?id=", Eval("Id"))%>" class="product-image">
                                                        <img src="<%# Eval("Imagen") %>" onerror="this.onerror=null; this.src='/assets/images/chalandefault.jpg'" alt="product">
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
                            </tbody>
                        </table>
                        <h3><strong></strong><u>Destino:</u></h3>
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
                </div>
                <!-- End .cart-table-container -->
                <div class="portlet light portlet-fit float-md-none" style="padding-top: 1%;">


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
