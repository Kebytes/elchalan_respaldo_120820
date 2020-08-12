<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCard.aspx.cs" Inherits="ChalanWeb.PutaMadre" Async="true" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Agregar nueva tarjeta</title>
    <link href="https://fonts.googleapis.com/css?family=Lato|Liu+Jian+Mao+Cao&display=swap" rel="stylesheet">
    <script src="//rawgit.com/notifyjs/notifyjs/master/dist/notify.js"></script>
    <%--<link rel="stylesheet" href="css/estilos.css">--%>

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
            width: 90%;
            max-width: 1000px;
            padding: 40px 20px;
            margin: auto;
            display: flex;
            flex-direction: column;
            align-items: center;
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
                padding: 30px;
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
            /*height:100px;*/
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
    </style>

    <script>
        function goPrev()
            {
                window.history.back();
            }
    </script>
</head>

<body>
    
    <script src="https://kit.fontawesome.com/2c36e9b7b1.js" crossorigin="anonymous"></script>
    <%--<script src="js/main.js"></script>--%>
    <div class="contenedor">
        <%--<button value="Regresar" class="btn-regresar" onclick="goPrev()" name="Regresar"></button>--%>
        <input type="submit" name="cancel" value="Regresar" onclick="goPrev()" class="btn-regresar"/>
        <br />
        <section class="tarjeta" id="tarjeta">
            <div class="delantera">
                <div class="logo-marca" id="logo-marca">
                    <%--<img src="Recursos/mastercard.png" alt="" />--%>
                </div>
                <img src="Recursos/chip-tarjeta.png" alt="" class="chip" />
                <div class="datos">
                    <div class="grupo" id="numero">
                        <p class="label">Número tarjeta</p>
                        <p class="numero">#### #### #### ####</p>
                    </div>

                    <div class="flexbox">
                        <div class="grupo" id="nombre">
                            <p class="label">Nombre de la tarjeta</p>
                            <p class="nombre">John Doe</p>
                        </div>
                        <div class="grupo" id="expiracion">
                            <p class="label">Expiración</p>
                            <p class="expiracion"><span class="mes">MM</span> / <span class="year">AA</span></p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="trasera">
                <div class="barra-magnetica"></div>
                <div class="datos">
                    <div class="grupo" id="firma">
                        <p class="label">Firma</p>
                        <div class="firma">
                            <p></p>
                        </div>
                    </div>
                    <div class="grupo" id="ccv">
                        <p class="label">CVV</p>
                        <p class="ccv"></p>
                    </div>
                </div>
                <p class="leyenda">
                    Taller de Diseño Web: Formulario Dinámico para Tarjeta de Crédito | HTML, CSS y Javascript.
                </p>
                <a href="#" class="link-banco">www.tubanco.com</a>
            </div>

        </section>

        <div class="contenedor-btn">
            <button class="btn-abrir-formulario" id="btn-abrir-formulario" type="button"><i class="fa fa-plus"></i></button>
        </div>


        <form id="formulario_tarjeta" class="formulario-tarjeta" runat="server" action="AddCard.aspx" method="post">
            <asp:ScriptManager ID="idScriptManager" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
            <asp:UpdatePanel ID="idUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="grupo">
                        <label for="inputNumero">Número tarjeta</label>
                        <input type="text" id="inputNumero" name="inputNumero" autocomplete="off" style="height: 50px;" />
                    </div>
                    <div class="grupo">
                        <label for="inputNombre">Nombre</label>
                        <input type="text" id="inputNombre" name="inputNombre" autocomplete="off" style="height: 50px;" />
                    </div>

                    <div class="flexbox">
                        <div class="grupo expira">
                            <label for="selectMes">Expiración</label>
                            <div class="flexbox">
                                <div class="grupo-select">
                                    <select id="selectMes" runat="server">
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
                                    <%--<asp:DropDownList runat="server" ID="Meses" />--%>
                                    <i class="fa fa-angle-down"></i>
                                </div>
                                <div class="grupo-select">
                                    <select name="year" id="selectYear" runat="server">
                                        <option value="2020">2020</option>
                                        <option value="2021">2021</option>
                                        <option value="2022">2022</option>
                                        <option value="2023">2023</option>
                                        <option value="2024">2024</option>
                                        <option value="2025">2025</option>
                                        <option value="2026">2026</option>
                                        <option value="2027">2027</option>
                                        <option value="2028">2028</option>
                                        <option value="2029">2029</option>
                                    </select>
                                    <i class="fa fa-angle-down"></i>
                                </div>
                            </div>
                        </div>

                        <div class="grupo ccv">
                            <label for="inputCCV">CCV</label>
                            <input type="text" id="inputCCV" name="inputCCV" maxlength="4" />
                        </div>
                    </div>
                    <asp:Button ID="ButtonAdd" runat="server" Text="Agregar tarjeta" CssClass="btn-enviar" OnClick="ButtonAdd_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true';"/>
                    <%--<button class="btn-enviar">Agregar tarjeta</button>--%>
                    <asp:Button ID="ButtonRegresar" runat="server" Text="Regresar" CssClass="btn-cancelar" OnClick="ButtonRegresar_Click" />

                    <script type="text/javascript">
                        const tarjeta = document.querySelector('#tarjeta');
                        const btnAbrirFormulario = document.querySelector('#btn-abrir-formulario');
                        const formulario = document.querySelector('#formulario_tarjeta');
                        const numeroTarjeta = document.querySelector('#tarjeta .numero');
                        const nombreTarjeta = document.querySelector('#tarjeta .nombre');
                        const logoMarca = document.querySelector('#logo-marca');
                        const firma = document.querySelector('#tarjeta .firma p');
                        const mesExpira = document.querySelector('#tarjeta .mes');
                        const yearExpira = document.querySelector('#tarjeta .year');
                        const ccv = document.querySelector('#tarjeta .ccv');


                        //const mostrarFrente = () => {
                        //    if (tarjeta.classList.contains('active')) {
                        //        tarjeta.classList.remove('active');
                        //    }
                        //}

                        //tarjeta.addEventListener('click', () => {
                        //    tarjeta.classList.toggle('active');
                        //});

                        btnAbrirFormulario.addEventListener('click', () => {
                            btnAbrirFormulario.classList.toggle('active');
                            formulario.classList.toggle('active');
                        });

                        //for (let i = 1; i <= 12; i++) {
                        //    console.log(i);
                        //    let option = document.createElement('option');
                        //    option.value = i;
                        //    option.innerText = i;
                        //    formulario.selectMes.appendChild(option);
                        //}

                        //const actualYear = new Date().getFullYear();
                        //console.log(actualYear);
                        //for (let i = actualYear; i <= actualYear + 8; i++) {
                        //    let option = document.createElement('option');
                        //    option.value = i;
                        //    option.innerText = i;
                        //    formulario.selectYear.appendChild(option);
                        //}

                        formulario.inputNumero.addEventListener('keyup', (e) => {
                            let valor = e.target.value;

                            formulario.inputNumero.value = valor.replace(/\s/g, '').replace(/\D/g, '').replace(/([0-9]{4})/g, '$1 ').trim();

                            numeroTarjeta.textContent = valor;

                            if (valor == '') {
                                numeroTarjeta.textContent = '#### #### #### ####';
                                logoMarca.innerHTML = '';
                            }

                            if (valor[0] == 4) {
                                logoMarca.innerHTML = '';
                                const imagen = document.createElement('img');
                                imagen.src = 'Recursos/visa.png';
                                logoMarca.appendChild(imagen);
                            }

                            else if (valor[0] == 5) {
                                logoMarca.innerHTML = '';
                                const imagen = document.createElement('img');
                                imagen.src = 'Recursos/mastercard.png';
                                logoMarca.appendChild(imagen);
                            }

                            //mostrarFrente();
                        });


                        formulario.inputNombre.addEventListener('keyup', (e) => {
                            let valor = e.target.value;

                            formulario.inputNombre.value = valor.replace(/[0-9]/g, '');
                            nombreTarjeta.textContent = valor;
                            firma.textContent = valor;

                            if (valor == '') {
                                nombreTarjeta.textContent = 'John Doe';
                            }

                            //mostrarFrente();
                        });

                        formulario.selectMes.addEventListener('change', (e) => {
                            mesExpira.textContent = e.target.value;
            //mesExpiraHidden.value = e.target.value;
            <%--document.getElementById('<%= MesExpiraHidden.ClientID %>').value = e.target.value;--%>
                            //mostrarFrente();
                        });

                        formulario.selectYear.addEventListener('change', (e) => {
                            yearExpira.textContent = e.target.value.slice(2);
                            //mostrarFrente();
                        });

                        formulario.inputCCV.addEventListener('keyup', () => {
                            //if (!tarjeta.classList.contains('active')) {
                            //    tarjeta.classList.toggle('active');
                            //}

                            formulario.inputCCV.value = formulario.inputCCV.value.replace(/\s/g, '').replace(/\D/g, '');
                            ccv.textContent = formulario.inputCCV.value;
                        });

                    </script>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ButtonAdd" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="idUpdatePanel">
                <ProgressTemplate>
                    <div style="position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                        <img id="idAjaxLoader" alt="" src="Recursos/loading_gif.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </form>
    </div>



</body>
</html>
