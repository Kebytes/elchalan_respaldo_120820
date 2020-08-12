<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="ChalanWeb.Categoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .labelProducto:hover{
            color:#0080FF;
        }
    </style>

    <main class="main">
        <div class="banner banner-cat" style="background-image: url('assets/images/banners/banner-top.jpg');">
            <div class="banner-content container">

                <h1 class="banner-title" style="color: white; font-size: 50px;">
                    <asp:Literal ID="litNombreCategoria" runat="server"></asp:Literal>
                </h1>

            </div>
            <!-- End .banner-content -->
        </div>
        <!-- End .banner -->
        </br>


        <div class="container">
            <nav class="toolbox">
                <div class="toolbox-left">
                    <div class="toolbox-item toolbox-sort">
                        <div class="select-custom">
                            <select name="orderby" class="form-control">
                                <option value="menu_order" selected="selected">Ordenar</option>


                                <option value="price">Ordenar por Precio: menor a mayor</option>
                                <option value="price-desc">Ordenar por Precio: mayor a menor<</option>
                            </select>
                        </div>
                        <!-- End .select-custom -->

                        <%--        <a href="#" class="sorter-btn" title="Set Ascending Direction"><span class="sr-only">Ordenar </span></a>--%>
                    </div>
                    <!-- End .toolbox-item -->
                </div>
                <!-- End .toolbox-left -->


                <!-- End .layout-modes -->
            </nav>

            <div class="row row-sm">

                <asp:Repeater runat="server" ID="repetidorCategoria" OnItemCommand="repetidorCategoria_ItemCommand">
                    <ItemTemplate>
                        <div class="col-6 col-md-4 col-xl-2">
                            <div class="product-default">
                                <figure>
                                    <a href="<%# string.Concat( "Product?id=", Eval("Id") , "&producto=", Eval("Nombre")  )%>" >
                                        <img style="width: 178px !important; height: 178px !important; object-fit: contain;" onerror="this.onerror=null; this.src='/assets/images/chalandefault.jpg'" src="<%# Eval("Imagen") %> ">
                                    </a>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </figure>
                                <div class="product-details">
                                   <%-- <div class="ratings-container">
                                        <div class="product-ratings">
                                            <span class="ratings" style="width: 100%">4</span>
                                            <!-- End .ratings -->
                                            <span class="tooltiptext tooltip-top"></span>
                                        </div>
                                        <!-- End .product-ratings -->
                                    </div>--%>
                                    <!-- End .product-container -->
                                    <h1 class="product-title" >
                                        <a href="<%# string.Concat( "Product?id=", Eval("Id"))%>"><label class="labelProducto" style="word-break:break-word; cursor:pointer;"><%# Eval("Nombre") %></label></a>
                                    </h1>
                                    <div class="price-box">
                                        <span class="product-price" style='text-decoration: line-through; <%# Convert.ToDecimal(Eval("Precio_Simbolico")) == 0 || Convert.ToDecimal(Eval("Precio_Simbolico")) == Convert.ToDecimal("0.00") ? "display:none;": "display:inline;" %>'>$<%# Eval("Precio_Simbolico") %></span>
                                        <span class="product-price">$<%# Eval("Precio") %></span>
                                    </div>
                                    <!-- End .price-box -->
                                    <div class="product-action">
                                        <asp:Button ID="ButtonAgregarCarrito" runat="server" CssClass="btn-icon btn-add-cart" Text="Agregar a Carrito" CommandName="Add" CommandArgument='<%# Eval("Id") %>' />
                                    </div>
                                </div>
                                <!-- End .product-details -->
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>

        </div>
        <!-- End .container -->

        <div class="mb-5"></div>
        <!-- margin -->
    </main>


</asp:Content>
