<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="ChalanWeb.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
           <main class="main">
    <br />
            <div class="container">
                <div class="row">

                    <asp:Repeater runat="server" ID="repetidorProducto"  OnItemCommand="repetidorProducto_ItemCommand">

                        <ItemTemplate>
                               <div class="col-lg-9">
                        <div class="product-single-container product-single-default">
                            <div class="row">
                                <div class="col-lg-7 col-md-6">
                                    <div class="product-single-gallery">
                                        <div class="product-slider-container product-item">
                                            <div class="product-single-carousel owl-carousel owl-theme">
                                                <div class="product-item">
                                                    <img class="product-single-image" src="<%# Eval("Imagen") %>" data-zoom-image="<%# Eval("Imagen") %>"/>
                                                </div>
                                                
                                            </div>
                                            <!-- End .product-single-carousel -->
                                            <span class="prod-full-screen">
                                                <i class="icon-plus"></i>
                                            </span>
                                        </div>
                                        
                                    </div><!-- End .product-single-gallery -->
                                </div><!-- End .col-lg-7 -->

                                <div class="col-lg-5 col-md-6">
                                    <div class="product-single-details">
                                        <h1 class="product-title"><%# Eval("Nombre") %></h1>


                                        <div class="price-box">
                                           
                                            <span class="product-price">$<%# Eval("Precio") %></span>
                                        </div><!-- End .price-box -->

                                        <div class="product-desc">
                                            <p><%# Eval("Descripcion") %></p>
                                        </div><!-- End .product-desc -->

                                        
                                        <div class="product-action">
                                            <div class="product-single-qty">
                                                <input class="horizontal-quantity form-control" type="text" id="cantidadProducto" name="cantidadProducto">
                                            </div><!-- End .product-single-qty -->

                                        <asp:Button ID="ButtonAgregarCarrito" runat="server" CssClass="paction add-cart" Text="Agregar a Carrito" CommandName="Add" CommandArgument='<%# Eval("Id") %>'/>
                                  
                                      
                                        </div><!-- End .product-action -->

                                        
                                    </div><!-- End .product-single-details -->
                                </div><!-- End .col-lg-5 -->
                            </div><!-- End .row -->
                        </div><!-- End .product-single-container -->

                  
                    </div>


                        </ItemTemplate>
                    </asp:Repeater>
                 
                <div class="mb-lg-4"></div><!-- margin -->
            </div><!-- End .container -->
                </div>
        </main>

</asp:Content>
