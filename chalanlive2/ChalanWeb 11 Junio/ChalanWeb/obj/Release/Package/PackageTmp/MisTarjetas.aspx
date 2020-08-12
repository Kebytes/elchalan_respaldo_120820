<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisTarjetas.aspx.cs" Inherits="ChalanWeb.MisTarjetas" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .btn-regresar {
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

        .btn-eliminar {
            border: none;
            padding: 10px;
            font-size: 22px;
            color: #FFF;
            background: #cb3234;
            box-shadow: 2px 2px 10px 0 rgba(0,85,212,0.4);
            cursor: pointer;
            border: 2px solid #CED6E0;
            font-size: 18px;
            height: 50px;
            width: 100%;
            padding: 5px 12px;
            transition: .3s ease all;
            border-radius: 15px;
        }
    </style>

    <nav aria-label="breadcrumb" class="breadcrumb-nav">
        <div class="container">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Cuenta.aspx"><i class="icon-home"></i></a></li>
                <li class="breadcrumb-item active" aria-current="page">Mis tarjetas</li>
            </ol>
            
        </div>
        <!-- End .container -->
    </nav>

    <div class="container">

        <div class="row">
            <div class="col-lg-8">

                <ol class="checkout-steps">
                    <li>
                        <h2 class="step-title">Resumen de tu(s) tarjeta(s):</h2>


                        <%--<asp:Button ID="ButtonAdd" runat="server" Text="Agregar tarjeta" CssClass="btn-enviar"/>--%>

                        <br />

                        <h1 class="banner-title" style="color: white; font-size: 50px;">
                            <span style="color: black">
                                <asp:Literal ID="litTarjetas" runat="server"></asp:Literal></span>
                        </h1>

                        <%--<div class="shipping-step-addresses">--%>


                        <%--</div>--%>
                        <!-- End .shipping-step-addresses -->
                        <%--<a href="NuevaDireccion.aspx" class="btn btn-sm btn-outline-secondary btn-new-address" data-toggle="modal" data-target="#addressModal">+ Agregar nueva dirección</a>--%>
                        
                    </li>
                </ol>
                <div style="display: inline;">
                <asp:Button ID="ButtonAgregar" runat="server" Text="+ Agregar tarjeta" CssClass="btn-regresar" OnClick="ButtonAgregar_Click" />
                <asp:Button ID="ButtonRegresar" runat="server" Text="Regresar" CssClass="btn-regresar" OnClick="ButtonRegresar_Click" />
            </div>
            </div>
            

        </div>
        <!-- End .col-lg-4 -->
    </div>

    <%--<div class="row">--%>
    <%--<div class="column">--%>
    <asp:Repeater runat="server" ID="repetidorTarjetas" OnItemCommand="repetidorTarjetas_ItemCommand">
        <ItemTemplate>

            <%--<div class="shipping-address-box" style="border: 1px solid black;">
                                        <address>
                                            <%# Eval("type") %>
                                            <br>
                                            <%# Eval("brand") %>
                                            <br>
                                            <%# Eval("card_number") %>
                                            <br>
                                            <%# Eval("holder_name") %>--%>
            <%--<input type="text" class="form-control" name="idDireccion" placeholder="Nombre" value="<%# Eval("Id") %>">--%>
            <%--</address>--%>

            <%--<div class="address-box-action clearfix">--%>

            <%--<a href="#" id="<%# Eval("Id") %>" class="btn btn-sm btn-outline-secondary float-right">Enviar aquí
                                                </a>--%>
            <%--<asp:Button ID="ButtonEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger float-right" Text="Eliminar" CommandName="Add" CommandArgument='<%# Eval("id") %>' />--%>

            <%--</div>--%>
            <!-- End .address-box-action -->
            <%--</div>--%>

            <%--<div class="contenedor2">--%>
            <div style="display: inline-block; width: 40%; padding: 25px;">
                <section class="tarjeta" id="tarjeta">
                    <div class="delantera">
                        <div class="logo-marca" id="logo-marca">
                            <img src="<%# Eval("imagen") %>" alt="" />
                        </div>
                        <img src="Recursos/chip-tarjeta.png" alt="" class="chip" />
                        <div class="datos">
                            <div class="flexbox">
                                <div class="grupo" id="numero">
                                    <p class="label">Número tarjeta</p>
                                    <p class="numero"><%# Eval("ULTIMOS4") %></p>
                                </div>
                                <div class="grupo" id="nombre">
                                    <p class="label">Nombre</p>
                                    <p class="nombre"><%# Eval("holder_name") %></p>
                                </div>
                            </div>


                            <div class="flexbox">
                                <div class="grupo" id="expiracion">
                                    <p class="label">Tipo</p>
                                    <%--<p class="expiracion"><span class="mes"><%# Eval("expiration_month") %></span> / <span class="year"><%# Eval("expiration_year") %></span></p>--%>
                                    <p class="expiracion"><span class="mes"><%# Eval("TIPO") %></span></p>
                                </div>
                            </div>
                            <asp:Button ID="ButtonEliminar" runat="server" Text="Eliminar tarjeta" CssClass="btn-eliminar" CommandName="Del" CommandArgument='<%# Eval("id") %>' OnClientClick="return Confirmation();" />
                        </div>
                        
                    </div>
                    
                </section>
            </div>

            <%--</div>--%>
        </ItemTemplate>
    </asp:Repeater>
    <%--</div>--%>

    <%--</div>--%>



    <%--<style type="text/css">
        .creditCardForm {
            max-width: 700px;
            background-color: #fff;
            margin: 100px auto;
            overflow: hidden;
            padding: 25px;
            color: #4c4e56;
        }

            .creditCardForm label {
                width: 100%;
                margin-bottom: 10px;
            }

            .creditCardForm .heading h1 {
                text-align: center;
                font-family: 'Open Sans', sans-serif;
                color: #4c4e56;
            }

            .creditCardForm .payment {
                float: left;
                font-size: 18px;
                padding: 10px 25px;
                margin-top: 20px;
                position: relative;
            }

                .creditCardForm .payment .form-group {
                    float: left;
                    margin-bottom: 15px;
                }

                .creditCardForm .payment .form-control {
                    line-height: 40px;
                    height: auto;
                    padding: 0 16px;
                }

            .creditCardForm .owner {
                width: 63%;
                margin-right: 10px;
            }

            .creditCardForm .CVV {
                width: 35%;
            }

            .creditCardForm #card-number-field {
                width: 100%;
            }

            .creditCardForm #expiration-date {
                width: 49%;
            }

            .creditCardForm #credit_cards {
                width: 50%;
                margin-top: 25px;
                text-align: right;
            }

            .creditCardForm #pay-now {
                width: 100%;
                margin-top: 25px;
            }

            .creditCardForm .payment .btn {
                width: 100%;
                margin-top: 3px;
                font-size: 24px;
                background-color: #2ec4a5;
                color: white;
            }

            .creditCardForm .payment select {
                padding: 10px;
                margin-right: 15px;
            }

        .transparent {
            opacity: 0.2;
        }

        @media(max-width: 650px) {
            .creditCardForm .owner,
            .creditCardForm .CVV,
            .creditCardForm #expiration-date,
            .creditCardForm #credit_cards {
                width: 100%;
            }

            .creditCardForm #credit_cards {
                text-align: left;
            }
        }
    </style>--%>

    <style>
        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }

        body {
            background: #ddeefc;
            font-family: 'Lato', sans-serif;
        }

        .contenedor {
            width: 70%;
            max-width: 700px;
            padding: 40px 20px;
            margin: auto;
            display: flex;
            flex-direction: column;
            align-items: center;
            column-count: 2;
        }

        .contenedor2 {
            width: 70%;
            max-width: 700px;
            align-items: center;
            padding: 20px 20px;
            flex-direction: row;
            display: inline-flex;
            column-count: 2;
        }

        .column {
            float: left;
            width: 50%;
            padding: 25px;
            display: table;
        }

        /* Clear floats after the columns */
        .row:after {
            content: "";
            display: table;
            clear: both;
        }

        .tarjeta {
            width: 100%;
            max-width: 550px;
            position: relative;
            color: #FFF;
            transition: .3s ease all;
            transform: rotateY(0deg);
            transform-style: preserve-3d;
            backface-visibility: hidden;
            cursor: pointer;
            z-index: 2;
        }

            .tarjeta.active {
                transform: rotateY(180deg);
            }

            .tarjeta > div {
                padding: 20px;
                border-radius: 15px;
                min-height: 315px;
                display: flex;
                flex-direction: column;
                justify-content: space-between;
                box-shadow: 0 10px 10px 0 rgba(90,116,148,0.3);
            }

            .tarjeta .delantera {
                width: 100%;
                background-image: url("Recursos/bg-tarjeta-02.jpg");
                background-size: cover;
            }

        .delantera .logo-marca {
            text-align: right;
            min-height: 50px;
        }

            .delantera .logo-marca img {
                width: 100%;
                height: 100%;
                object-fit: cover;
                max-width: 80px;
            }

        .delantera .chip {
            width: 100%;
            max-width: 50px;
            margin-bottom: 20px;
        }

        .delantera .grupo .label {
            font-size: 16px;
            color: #7D8994;
            margin-bottom: 5px;
        }

        .delantera .grupo .numero,
        .delantera .grupo .nombre,
        .delantera .grupo .expiracion {
            color: #FFF;
            font-size: 22px;
            text-transform: uppercase;
        }

        .delantera .flexbox {
            display: flex;
            justify-content: space-between;
            margin-top: 20px;
        }

        .trasera {
            background: url("Recursos/bg-tarjeta-02.jpg");
            background-size: cover;
            position: absolute;
            top: 0;
            transform: rotateY(180deg);
            backface-visibility: hidden;
        }

            .trasera .barra-magnetica {
                height: 40px;
                background: #000;
                width: 100%;
                position: absolute;
                top: 30px;
                left: 0;
            }

            .trasera .datos {
                margin-top: 60px;
                display: flex;
                justify-content: space-between;
            }

                .trasera .datos p {
                    margin-bottom: 5px;
                }

                .trasera .datos #firma {
                    width: 70%;
                }

                    .trasera .datos #firma .firma {
                        height: 40px;
                        background: repeating-linear-gradient(skyblue 0, skyblue 5px, orange 5px, orange 10px);
                    }

                        .trasera .datos #firma .firma p {
                            line-height: 40px;
                            font-family: 'Liu Jian Mao Cao', cursive;
                            color: #000;
                            font-size: 30px;
                            padding: 0 10px;
                            text-transform: capitalize;
                        }

                .trasera .datos #ccv {
                    width: 20%;
                }

                    .trasera .datos #ccv .ccv {
                        background: #FFF;
                        height: 40px;
                        color: #000;
                        padding: 10px;
                        text-align: center;
                    }

            .trasera .leyenda {
                font-size: 14px;
                line-height: 24px;
            }

            .trasera .link-banco {
                font-size: 14px;
                color: #FFF;
            }

        .contenedor-btn .btn-abrir-formulario {
            width: 50px;
            height: 50px;
            font-size: 20px;
            line-height: 20px;
            background: #2364D2;
            color: #FFF;
            position: relative;
            top: -25px;
            z-index: 3;
            border-radius: 100%;
            box-shadow: 5px 4px 8px rgba(24,56,182,0.4);
            padding: 5px;
            transition: all .2s ease;
            border: none;
            cursor: pointer;
        }

            .contenedor-btn .btn-abrir-formulario:hover {
                background: #1850B1;
            }

            .contenedor-btn .btn-abrir-formulario.active {
                transform: rotate(45deg);
            }

        .formulario-tarjeta {
            background: #FFF;
            width: 100%;
            max-width: 700px;
            padding: 150px 30px 30px 30px;
            border-radius: 10px;
            position: relative;
            top: -150px;
            z-index: 1;
            clip-path: polygon(0 0, 100% 0, 100% 0, 0 0);
            transition: clip-path .3s ease-out;
        }

            .formulario-tarjeta.active {
                clip-path: polygon(0 0, 100% 0, 100% 100%, 0 100%);
            }

            .formulario-tarjeta label {
                display: block;
                color: #7D8994;
                margin-bottom: 5px;
                font-size: 16px;
            }

            .formulario-tarjeta input,
            .formulario-tarjeta select,
            .btn-enviar {
                border: 2px solid #CED6E0;
                font-size: 18px;
                height: 50px;
                width: 100%;
                padding: 5px 12px;
                transition: .3s ease all;
                border-radius: 5px;
            }

                .formulario-tarjeta input:hover,
                .formulario-tarjeta select:hover {
                    border: 2px solid #93BDED;
                }

                .formulario-tarjeta input:focus,
                .formulario-tarjeta select:focus {
                    outline: rgb(4,4,4);
                    box-shadow: 1px 7px 10px -5px rgba(90,116,148,0.3);
                }

            .formulario-tarjeta input {
                margin-bottom: 20px;
                text-transform: uppercase;
            }

            .formulario-tarjeta .flexbox {
                display: flex;
                justify-content: space-between;
            }

            .formulario-tarjeta .expira {
                width: 100%;
            }

            .formulario-tarjeta .ccv {
                min-width: 100px;
            }

            .formulario-tarjeta .grupo-select {
                width: 100%;
                margin-right: 15px;
                position: relative;
            }

            .formulario-tarjeta select {
                -webkit-appearance: none;
            }

            .formulario-tarjeta .grupo-select i {
                position: absolute;
                color: #CED6E0;
                top: 18px;
                right: 15px;
                transition: .3s ease all;
            }

            .formulario-tarjeta .grupo-select:hover {
                color: #93BFED;
            }

            .formulario-tarjeta .btn-enviar {
                border: none;
                padding: 10px;
                font-size: 22px;
                color: #FFF;
                background: #2364D2;
                box-shadow: 2px 2px 10px 0 rgba(0,85,212,0.4);
                cursor: pointer;
            }

            .formulario-tarjeta .btn-cancelar {
                border: none;
                padding: 10px;
                font-size: 22px;
                color: #FFF;
                background: #CB3234;
                box-shadow: 2px 2px 10px 0 rgba(0,85,212,0.4);
                cursor: pointer;
            }

            .formulario-tarjeta .btn-enviar:hover {
                background: #1850B1;
            }
    </style>

    <script type="text/javascript">
        function Confirmation() {
            return confirm("¿Estás seguro de eliminar esta tarjeta?");
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
            text-align: center;
        }

            .btn-enviar:hover {
                background: #1850B1;
            }
    </style>

</asp:Content>
