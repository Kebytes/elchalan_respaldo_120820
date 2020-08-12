using ChalanWeb.Controllers;
using ChalanWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChalanWeb
{
    public partial class PutaMadre : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }


        protected async void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (Session["cliente"] != null)
            {
                Cliente cliente = (Cliente)Session["cliente"];

                CreditCard card = new CreditCard();
                card.Dueño = Request.Form["inputNombre"].ToString();
                card.NumeroTarjeta = string.Concat(Request.Form["inputNumero"].ToString().Where(c => !char.IsWhiteSpace(c)));
                card.CVV = Request.Form["inputCCV"].ToString();
                card.Expira_Mes = selectMes.Items[selectMes.SelectedIndex].Value;
                string year = selectYear.Items[selectYear.SelectedIndex].Value;
                card.Expira_Anio = year.Substring(year.Length - 2);

                cliente.Pedidos = null;
                cliente.Direcciones = null;
                string clienteNuevo = JsonConvert.SerializeObject(cliente);
                string tarjeta = "{\"card_number\":\"" + card.NumeroTarjeta + "\",\"holder_name\":\"" + card.Dueño + "\",\"expiration_year\":\"" + card.Expira_Anio + "\",\"expiration_month\":\"" + card.Expira_Mes + "\",\"cvv2\":\"" + card.CVV + "\"}";

                Controllers.ConexionApi conexion = new Controllers.ConexionApi();

                List<string> respuesta = await conexion.ActualizarClienteToken(clienteNuevo, tarjeta);

                Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(respuesta[0]);

                if (mensaje.Titulo.Equals("ERROR"))
                {
                    string error_code = mensaje.Contenido.Substring(0, 4);
                    string error = Checar_Codigo(Convert.ToInt32(error_code));

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('"+error+"');window.location ='AddCard.aspx';",
                    true);

                    //Response.Write("<script language='javascript'>window.alert("+error+");window.location='AddCard.aspx';</script>");

                    //            ScriptManager.RegisterClientScriptBlock(this, GetType(), "redirect",
                    //"location.href = 'AddCard.aspx#CardError';", true);
                    //Response.Redirect("AddCard.aspx#CardError");
                    //Response.Redirect(string.Concat("AddCard.aspx#",error));
                }

                else
                {
                    Cliente cli = JsonConvert.DeserializeObject<Cliente>(respuesta[1]);
                    Session["cliente"] = cli;
                    Response.Redirect("MisTarjetas.aspx#CardSuccess");
                }
            }
            else
            {
                Response.Redirect("Default.aspx#notsession");
            }
        }

        protected string Checar_Codigo(int error_code)
        {
            string error = "";
            switch (error_code)
            {
                case (1000):
                    //error = "MQAwADAAMAA=";
                    error = "Ocurrió un error interno.";
                    break;
                case (2004):
                    //error = "MgAwADAANAA=";
                    error = "El dígito de verificación del número de tarjeta no es válido.";
                    break;
                case (2005):
                    //error = "MgAwADAANQA=";
                    error = "La fecha de expiración de la tarjeta es anterior a la fecha actual.";
                    break;
                case (2006):
                    //error = "MgAwADAANgA=";
                    error = "El código de seguridad de la tarjeta (CVV2) no fue proporcionado.";
                    break;
                case (2009):
                    //error = "MgAwADAAOQA=";
                    error = "El código de seguridad de la tarjeta (CVV2) no es válido.";
                    break;
                case (3001):
                    //error = "MwAwADAAMQA=";
                    error = "La tarjeta fue declinada.";
                    break;
                case (3002):
                    //error = "MwAwADAAMgA=";
                    error = "La tarjeta ha expirado.";
                    break;
                case (3003):
                    //error = "MwAwADAAMwA=";
                    error = "La tarjeta no tiene fondos suficientes.";
                    break;
                case (3004):
                    //error = "MwAwADAANAA=";
                    error = "La tarjeta ha sido identificada como una tarjeta robada.";
                    break;
                case (3005):
                    //error = "MwAwADAANQA=";
                    error = "La tarjeta ha sido identificada como una tarjeta fraudulenta.";
                    break;
                case (3008):
                    //error = "MwAwADAAOAA=";
                    error = "La tarjeta no es soportada en transacciones en línea.";
                    break;
                case (3009):
                    //error = "MwAwADAAOQA=";
                    error = "La tarjeta fue reportada como perdida.";
                    break;
                case (3010):
                    //error = "MwAwADEAMAA=";
                    error = "El banco ha restringido la tarjeta.";
                    break;
                case (3011):
                    //error = "MwAwADEAMQA=";
                    error = "El banco ha solicitado que la tarjeta sea retenida. Contacte al banco.";
                    break;
                case (3012):
                    //error = "MwAwADEAMgA=";
                    error = "Se requiere solicitar al banco autorización para realizar este pago.";
                    break;
                default:
                    error = "Ocurrió un problema. Inténtalo mas tarde.";
                    break;
            }
            return error;
        }

        protected void ButtonRegresar_Click(object sender, EventArgs e)
        {
            if (ViewState["RefUrl"] != null)
            {
                var url = ViewState["RefUrl"];
                Response.Redirect((string)url);
            }
        }
    }
}