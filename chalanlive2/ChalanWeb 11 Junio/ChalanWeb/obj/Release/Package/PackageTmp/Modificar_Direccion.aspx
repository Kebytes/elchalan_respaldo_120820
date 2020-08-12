<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Modificar_Direccion.aspx.cs" Inherits="ChalanWeb.Modificar_Direccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        var map;
        var marker;

        function initMap() {
            //Ubicación de inicio
            var cdmx = { lat: <%= this.lati %>, lng: <%= this.lon %> };

            //Mapa
            var map = new google.maps.Map(document.getElementById('map'), { center: cdmx, zoom: 13 });

            //Marcador
            marker = new google.maps.Marker({ position: cdmx, map: map, draggable: true, animation: google.maps.Animation.DROP, icon: '<%= ResolveClientUrl("~/assets/images/place.png")%>' });

            //Geocoder
            var geocoder = new google.maps.Geocoder();

            //Buscar con dirección
            document.getElementById('submit').addEventListener('click', function () {
                geocodeAddress(geocoder, map);
            });

            //Dirección del marcador
            google.maps.event.addListener(marker, "dragend", function (event) {
                var lat, long, address, resultArray, citi;

                document.getElementById('<%=latitud.ClientID%>').value = lat = marker.getPosition().lat();
                document.getElementById('<%=longitud.ClientID%>').value = long = marker.getPosition().lng();

                var geocoder = new google.maps.Geocoder();

                geocoder.geocode({ latLng: marker.getPosition() }, function (result, status) {
                    if ('OK' == status) {
                        address = result[0].formatted_address;
                        resultArray = result[0].address_components;

                        if (resultArray.lenght == 7) {
                            document.getElementById('<%=NumeroE.ClientID%>').value = resultArray[0].long_name;
                            document.getElementById('<%=Calle.ClientID%>').value = resultArray[1].long_name;
                            document.getElementById('<%=Colonia.ClientID%>').value = resultArray[2].long_name;
                            document.getElementById('<%=Municipio.ClientID%>').value = resultArray[3].long_name;
                            document.getElementById('<%=CodPos.ClientID%>').value = resultArray[6].long_name;
                        }
                        else {
                            alert("Hola");
                            document.getElementById('<%=NumeroE.ClientID%>').value = '';
                            document.getElementById('<%=Calle.ClientID%>').value = '';
                            document.getElementById('<%=Colonia.ClientID%>').value = '';
                            document.getElementById('<%=Municipio.ClientID%>').value = '';
                            document.getElementById('<%=CodPos.ClientID%>').value = '';
                        }
                    }

                    else { console.log('Geocode was not successful for the following reason: ' + status); }
                });
            });
        }

        //Para colocar el marcador en el mapa de acuerdo a la direccion
        function geocodeAddress(geocoder, resultsMap) {
            var address = document.getElementById('<%=Calle.ClientID%>').value + " " + document.getElementById('<%=NumeroE.ClientID%>').value + " " + document.getElementById('<%=Colonia.ClientID%>').value + " " + document.getElementById('<%=CodPos.ClientID%>').value + " " + document.getElementById('<%=Municipio.ClientID%>').value + " " + document.getElementById('<%=Estado.ClientID%>').value;
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status === 'OK') {
                    resultsMap.setCenter(results[0].geometry.location);
                    marker.setMap(null);
                    marker = new google.maps.Marker({
                        map: resultsMap,
                        position: results[0].geometry.location,
                        draggable: true,
                        animation: google.maps.Animation.DROP,
                        icon: '<%= ResolveClientUrl("~/assets/images/place.png")%>'
                    });

                    document.getElementById('<%=latitud.ClientID%>').value = lat = marker.getPosition().lat();
                    document.getElementById('<%=longitud.ClientID%>').value = long = marker.getPosition().lng();
                    //Completamos el campo del estado en caso que no sea capturado
                    var x = results[0].address_components;
                    document.getElementById('<%=Estado.ClientID%>').value = x[4].long_name;
                    //obiene la direccion del marcador cuando lo arrastras
                    google.maps.event.addListener(marker, "dragend", function (event) {
                        var lat, long, address, resultArray, citi;

                        document.getElementById('<%=latitud.ClientID%>').value = lat = marker.getPosition().lat();
                        document.getElementById('<%=longitud.ClientID%>').value = long = marker.getPosition().lng();

                        var geocoder = new google.maps.Geocoder();

                        geocoder.geocode({ latLng: marker.getPosition() }, function (result, status) {
                            if ('OK' === status) {
                                address = result[0].formatted_address;
                                resultArray = result[0].address_components;

                                // Obtiene la direccion en sus componentes
                                if (resultArray.length === 7) {
                                    document.getElementById('<%=NumeroE.ClientID%>').value = resultArray[0].long_name;
                                    document.getElementById('<%=Calle.ClientID%>').value = resultArray[1].long_name;
                                    document.getElementById('<%=Colonia.ClientID%>').value = resultArray[2].long_name;
                                    document.getElementById('<%=Municipio.ClientID%>').value = resultArray[3].long_name;
                                    document.getElementById('<%=Estado.ClientID%>').value = resultArray[4].long_name;
                                    document.getElementById('<%=CodPos.ClientID%>').value = resultArray[6].long_name;


                                    for (var i = 0; i < resultArray.length; i++) {
                                        alert(resultArray[i].long_name);
                                    }
                                }
                                else {
                                    document.getElementById('<%=NumeroE.ClientID%>').value = '';
                                    document.getElementById('<%=Calle.ClientID%>').value = '';
                                    document.getElementById('<%=Colonia.ClientID%>').value = '';
                                    document.getElementById('<%=Municipio.ClientID%>').value = '';
                                    document.getElementById('<%=CodPos.ClientID%>').value = '';

                                    for (var i = 0; i < resultArray.length; i++) {
                                        console.log(resultArray[i].long_name);
                                    }
                                }

                            } else {
                                console.log('Geocode was not successful for the following reason: ' + status);
                            }
                        });
                    });

                } else {
                    alert('No se encontraron resultados: ' + status);
                }
            });
        }
    </script>


    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDydLi3gMd1ewUXvc1A2nRtgJwxDqOrA1Y&callback=initMap"
        async defer></script>

    <form id="forma">
        <main class="main">
            <nav aria-label="breadcrumb" class="breadcrumb-nav">
                <div class="container">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="Default.aspx"><i class="icon-home"></i></a></li>
                        <li class="breadcrumb-item active" aria-current="page">Dirección</li>
                    </ol>
                </div>
                <!-- End .container -->
            </nav>

            <div class="container">
                <div class="row">
                    <div class="col-lg-8">
                        <ul class="checkout-steps">
                            <li>
                                <h2 class="step-title">Modifica tu dirección</h2>
                                <br />

                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <div class="form-group required-field">
                                            <label>Calle </label>
                                            <input type="text" class="form-control" id="Calle" required placeholder="Calle" runat="server">
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group required-field">
                                            <label>Número exterior </label>
                                            <input type="text" class="form-control" id="NumeroE" required placeholder="Número exterior" runat="server">
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group">
                                            <label>Número interior </label>
                                            <input type="text" class="form-control" id="NumeroI" placeholder="Número interior" runat="server">
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group">
                                            <label>Colonia</label>
                                            <input type="text" class="form-control" id="Colonia" placeholder="Colonia" runat="server">
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group">
                                            <label>Código postal </label>
                                            <input type="text" class="form-control" id="CodPos" placeholder="Código postal" runat="server">
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group">
                                            <label>Municipio </label>
                                            <input type="text" class="form-control" id="Municipio" placeholder="Municipio" runat="server">
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group">
                                            <label>Estado </label>
                                            <input type="text" class="form-control" id="Estado" placeholder="Estado" runat="server">
                                        </div>

                                        <%-- boton de buscar --%>
                                        <div id="floating-panel col-lg-6" style="padding-top: 19%; text-align: center;">
                                            <input style="width: 25%;" id="submit" class="btn btn-circle btn-primary" type="button" value="Buscar">
                                        </div>

                                        <%-- seccion del mapa --%>
                                        <div class="portlet light portlet-fit" style="padding-top: 1%;">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <span class="caption-subject font-red bold uppercase">Selecciona el punto de la dirección</span>
                                                </div>
                                            </div>
                                            <%-- mapa --%>
                                            <div class="portlet-body">
                                                <div id="map" class="gmaps"></div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label hidden="hidden">Latitud </label>
                                            <input id="latitud" type="text" placeholder="Latitud" class="form-control" runat="server" hidden="hidden"/>
                                        </div>

                                        <div class="form-group">
                                            <label hidden="hidden">Longitud </label>
                                            <input id="longitud" type="text" placeholder="Longitud" class="form-control" runat="server" hidden="hidden"/>
                                        </div>
                                        <!-- End .form-group -->
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </li>
                        </ul>
                    </div>
                </div>
                <!-- End .row -->

                <div class="row">
                    <div class="col-lg-8">
                        <div class="checkout-steps-action">
                            <%--<a href="checkout-review.html" class="btn btn-primary float-right">Agregar dirección</a>--%>
                            <asp:Button ID="ButtonModificarDireccion" runat="server" CssClass="btn btn-primary" Text="Modificar dirección" OnClick="ButtonModificarDireccion_Click" />
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
        <!-- End .main -->
    </form>
</asp:Content>
