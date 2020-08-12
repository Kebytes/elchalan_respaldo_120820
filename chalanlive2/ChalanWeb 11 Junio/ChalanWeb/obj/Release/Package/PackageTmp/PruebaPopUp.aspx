<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaPopUp.aspx.cs" Inherits="ChalanWeb.PruebaPopUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            background-color: #fefefe;
            margin: auto;
            padding: 0;
            border: 1px solid #888;
            width: 80%;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
            -webkit-animation-name: animatetop;
            -webkit-animation-duration: 0.4s;
            animation-name: animatetop;
            animation-duration: 0.4s
        }

        /* Add Animation */
        @-webkit-keyframes animatetop {
            from {
                top: -300px;
                opacity: 0
            }

            to {
                top: 0;
                opacity: 1
            }
        }

        @keyframes animatetop {
            from {
                top: -300px;
                opacity: 0
            }

            to {
                top: 0;
                opacity: 1
            }
        }

        /* The Close Button */
        .close {
            color: white;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }

        .modal-header {
            padding: 2px 16px;
            background-color: #cb3234;
            color: white;
        }

        .modal-body {
            padding: 2px 16px;
        }

        .modal-footer {
            padding: 2px 16px;
            background-color: #5cb85c;
            color: white;
        }

        .container {
            text-align: center;
            margin-top: 360px;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .btn {
            border: 1px solid #3498db;
            background: none;
            padding: 10px 20px;
            font-size: 20px;
            font-family: Montserrat;
            cursor: pointer;
            margin: 10px;
            transition: 0.8s;
            position: relative;
            overflow: hidden;
        }

        .btn1, .btn2 {
            color: #3498db;
        }

        .btn3, .btn4 {
            color: #fff;
        }

        .btn1:hover, btn2:hover {
            color: #fff;
        }

        .btn3:hover, btn4:hover {
            color: #3498db;
        }

        .btn::before {
            content: none;
            position: absolute;
            left: 0;
            width: 100%;
            height: 0%;
            background: #3498db;
            z-index: -1;
            transition: 0.8s;
        }

        .btn1::before, .btn3::before {
            top: 0;
            border-radius: 0 0 50% 50%;
        }

        .btn2::before, .btn4::before {
            bottom: 0;
            border-radius: 50% 50% 0 0;
        }

        .btn3::before, .btn4::before {
            height: 180%;
        }

        .btn1:hover::before, .btn2:hover::before {
            height: 180%;
        }

        .btn3:hover::before, .btn4:hover::before {
            height: 0%;
        }
    </style>


</head>
<body>

    <div>
        <h2>Animated Modal with Header and Footer</h2>

        <!-- Trigger/Open The Modal -->
        <button id="myBtn">Open Modal</button>


        <!-- The Modal -->
        <div id="myModal" class="modal" runat="server">

            <!-- Modal content -->
            <div class="modal-content">
                <div class="modal-header">
                    <%--<span class="close">&times;</span>--%>
                    <h2 style="text-align:center;">Confirmación de edad</h2>
                </div>
                <div class="modal-body">
                    <p style="text-align:center;">En El Chalán nos preocupamos por un consumo responsable y prevenimos la venta de alcohol a menores, te pedimos
                    contestar con responsabilidad.</p>
                </div>
                <%--<br />--%>
                <div style="text-align:center;">
                    <button class="btn btn2">Entrar, soy mayor de 18 años.</button>
                    <button class="btn btn2">Salir, soy mayor de 18 años.</button>
                    
                </div>
            </div>

        </div>

    </div>


    <script type="text/javascript">
        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("myBtn");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks the button, open the modal 
        btn.onclick = function () {
            //alert("Click");
            modal.style.display = "block";
        }


        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "block";
            }
        }
    </script>

</body>
</html>
