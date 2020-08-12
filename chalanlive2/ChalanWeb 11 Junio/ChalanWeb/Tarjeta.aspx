<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tarjeta.aspx.cs" Inherits="ChalanWeb.Tarjeta" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script src="Scripts/jquery.creditCardValidator.js"></script>--%>

    <div class="creditCardForm" style="border: 1px solid #3399FF;">
        <div class="heading">
            <h1>Agrega tu tarjeta</h1>
        </div>
        <div class="payment">
            <form>
                <div class="form-group owner">
                    <label for="owner">Nombre</label>
                    <input type="text" class="form-control" id="Dueno" runat="server" pattern="^[a-zA-Z ]*$" title="Solamente texto">
                </div>
                <div class="form-group CVV">
                    <label for="cvv">CVV</label>
                    <input type="password" class="form-control" id="cvvNumber" runat="server" pattern="^\d{3,4}$" title="Solamente números de 3 o 4 cifras">
                    <%--<button onclick="showCVV()" type="button"><span class="glyphicon glyphicon-eye-open"></span></button>--%>
                </div>
                <div class="form-group" id="card-number-field">
                    <label for="cardNumber">Número de tarjeta</label>
                    <input type="text" class="form-control" id="cardNumber" runat="server" pattern="\d+" title="Solamente números">
                </div>
                <div class="form-group" id="expiration-date">
                    <label>Expira</label>
                    <select id="Meses" runat="server">
                        <option value="01">Enero</option>
                        <option value="02">Febrero </option>
                        <option value="03">Marzo</option>
                        <option value="04">Abril</option>
                        <option value="05">Mayo</option>
                        <option value="06">Junio</option>
                        <option value="07">Julio</option>
                        <option value="08">Agosto</option>
                        <option value="09">Septiembre</option>
                        <option value="10">Octubre</option>
                        <option value="11">Noviembre</option>
                        <option value="12">Diciembre</option>
                    </select>
                    <select id="Anio" runat="server">
                        <option value="20">2020</option>
                        <option value="21">2021</option>
                        <option value="22">2022</option>
                        <option value="23">2023</option>
                        <option value="24">2024</option>
                        <option value="25">2025</option>
                    </select>
                </div>
                <%--<div class="form-group" id="credit_cards">
                    <img src="assets/images/visa.jpg" id="visa">
                    <img src="assets/images/mastercard.jpg" id="mastercard">
                    <img src="assets/images/amex.jpg" id="amex">
                </div>--%>
                <div class="form-group" id="pay-now">
                    <%--<button type="submit" class="btn btn-default" id="confirm-purchase">Agregar</button>--%>
                    <asp:Button ID="AgregarTarjeta" CssClass="btn btn-default" runat="server" Text="Agregar" OnClick="AgregarTarjeta_Click"/>
                </div>
            </form>
        </div>
    </div>

<%--    <script>
        $(function () {
            $('<%=cardNumber.ClientID%>').validateCreditCard(function (result) {
                var tarjeta = result.card_type == null ? "-" : result.card_type.name + '<br>Valid: ' + result.valid
                    + '<br>Length valid: ' + result.length_valid
                    + '<br>Luhn valid: ' + result.luhn_valid;
                alert(tarjeta);
            });
        });
    </script>--%>

<%--    <script>
        $(function () {
            $('#cardNumber').validateCreditCard(function (result) {
                $('.log').html('Card type: ' + (result.card_type == null ? '-' : result.card_type.name)
                    + '<br>Valid: ' + result.valid
                    + '<br>Length valid: ' + result.length_valid
                    + '<br>Luhn valid: ' + result.luhn_valid);
            });
        });
    </script>--%>    

    <style type="text/css">
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
    </style>

</asp:Content>


