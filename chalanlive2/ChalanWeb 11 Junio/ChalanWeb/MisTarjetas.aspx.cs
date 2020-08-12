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
    public partial class MisTarjetas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["cliente"] != null)
                {
                    PintarTarjetasAsync();
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                }
            }
        }

        private async void PintarTarjetasAsync()
        {
            Cliente cliente = (Cliente)Session["cliente"];

            Controllers.ConexionApi conexion = new Controllers.ConexionApi();

            List<TarjetaOP> tarjetas = await conexion.ObtenerTarjetasOP(cliente.TOKENOP);

            if (tarjetas != null && tarjetas.Any()) //tarjetas != null &&
            {
                for (var i = 0; i < tarjetas.Count; i++)
                {
                    switch (tarjetas[i].brand.ToUpper())
                    {
                        case "VISA":
                            tarjetas[i].imagen = "Recursos/visa.png";
                            break;
                        case "MASTERCARD":
                            tarjetas[i].imagen = "Recursos/mastercard.png";
                            break;
                    }
                }

                repetidorTarjetas.DataSource = tarjetas;
                repetidorTarjetas.DataBind();
            }

            else
            {
                litTarjetas.Text = "POR EL MOMENTO, NO CUENTAS CON TARJETAS ASOCIADAS.";
                litTarjetas.Visible = true;
            }
        }

        protected async void repetidorTarjetas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Cliente cliente = new Cliente();
            string idTarjeta = e.CommandArgument.ToString();
            if(Session["cliente"] != null)
            {
                cliente = (Cliente)Session["cliente"];
            }

            ConexionApi conexion = new ConexionApi();
            string respuesta = await conexion.EliminarTarjetaOP(idTarjeta, cliente.TOKENOP);

            if (!String.IsNullOrEmpty(respuesta))
            {
                ErrorOP error = JsonConvert.DeserializeObject<ErrorOP>(respuesta);
                //await DisplayAlert("ERROR", error.DescripcionError, "Aceptar");
            }
            else
            {
                Response.Redirect("MisTarjetas.aspx#CardDel");
            }
        }

        protected void ButtonAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCard.aspx");
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