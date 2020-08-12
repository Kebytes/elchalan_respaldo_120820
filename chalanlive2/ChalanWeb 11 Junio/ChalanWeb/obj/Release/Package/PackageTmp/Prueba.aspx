<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prueba.aspx.cs" Inherits="ChalanWeb.Prueba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDydLi3gMd1ewUXvc1A2nRtgJwxDqOrA1Y&callback=initMap"
            async defer></script>
        <%--<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>--%>
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
                        zoom: 6,
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
        <asp:Repeater ID="rptMaps" runat="server">
            <ItemTemplate>
                <div id='dvMap<%# Container.ItemIndex + 1 %>' style="width: 400px; height: 200px">
                </div>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
        </asp:Repeater>

    </body>
    </html>
</asp:Content>
