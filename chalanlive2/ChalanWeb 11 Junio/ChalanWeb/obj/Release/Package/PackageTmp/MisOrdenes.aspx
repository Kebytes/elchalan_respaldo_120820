<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisOrdenes.aspx.cs" Inherits="ChalanWeb.MisOrdenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script>
        var map;
        var marker;

        function initMap() {
            //Ubicación de inicio
            var cdmx = { lat: 19.432478, lng: -99.133134 };

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
    </script>--%>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDydLi3gMd1ewUXvc1A2nRtgJwxDqOrA1Y&callback=initMap"
        async defer></script>

    <script type="text/javascript">
        var markers = [
            <asp:Repeater ID="rptMarkers" runat="server">
                <ItemTemplate>
                    {
                        "title": '<%# Eval("Colonia") %>',
"lat": '<%# Eval("Latitud") %>',
                "lng": '<%# Eval("Longitud") %>',
                "description": '<%# Eval("Municipio") %>'
}
    </ItemTemplate>
                <SeparatorTemplate>
                    ,
    </SeparatorTemplate>
            </asp:Repeater >
    ];
    </script>

    <script type="text/javascript">

        window.onload = function () {
            for (i = 0; i < markers.length; i++) {
                var mapOptions = {
                    center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                    zoom: 14,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                var infoWindow = new google.maps.InfoWindow();
                var map = new google.maps.Map(document.getElementById("dvMap" + (i + 1)), mapOptions);

                var data = markers[i];
                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: data.title
                });
                (function (marker, data) {
                    google.maps.event.addListener(marker, "click", function (e) {
                        infoWindow.setContent(data.description);
                        infoWindow.open(map, marker);
                    });
                })(marker, data);
            }
        }
    </script>

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

        .child_div {
            height: 200px;
            width: 200px;
            overflow: hidden;
            position: relative;
            display: inline-block;
            float:right;
        }

        .parent_div {
            display: block;
            width: 80%;
        }
        
        table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 90%;
  margin: 10px 60px;
}

td, th {
  border: 1px solid #dddddd;
  text-align: center;
  padding: 8px;
}

        tr:nth-child(even) {
            background-color: #FFF;
        }

    </style>


    <main class="main">
        <nav aria-label="breadcrumb" class="breadcrumb-nav">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Cuenta.aspx"><i class="icon-home"></i></a></li>
                    <li class="breadcrumb-item active" aria-current="page">Mis pedidos</li>
                </ol>
                <asp:Button ID="ButtonRegresar" runat="server" Text="Regresar" CssClass="btn-enviar" OnClick="ButtonRegresar_Click"/>
            </div>
            <!-- End .container -->
        </nav>

        <div class="container">

            <div class="row">
                <div class="col-lg-8"> <%--"col-lg-8"--%>

                    <ul class="checkout-steps">
                        <li>
                            <h2 class="step-title">Mis órdenes:</h2>

                            <div class="shipping-step-addresses">

                                <%--Iba aqui--%>
                            </div>
                            <!-- End .shipping-step-addresses -->
                            <%--<a href="NuevaDireccion.aspx" class="btn btn-sm btn-outline-secondary btn-new-address" data-toggle="modal" data-target="#addressModal">+ Agregar nueva dirección</a>--%>
                            <%--<a href="NuevaDireccion.aspx" class="btn btn-sm btn-outline-secondary btn-new-address">+ Agregar nueva dirección</a>--%>
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

        <asp:Repeater runat="server" ID="repetidorOrdenes" OnItemCommand="repetidorOrdenes_ItemCommand">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <th>Datos</th>
                                                <th>Enviado a</th>
                                                <th>Estado</th>
                                                <th>Información</th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%--<div class="shipping-address-box" style="border: 1px solid black;">--%>
                                            <address>
                                                <strong>Orden </strong>#<%# Eval("Id") %>, <strong>Estado: </strong><%# Eval("OracionStatus") %>
                                                
                                                  
                                                <br>
                                                <%--<strong>Estado: </strong><%# Eval("OracionStatus") %>
                                                <br />--%>
                                                <strong>Agendado el: </strong><%# Eval("Fecha_Pedido") %>
                                                <br>
                                                <strong>Subtotal: </strong>$<%# Eval("Costo_Total") %>,
                                                <strong>Costo envío: </strong>$<%# Eval("Costo_Envio") %>
                                                
                                                
                                                <br>
                                               <%--<strong>Costo envío: </strong>$<%# Eval("Costo_Envio") %>
                                                <br>--%>
                                               <strong>Dirección:</strong>
                                                <br />
                                                <%# Eval("Calle") %>, <%# Eval("Colonia") %>, <%# Eval("Numero_Ext") %>,
                                                <%# Eval("Municipio") %>, <%# Eval("Estado") %>
                                                <br />
                                                
                                                      <%--<div style="display:inline">
                                                <asp:Button ID="ButtonDetalles" runat="server" CssClass="btn btn-sm btn-outline-info" Text="Detalles" CommandName="Add" CommandArgument='<%# Eval("Id") %>' />--%>
                                               <%-- <asp:Button ID="ButtonDelete" runat="server" CssClass="btn btn-sm btn-outline-danger" Text="Cancelar" CommandName="Del" CommandArgument='<%# Eval("Id") %>' />
                                                <asp:Button ID="ButtonUpdate" runat="server" CssClass="btn btn-sm btn-outline-warning" Text="Modificar" CommandName="Upd" CommandArgument='<%# Eval("Id") %>' />--%>
                                            <%--</div>--%>

                                                <%--                                                <div class="portlet light portlet-fit float-lg-right" style="padding-top: 1%;">
                                                    
                                                    <div class="portlet-body" style="width:50px; height:50px;">
                                                        <div id="map" class="gmaps"></div>
                                                    </div>
                                                </div>--%>
                                                <%--<input type="text" class="form-control" name="idDireccion" placeholder="Nombre" value="<%# Eval("Id") %>">--%>
                                            </address>
                                            
                                            
                                                
                                            <%--                                            <div class="address-box-action clearfix">--%>
                                          

                                            <%--<a href="#" id="<%# Eval("Id") %>" class="btn btn-sm btn-outline-secondary float-right">Enviar aquí--%>
                                            <%--                                            </div>--%>
                                            <!-- End .address-box-action -->
                                        <%--</div>--%>
                                                </td>
                                                <td>
                                                     <div style="text-align:center;">
                                                         <div id='dvMap<%# Container.ItemIndex + 1 %>' style="width:300px; height:200px; margin: 0 auto;"> <%----%>
                                                    </div>
                                                     </div>
                                                </td>
                                                <td>
                                                    <%--<asp:Label  runat="server"></asp:Label>--%>
                                                    
                                                        
                                                        <asp:Panel runat="server" BackColor='<%# System.Drawing.ColorTranslator.FromHtml(Eval("ColorStatus").ToString()) %>' Height="200px"></asp:Panel>
                                                    
                                                </td>
                                                <td>
                                                    
                                                <asp:Button ID="ButtonDetalles" runat="server" CssClass="btn btn-sm btn-outline-info" Text="Detalles" CommandName="Add" CommandArgument='<%# Eval("Id") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                        
                                       
                                    </ItemTemplate>
                                   <%-- <SeparatorTemplate>
                                        <h4 class="OrderHistory_RowSeparator"></h4>
                                    </SeparatorTemplate>--%>
                                </asp:Repeater>

       

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

    <style type="text/css">
        .OrderHistory_RowSeparator {
            margin: 0px 0px 5px 0px;
        }
    </style>

</asp:Content>
