using ChalanWeb.Controllers;
using ChalanWeb.DAO;
using ChalanWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChalanWeb
{
    public partial class DetallePedidoNuevo : System.Web.UI.Page
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
                if (Session["cliente"] != null)
                {
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                    PintarOrden();

                    //if (Session["carro"] != null)
                    //    PintarCarro();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void PintarOrden()
        {
            Cliente cliente = null;
            if (Session["cliente"] != null)
                cliente = (Cliente)Session["cliente"];


            DAOPedido daoPedido = new DAOPedido();
            Pedido ped = daoPedido.GetPedidoActual(cliente.Id);
            //Pedido ped = cliente.Pedidos.OrderByDescending(x => x.Id).FirstOrDefault();
            if(ped.Status == 0 || ped.Status == 1)
            {
                if(ped.Status == 1) //Aceptado
                {
                    Buttonprueba.Visible = false;
                }


                DAOProducto prod = new DAOProducto();

                List<DetallePedido> detalles = ped.Detalles;

                for(var i=0; i<detalles.Count; i++)
                {
                    Producto item = prod.GetProducto(detalles[i].Id_Producto.ToString());
                    detalles[i].Imagen = item.Imagen;
                }

                repetidorDetalles.DataSource = detalles;
                repetidorDetalles.DataBind();

                Decimal total = detalles.Select(x => x.Costo).Sum();
                LiteralSubTotal.Text = total.ToString();
                LiteralTotal.Text = total.ToString();

                literalFechaCreacion.Text = ped.Fecha_Pedido.ToString(CultureInfo.GetCultureInfo("es-MX")); //"dddd, dd MMMM yyyy, hh:mm tt", 
                int entregado = DateTime.Compare(ped.Fecha_Estimada, DateTime.MinValue);

                switch (entregado)
                {
                    case 0:
                        literalFechaEntrega.Text = "Fecha por confirmar.";
                        break;
                    case -1:
                        literalFechaEntrega.Text = ped.Fecha_Estimada.ToString(CultureInfo.GetCultureInfo("es-MX"));
                        break;
                }

                
                //Fecha_Creacion = ped.Fecha_Pedido.ToString("dddd, dd MMMM yyyy, hh:mm tt");
                //Fecha_Entrega = ped.Fecha_Entrega.ToString("dddd, dd MMMM yyyy, hh:mm tt");
                switch (ped.Status)
                {
                    case 0:
                        literalStatusOrden.Text = "Pedido pendiente de confirmación.";
                        //Status = "Pedido pendiente de confirmación.";
                        break;
                    case 1:
                        literalStatusOrden.Text = "Pedido aceptado.";
                        //Status = "Pedido aceptado.";
                        break;
                }

                literalNumeroOrden.Text = Convert.ToString(ped.Id);
                //Orden = Convert.ToString(ped.Id);
                this.latitud = Convert.ToDecimal(ped.Latitud);
                this.longitud = Convert.ToDecimal(ped.Longitud);   
            }

            else
            {
                Response.Redirect("Cuenta.aspx#NoOpOrder");
            }


            //repetidorDetalles.DataSource = detalles;
            //repetidorDetalles.DataBind();

            //Decimal total = detalles.Select(x => x.Costo).Sum();

            //LiteralSubTotal.Text = total.ToString();
            //LiteralTotal.Text = total.ToString();
        }

        private void PintarCarro()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            //List<DetallePedido> carrito = (List<DetallePedido>) HttpContext.Current.Session["carro"];
            Pedido ped = cliente.Pedidos.OrderByDescending(x => x.Id).FirstOrDefault();
            //List<DetallePedido> carrito = cliente.DetallesUltimoPedido;
            repetidorDetalles.DataSource = ped.Detalles;
            repetidorDetalles.DataBind();

            Decimal total = ped.Detalles.Select(x => x.Costo).Sum();

            LiteralSubTotal.Text = total.ToString();
            LiteralTotal.Text = total.ToString();
        }

        protected async void Buttonprueba_Click(object sender, EventArgs e)
        {
            Cliente cliente = (Cliente)Session["cliente"];
            //Pedido ultimo = cliente.Pedidos.OrderByDescending(x => x.Id).FirstOrDefault();
            DAOPedido daoPedido = new DAOPedido();
            Pedido ped = daoPedido.GetPedidoActual(cliente.Id);
            List<string> respuesta = await ConexionApi.CancelarPedido(ped.Id, cliente.Id, ped.Token);
            Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(respuesta[0]);
            if (mensaje.Titulo.Equals("OK"))
            {
                Session["cliente"] = JsonConvert.DeserializeObject<Cliente>(respuesta[1]);
                Response.Redirect("Cuenta.aspx#OrderCanceled");
            }

            else
            {

            }
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