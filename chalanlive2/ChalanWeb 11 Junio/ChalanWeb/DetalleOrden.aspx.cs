using ChalanWeb.Controllers;
using ChalanWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChalanWeb
{
    public partial class DetalleOrden : System.Web.UI.Page
    {
        protected string Fecha_Creacion { get; set; }
        protected string Fecha_Entrega { get; set; }
        protected string Status { get; set; }
        protected string Orden { get; set; }
        protected Decimal latitud { get; set; }
        protected Decimal longitud { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["cliente"] != null)
                {
                    string hash = Seguridad.DesEncriptar(Request.QueryString["id"]);
                    int id = Convert.ToInt32(hash);
                    PintarOrden(id);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void PintarOrden(int id)
        {
            Pedido ped = null;

            Cliente cliente = (Cliente)Session["cliente"];
            for (int i = 0; i < cliente.Pedidos.Count; i++)
                if (cliente.Pedidos[i].Id == id)
                    ped = cliente.Pedidos[i];

            latitud = Convert.ToDecimal(ped.Latitud);
            longitud = Convert.ToDecimal(ped.Longitud);
            List<DetallePedido> detalles = ped.Detalles;
            literalFechaCreacion.Text = ped.Fecha_Pedido.ToString("dddd, dd MMMM yyyy, hh:mm tt");
            //Fecha_Creacion = ped.Fecha_Pedido.ToString("dddd, dd MMMM yyyy, hh:mm tt");
            literalFechaEntrega.Text = ped.Fecha_Entrega.ToString("dddd, dd MMMM yyyy, hh:mm tt");
            //Fecha_Entrega = ped.Fecha_Entrega.ToString("dddd, dd MMMM yyyy, hh:mm tt");


            switch (ped.Status)
            {
                case 0:
                    literalStatusOrden.Text = "Pendiente de confirmación.";
                    break;
                case 1:
                    literalStatusOrden.Text = "Pedido aceptado.";
                    break;
                case 2:
                    literalStatusOrden.Text = "Pedido entregado.";
                    break;
                case 3:
                    literalStatusOrden.Text = "Pedido cancelado.";
                    break;

            }
            //Status = Convert.ToString(ped.Status);
            literalNumeroOrden.Text = Convert.ToString(ped.Id);
            //Orden = Convert.ToString(ped.Id); 

            repetidorDetalles.DataSource = detalles;
            repetidorDetalles.DataBind();

            Decimal total = detalles.Select(x => x.Costo).Sum();

            LiteralSubTotal.Text = total.ToString();
            LiteralTotal.Text = total.ToString();
        }
    }
}