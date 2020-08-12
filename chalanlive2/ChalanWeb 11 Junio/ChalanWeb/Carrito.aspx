<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="ChalanWeb.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="main">
        <nav aria-label="breadcrumb" class="breadcrumb-nav">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Default.aspx"><i class="icon-home"></i></a></li>
                    <li class="breadcrumb-item active" aria-current="page">Tu carrito</li>
                </ol>
            </div>
            <!-- End .container -->
        </nav>

        <div class="container">
            <div class="row">


                <form action="Carrito.aspx" method="post">
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

                                    <asp:Repeater ID="repetidorCarro" runat="server" OnItemCommand="repetidorCarro_ItemCommand">

                                        <ItemTemplate>

                                            <tr class="product-row">
                                                <td class="product-col">
                                                    <figure class="product-image-container">
                                                        <a href="<%# string.Concat( "Product?id=", Eval("Id_Producto"))%>" class="product-image">
                                                            <img src="<%# Eval("Imagen") %>" onerror="this.onerror=null; this.src='/assets/images/chalandefault.jpg'" alt="product">
                                                        </a>
                                                    </figure>
                                                    <h1 class="product-title">
                                                        <%--<input type="text" name="Nombres" id="Nombres" value='<%# Eval("Nombre") %>' class="asd"/>--%>
                                                        <textarea name="Nombres" cols="14" rows="3" readonly="readonly" style="resize:none;" class="asd"><%# Eval("Nombre") %></textarea>
                                                        <%--<a name="Nombres"><%# Eval("Nombre") %></a>--%>

                                                        <%--<input class="form-control" name="Nombres" type="text" value="<%# Eval("Nombre") %>" id="Nombres" readonly="readonly">--%>
                                                        <%--<label data-name="Nombres"><%# Eval("Nombre") %></label>--%>
                                                    </h1>
                                                </td>
                                                <td>$<span class="precio" id="PrecioProducto"><%# Eval("Precio") %></span></td>



                                                <td>
                                                    <input class="vertical-quantity form-control" name="Cantidad" type="text" value="<%# Eval("Cantidad") %>" id="Cantidad">
                                                </td>
                                                <td>$<span class="replaceme" id="SubTotal"><%# Eval("Costo") %></span></td>


                                                <td>
                                                    <asp:Button ID="ButtonEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger float-right" Text="Eliminar producto" CommandName="Del" CommandArgument='<%# Eval("Id_Producto") %>' OnClientClick="return Confirmation();" /></td>

                                                <%--<asp:Literal ID="LiteralTotalProducto" runat="server" Text='<%# Eval("Costo") %>'></asp:Literal>--%>
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
                            </table>
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
                </form>
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
                                    <td>$<span class="replaceSubTotal"><asp:Literal ID="literalSubtotal" runat="server"></asp:Literal></span></td>


                                </tr>

                                <tr>
                                    <td>Impuestos</td>
                                    <td>$0.00</td>
                                </tr>

                                <tr>
                                    <td>Costo del envío</td>
                                    <td>$29.00</td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>Precio Total</td>
                                    <td>$<span class="replaceTotal"><asp:Literal ID="literalTotal" runat="server"></asp:Literal></span></td>
                                </tr>
                            </tfoot>
                        </table>

                        <div class="checkout-methods">
                            <%--<a href="Envio" class="btn btn-block btn-sm btn-primary">Finalizar compra</a>--%>
                            <asp:Button ID="ButtonFinalizar" CssClass="btn btn-block btn-sm btn-primary" runat="server" Text="Finalizar compra" OnClick="ButtonFinalizar_Click" />

                        </div>
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


    <%--    <script>
        $(document).ready(function () {
            $("#Cantidad").on("input", function () {
                alert("Entre");
            });
        });
    </script>--%>

    <style type="text/css">
        .asd {
            background: rgba(0, 0, 0, 0);
            border: none;
        }
    </style>

    <script type="text/javascript">
        function Confirmation() {
            return confirm("¿Estás seguro de eliminar este producto?");
        }
    </script>
</asp:Content>
