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
    public partial class Tarjeta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void AgregarTarjeta_Click(object sender, EventArgs e)
        {
            if(Session["cliente"] != null)
            {
                Cliente cliente = (Cliente)Session["cliente"];

                CreditCard card = new CreditCard();
                card.Dueño = Dueno.Value;
                card.NumeroTarjeta = cardNumber.Value;
                card.CVV = cvvNumber.Value;
                card.Expira_Mes = Meses.Items[Meses.SelectedIndex].Value;
                card.Expira_Anio = Anio.Items[Anio.SelectedIndex].Value;

                cliente.Pedidos = null;
                cliente.Direcciones = null;
                string clienteNuevo = JsonConvert.SerializeObject(cliente);
                string tarjeta = "{\"card_number\":\"" + card.NumeroTarjeta + "\",\"holder_name\":\"" + card.Dueño + "\",\"expiration_year\":\"" + card.Expira_Anio + "\",\"expiration_month\":\"" + card.Expira_Mes + "\",\"cvv2\":\"" + card.CVV + "\"}";

                Controllers.ConexionApi conexion = new Controllers.ConexionApi();

                List<string> respuesta = await conexion.ActualizarClienteToken(clienteNuevo, tarjeta);

                Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(respuesta[0]);

                if (mensaje.Titulo.Equals("ERROR"))
                {
                    Response.Redirect("Tarjeta.aspx#CardError");
                }

                else
                {
                    Cliente cli = JsonConvert.DeserializeObject<Cliente>(respuesta[1]);
                    Session["cliente"] = cli;
                    Response.Redirect("MisTarjetas.aspx#CardSuccess");
                }

            }


        }
    }
}