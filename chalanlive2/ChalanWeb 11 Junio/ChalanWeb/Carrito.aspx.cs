using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChalanWeb.DAO;
using ChalanWeb.Models;

namespace ChalanWeb
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected string SubTotal { get; set; }
        protected string Total { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["carro"] != null)
                {
                    PintarCarro();
                }
            }
        }

        private void PintarCarro()
        {
            List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];

            if(carrito != null && carrito.Any())
            {
                repetidorCarro.DataSource = carrito;
                repetidorCarro.DataBind();

                Decimal total = carrito.Select(x => x.Costo).Sum();
                decimal envio = Convert.ToDecimal(29);

                Decimal totalPedido = total + envio;

                literalSubtotal.Text = total.ToString("#,##0.00");
                literalTotal.Text = totalPedido.ToString("#,##0.00");
            }

            else
            {
                Response.Redirect("Default.aspx#EmptyCard");
            }
            
        }

        protected void ButtonFinalizar_Click(object sender, EventArgs e)
        {
            List<DetallePedido> carrito = new List<DetallePedido>();
            string articulos = "";
            string nombres = "";

            if(Request.Form["Cantidad"] != null)
                articulos = Request.Form["Cantidad"].ToString();

            if (Request.Form["Nombres"] != null)
                nombres = Request.Form["Nombres"].ToString();

            var productos = nombres.Split(',').ToList();
            var cantidades = articulos.Split(',').Select(Int32.Parse).ToList();

            if(Session["carro"] != null)
            {
                carrito = (List<DetallePedido>) HttpContext.Current.Session["carro"];
            }
            
            for(int i=0; i<carrito.Count; i++)
            {
                if (carrito[i].Nombre.Equals(productos[i]))
                    carrito[i].Cantidad = cantidades[i];
            }

            Session["carro"] = carrito;

            Response.Redirect("Envio.aspx");
        }

        protected void repetidorCarro_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];

            for(var i=0; i<carrito.Count; i++)
            {
                if(carrito[i].Id_Producto == id)
                {
                    carrito.Remove(carrito[i]);
                    Session["carro"] = carrito;
                    Response.Redirect("Carrito.aspx");
                }
            }

        }
    }
}