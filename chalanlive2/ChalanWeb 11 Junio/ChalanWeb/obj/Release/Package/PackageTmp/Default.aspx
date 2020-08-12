<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ChalanWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <script type="text/javascript">
        function AgregarACarrito(o) {
            alert(o.id);
            var idProducto = o.id;
            PageMethods.AddCart(idProducto);
        }

        function onsuccess(result) {

            alert(result);

        }
        function onfailure(error) {

            alert(error);
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

            <main class="main">
                <div class="home-top-container">
                    <div class="container">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="home-slider owl-carousel owl-carousel-lazy owl-loaded owl-drag loaded">
                                    <!-- End .home-slide -->

                                    <!-- End .home-slide -->

                                    <!-- End .home-slide -->
                                    <div class="owl-stage-outer">
                                        <div class="owl-stage" style="transform: translate3d(-1740px, 0px, 0px); transition: all 0s ease 0s; width: 6090px;">

                                            <div class="owl-item cloned" style="width: 870px;">
                                                <div class="home-slide">
                                                    <div class="owl-lazy slide-bg" data-src="assets/images/slider/slide-1.jpg" style="background-image: url(&quot;assets/images/slider/slide-1.jpg&quot;); opacity: 1;"></div>
                                                    <div class="home-slide-content text-white">
                                                        <%--     <h3>Get up to <span>60%</span> off</h3>
                                                <h1>Summer Sale</h1>
                                                <p>Limited items available at this price.</p>
                                                <a href="category.html" class="btn btn-dark">Shop Now</a>--%>
                                                    </div>
                                                    <!-- End .home-slide-content -->
                                                </div>
                                            </div>
                                            <div class="owl-item cloned" style="width: 870px;">
                                                <div class="home-slide">
                                                    <div class="owl-lazy slide-bg" data-src="assets/images/slider/slide-2.jpg" style="background-image: url(&quot;assets/images/slider/slide-2.jpg&quot;); opacity: 1;"></div>
                                                    <div class="home-slide-content">
                                                        <%-- <h3>OVER <span>200+</span></h3>
                                                <h1>GREAT DEALS</h1>
                                                <p>While they last!</p>
                                                <a href="category.html" class="btn btn-dark">Shop Now</a>--%>
                                                    </div>
                                                    <!-- End .home-slide-content -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="owl-nav disabled">
                                        <button type="button" role="presentation" class="owl-prev"><i class="icon-left-open-big"></i></button>
                                        <button type="button" role="presentation" class="owl-next"><i class="icon-right-open-big"></i></button>
                                    </div>
                                    <div class="owl-dots">
                                        <button role="button" class="owl-dot active"><span></span></button>
                                        <button role="button" class="owl-dot"><span></span></button>

                                    </div>
                                </div>
                                <!-- End .home-slider -->
                            </div>
                            <!-- End .col-lg-9 -->

                            <%--                <div class="col-lg-3 order-lg-first">
                        <div class="side-custom-menu">
                            <h2>TOP CATEGORIES</h2>

                            <div class="side-menu-body">
                                <ul>
                                    <li><a href="#"><i class="icon-cat-shirt"></i>Fashion</a></li>
                                    <li><a href="#"><i class="icon-cat-computer"></i>Electronics</a></li>
                                    <li><a href="#"><i class="icon-cat-gift"></i>Gifts</a></li>
                                    <li><a href="#"><i class="icon-cat-couch"></i>Home &amp; Garden</a></li>
                                    <li><a href="#"><i class="icon-cat-computer"></i>Music</a></li>
                                    <li><a href="#"><i class="icon-cat-sport"></i>Sports</a></li>
                                </ul>

                                <a href="#" class="btn btn-block btn-primary">HUGE SALE - <strong>70%</strong> Off</a>
                            </div>
                            <!-- End .side-menu-body -->
                        </div>
                        <!-- End .side-custom-menu -->
                    </div>--%>
                            <!-- End .col-lg-3 -->
                        </div>
                        <!-- End .row -->
                    </div>
                    <!-- End .container -->
                </div>
                <!-- End .home-top-container -->

                <div class="info-boxes-container">
                    <div class="container">
                        <div class="info-box">
                            <i class="icon-shipping"></i>

                            <div class="info-box-content">
                                <h4>ENVÍOS A TODO TORREÓN, COAHUILA</h4> <%--&amp;--%>
                                <p>Envíos gratuitos en la compra de $599.</p>
                            </div>
                            <!-- End .info-box-content -->
                        </div>
                        <!-- End .info-box -->

                        <div class="info-box">
                            <i class="icon-us-dollar"></i>

                            <div class="info-box-content">
                                <h4>GARANTÍA POR TU DINERO</h4>
                                <p>100% de tu pedido está asegurado</p>
                            </div>
                            <!-- End .info-box-content -->
                        </div>
                        <!-- End .info-box -->

                        <div class="info-box">
                            <i class="icon-support"></i>

                            <div class="info-box-content">
                                <h4>COMPRA EN LÍNEA TODOS LOS DÍAS</h4>
                                <p>Compra en línea en cualquier momento</p>
                            </div>
                            <!-- End .info-box-content -->
                        </div>
                        <!-- End .info-box -->
                    </div>
                    <!-- End .container -->
                </div>
                <!-- End .info-boxes-container -->


                <div class="mb-4"></div>
                <!-- margin -->
                <!-- End .info-boxes-container -->

                <div class="container">
                    <h2 class="title text-center">FAVORITOS DEL PATRÓN</h2>
                    <div class="row justify-content-center">

                        <asp:Repeater ID="RepetidorFavoritos" runat="server" OnItemCommand="RepetidorFavoritos_ItemCommand">
                            <ItemTemplate>

                                <div class="col-6 col-md-4 col-xl-2">
                                    <div class="product-default left-details mb-4">
                                        <figure>
                                            <a href="<%# string.Concat( "Product?id=", Eval("Id"))%>">
                                                <img style="width: 178px !important; height: 178px !important; object-fit: contain;" onerror="this.onerror=null; this.src='/assets/images/chalandefault.jpg'" src="<%# Eval("Imagen") %> ">
                                            </a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </figure>



                                        <div class="product-details">
                                            <div class="category-list">
                                                <a href="category.html" class="product-category"><%# Eval("Categoria") %></a>
                                            </div>
                                            <h2 class="product-title">
                                                <%--<a href="<%# string.Concat( "Product?id=", Eval("Id"))%>"><label class="labelProducto" style="word-break:break-word; cursor:pointer;"><%# Eval("Nombre") %></label></a>--%>
                                                <a href="<%# string.Concat( "Product?id=", Eval("Id"))%>"><%# Eval("Nombre") %></a>
                                            </h2>
                                            <%-- <div class="ratings-container">
                                        <div class="product-ratings">
                                            <span class="ratings" style="width: 0%"></span>
                                            <!-- End .ratings -->
                                            <span class="tooltiptext tooltip-top"></span>
                                        </div>
                                        <!-- End .product-ratings -->
                                    </div>--%>
                                            <!-- End .product-container -->
                                            <div class="price-box">
                                                <span class="product-price" style='text-decoration: line-through; <%# Convert.ToDecimal(Eval("Precio_Simbolico")) == 0 || Convert.ToDecimal(Eval("Precio_Simbolico")) == Convert.ToDecimal("0.00") ? "display:none;": "display:inline;" %>'>$<%# Eval("Precio_Simbolico") %></span>
                                                <span class="product-price">$<%# Eval("Precio") %></span>
                                            </div>
                                            <!-- End .price-box -->
                                            <div class="product-action">

                                                <asp:Button ID="Button1" runat="server" CssClass="btn-icon btn-add-cart" Text="Agregar a Carrito" CommandName="Add" CommandArgument='<%# Eval("Id") %>' OnClick="Button1_Click"/>
                                                <%--<button class="btn-icon btn-add-cart" data-toggle="modal" data-target="#addCartModal" id="<%# Eval("Id") %>" onclick="AgregarACarrito(this); return false;"><i class="icon-bag"></i>AGREGAR A CARRITO</button>--%>
                                            </div>
                                        </div>
                                        <!-- End .product-details -->
                                    </div>
                                </div>

                            </ItemTemplate>

                        </asp:Repeater>



                        <!-- End .col-lg-3 -->

                    </div>
                    <!-- End .row -->
                </div>
            </main>

</asp:Content>
